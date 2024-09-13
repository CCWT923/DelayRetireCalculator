using System;

namespace ConsoleApp2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            while (true) 
            {
                Console.WriteLine("输入生日：");
                string dtStr = Console.ReadLine();
                var dt = DateTime.Parse(dtStr);
                Console.WriteLine("输入性别（1=男，2=女）：");
                var sex = int.Parse(Console.ReadLine());

                var res = DelayRetireHelper.CalcRetireDate(dt, sex, 55, out int ms);
                if(res == null || res.Value.Year == 1)
                {
                    Console.WriteLine("无结果");
                    continue;
                }
                Console.WriteLine($"{(sex == 1 ? '男':'女')}, {dt.ToString("yyyy年M月")}出生，退休时间：{res?.ToString("yyyy年M月")}，延迟退休 {ms} 个月");
            }
            
        }
    }
}
