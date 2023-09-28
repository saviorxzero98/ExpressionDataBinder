using ExpressionDataBinder.Extensions;
using System.Globalization;

namespace ExpressionDataBinder.Functions.AdaptiveFunctions
{
    public static class DateTimeFunctions
    {
        private const string ClassName = "datetime";

        public const string DateFormat = "yyyy-MM-dd";
        public const string TimeFormat = "HH:mm:ss";
        public const string IsoFormat = "yyyy-MM-ddTHH:mm:ss.fffZ";

        /// <summary>
        /// 取得目前時間
        /// datetime.now('{format}'?, '{locale}'?)
        /// </summary>
        public static AdaptiveFunction Now
        {
            get
            {
                object function(IReadOnlyList<dynamic> args)
                {
                    var arguments = new ArgumentCollection(args);
                    string format = arguments.Get(IsoFormat);
                    string culture = arguments.Get(string.Empty);

                    var now = DateTime.Now;

                    if (string.IsNullOrEmpty(culture))
                    {
                        return now.ToString(format);
                    }
                    else
                    {
                        CultureInfo cultureInfo = new CultureInfo(culture);
                        return now.ToString(format, cultureInfo);
                    }
                }

                return new AdaptiveFunction(ClassName, nameof(Now), function);
            }
        }

        /// <summary>
        /// 取得目前日期
        /// datetime.today('{format}'?, '{locale}'?)
        /// </summary>
        public static AdaptiveFunction Today
        {
            get
            {
                object function(IReadOnlyList<dynamic> args)
                {
                    var arguments = new ArgumentCollection(args);
                    string format = arguments.Get(IsoFormat);
                    string culture = arguments.Get(string.Empty);

                    var now = DateTime.Today;

                    if (string.IsNullOrEmpty(culture))
                    {
                        return now.ToString(format);
                    }
                    else
                    {
                        CultureInfo cultureInfo = new CultureInfo(culture);
                        return now.ToString(format, cultureInfo);
                    }
                }

                return new AdaptiveFunction(ClassName, nameof(Today), function);
            }
        }

        /// <summary>
        /// 取得指定日期
        /// datetime.date({year}, {month}, {day}, '{format}'?)
        /// </summary>
        public static AdaptiveFunction Date
        {
            get
            {
                object function(IReadOnlyList<dynamic> args)
                {
                    if (args.Count < 3)
                    {
                        return (new DateTime().ToString(IsoFormat));
                    }

                    var arguments = new ArgumentCollection(args);
                    int year = arguments.Get(DateTime.Today.Year);
                    int month = arguments.Get(1);
                    int day = arguments.Get(1);
                    string format = arguments.Get(IsoFormat);

                    return new DateTime(year, month, day).ToString(format);
                }

                return new AdaptiveFunction(ClassName, nameof(Date), function);
            }
        }

        /// <summary>
        /// 取得指定時間
        /// datetime.time({hours}, {minutes}, {second}, '{format}'?)
        /// </summary>
        public static AdaptiveFunction Time
        {
            get
            {
                object function(IReadOnlyList<dynamic> args)
                {
                    if (args.Count < 3)
                    {
                        return (new TimeSpan().ToString(TimeFormat));
                    }

                    var arguments = new ArgumentCollection(args);
                    int hours = arguments.Get(0);
                    int minutes = arguments.Get(0);
                    int seconds = arguments.Get(0);
                    string format = arguments.Get(TimeFormat);

                    return new TimeSpan(hours, minutes, seconds).ToString(format);
                }

                return new AdaptiveFunction(ClassName, nameof(Date), function);
            }
        }

        /// <summary>
        /// 計算日期差
        /// datetime.diff({datetime1}, {datetime2}, {unit})
        /// unit: Y: year, M: month D: day, W: week, h: hours, m: mintues, s: seconds, ms: miliseconds
        /// </summary>
        public static AdaptiveFunction Diff
        {
            get
            {
                object function(IReadOnlyList<dynamic> args)
                {
                    double diffReuslt = 0;
                    if (args.Count != 3)
                    {
                        return diffReuslt;
                    }

                    var arguments = new ArgumentCollection(args);
                    DateTime dateTimeA = arguments.GetDateTime(DateTime.Now);
                    DateTime dateTimeB = arguments.GetDateTime(DateTime.Now);
                    string unit = arguments.Get("ms");

                    TimeSpan diff = (dateTimeB > dateTimeA) ? (dateTimeB - dateTimeA) : (dateTimeA - dateTimeB);

                    switch (unit)
                    {
                        case "Y":
                            diffReuslt = diff.GetTotalYears();
                            break;

                        case "M":
                            diffReuslt = diff.GetTotalMonths();
                            break;

                        case "D":
                            diffReuslt = diff.TotalDays;
                            break;

                        case "W":
                            diffReuslt = diff.GetTotleWeeks();
                            break;

                        case "h":
                            diffReuslt = diff.TotalHours;
                            break;

                        case "m":
                            diffReuslt = diff.TotalMinutes;
                            break;

                        case "s":
                            diffReuslt = diff.TotalSeconds;
                            break;

                        case "ms":
                            diffReuslt = diff.TotalMilliseconds;
                            break;
                    }


                    return diffReuslt;
                }

                return new AdaptiveFunction(ClassName, nameof(Diff), function);
            }
        }

        /// <summary>
        /// 取得所有 Function
        /// </summary>
        /// <returns></returns>
        public static List<AdaptiveFunction> GetFunctions()
        {
            var functions = new List<AdaptiveFunction>()
            {
                Now, Today, Date, Time, Diff
            };

            return functions;
        }
    }
}
