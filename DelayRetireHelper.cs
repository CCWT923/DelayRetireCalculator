using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    internal class DelayRetireHelper
    {
        /// <summary>
        /// 男职工延迟退休的起始生日
        /// </summary>
        private static readonly DateTime maleDelayRetireStart = new DateTime(1965, 1, 1);
        /// <summary>
        /// 男职工延迟退休结束的年份
        /// </summary>
        private static readonly DateTime maleDelayRetireEnd = new DateTime(1976, 12, 1);
        /// <summary>
        /// 原55周岁退休的女职工延迟退休的起始生日
        /// </summary>
        private static readonly DateTime femalDelayRetireStart55 = new DateTime(1970, 1, 1);
        /// <summary>
        /// 原55周岁退休的女职工延迟退休的结束生日
        /// </summary>
        private static readonly DateTime femalDelayRetireEnd55 = new DateTime(1981, 12, 1);
        /// <summary>
        /// 原50周岁退休的女职工延迟退休的开始日期
        /// </summary>
        private static readonly DateTime femalDelayRetireStart50 = new DateTime(1975,1,1);
        /// <summary>
        /// 原50周岁退休的女职工延迟退休的结束日期
        /// </summary>
        private static readonly DateTime femalDelayRetireEnd50 = new DateTime(1984,12, 1);

        /// <summary>
        /// 男职工原退休年龄
        /// </summary>
        private static readonly int maleRetireBaseYear = 60;
        private static readonly int femaleRetireBaseYear55 = 55;
        private static readonly int femaleRetireBaseYear50 = 50;
        /// <summary>
        /// 男职工：每4个月延迟1个月
        /// </summary>
        private static readonly double maleRetireStep = 4.0;
        /// <summary>
        /// 原55岁女职工：每4个月延迟1个月
        /// </summary>
        private static readonly double femaleRetireStep55 = 4.0;
        /// <summary>
        /// 原55岁女职工：每2个月延迟1个月
        /// </summary>
        private static readonly double femaleRetireStep50 = 2.0;

        /// <summary>
        /// 每4个月增加1个月
        /// </summary>
        private static readonly int maleRetireMonthIncrement = 1;
        

        /// <summary>
        /// 计算退休年龄
        /// </summary>
        /// <param name="birthday">出生日期</param>
        /// <param name="sex">男：1，原50岁退休女：2，原55岁退休女：3</param>
        /// <param name="femaleType">女员工类型，原退休年龄是55岁或者50岁</param>
        /// <param name="delayMonths">延迟月数</param>
        /// <returns></returns>
        internal static DateTime CalcRetireDate(DateTime birthday, int sex, out int delayMonths)
        {
            delayMonths = 0;

            delayMonths = 0;
            if (sex == 1)
            {
                if (birthday < maleDelayRetireStart)
                {
                    return new DateTime(1, 1, 1);
                }
                if (birthday > maleDelayRetireEnd)
                {
                    return new DateTime(2, 2, 2);
                }
            }
            else if (sex == 2)
            {
                if (birthday < femalDelayRetireStart50)
                {
                    return new DateTime(1, 1, 1);
                }
                if (birthday > femalDelayRetireEnd50)
                {
                    return new DateTime(2, 2, 2);
                }
            }
            else if (sex == 3)
            {
                if (birthday < femalDelayRetireStart55)
                {
                    return new DateTime(1, 1, 1);
                }

                if (birthday > femalDelayRetireEnd55)
                {
                    return new DateTime(2, 2, 2);
                }
            }
            else
            {
                return new DateTime(3, 3, 3);
            }

            DateTime tmpDt;
            if (sex == 1)
            {
                tmpDt = maleDelayRetireStart;
            }
            else if (sex == 2)
            {
                tmpDt = femalDelayRetireStart50;
            }
            else
            {
                tmpDt = femalDelayRetireStart55;
            }

            while (tmpDt <= birthday)
            {
                delayMonths++;
                tmpDt = tmpDt.AddMonths(maleRetireMonthIncrement);
            }

            if (sex == 1)
            {
                delayMonths = (int)Math.Ceiling(delayMonths / maleRetireStep);
                return birthday.AddYears(maleRetireBaseYear).AddMonths(delayMonths);
            }
            else if (sex == 2)
            {
                delayMonths = (int)Math.Ceiling(delayMonths / femaleRetireStep50);
                return birthday.AddYears(femaleRetireBaseYear50).AddMonths(delayMonths);
            }
            else
            {
                delayMonths = (int)Math.Ceiling(delayMonths / femaleRetireStep55);
                return birthday.AddYears(femaleRetireBaseYear55).AddMonths(delayMonths);
            }
        }

        internal static string GetRetireAgeDescription(DateTime birthDate, DateTime targetDate)
        {
            // 计算年龄年份和月份
            int years = targetDate.Year - birthDate.Year;
            int months = targetDate.Month - birthDate.Month;

            // 如果当前月还没有到出生月份，减少一年，增加月份
            if (months < 0)
            {
                years--;
                months += 12;
            }

            // 构建返回的年龄字符串
            string ageString = $"{years}岁";
            if (months > 0)
            {
                ageString += $"零{months}个月";
            }

            return ageString;
        }
    }
}
