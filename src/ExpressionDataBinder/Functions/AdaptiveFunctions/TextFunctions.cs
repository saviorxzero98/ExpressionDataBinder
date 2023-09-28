using ExpressionDataBinder.Extensions;
using Newtonsoft.Json.Linq;

namespace ExpressionDataBinder.Functions.AdaptiveFunctions
{
    public static class TextFunctions
    {
        private const string ClassName = "text";

        /// <summary>
        /// 檢查文字是否相等
        /// text.exact('{textA}', '{textB}', {ignoreCase}?, {ignoreWidth}?)
        /// </summary>
        public static AdaptiveFunction Exact
        {
            get
            {
                object function(IReadOnlyList<dynamic> args)
                {
                    if (args.Count < 2)
                    {
                        return false;
                    }

                    var arguments = new ArgumentCollection(args);
                    string textA = arguments.Get(string.Empty);
                    string textB = arguments.Get(string.Empty);
                    bool ignoreCase = arguments.Get(false);
                    bool ignoreWidth = arguments.Get(false);


                    if (string.IsNullOrEmpty(textA) ||
                        string.IsNullOrEmpty(textB))
                    {
                        return false;
                    }

                    return textA.EqualsIngoreWidth(textB, ignoreCase, ignoreWidth);
                }

                return new AdaptiveFunction(ClassName, nameof(Exact), function);
            }
        }

        /// <summary>
        /// 檢查文字是否為 Null 或是 Empty
        /// text.isNullOrEmpty('{text}')
        /// </summary>
        public static AdaptiveFunction IsNullOrEmpty
        {
            get
            {
                object function(IReadOnlyList<dynamic> args)
                {
                    if (args.Count < 1)
                    {
                        return false;
                    }
                    var arguments = new ArgumentCollection(args);

                    string text = arguments.Get(string.Empty);
                    return string.IsNullOrEmpty(text);
                }

                return new AdaptiveFunction(ClassName, nameof(IsNullOrEmpty), function);
            }
        }

        /// <summary>
        /// 檢查文字是否為 Null、Empty 或是空白
        /// text.isNullOrWhiteSpace('{text}')
        /// </summary>
        public static AdaptiveFunction IsNullOrWhiteSpace
        {
            get
            {
                object function(IReadOnlyList<dynamic> args)
                {
                    if (args.Count < 1)
                    {
                        return false;
                    }
                    var arguments = new ArgumentCollection(args);

                    string text = arguments.Get(string.Empty);
                    return string.IsNullOrWhiteSpace(text);
                }

                return new AdaptiveFunction(ClassName, nameof(IsNullOrWhiteSpace), function);
            }
        }

        /// <summary>
        /// Trim
        /// text.trim('{text}', '{trim chars}'?)
        /// </summary>
        public static AdaptiveFunction Trim
        {
            get
            {
                object function(IReadOnlyList<dynamic> args)
                {
                    if (args.Count < 1)
                    {
                        return string.Empty;
                    }
                    var arguments = new ArgumentCollection(args);

                    string text = arguments.Get(string.Empty);
                    string trimChars = arguments.Get(string.Empty);

                    if (string.IsNullOrEmpty(trimChars))
                    {
                        return text.Trim();
                    }
                    else
                    {
                        return text.Trim(trimChars);
                    }
                }

                return new AdaptiveFunction(ClassName, nameof(Trim), function);
            }
        }

        /// <summary>
        /// Trim Start
        /// text.trimStart('{text}', '{trim chars}'?)
        /// </summary>
        public static AdaptiveFunction TrimStart
        {
            get
            {
                object function(IReadOnlyList<dynamic> args)
                {
                    if (args.Count < 1)
                    {
                        return string.Empty;
                    }
                    var arguments = new ArgumentCollection(args);

                    string text = arguments.Get(string.Empty);
                    string trimChars = arguments.Get(string.Empty);

                    if (string.IsNullOrEmpty(trimChars))
                    {
                        return text.TrimStart();
                    }
                    else
                    {
                        return text.TrimStart(trimChars);
                    }
                }

                return new AdaptiveFunction(ClassName, nameof(TrimStart), function);
            }
        }

        /// <summary>
        /// Trim End
        /// text.trimEnd('{text}', '{trim chars}'?)
        /// </summary>
        public static AdaptiveFunction TrimEnd
        {
            get
            {
                object function(IReadOnlyList<dynamic> args)
                {
                    if (args.Count < 1)
                    {
                        return string.Empty;
                    }
                    var arguments = new ArgumentCollection(args);

                    string text = arguments.Get(string.Empty);
                    string trimChars = arguments.Get(string.Empty);

                    if (string.IsNullOrEmpty(trimChars))
                    {
                        return text.TrimEnd();
                    }
                    else
                    {
                        return text.TrimEnd(trimChars);
                    }
                }

                return new AdaptiveFunction(ClassName, nameof(TrimEnd), function);
            }
        }

