using System;
using System.Reflection.Metadata;

namespace ConsoleApp2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            while (true) 
            {
                Console.WriteLine("输入生日 (年-月)：");
                string str = Console.ReadLine();
                if(!DateTime.TryParse(str, out DateTime birthdate))
                {
                    Console.WriteLine("输入的日期不正确：" + str);
                    continue;
                }

                Console.WriteLine("输入职工类型（1=男职工，2=原50岁退休女职工，3 = 原55岁退休女职工）：");
                str = Console.ReadLine();
                if(!int.TryParse(str, out int sex) || sex < 1 || sex > 3)
                {
                    Console.WriteLine("输入的类型不正确：" + str);
                    continue;
                }

                var res = DelayRetireHelper.CalcRetireDate(birthdate, sex, out int ms);
                if(res.Year == 1)
                {
                    Console.WriteLine("----------------不在延迟退休范围内----------------");
                    continue;
                }
                if(res.Year == 2)
                {
                    Console.WriteLine("----------------暂无相关政策----------------");
                    continue;
                }

                if(res.Year == 3)
                {
                    Console.WriteLine("----------------参数错误----------------");
                    continue;
                }

                Console.WriteLine();
                Console.WriteLine("---------------------计算结果---------------------");
                Console.WriteLine($"职工类型： {(sex == 1 ? '男' : sex == 2 ? "原50岁退休女职工" : "原55岁退休女职工")}");
                Console.WriteLine($"出生日期： {birthdate:yyyy年M月}");
                Console.WriteLine($"退休时间： {res:yyyy年M月}");
                Console.WriteLine($"退休年龄： {DelayRetireHelper.GetRetireAgeDescription(birthdate, res)}");
                Console.WriteLine($"延迟退休： {ms}个月");
                Console.WriteLine("_________________________________________________");
                Console.WriteLine();
            }
            
        }
    }
}
