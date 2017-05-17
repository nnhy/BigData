using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Big.Data.Entity;
using NewLife.Log;
using NewLife.Security;
using XCode;

namespace BigData
{
    class Program
    {
        static void Main(string[] args)
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

            Console.WriteLine("OK!");
            Console.ReadKey(false);
        }

        static void Test1()
        {
            // 关闭日志
            var set = XCode.Setting.Current;
            set.ShowSQL = false;

            // 预热初始化
            var count = SalesOrder.Meta.Count;
            Console.WriteLine("共有数据 {0:n0}", count);

            // 生成海量数据
            var total = 400000000;
            if (count >= total) return;
            var batch = 10000;

            //var ss = SalesOrder.Meta.Session;
            //ss.BeginTrans();

            // 模拟时序数据
            //var time = new DateTime(2000, 1, 1);
            var time = DateTime.Now.Date;
            if (count > 0) time = SalesOrder.FindAll(null, SalesOrder._.ID.Desc(), null, 0, 1)[0].CreateTime;

            // 时间均摊到某一天，随机产生，几率加倍
            var add = (Int32)((24.0 * 3600 * 1000 / total) * 2);

            Task task = null;
            var list = new EntityList<SalesOrder>();
            var sw = Stopwatch.StartNew();
            for (int i = count; i < total; i++)
            {
                // 批量提交事务
                if (i > 0 && i % batch == 0)
                {
                    //ss.Commit();
                    //ss.BeginTrans();
                    //if (task != null && !task.IsOK()) task.Wait();
                    //task = Task.Run(() =>
                    //{
                    //    var es = list;
                    //    list = new EntityList<SalesOrder>();
                    //    es.Insert();
                    //});

                    sw.Stop();
                    var speed = 0;
                    if (sw.ElapsedMilliseconds > 0) speed = (Int32)(batch * 1000 / sw.ElapsedMilliseconds);
                    Console.Title = "进度 {0:p2} 速度 {1:n0}tps".F((double)i / total, speed);
                    sw.Reset();
                    sw.Start();
                }

                var sd = new SalesOrder();
                //sd.Number = Rand.Next().ToString().PadLeft(12, '0');
                sd.Number = i.ToString().PadLeft(12, '0');
                sd.NodeID = Rand.Next(1, 32);
                //sd.NodeID = 1;

                //time = time.AddMilliseconds(Rand.Next(0, add));
                time = time.AddMilliseconds(add / 2);
                sd.CreateTime = time;
                //sd.Insert();
                //list.Add(sd);
                sd.SaveAsync();
            }

            //ss.Commit();
        }
    }
}