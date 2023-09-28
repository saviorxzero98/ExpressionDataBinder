using AdaptiveExpressions;
using ExpressionDataBinder.Functions.AdaptiveFunctions;
using Newtonsoft.Json.Linq;

namespace ExpressionDataBinder.Evaluators
{
    /// <summary>
    /// Microsoft Adaptive Expression
    /// https://docs.microsoft.com/en-US/azure/bot-service/bot-builder-concept-adaptive-expressions
    /// </summary>
    public class AdaptiveExpressionEvaluator : IExpressionEvaluator
    {
        public List<AdaptiveFunction> CustomFunctions { get; set; }


        #region Default Custom Functions

        /// <summary>
        /// 預設自訂 Function
        /// </summary>
        protected List<AdaptiveFunction> DefaultCustomFunctions
        {
            get
            {
                List<AdaptiveFunction> functions = new List<AdaptiveFunction>();

                functions.AddRange(DateTimeFunctions.GetFunctions());
                functions.AddRange(MathFunctions.GetFunctions());
                functions.AddRange(TextFunctions.GetFunctions());

                return functions;
            }
        }

        #endregion


        public AdaptiveExpressionEvaluator()
        {
            CustomFunctions = new List<AdaptiveFunction>();

            // 加入預設自訂 Function
            CustomFunctions.AddRange(DefaultCustomFunctions);
        }


        #region Custom Function

        /// <summary>
        /// 加入自訂 Function
        /// </summary>
        /// <param name="tupleFunction"></param>
        /// <returns></returns>
        public AdaptiveExpressionEvaluator AddCustomFunction(AdaptiveFunction adaptiveFunction)
        {
            if (CustomFunctions == null)
            {
                CustomFunctions = new List<AdaptiveFunction>();
            }

            CustomFunctions.Add(adaptiveFunction);

            return this;
        }

        /// <summary>
        /// 加入自訂 Function
        /// </summary>
        /// <param name="name"></param>
        /// <param name="function"></param>
        /// <returns></returns>
        public AdaptiveExpressionEvaluator AddCustomFunction(string name, Func<IReadOnlyList<dynamic>, object> function)
        {
            return AddCustomFunction(new AdaptiveFunction(name, function));
        }

        /// <summary>
        /// 加入自訂 Function
        /// </summary>
        /// <param name="tupleFunction"></param>
        /// <returns></returns>
        public AdaptiveExpressionEvaluator AddCustomFunctions(List<AdaptiveFunction> adaptiveFunctions)
        {
            if (CustomFunctions == null)
            {
                CustomFunctions = new List<AdaptiveFunction>();
            }

            CustomFunctions.AddRange(adaptiveFunctions);

            return this;
        }

        /// <summary>
        /// 初始化自訂 Function
        /// </summary>
        protected void InitializeCustomFunction()
        {
            if (CustomFunctions != null &&
                CustomFunctions.Any())
            {
                // 清空
                Expression.Functions.Clear();

                // 加入自訂 Function
                foreach (var function in CustomFunctions)
                {
                    Expression.Functions.Add(function.Name, function.Function);
                }
            }
        }

        #endregion


        #region Evaluate

        /// <summary>
        /// Evaluate Expresson
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public JToken Evaluate(string expression)
        {
            return Evaluate(expression, null);
        }

        /// <summary>
        /// Evaluate Expresson With Data
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public JToken Evaluate(string expression, object data)
        {
            try
            {
                // 初始化 Custom Function
                InitializeCustomFunction();

                // 剖析 Expression
                var exp = Expression.Parse(expression);

                // 執行 Expression
                var (value, error) = exp.TryEvaluate(data);

                // 剖析執行結果
                if (error == null && value != null)
                {
                    return JToken.FromObject(value);
                }
                return null;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Evaluate Expresson With Variables
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public JToken Evaluate(string expression, Dictionary<string, object> parameters)
        {
            try
            {
                // 初始化 Custom Function
                InitializeCustomFunction();

                // 剖析 Expression
                var exp = Expression.Parse(expression);

                // 執行 Expression
                var (value, error) = exp.TryEvaluate(parameters);

                // 剖析執行結果
                if (error == null && value != null)
                {
                    return JToken.FromObject(value);
                }
                return JToken.FromObject(expression);
            }
            catch
            {
                return JToken.FromObject(expression);
            }
        }

        #endregion
    }
}
