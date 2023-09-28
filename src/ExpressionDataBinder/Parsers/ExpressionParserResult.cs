namespace ExpressionDataBinder.Parsers
{
    public class ExpressionParserResult
    {
        public ExpressionParserResult()
        {

        }
        public ExpressionParserResult(string expression, string token)
        {
            Expression = expression;
            MatchedToken = token;
        }


        /// <summary>
        /// 符合的 Token
        /// </summary>
        public string MatchedToken { get; set; } = string.Empty;

        /// <summary>
        /// Expression 內容
        /// </summary>
        public string Expression { get; set; } = string.Empty;
    }
}
