namespace ExpressionDataBinder.Parsers
{
    /// <summary>
    /// Expression 解析器
    /// </summary>
    public interface IExpressionParser
    {
        List<ExpressionParserResult> Match(string text);

        bool IsMatch(string text);
    }
}
