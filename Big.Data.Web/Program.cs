using Big.Data.Web.Services;
using NewLife.Cube;
using NewLife.Log;
using System.Diagnostics;

//!!! 标准Web项目模板，新生命团队强烈推荐

// 启用控制台日志，拦截所有异常
XTrace.UseConsole();

// 提高当前进程优先级，便于生成数据时更快完成
Process.GetCurrentProcess().PriorityClass = ProcessPriorityClass.AboveNormal;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

// 配置星尘。借助StarAgent，或者读取配置文件 config/star.config 中的服务器地址
var star = services.AddStardust(null);

services.AddSingleton<BuildService>();

services.AddControllersWithViews();
services.AddCube();

var app = builder.Build();
app.UseStaticFiles();

app.UseCube(app.Environment);
app.UseCubeHome();

app.UseAuthorization();

app.RegisterService("BigData", null, builder.Environment.EnvironmentName, "/cube/info");

app.Run();