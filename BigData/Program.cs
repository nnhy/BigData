using System;
using System.Threading.Tasks;
using Big.Data.Entity;
using NewLife.Log;
using NewLife.Net;
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

            GC.Collect();
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

            // 模拟时序数据
            var time = DateTime.Now.Date;

            var stat = new Statistics();
            Task task = null;
            var list = new EntityList<SalesOrder>();
            for (int i = count; i <= total; i++)
            {
                stat.Increment();
                // 批量提交事务
                if (i > 0 && i % batch == 0)
                {
                    if (task != null && !task.IsOK()) task.Wait();
                    task = Task.Run(() =>
                    {
                        var es = list;
                        list = new EntityList<SalesOrder>();
                        es.Insert();
                    });

                    Console.Title = "进度 {0:p2} 速度 {1}".F((double)i / total, stat);
                }

                var sd = new SalesOrder();
                //sd.Number = Rand.Next().ToString().PadLeft(12, '0');
                sd.Number = (i + 1).ToString().PadLeft(12, '0');
                sd.NodeID = Rand.Next(1, 32);
                //sd.NodeID = 1;

                time = time.AddMilliseconds(Rand.Next(0, 24 * 3600 * 1000));
                sd.CreateTime = time;
                //sd.Insert();
                list.Add(sd);
            }
        }
    }
}