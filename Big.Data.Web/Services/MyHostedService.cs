using NewLife;
using NewLife.Log;
using NewLife.Remoting;
using NewLife.Serialization;
using NewLife.Threading;
using Stardust;
using Stardust.Registry;

namespace Zero.Web.Services;

/// <summary>
/// 星尘注册中心用法，消费其它应用提供的服务
/// </summary>
public class MyHostedService : IHostedService
{
    private readonly IRegistry _registry;
    private readonly StarFactory _factory;
    private ApiHttpClient _client;
    TimerX _timer;

    public MyHostedService(IRegistry registry, StarFactory factory)
    {
        _registry = registry;
        _factory = factory;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _timer = new TimerX(DoGetInfo, null, 1_000, 60_000);

        return Task.CompletedTask;
    }

    async Task DoGetInfo(Object state)
    {
        if (_client == null && _registry != null)
        {
            // 从注册中心获取地址
            var services = await _registry.ResolveAsync("Zero.WebApi");
            XTrace.WriteLine("Zero.WebApi服务信息：{0}", services.ToJson(true));

            // 创建指定服务的客户端，它的服务端地址绑定注册中心，自动更新
            _client = await _factory.CreateForServiceAsync("Zero.WebApi") as ApiHttpClient;
            XTrace.WriteLine("Zero.WebApi服务地址：{0}", _client.Services.Select(e => e.Address).Join());
        }

        if (_client != null && _client.Services.Count > 0)
        {
            // 尝试调用接口
            var rs = await _client?.GetAsync<Object>("api/info", new { state = "NewLife1234" });
            XTrace.WriteLine("api接口信息：{0}", rs.ToJson(true));
        }
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _timer.TryDispose();
        _client.TryDispose();

        return Task.CompletedTask;
    }
}
