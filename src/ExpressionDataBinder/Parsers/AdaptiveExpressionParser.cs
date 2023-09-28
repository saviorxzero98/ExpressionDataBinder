using System.Text;
using System.Text.RegularExpressions;

namespace ExpressionDataBinder.Parsers
{
    public class AdaptiveExpressionParser : IExpressionParser
    {
        private const string ExpressionGroup = "Expression";

        public const char BeginBracketChar = '{';
        public const char EndBracketChar = '}';
        public const char QuotationMarkChar = '\'';
        public const char DoubleQuotationMarkChar = '"';

        public const string BeginBracket = "${";
        public const string BeginBracketPattern = "\\${";   // 提供給 Regex 使用
        public const string EndBracket = "}";
        public const string ExpressionBodyPattern = "\\s*(?<" + ExpressionGroup + ">((?!" + BeginBracketPattern + ").)*)\\s*";


        /// <summary>
        /// Adaptive Expression 規則
        /// Pattern: ${Variable} or ${Expression}
        /// Rule: \${\s*(?<Expression>((?!\${).)*)\s*}
        /// </summary>
        public static string ExpressionPattern
        {
            get
            {
                return $"{BeginBracketPattern}{ExpressionBodyPattern}{EndBracket}";
            }
        }

        /// <summary>
        /// 是否混用其他的 Expression Binder
        /// </summary>
        public bool IsMixedExpressionBinder { get; set; }


        public AdaptiveExpressionParser(bool isMixedExpressionBinder = true)
        {
            IsMixedExpressionBinder = isMixedExpressionBinder;
        }

        /// <summary>
        /// 取得符合 Expression
        /// "${[Expression]}"
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public List<ExpressionParserResult> Match(string text)
        {
            var matchedResules = new List<ExpressionParserResult>();

            // Find "${[Expression]}"
            var pattern = ExpressionPattern;
            var regex = new Regex(pattern, RegexOptions.IgnoreCase);

            var match = regex.Match(text);
            while (match.Success)
            {
                // Remove "${" and "}"
                string expression = GetExpression(match);
                string matchedToken = GetMatchedToken(match);

                // Add Match Result
                matchedResules.Add(new ExpressionParserResult(expression, matchedToken));

                match = match.NextMatch();
            }

            return matchedResules;
        }

        /// <summary>
        /// 檢查是否符合 Expression
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public bool IsMatch(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return false;
            }

            var pattern = ExpressionPattern;
            return Regex.IsMatch(text, $"^{pattern}$", RegexOptions.IgnoreCase);
        }

        /// <summary>
        /// 取出正確的 Expression
        /// </summary>
        /// <param name="match"></param>
        /// <returns></returns>
        private string GetExpression(Match match)
        {
            // 取出 Pattern 符合的 Expression
            string matchedExpression = match.Groups[ExpressionGroup]
                                            .Value
                                            .TrimStart()
                                            .TrimEnd();

            // 修正大括弧未正確取出 Expression
            string expression = FixMatchedText(matchedExpression, 1);
            return expression;
        }

        /// <summary>
        /// 取出正確的 Expression
        /// </summary>
        /// <param name="matchedExpression"></param>
        /// <returns></returns>
        private string GetMatchedToken(Match match)
        {
            // 取出 Pattern 符合的 Text
            string matchText = match.Value;

            // 修正大括弧未正確取出 Text
            string expression = FixMatchedText(matchText, 0);
            return expression;
        }

        /// <summary>
        /// 修正大括弧未正確取出 Text
        /// </summary>
        /// <param name="matchText"></param>
        /// <param name="startBucketLevel"></param>
        /// <returns></returns>
        private string FixMatchedText(string matchText, int startBucketLevel) // TODO
        {
            // 修正後的字串
            StringBuilder fixText = new StringBuilder();

            // 括號層數
            int bucketLevel = startBucketLevel;

            // 是否從左括號開始處理
            bool isHasBucket = (bucketLevel != 0);

            // 字串
            bool charFlag = false;
            bool stringFlag = false;

            // 修正字串
            foreach (var c in matchText)
            {
                switch (c)
                {
                    // 左括號
                    case BeginBracketChar:
                        // 不計算字串內的括號
                        if (!charFlag && !stringFlag)
                        {
                            // 括號層數+1
                            bucketLevel++;
                            isHasBucket = true;
                        }
                        break;

                    // 右括號
                    case EndBracketChar:
                        // 不計算字串內的括號
                        if (!charFlag && !stringFlag)
                        {
                            // 括號層數-1
                            bucketLevel--;
                        }
                        break;

                    // 單引號 (字串)
                    case QuotationMarkChar:
                        if (!stringFlag)
                        {
                            charFlag = !charFlag;
                        }
                        break;

                    // 雙引號 (字串)
                    case DoubleQuotationMarkChar:
                        if (!charFlag)
                        {
                            stringFlag = !stringFlag;
                        }
                        break;
                }

                if (bucketLevel == 0 && isHasBucket)
                {
                    // 括號層數歸零，且至少需要有一層括號

                    if (startBucketLevel == 0)
                    {
                        // 加上右括號
                        fixText.Append(c);
                    }
                    break;
                }

                fixText.Append(c);
            }

            return fixText.ToString();
        }
    }
}
