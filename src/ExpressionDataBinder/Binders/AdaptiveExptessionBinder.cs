using ExpressionDataBinder.Evaluators;
using ExpressionDataBinder.Parsers;
using Newtonsoft.Json.Linq;

namespace ExpressionDataBinder.Binders
{
    public class AdaptiveExptessionBinder : IExptessionBinder
    {
        /// <summary>
        /// 設定
        /// </summary>
        public AdaptiveExptessionBinderSettings Settings { get; protected set; }

        public AdaptiveExptessionBinder(bool isForce = true)
        {
            Settings = new AdaptiveExptessionBinderSettings().UseForceBinding(isForce);
        }
        public AdaptiveExptessionBinder(AdaptiveExptessionBinderSettings settings)
        {
            Settings = settings;
        }


        #region 綁資料到文字上

        /// <summary>
        /// 綁定資料
        /// </summary>
        /// <param name="text"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public ExpressionBindResult BindData(string text, object data = null)
        {
            // 檢查訊息和JSON物件
            if (string.IsNullOrEmpty(text))
            {
                return new ExpressionBindResult(JToken.FromObject(text ?? string.Empty), false);
            }

            // Expression Parser
            var parser = new AdaptiveExpressionParser();

            // 尋找訊息中需要 Binding的地方，以 ${函式} 註記要 Binding 的地方
            List<ExpressionParserResult> matchedResults = parser.Match(text);

            return GetTextByExpression(matchedResults, text, data, Settings.IsForce);
        }

        #endregion


        #region 透過 Expression 取出資料

        /// <summary>
        /// 透過 Expression 取出資料
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public ExpressionBindResult GetDataByExpression(string expression, object data = null)
        {
            // 檢查訊息和JSON物件
            if (string.IsNullOrEmpty(expression))
            {
                return new ExpressionBindResult(JToken.FromObject(expression ?? string.Empty), false);
            }

            // Expression Parser
            var parser = new AdaptiveExpressionParser();

            // 尋找訊息中需要 Binding的地方，以 ${函式} 註記要 Binding 的地方
            List<ExpressionParserResult> matchedResults = parser.Match(expression);


            if (matchedResults.Count == 1)
            {
                var matchedResult = matchedResults.FirstOrDefault();
                return GetValueByExpression(matchedResult, expression, data, Settings.IsForce);
            }
            else
            {
                return GetTextByExpression(matchedResults, expression, data, Settings.IsForce);
            }
        }


        /// <summary>
        /// 透過 Adaptive Expression 取出 Value
        /// </summary>
        /// <param name="matchedResult"></param>
        /// <param name="text"></param>
        /// <param name="data"></param>
        /// <param name="isForce"></param>
        /// <returns></returns>
        protected ExpressionBindResult GetValueByExpression(ExpressionParserResult matchedResult,
                                                            string text, object data, bool isForce)
        {
            bool isSuccess = false;
            bool hasForcedBind = false;

            if (matchedResult == null)
            {
                // 回傳結果
                return new ExpressionBindResult(isSuccess, text, hasForcedBind);
            }

            try
            {
                var evaluator = new AdaptiveExpressionEvaluator();

                string expression = matchedResult.Expression;
                var output = (data != null) ? evaluator.Evaluate(expression, data) : evaluator.Evaluate(expression);

                if (output != null)
                {
                    isSuccess = true;

                    // Expression 執行成功時
                    return new ExpressionBindResult(isSuccess, output, hasForcedBind);
                }
            }
            catch
            {

            }

            // Expression 執行失敗時
            if (isForce)
            {   // 強制轉換
                hasForcedBind = true;

                // 回傳結果
                return new ExpressionBindResult(isSuccess, string.Empty, hasForcedBind);
            }
            else
            {
                // 回傳結果
                return new ExpressionBindResult(isSuccess, text, hasForcedBind);
            }
        }

        /// <summary>
        ///  透過 Adaptive Expression 取出 Text
        /// </summary>
        /// <param name="matchedResults"></param>
        /// <param name="text"></param>
        /// <param name="data"></param>
        /// <param name="isForce"></param>
        /// <returns></returns>
        protected ExpressionBindResult GetTextByExpression(List<ExpressionParserResult> matchedResults,
                                                           string text, object data, bool isForce)
        {
            // 替換訊息中需要 Binding的地方
            bool isSuccess = false;
            bool hasForcedBind = false;
            string textValue = text;

            foreach (var matchedResult in matchedResults)
            {
                try
                {
                    var evaluator = new AdaptiveExpressionEvaluator();

                    string expression = matchedResult.Expression;
                    var output = (data != null) ? evaluator.Evaluate(expression, data) : evaluator.Evaluate(expression);

                    if (output != null)
                    {
                        textValue = textValue.Replace(matchedResult.MatchedToken, Convert.ToString(output));
                        isSuccess = true;
                    }
                    else
                    {
                        if (isForce)
                        {   // 強制轉換
                            textValue = textValue.Replace(matchedResult.MatchedToken, string.Empty);
                            hasForcedBind = true;
                        }
                    }
                }
                catch
                {
                    if (isForce)
                    {   // 強制轉換
                        textValue = textValue.Replace(matchedResult.MatchedToken, string.Empty);
                        hasForcedBind = true;
                    }
                }
            }

            // 回傳結果
            return new ExpressionBindResult(isSuccess, textValue, hasForcedBind);
        }

        #endregion
    }

    public class AdaptiveExptessionBinderSettings
    {
        /// <summary>
        /// 是否啟用
        /// </summary>
        public bool IsEnable { get; set; } = true;

        /// <summary>
        /// 如果資料不存在或是為空時，是否強制綁定空字元上去
        /// </summary>
        public bool IsForce { get; set; } = true;

        /// <summary>
        /// 啟用
        /// </summary>
        /// <returns></returns>
        public AdaptiveExptessionBinderSettings Enable()
        {
            IsEnable = true;
            return this;
        }
        /// <summary>
        /// 停用
        /// </summary>
        /// <returns></returns>
        public AdaptiveExptessionBinderSettings Disable()
        {
            IsEnable = false;
            return this;
        }
        /// <summary>
        /// 使用強制綁定
        /// </summary>
        /// <param name="isForce"></param>
        /// <returns></returns>
        public AdaptiveExptessionBinderSettings UseForceBinding(bool isForce = true)
        {
            IsForce = isForce;
            return this;
        }
    }
}
