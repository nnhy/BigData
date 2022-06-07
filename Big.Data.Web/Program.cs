using NewLife.Cube;
using NewLife.Log;
using XCode;
using Zero.Web.Services;

//!!! 标准Web项目模板，新生命团队强烈推荐

// 启用控制台日志，拦截所有异常
XTrace.UseConsole();

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

// 配置星尘。借助StarAgent，或者读取配置文件 config/star.config 中的服务器地址
var star = services.AddStardust(null);

services.AddControllersWithViews();

// 引入魔方
services.AddCube();

// 后台服务s
services.AddHostedService<MyHostedService>();

var app = builder.Build();

// 预热数据层，执行反向工程建表等操作
EntityFactory.InitConnection("Data");

// 使用Cube前添加自己的管道
if (app.Environment.IsDevelopment())
    app.UseDeveloperExceptionPage();
else
    app.UseExceptionHandler("/CubeHome/Error");

// 使用魔方
app.UseCube(app.Environment);

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=CubeHome}/{action=Index}/{id?}");
});

app.Run();