namespace ExpressionDataBinder.Functions.AdaptiveFunctions
{
    public static class MathFunctions
    {
        public const string ClassName = "math";

        /// <summary>
        /// 取絕對值
        /// math.abs({number})
        /// </summary>
        public static AdaptiveFunction Abs
        {
            get
            {
                object function(IReadOnlyList<dynamic> args)
                {
                    if (args.Count < 1)
                    {
                        return 0;
                    }

                    var arguments = new ArgumentCollection(args);
                    double number = arguments.Get(0.0);
                    return Math.Abs(number);
                }

                return new AdaptiveFunction(ClassName, nameof(Abs), function);
            }
        }

        /// <summary>
        /// 取得指數
        /// math.power({number}, {power})
        /// </summary>
        public static AdaptiveFunction Power
        {
            get
            {
                object function(IReadOnlyList<dynamic> args)
                {
                    if (args.Count < 2)
                    {
                        return 1;
                    }

                    var arguments = new ArgumentCollection(args);
                    double number = arguments.Get(0.0);
                    double power = arguments.Get(0.0);

                    return Math.Pow(number, power);
                }

                return new AdaptiveFunction(ClassName, nameof(Power), function);
            }
        }

        /// <summary>
        /// 取得 PI
        /// math.pi()
        /// </summary>
        public static AdaptiveFunction Pi
        {
            get
            {
                object function(IReadOnlyList<dynamic> args)
                {
                    return Math.PI;
                }

                return new AdaptiveFunction(ClassName, nameof(Pi), function);
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
                Abs, Power, Pi
            };

            return functions;
        }
    }
}
