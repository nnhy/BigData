﻿using Big.Data;
using NewLife.Log;
using NewLife.Security;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using XCode;

namespace BigData
{
    class Program
    {
        static void Main(String[] args)
        {
            XTrace.UseConsole();

            try
            {
                Test1();
            }
            catch (Exception ex)
            {
                XTrace.WriteException(ex?.GetTrue());
            }

            GC.Collect();
            Console.WriteLine("OK!");
            Console.ReadKey(false);
        }

        static Int32 _users = 4;
        static void Test1()
        {
            // 关闭日志
            var set = XCodeSetting.Current;
            set.ShowSQL = false;

            // 预热初始化
            var count = SalesOrder.Meta.Count;
            Console.WriteLine("共有数据 {0:n0}", count);

            // 生成海量数据
            var total = 400000000;
            if (count >= total) return;
            var batch = 10000;

            // 模拟时序数据
            var time = DateTime.Now.Date;

            var stat = new PerfCounter();
            Task task = null;
            var list = new List<SalesOrder>();
            for (var i = count; i <= total; i++)
            {
                stat.Increment(1, 0);
                // 批量提交事务
                if (i > 0 && i % batch == 0)
                {
                    var sw = Stopwatch.StartNew();
                    if (task != null && !task.IsCompleted && _users < 0) task.Wait();
                    task = Task.Run(() =>
                    {
                        var es = list;
                        list = new List<SalesOrder>();
                        Interlocked.Decrement(ref _users);
                        es.Insert();
                        Interlocked.Increment(ref _users);
                    });

                    sw.Stop();
                    Console.Title = $"进度 {(Double)i / total:p2} 速度 {stat} {sw.ElapsedMilliseconds:n0}ms";
                    //sw.Restart();
                }

                var sd = new SalesOrder();
                //sd.Number = Rand.Next().ToString().PadLeft(12, '0');
                sd.Number = (i + 1).ToString().PadLeft(12, '0');
                sd.NodeId = Rand.Next(1, 32);
                //sd.NodeID = 1;

                time = time.AddMilliseconds(Rand.Next(0, 24 * 3600 * 1000));
                sd.CreateTime = time;
                //sd.Insert();
                list.Add(sd);
            }
        }
    }
}