        /// <summary>
        /// Trim Start Word
        /// text.trimStartWord('{text}', '{trim start word}', {ignoreCase}?)
        /// </summary>
        public static AdaptiveFunction TrimStartWord
        {
            get
            {
                object function(IReadOnlyList<dynamic> args)
                {
                    if (args.Count < 1)
                    {
                        return string.Empty;
                    }
                    var arguments = new ArgumentCollection(args);

                    string text = arguments.Get(string.Empty);
                    string startWord = arguments.Get(string.Empty);
                    bool ignoreCase = arguments.Get(false);

                    if (string.IsNullOrEmpty(startWord))
                    {
                        return text.TrimStart();
                    }
                    else
                    {
                        return text.TrimStartWord(startWord);
                    }
                }

                return new AdaptiveFunction(ClassName, nameof(TrimStartWord), function);
            }
        }

        /// <summary>
        /// Trim End Word
        /// text.trimEndWord('{text}', '{trim start word}', {ignoreCase}?)
        /// </summary>
        public static AdaptiveFunction TrimEndWord
        {
            get
            {
                object function(IReadOnlyList<dynamic> args)
                {
                    if (args.Count < 1)
                    {
                        return string.Empty;
                    }
                    var arguments = new ArgumentCollection(args);

                    string text = arguments.Get(string.Empty);
                    string endWord = arguments.Get(string.Empty);
                    bool ignoreCase = arguments.Get(false);

                    if (string.IsNullOrEmpty(endWord))
                    {
                        return text.TrimEnd();
                    }
                    else
                    {
                        return text.TrimEndWord(endWord);
                    }
                }

                return new AdaptiveFunction(ClassName, nameof(TrimEndWord), function);
            }
        }


        /// <summary>
        /// 字串的第一個字轉大寫
        /// text.toFirstUpper('{text}')
        /// </summary>
        public static AdaptiveFunction ToFirstUpper
        {
            get
            {
                object function(IReadOnlyList<dynamic> args)
                {
                    if (args.Count < 1)
                    {
                        return string.Empty;
                    }
                    var arguments = new ArgumentCollection(args);

                    string text = arguments.Get(string.Empty);
                    return text.ToFirstUpper();
                }

                return new AdaptiveFunction(ClassName, nameof(ToFirstUpper), function);
            }
        }

        /// <summary>
        /// 字串的第一個字轉小寫
        /// text.toFirstLower('{text}')
        /// </summary>
        public static AdaptiveFunction ToFirstLower
        {
            get
            {
                object function(IReadOnlyList<dynamic> args)
                {
                    if (args.Count < 1)
                    {
                        return string.Empty;
                    }
                    var arguments = new ArgumentCollection(args);

                    string text = arguments.Get(string.Empty);
                    return text.ToFirstLower();
                }

                return new AdaptiveFunction(ClassName, nameof(ToFirstLower), function);
            }
        }

        /// <summary>
        /// 轉全形
        /// text.toFullWidth('{text}')
        /// </summary>
        public static AdaptiveFunction ToFullWidth
        {
            get
            {
                object function(IReadOnlyList<dynamic> args)
                {
                    if (args.Count < 1)
                    {
                        return string.Empty;
                    }
                    var arguments = new ArgumentCollection(args);

                    string text = arguments.Get(string.Empty);
                    return text.ToFullWidth();
                }

                return new AdaptiveFunction(ClassName, nameof(ToFullWidth), function);
            }
        }

        /// <summary>
        /// 轉半形
        /// text.toHalfWidth('{text}')
        /// </summary>
        public static AdaptiveFunction ToHalfWidth
        {
            get
            {
                object function(IReadOnlyList<dynamic> args)
                {
                    if (args.Count < 1)
                    {
                        return string.Empty;
                    }
                    var arguments = new ArgumentCollection(args);

                    string text = arguments.Get(string.Empty);
                    return text.ToHalfWidth();
                }

                return new AdaptiveFunction(ClassName, nameof(ToHalfWidth), function);
            }
        }

        /// <summary>
        /// 轉 Pascal Case
        /// text.toPascalCase('{text}')
        /// </summary>
        public static AdaptiveFunction ToPascalCase
        {
            get
            {
                object function(IReadOnlyList<dynamic> args)
                {
                    if (args.Count < 1)
                    {
                        return string.Empty;
                    }
                    var arguments = new ArgumentCollection(args);

                    string text = arguments.Get(string.Empty);
                    return text.ToPascalCase();
                }

                return new AdaptiveFunction(ClassName, nameof(ToPascalCase), function);
            }
        }

        /// <summary>
        /// 轉 Camel Case
        /// text.toCamelCase('{text}')
        /// </summary>
        public static AdaptiveFunction ToCamelCase
        {
            get
            {
                object function(IReadOnlyList<dynamic> args)
                {
                    if (args.Count < 1)
                    {
                        return string.Empty;
                    }
                    var arguments = new ArgumentCollection(args);

                    string text = arguments.Get(string.Empty);
                    return text.ToCamelCase();
                }

                return new AdaptiveFunction(ClassName, nameof(ToCamelCase), function);
            }
        }

