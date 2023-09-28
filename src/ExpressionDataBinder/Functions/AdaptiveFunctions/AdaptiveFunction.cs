using Newtonsoft.Json.Linq;

namespace ExpressionDataBinder.Functions.AdaptiveFunctions
{
    public class AdaptiveFunction
    {
        /// <summary>
        /// 函式名稱
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// 函式
        /// </summary>
        public Func<IReadOnlyList<dynamic>, object> Function { get; set; }


        public AdaptiveFunction()
        {

        }
        public AdaptiveFunction(string name, Func<IReadOnlyList<dynamic>, object> function)
        {
            Name = ToFirstLower(name);
            Function = function;
        }
        public AdaptiveFunction(string className, string functionName, Func<IReadOnlyList<dynamic>, object> function)
        {
            Name = $"{className}.{ToFirstLower(functionName)}";
            Function = function;
        }

        /// <summary>
        /// 字串的第一個字轉小寫
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private string ToFirstLower(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return value;
            }

            return value.FirstOrDefault().ToString().ToLower() + value.Substring(1);
        }
    }

    public class ArgumentCollection
    {
        private Queue<object> Arguments { get; set; }

        public ArgumentCollection(IReadOnlyList<dynamic> args)
        {
            Arguments = new Queue<object>(args);
        }


        /// <summary>
        /// 取得 Argument
        /// </summary>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public JToken Get()
        {
            if (Arguments != null && Arguments.Any())
            {
                try
                {
                    JToken jValue = JToken.FromObject(Arguments.Dequeue());
                    return jValue;
                }
                catch
                {

                }
            }
            return default(JToken);
        }

        /// <summary>
        /// 取得 Argument
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public T Get<T>(T defaultValue)
        {
            if (Arguments != null && Arguments.Any())
            {
                try
                {
                    JToken jValue = JToken.FromObject(Arguments.Dequeue());
                    return jValue.ToObject<T>();
                }
                catch
                {

                }
            }
            return defaultValue;
        }

        /// <summary>
        /// 取得 Argument DateTime
        /// </summary>
        /// <param name="defaultDateTime"></param>
        /// <returns></returns>
        public DateTime GetDateTime(DateTime defaultDateTime)
        {
            if (Arguments != null &&
                Arguments.Any() &&
                DateTime.TryParse(Convert.ToString(Arguments.Dequeue()), out DateTime dateTime))
            {
                return dateTime;
            }
            return defaultDateTime;
        }

        /// <summary>
        /// 取得 Argument
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool TryGet<T>(out T value)
        {
            if (Arguments != null && Arguments.Any())
            {
                try
                {
                    JToken jValue = JToken.FromObject(Arguments.Dequeue());
                    value = jValue.ToObject<T>();
                    return true;
                }
                catch
                {

                }
            }

            value = default(T);
            return false;
        }

        /// <summary>
        /// 取得 Argument DateTime
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public bool TryGetDateTime(out DateTime dateTime)
        {
            if (Arguments != null && Arguments.Any())
            {
                bool isSuccess = DateTime.TryParse(Convert.ToString(Arguments.Dequeue()), out dateTime);
                return isSuccess;
            }
            dateTime = DateTime.Now;
            return false;
        }
    }
}
