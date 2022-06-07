using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using NewLife;
using NewLife.Cube;
using NewLife.Data;
using NewLife.Reflection;

namespace Zero.Web.Controllers
{
    public class ApiController : ControllerBaseX
    {
        /// <summary>获取所有接口</summary>
        /// <returns></returns>
        [HttpGet]
        public Object Get() => Info(null);

        private static readonly String _OS = Environment.OSVersion + "";
        private static readonly String _MachineName = Environment.MachineName;
        private static readonly String _UserName = Environment.UserName;
        private static readonly String _LocalIP = NetHelper.MyIP() + "";
        /// <summary>服务器信息，用户健康检测</summary>
        /// <param name="state">状态信息</param>
        /// <returns></returns>
        [HttpGet(nameof(Info))]
        public Object Info(String state)
        {
            var conn = HttpContext.Connection;
            var asmx = AssemblyX.Entry;
            var asmx2 = AssemblyX.Create(Assembly.GetExecutingAssembly());

            var ip = HttpContext.GetUserHost();

            var rs = new
            {
                Server = asmx?.Name,
                asmx?.Version,
                OS = _OS,
                MachineName = _MachineName,
                UserName = _UserName,
                ApiVersion = asmx2?.Version,

                LocalIP = _LocalIP,
                Remote = ip + "",
                State = state,
                Time = DateTime.Now,
            };

            // 转字典
            var dic = rs.ToDictionary();

            dic["Port"] = conn.LocalPort;
            //dic["Online"] = nsvr.SessionCount;
            //dic["MaxOnline"] = nsvr.MaxSessionCount;

            // 进程
            dic["Process"] = GetProcess();

            return dic;
        }

        private Object GetProcess()
        {
            var proc = Process.GetCurrentProcess();

            return new
            {
                Environment.ProcessorCount,
                ProcessId = proc.Id,
                Threads = proc.Threads.Count,
                Handles = proc.HandleCount,
                WorkingSet = proc.WorkingSet64,
                PrivateMemory = proc.PrivateMemorySize64,
                GCMemory = GC.GetTotalMemory(false),
                GC0 = GC.GetGeneration(0),
                GC1 = GC.GetGeneration(1),
                GC2 = GC.GetGeneration(2),
            };
        }
    }
}