        /// <summary>
        /// 轉 Snake Case
        /// text.toSnakeCase('{text}', {isAllCap}?)
        /// </summary>
        public static AdaptiveFunction ToSnakeCase
        {
            get
            {
                object function(IReadOnlyList<dynamic> args)
                {
                    if (args.Count < 1)
                    {
                        return string.Empty;
                    }
                    var arguments = new ArgumentCollection(args);

                    string text = arguments.Get(string.Empty);
                    bool isAllCap = arguments.Get(false);
                    return text.ToSnakeCase(isAllCap);
                }

                return new AdaptiveFunction(ClassName, nameof(ToSnakeCase), function);
            }
        }

        /// <summary>
        /// 轉 Kebab-Case
        /// text.toKebabCase('{text}', {isAllCap}?)
        /// </summary>
        public static AdaptiveFunction ToKebabCase
        {
            get
            {
                object function(IReadOnlyList<dynamic> args)
                {
                    if (args.Count < 1)
                    {
                        return string.Empty;
                    }
                    var arguments = new ArgumentCollection(args);

                    string text = arguments.Get(string.Empty);
                    bool isAllCap = arguments.Get(false);
                    return text.ToKebabCase(isAllCap);
                }

                return new AdaptiveFunction(ClassName, nameof(ToKebabCase), function);
            }
        }

        /// <summary>
        /// Truncate Word
        /// text.truncateWord('{text}', {word count})
        /// </summary>
        public static AdaptiveFunction TruncateWord
        {
            get
            {
                object function(IReadOnlyList<dynamic> args)
                {
                    if (args.Count < 1)
                    {
                        return string.Empty;
                    }
                    var arguments = new ArgumentCollection(args);

                    string text = arguments.Get(string.Empty);
                    int wordCount = arguments.Get(0);

                    if (wordCount > 0)
                    {
                        return text.TruncateWord(wordCount);
                    }
                    return text;
                }

                return new AdaptiveFunction(ClassName, nameof(TruncateWord), function);
            }
        }

        /// <summary>
        /// 使用 Regular expression 取代指定文字
        /// text.replaceByRegex('{text}', '{pattern}', '{newText}', {ignoreCase}?)
        /// </summary>
        public static AdaptiveFunction ReplaceByRegex
        {
            get
            {
                object function(IReadOnlyList<dynamic> args)
                {
                    if (args.Count < 3)
                    {
                        return string.Empty;
                    }
                    var arguments = new ArgumentCollection(args);

                    string text = arguments.Get(string.Empty);
                    string pattern = arguments.Get(string.Empty);
                    string newText = arguments.Get(string.Empty);
                    bool ignoreCase = arguments.Get(false);


                    if (string.IsNullOrEmpty(pattern))
                    {
                        return text;
                    }
                    else
                    {
                        return text.ReplaceByRegex(pattern, newText, ignoreCase);
                    }
                }

                return new AdaptiveFunction(ClassName, nameof(ReplaceByRegex), function);
            }
        }

        /// <summary>
        /// 透過 Regular Expression 取得指定的字串
        /// text.getRegexGroupValue('{text}', '{pattern}', '{groupName}', {ignoreCase}?)
        /// text.getRegexGroupValue('{text}', '{pattern}', groupIndex, {ignoreCase}?)
        /// </summary>
        public static AdaptiveFunction GetRegexGroupValue
        {
            get
            {
                object function(IReadOnlyList<dynamic> args)
                {
                    if (args.Count < 3)
                    {
                        return string.Empty;
                    }
                    var arguments = new ArgumentCollection(args);

                    string text = arguments.Get(string.Empty);
                    string pattern = arguments.Get(string.Empty);
                    JToken group = arguments.Get();
                    bool ignoreCase = arguments.Get(false);


                    if (string.IsNullOrEmpty(pattern) || group == null)
                    {
                        return string.Empty;
                    }

                    switch (group.Type)
                    {
                        case JTokenType.Integer:
                            int groupIndex = group.ToObject<int>();
                            return text.GetRegexGroupValue(pattern, groupIndex, ignoreCase);

                        case JTokenType.String:
                        default:
                            string groupName = group.ToString();
                            return text.GetRegexGroupValue(pattern, groupName, ignoreCase);
                    }
                }

                return new AdaptiveFunction(ClassName, nameof(GetRegexGroupValue), function);
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
                Exact, IsNullOrEmpty, IsNullOrWhiteSpace,
                Trim, TrimStart, TrimEnd, TrimStartWord, TrimEndWord,
                ToFirstUpper, ToFirstLower, ToCamelCase, ToPascalCase, ToSnakeCase, ToKebabCase,
                TruncateWord, ReplaceByRegex, GetRegexGroupValue
            };

            return functions;
        }
    }
}
