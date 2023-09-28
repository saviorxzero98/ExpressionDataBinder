using Newtonsoft.Json.Linq;

namespace ExpressionDataBinder.Evaluators
{
    /// <summary>
    /// Expression 運算器
    /// </summary>
    public interface IExpressionEvaluator
    {
        /// <summary>
        /// Evaluate Expresson
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        JToken Evaluate(string expression);

        /// <summary>
        /// Evaluate Expresson With Data
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        JToken Evaluate(string expression, object data);

        /// <summary>
        /// Evaluate Expresson With Variables
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        JToken Evaluate(string expression, Dictionary<string, object> parameters);
    }
}
