using Newtonsoft.Json.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace ExpressionDataBinder.Extensions
{
    public static class StringExtensions
    {
        private const StringComparison _ignoreCase = StringComparison.InvariantCultureIgnoreCase;


        #region 字串檢查與比對

        /// <summary>
        /// 字串是否為 Null
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsNull(this string value)
        {
            return (value == null);
        }
        /// <summary>
        /// 字串是否為 Null 或空白
        /// </summary>
        public static bool IsNullOrEmpty(this string value)
        {
            return string.IsNullOrEmpty(value);
        }
        /// <summary>
        /// 字串是否為 Null、空白或空格字元
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsNullOrWhiteSpace(this string value)
        {
            return string.IsNullOrWhiteSpace(value);
        }


        /// <summary>
        /// 檢查字串是否相等 (忽略大小寫)
        /// </summary>
        /// <param name="valueA"></param>
        /// <param name="valueB"></param>
        /// <param name="ignoreCase"></param>
        /// <returns></returns>
        public static bool EqualsIgnoreCase(this string valueA, string valueB, bool ignoreCase = true)
        {
            if (valueA.IsNull() || valueB.IsNull())
            {
                return false;
            }

            if (ignoreCase)
            {
                return valueA.Equals(valueB, _ignoreCase);
            }
            else
            {
                return valueA.Equals(valueB);
            }
        }
        /// <summary>
        /// 檢查字串是否相等 (忽略全形、半形)
        /// </summary>
        /// <param name="valueA"></param>
        /// <param name="valueB"></param>
        /// <param name="ignoreWidth"></param>
        /// <returns></returns>
        public static bool EqualsIngoreWidth(this string valueA, string valueB, bool ignoreWidth = true)
        {
            return valueA.EqualsIngoreWidth(valueB, false, ignoreWidth);
        }
        /// <summary>
        /// 檢查字串是否相等 (忽略全形、半形)
        /// </summary>
        /// <param name="valueA"></param>
        /// <param name="valueB"></param>
        /// <param name="ignoreCase"></param>
        /// <param name="ignoreWidth"></param>
        /// <returns></returns>
        public static bool EqualsIngoreWidth(this string valueA, string valueB, bool ignoreCase = false, bool ignoreWidth = true)
        {
            if (valueA.IsNull() || valueB.IsNull())
            {
                return false;
            }

            if (ignoreWidth)
            {
                if (ignoreCase)
                {
                    return valueA.ToHalfWidth()
                                 .EqualsIgnoreCase(valueB.ToHalfWidth());
                }
                else
                {
                    return valueA.ToHalfWidth()
                                 .Equals(valueB.ToHalfWidth());
                }
            }
            else
            {
                return valueA.EqualsIgnoreCase(valueB, ignoreCase);
            }
        }


        /// <summary>
        /// 檢查字首 (忽略大小寫)
        /// </summary>
        /// <param name="value"></param>
        /// <param name="findValue"></param>
        /// <param name="ignoreCase"></param>
        /// <returns></returns>
        public static bool StartsWithIgnoreCase(this string value, string findValue, bool ignoreCase = true)
        {
            if (value.IsNullOrEmpty() || findValue.IsNullOrEmpty())
            {
                return false;
            }

            if (ignoreCase)
            {
                return value.StartsWith(findValue, _ignoreCase);
            }
            else
            {
                return value.StartsWith(findValue);
            }
        }

        /// <summary>
        /// 檢查字首 (忽略全形、半形)
        /// </summary>
        /// <param name="value"></param>
        /// <param name="findValue"></param>
        /// <param name="ignoreWidth"></param>
        /// <param name="ignoreCase"></param>
        /// <returns></returns>
        public static bool StartsWithIgnoreWidth(this string value, string findValue, bool ignoreWidth = true, bool ignoreCase = false)
        {
            if (value.IsNullOrEmpty() || findValue.IsNullOrEmpty())
            {
                return false;
            }

            if (ignoreWidth)
            {
                if (ignoreCase)
                {
                    return value.ToHalfWidth()
                                .StartsWithIgnoreCase(findValue.ToHalfWidth());
                }
                else
                {
                    return value.ToHalfWidth()
                                .StartsWith(findValue.ToHalfWidth());
                }
            }
            else
            {
                return value.StartsWithIgnoreCase(findValue);
            }
        }


        /// <summary>
        /// 檢查字尾 (忽略大小寫)
        /// </summary>
        /// <param name="value"></param>
        /// <param name="findValue"></param>
        /// <param name="ignoreCase"></param>
        /// <returns></returns>
        public static bool EndsWithIgnoreCase(this string value, string findValue, bool ignoreCase = true)
        {
            if (value.IsNullOrEmpty() || findValue.IsNullOrEmpty())
            {
                return false;
            }

            if (ignoreCase)
            {
                return value.EndsWith(value, _ignoreCase);
            }
            else
            {
                return value.EndsWith(value);
            }
        }
        /// <summary>
        /// 檢查字尾 (忽略全形、半形)
        /// </summary>
        /// <param name="value"></param>
        /// <param name="findValue"></param>
        /// <param name="ignoreWidth"></param>
        /// <returns></returns>
        public static bool EndsWithIgnoreWidth(this string value, string findValue, bool ignoreWidth = true)
        {
            return value.EndsWithIgnoreWidth(findValue, false, ignoreWidth);
        }
        /// <summary>
        /// 檢查字尾 (忽略全形、半形)
        /// </summary>
        /// <param name="value"></param>
        /// <param name="findValue"></param>
        /// <param name="ignoreCase"></param>
        /// <param name="ignoreWidth"></param>
        /// <returns></returns>
        public static bool EndsWithIgnoreWidth(this string value, string findValue, bool ignoreCase, bool ignoreWidth = true)
        {
            if (value.IsNullOrEmpty() || findValue.IsNullOrEmpty())
            {
                return false;
            }

            if (ignoreWidth)
            {
                if (ignoreCase)
                {
                    return value.ToHalfWidth()
                                .EndsWithIgnoreCase(findValue.ToHalfWidth());
                }
                else
                {
                    return value.ToHalfWidth()
                                .EndsWith(findValue.ToHalfWidth());
                }
            }
            else
            {
                return value.EndsWithIgnoreCase(findValue);
            }
        }


        /// <summary>
        /// 檢查是否包含 (忽略大小寫)
        /// </summary>
        /// <param name="value"></param>
        /// <param name="findValue"></param>
        /// <returns></returns>
        public static bool ContainsIgnoreCase(this string value, string findValue, bool ignoreCase = true)
        {
            if (value.IsNullOrEmpty() || findValue.IsNullOrEmpty())
            {
                return false;
            }

            if (ignoreCase)
            {
                return (value.IndexOf(findValue, _ignoreCase) >= 0);
            }
            else
            {
                return value.Contains(findValue);
            }
        }
        /// <summary>
        /// 檢查是否包含 (忽略大小寫)
        /// </summary>
        /// <param name="value"></param>
        /// <param name="findValue"></param>
        /// <returns></returns>
        public static bool ContainsIgnoreCase(this string value, char findValue, bool ignoreCase = true)
        {
            string findText = findValue.ToString();
            if (value.IsNullOrEmpty() || findText.IsNullOrEmpty())
            {
                return false;
            }

            if (ignoreCase)
            {
                return (value.IndexOf(findText, _ignoreCase) >= 0);
            }
            else
            {
                return value.Contains(findText);
            }
        }

        /// <summary>
        /// 檢查是否包含 (忽略全形、半形)
        /// </summary>
        /// <param name="value"></param>
        /// <param name="findValue"></param>
        /// <returns></returns>
        public static bool ContainsIgnoreWidth(this string value, string findValue, bool ignoreWidth = true)
        {
            return value.ContainsIgnoreWidth(findValue, false, ignoreWidth);
        }
        /// <summary>
        /// 檢查是否包含 (忽略全形、半形)
        /// </summary>
        /// <param name="value"></param>
        /// <param name="findValue"></param>
        /// <param name="ignoreCase"></param>
        /// <returns></returns>
        public static bool ContainsIgnoreWidth(this string value, string findValue, bool ignoreCase, bool ignoreWidth = true)
        {
            if (value.IsNullOrEmpty() || findValue.IsNullOrEmpty())
            {
                return false;
            }

            if (ignoreWidth)
            {
                if (ignoreCase)
                {
                    return value.ToHalfWidth()
                                .ContainsIgnoreCase(findValue.ToHalfWidth());
                }
                else
                {
                    return value.ToHalfWidth()
                                .Contains(findValue.ToHalfWidth());
                }
            }
            else
            {
                return value.ContainsIgnoreCase(findValue);
            }
        }
        /// <summary>
        /// 檢查是否包含 (忽略全形、半形)
        /// </summary>
        /// <param name="value"></param>
        /// <param name="findValue"></param>
        /// <returns></returns>
        public static bool ContainsIgnoreWidth(this string value, char findValue, bool ignoreWidth = true)
        {
            return value.ContainsIgnoreWidth(findValue, false, ignoreWidth);
        }
        /// <summary>
        /// 檢查是否包含 (忽略全形、半形)
        /// </summary>
        /// <param name="value"></param>
        /// <param name="findValue"></param>
        /// <param name="ignoreCase"></param>
        /// <returns></returns>
        public static bool ContainsIgnoreWidth(this string value, char findValue, bool ignoreCase, bool ignoreWidth = true)
        {
            string findText = findValue.ToString();
            if (value.IsNullOrEmpty() || findText.IsNullOrEmpty())
            {
                return false;
            }

            if (ignoreWidth)
            {
                if (ignoreCase)
                {
                    return value.ToHalfWidth()
                                .ContainsIgnoreCase(findText.ToHalfWidth());
                }
                else
                {
                    return value.ToHalfWidth()
                                .Contains(findText.ToHalfWidth());
                }
            }
            else
            {
                return value.ContainsIgnoreCase(findValue);
            }
        }


        /// <summary>
        /// 檢查字串是否符合 Regular Expression
        /// </summary>
        /// <param name="value"></param>
        /// <param name="pattern"></param>
        /// <returns></returns>
        public static bool IsMatchRegex(this string value, string pattern)
        {
            return value.IsMatchRegex(pattern, false);
        }
        /// <summary>
        /// 檢查字串是否符合 Regular Expression
        /// </summary>
        /// <param name="value"></param>
        /// <param name="pattern"></param>
        /// <param name="ignoreCase"></param>
        /// <returns></returns>
        public static bool IsMatchRegex(this string value, string pattern, bool ignoreCase)
        {
            if (value.IsNullOrEmpty() || pattern.IsNullOrEmpty())
            {
                return false;
            }

            if (ignoreCase)
            {
                return Regex.IsMatch(value, pattern, RegexOptions.IgnoreCase);
            }
            else
            {
                return Regex.IsMatch(value, pattern);
            }
        }

        /// <summary>
        /// 檢查字串是否符合 Regular Expression (忽略全形、半形)
        /// </summary>
        /// <param name="value"></param>
        /// <param name="pattern"></param>
        /// <returns></returns>
        public static bool IsMatchRegexIgnoreWidth(this string value, string pattern)
        {
            return value.ToHalfWidth().IsMatchRegex(pattern.ToHalfWidth());
        }
        /// <summary>
        /// 檢查字串是否符合 Regular Expression (忽略全形、半形)
        /// </summary>
        /// <param name="value"></param>
        /// <param name="pattern"></param>
        /// <param name="ignoreCase"></param>
        /// <returns></returns>
        public static bool IsMatchRegexIgnoreWidth(this string value, string pattern, bool ignoreCase)
        {
            return value.ToHalfWidth().IsMatchRegex(pattern.ToHalfWidth(), ignoreCase);
        }


        /// <summary>
        /// 字串是否為數字
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsNumeric(this string value)
        {
            if (value.IsNullOrWhiteSpace())
            {
                return false;
            }

            return double.TryParse(value, out double numeric);
        }

        /// <summary>
        /// 字串是否為日期時間
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsDateTime(this string value)
        {
            if (value.IsNullOrWhiteSpace())
            {
                return false;
            }

            return DateTime.TryParse(value, out DateTime datetime);
        }

        /// <summary>
        /// 字串是否為時間長度
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsTimeSpan(this string value)
        {
            if (value.IsNullOrWhiteSpace())
            {
                return false;
            }

            return TimeSpan.TryParse(value, out TimeSpan timeSpan);
        }

        /// <summary>
        /// 字串是否為 JSON
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsJson(this string value)
        {
            try
            {
                JToken.Parse(value);
                return true;
            }
            catch
            {
                return false;
            }
        }

        #endregion


        #region 字串尋找

        /// <summary>
        /// 從字首開始尋找字串 (忽略大小寫)
        /// </summary>
        /// <param name="value"></param>
        /// <param name="findValue"></param>
        /// <returns></returns>
        public static int IndexOfIgnoreCase(this string value, string findValue)
        {
            if (value.IsNullOrEmpty() || findValue.IsNullOrEmpty())
            {
                return -1;
            }

            return value.IndexOf(findValue, _ignoreCase);
        }
        /// <summary>
        /// 從字首開始尋找字串 (忽略大小寫)
        /// </summary>
        /// <param name="value"></param>
        /// <param name="findValue"></param>
        /// <param name="startIndex"></param>
        /// <returns></returns>
        public static int IndexOfIgnoreCase(this string value, string findValue, int startIndex)
        {
            if (value.IsNullOrEmpty() || findValue.IsNullOrEmpty())
            {
                return -1;
            }

            return value.IndexOf(findValue, startIndex, _ignoreCase);
        }
        /// <summary>
        /// 從字首開始尋找字串 (忽略大小寫)
        /// </summary>
        /// <param name="value"></param>
        /// <param name="findValue"></param>
        /// <param name="startIndex"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static int IndexOfIgnoreCase(this string value, string findValue, int startIndex, int count)
        {
            if (value.IsNullOrEmpty() || findValue.IsNullOrEmpty())
            {
                return -1;
            }

            return value.IndexOf(findValue, startIndex, count, _ignoreCase);
        }


        /// <summary>
        /// 從字尾開始尋找字串 (忽略大小寫)
        /// </summary>
        /// <param name="value"></param>
        /// <param name="findValue"></param>
        /// <returns></returns>
        public static int LastIndexOfIgnoreCase(this string value, string findValue)
        {
            if (value.IsNullOrEmpty() || findValue.IsNullOrEmpty())
            {
                return -1;
            }

            return value.LastIndexOf(findValue, _ignoreCase);
        }
        /// <summary>
        /// 從字尾開始尋找字串 (忽略大小寫)
        /// </summary>
        /// <param name="value"></param>
        /// <param name="findValue"></param>
        /// <param name="startIndex"></param>
        /// <returns></returns>
        public static int LastIndexOfIgnoreCase(this string value, string findValue, int startIndex)
        {
            if (value.IsNullOrEmpty() || findValue.IsNullOrEmpty())
            {
                return -1;
            }

            return value.LastIndexOf(findValue, startIndex, _ignoreCase);
        }
        /// <summary>
        /// 從字尾開始尋找字串 (忽略大小寫)
        /// </summary>
        /// <param name="value"></param>
        /// <param name="findValue"></param>
        /// <param name="startIndex"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static int LastIndexOfIgnoreCase(this string value, string findValue, int startIndex, int count)
        {
            if (value.IsNullOrEmpty() || findValue.IsNullOrEmpty())
            {
                return -1;
            }

            return value.LastIndexOf(findValue, startIndex, count, _ignoreCase);
        }

        #endregion


        #region 字串取代

        /// <summary>
        /// 使用 Regular Expression 取代字串
        /// </summary>
        /// <param name="mainValue"></param>
        /// <param name="oldValuePattern"></param>
        /// <param name="newValue"></param>
        /// <returns></returns>
        public static string ReplaceByRegex(this string mainValue, string oldValuePattern, string newValue)
        {
            if (mainValue.IsNullOrEmpty() || oldValuePattern.IsNullOrEmpty())
            {
                return mainValue;
            }

            return mainValue.ReplaceByRegex(oldValuePattern, newValue, false);
        }
        /// <summary>
        /// 使用 Regular Expression 取代字串
        /// </summary>
        /// <param name="mainValue"></param>
        /// <param name="oldValuePattern"></param>
        /// <param name="newValue"></param>
        /// <param name="ignoreCase"></param>
        /// <returns></returns>
        public static string ReplaceByRegex(this string mainValue, string oldValuePattern, string newValue, bool ignoreCase)
        {
            if (mainValue.IsNullOrEmpty() || oldValuePattern.IsNullOrEmpty())
            {
                return mainValue;
            }

            if (ignoreCase)
            {
                return Regex.Replace(mainValue, oldValuePattern, newValue, RegexOptions.IgnoreCase);
            }
            else
            {
                return Regex.Replace(mainValue, oldValuePattern, newValue);
            }
        }
        /// <summary>
        /// 使用 Regular Expression 取代字串
        /// </summary>
        /// <param name="mainValue"></param>
        /// <param name="oldValuePattern"></param>
        /// <param name="newValueCallback"></param>
        /// <returns></returns>
        public static string ReplaceByRegex(this string mainValue, string oldValuePattern, Func<string, string> newValueCallback)
        {
            var regex = new Regex(oldValuePattern);

            if (mainValue.IsNullOrEmpty() || oldValuePattern.IsNullOrEmpty())
            {
                return mainValue;
            }

            return mainValue.ReplaceByRegex(oldValuePattern, newValueCallback, false);
        }
        /// <summary>
        /// 使用 Regular Expression 取代字串
        /// </summary>
        /// <param name="mainValue"></param>
        /// <param name="oldValuePattern"></param>
        /// <param name="newValueCallback"></param>
        /// <param name="ignoreCase"></param>
        /// <returns></returns>
        public static string ReplaceByRegex(this string mainValue, string oldValuePattern, Func<string, string> newValueCallback, bool ignoreCase)
        {
            if (mainValue.IsNullOrEmpty() || oldValuePattern.IsNullOrEmpty())
            {
                return mainValue;
            }

            var regex = (ignoreCase) ? new Regex(oldValuePattern, RegexOptions.IgnoreCase) : new Regex(oldValuePattern);

            if (regex.IsMatch(mainValue))
            {
                Match match = regex.Match(mainValue);

                // 尋找要取代的文字
                List<string> matchValues = new List<string>();
                while (match.Success)
                {
                    matchValues.Add(match.Value);
                    match = match.NextMatch();
                }

                // 移除重複的要取代之文字
                matchValues = matchValues.Distinct().ToList();

                // 要取代的文字
                foreach (string matchValue in matchValues)
                {
                    string newValue = newValueCallback(matchValue);
                    mainValue = mainValue.Replace(matchValue, newValue);
                }
            }
            return mainValue;
        }

        /// <summary>
        /// 字串取代 (不分大小寫)
        /// </summary>
        /// <param name="mainValue"></param>
        /// <param name="oldValue"></param>
        /// <param name="newValue"></param>
        /// <returns></returns>
        public static string ReplaceIgnoreCase(this string mainValue, string oldValue, string newValue)
        {
            if (mainValue.IsNullOrEmpty() || oldValue.IsNullOrEmpty())
            {
                return mainValue;
            }

            return mainValue.ReplaceByRegex(oldValue, newValue, true);
        }

        /// <summary>
        /// 截掉字首與字尾指定字元集
        /// </summary>
        /// <param name="value"></param>
        /// <param name="trimChars"></param>
        /// <returns></returns>
        public static string Trim(this string value, string trimChars = null)
        {
            if (!string.IsNullOrEmpty(trimChars))
            {
                return value.Trim(trimChars.ToCharArray());
            }
            else
            {
                return value.Trim();
            }
        }

        /// <summary>
        /// 截掉字首指定字元集
        /// </summary>
        /// <param name="value"></param>
        /// <param name="trimChars"></param>
        /// <returns></returns>
        public static string TrimStart(this string value, string trimChars = null)
        {
            if (!string.IsNullOrEmpty(trimChars))
            {
                return value.TrimStart(trimChars.ToCharArray());
            }
            else
            {
                return value.TrimStart();
            }
        }

        /// <summary>
        /// 截掉字尾指定字元集
        /// </summary>
        /// <param name="value"></param>
        /// <param name="trimChars"></param>
        /// <returns></returns>
        public static string TrimEnd(this string value, string trimChars = null)
        {
            if (!string.IsNullOrEmpty(trimChars))
            {
                return value.TrimEnd(trimChars.ToCharArray());
            }
            else
            {
                return value.TrimEnd();
            }
        }

        /// <summary>
        /// 截掉字首的字串
        /// </summary>
        /// <param name="value"></param>
        /// <param name="startWord"></param>
        /// <param name="ignoreCase"></param>
        /// <param name="ignoreWidth"></param>
        /// <returns></returns>
        public static string TrimStartWord(this string value, string startWord = null, bool ignoreCase = false, bool ignoreWidth = false)
        {
            if (string.IsNullOrEmpty(startWord))
            {
                return value.TrimStart();
            }

            bool isMatch = value.StartsWithIgnoreWidth(startWord, ignoreCase, ignoreWidth);

            if (isMatch)
            {
                return value.Substring(startWord.Length);
            }
            return value;
        }

        /// <summary>
        /// 截掉字尾的字串
        /// </summary>
        /// <param name="value"></param>
        /// <param name="endWord"></param>
        /// <param name="ignoreCase"></param>
        /// <param name="ignoreWidth"></param>
        /// <returns></returns>
        public static string TrimEndWord(this string value, string endWord = null, bool ignoreCase = false, bool ignoreWidth = false)
        {
            if (string.IsNullOrEmpty(endWord))
            {
                return value.TrimEnd();
            }

            bool isMatch = value.EndsWithIgnoreWidth(endWord, ignoreCase, ignoreWidth);

            if (isMatch &&
                value.Length > endWord.Length)
            {
                return value.Substring(0, value.Length - endWord.Length);
            }
            return value;
        }

        #endregion


        #region 字串分割

        /// <summary>
        /// 字串分割
        /// </summary>
        /// <param name="value"></param>
        /// <param name="separator"></param>
        /// <returns></returns>
        public static string[] Split(this string value, string separator)
        {
            return value.Split(new string[] { separator }, StringSplitOptions.None);
        }
        /// <summary>
        /// 字串分割
        /// </summary>
        /// <param name="value"></param>
        /// <param name="separator"></param>
        /// <returns></returns>
        public static string[] Split(this string value, char separator)
        {
            return value.Split(new char[] { separator }, StringSplitOptions.None);
        }

        #endregion


        #region Regular Expression

        /// <summary>
        /// 透過 Regular Expression 取得指定的字串
        /// </summary>
        /// <param name="text"></param>
        /// <param name="pattern"></param>
        /// <param name="groupName"></param>
        /// <returns></returns>
        public static string GetRegexGroupValue(this string text, string pattern, string groupName)
        {
            return text.GetRegexGroupValue(pattern, groupName, false);
        }
        /// <summary>
        /// 透過 Regular Expression 取得指定的字串
        /// </summary>
        /// <param name="text"></param>
        /// <param name="pattern"></param>
        /// <param name="groupIndex"></param>
        /// <returns></returns>
        public static string GetRegexGroupValue(this string text, string pattern, int groupIndex)
        {
            return text.GetRegexGroupValue(pattern, groupIndex, false);
        }
        /// <summary>
        /// 透過 Regular Expression 取得指定的字串
        /// </summary>
        /// <param name="text"></param>
        /// <param name="pattern"></param>
        /// <param name="groupName"></param>
        /// <param name="ignoreCase"></param>
        /// <returns></returns>
        public static string GetRegexGroupValue(this string text, string pattern, string groupName, bool ignoreCase)
        {
            var regex = (ignoreCase) ? new Regex(pattern, RegexOptions.IgnoreCase) : new Regex(pattern);

            if (!regex.IsMatch(text))
            {
                return string.Empty;
            }

            // Match Regex Groups
            Match match = regex.Match(text);

            if (match.Groups.Count != 0)
            {
                return match.Groups[groupName].Value;
            }
            return string.Empty;
        }
        /// <summary>
        /// 透過 Regular Expression 取得指定的字串
        /// </summary>
        /// <param name="text"></param>
        /// <param name="pattern"></param>
        /// <param name="groupIndex"></param>
        /// <param name="ignoreCase"></param>
        /// <returns></returns>
        public static string GetRegexGroupValue(this string text, string pattern, int groupIndex, bool ignoreCase)
        {
            var regex = (ignoreCase) ? new Regex(pattern, RegexOptions.IgnoreCase) : new Regex(pattern);

            if (!regex.IsMatch(text))
            {
                return string.Empty;
            }

            // Match Regex Groups
            Match match = regex.Match(text);

            if (match.Groups.Count != 0)
            {
                return match.Groups[groupIndex].Value;
            }
            return string.Empty;
        }

        #endregion


        #region 字串大寫、小寫、全形、半形轉換

        /// <summary>
        /// 字串的第一個字轉大寫
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToFirstUpper(this string value)
        {
            if (value.IsNullOrEmpty())
            {
                return value;
            }

            return value.FirstOrDefault().ToString().ToUpper() + value.Substring(1);
        }

        /// <summary>
        /// 字串的第一個字轉小寫
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToFirstLower(this string value)
        {
            if (value.IsNullOrEmpty())
            {
                return value;
            }

            return value.FirstOrDefault().ToString().ToLower() + value.Substring(1);
        }

        /// <summary>
        /// 每個單字 (Word) 的第一個字元轉大寫
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToFirstLetterUpper(this string value)
        {
            if (value.IsNullOrEmpty())
            {
                return value;
            }

            string[] words = value.Split(' ');

            for (int i = 0; i < words.Length; i++)
            {
                words[i] = words[i].FirstOrDefault().ToString().ToUpper() + words[i].Substring(1);
            }

            return string.Join(" ", words);
        }

        /// <summary>
        /// 每個單字 (Word) 的第一個字元轉小寫
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToFirstLetterLower(this string value)
        {
            if (value.IsNullOrEmpty())
            {
                return value;
            }

            string[] words = value.Split(' ');

            for (int i = 0; i < words.Length; i++)
            {
                words[i] = words[i].FirstOrDefault().ToString().ToLower() + words[i].Substring(1);
            }

            return string.Join(" ", words);
        }

        /// <summary>
        /// 轉成全形
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToFullWidth(this string value)
        {
            char[] chars = value.ToCharArray();
            chars = chars.Select(c => c.ToFullWidth()).ToArray();
            return new string(chars);
        }

        /// <summary>
        /// 轉成半形
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToHalfWidth(this string value)
        {
            char[] chars = value.ToCharArray();
            chars = chars.Select(c => c.ToHalfWidth()).ToArray();
            return new string(chars);
        }

        /// <summary>
        /// 轉成 Pascal Case
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToPascalCase(this string value)
        {
            return value.ToCamelCase(false);
        }
        /// <summary>
        /// 轉成 Pascal Case
        /// </summary>
        /// <param name="value"></param>
        /// <param name="isKeepSpaceOrPunctuation"></param>
        /// <returns></returns>
        public static string ToPascalCase(this string value, bool isKeepSpaceOrPunctuation)
        {
            if (value.IsNullOrEmpty())
            {
                return value;
            }

            string newValue = value;
            if (!isKeepSpaceOrPunctuation)
            {
                newValue = newValue.ReplaceByRegex(@"_|-", " ")
                                   .ToFirstLetterUpper()
                                   .ReplaceByRegex(@"_|-| ", string.Empty);
            }
            return newValue.ToFirstUpper();
        }

        /// <summary>
        /// 轉成 Camel Case
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToCamelCase(this string value)
        {
            return value.ToCamelCase(false);
        }
        /// <summary>
        /// 轉成 Camel Case
        /// </summary>
        /// <param name="value"></param>
        /// <param name="isKeepSpaceOrPunctuation"></param>
        /// <returns></returns>
        public static string ToCamelCase(this string value, bool isKeepSpaceOrPunctuation)
        {
            if (value.IsNullOrEmpty())
            {
                return value;
            }

            string newValue = value;
            if (!isKeepSpaceOrPunctuation)
            {
                newValue = newValue.ReplaceByRegex(@"_|-", " ")
                                   .ToFirstLetterUpper()
                                   .ReplaceByRegex(@"_|-| ", string.Empty);
            }

            return newValue.ToFirstLower();
        }

        /// <summary>
        /// 轉成 Snake Case
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToSnakeCase(this string value)
        {
            return value.ToSnakeCase(false);
        }
        /// <summary>
        /// 轉成 Snake Case
        /// </summary>
        /// <param name="value"></param>
        /// <param name="isAllCaps"></param>
        /// <returns></returns>
        public static string ToSnakeCase(this string value, bool isAllCaps)
        {
            string newValue = value.ReplaceByRegex(@"\s|-|_", string.Empty);

            var stringBuilder = new StringBuilder();
            stringBuilder.Append(newValue[0]);
            for (int i = 1; i < newValue.Length; ++i)
            {
                char c = newValue[i];
                if (char.IsUpper(c))
                {
                    stringBuilder.Append('_');
                    stringBuilder.Append(c);
                }
                else
                {
                    stringBuilder.Append(c);
                }
            }

            if (isAllCaps)
            {
                return stringBuilder.ToString().ToUpper();
            }
            else
            {
                return stringBuilder.ToString().ToLower();
            }
        }

        /// <summary>
        /// 轉成 Kebab-Case
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToKebabCase(this string value)
        {
            return value.ToKebabCase(false);
        }
        /// <summary>
        /// 轉成 Kebab-Case
        /// </summary>
        /// <param name="value"></param>
        /// <param name="isAllCaps"></param>
        /// <returns></returns>
        public static string ToKebabCase(this string value, bool isAllCaps)
        {
            return value.ToSnakeCase(isAllCaps).ReplaceByRegex(@"_", "-");
        }

        #endregion


        #region 其他

        /// <summary>
        /// 單字的 Regular Expression Pattern
        /// </summary>
        private const string WordPattern = "[-’'.a-zA-Z0-9]+|[ ]+|[^ ^-’'.a-zA-Z0-9]";

        /// <summary>
        /// Truncate Word
        /// </summary>
        /// <param name="value"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string TruncateWord(this string value, int length)
        {
            return value.TruncateWord(length, string.Empty);
        }
        /// <summary>
        /// Truncate Word
        /// </summary>
        /// <param name="value"></param>
        /// <param name="length"></param>
        /// <param name="trailingString"></param>
        /// <returns></returns>
        public static string TruncateWord(this string value, int length, string trailingString)
        {
            if (value.IsNull() || value.Length < length)
            {
                return value;
            }

            if (!trailingString.IsNullOrEmpty())
            {
                length -= trailingString.Length;
            }

            Regex regex = new Regex(WordPattern);
            var tokens = regex.Matches(value);

            StringBuilder builder = new StringBuilder();
            foreach (var token in tokens)
            {
                if (builder.Length + token.ToString().Length <= length)
                {
                    builder.Append(token);
                }
                else
                {
                    break;
                }
            }

            if (!trailingString.IsNullOrEmpty())
            {
                builder.Append(" ");
                builder.Append(trailingString);
            }

            return builder.ToString();
        }

        /// <summary>
        /// Count Word
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static int CountWord(this string value)
        {
            if (value.IsNullOrEmpty())
            {
                return 0;
            }

            Regex regex = new Regex(WordPattern);
            var tokens = regex.Matches(value);
            return tokens.Count;
        }

        #endregion
    }
}
