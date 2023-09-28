# Expression Data Binder 

> 透過 Expression 綁定相關資料

* 目前支援 [Adaptive Expresion](https://learn.microsoft.com/en-us/azure/bot-service/bot-builder-concept-adaptive-expressions?view=azure-bot-service-4.0&tabs=arithmetic)，以及內建的 [Prebuild Function](https://learn.microsoft.com/en-us/azure/bot-service/adaptive-expressions/adaptive-expressions-prebuilt-functions?view=azure-bot-service-4.0)
* 基於 Adptive Expression 擴充一些實用的 Function



## Usage

* **Use Variable**

```c#
// Using
using ExpressionDataBinder.Binders;

// Data
var data = new {
    Id = 1,
    Name = "Ace"
};

// Expressions
string text1 = "${Name}";
string text2 = "ID: ${Id}; Name: ${Name}";
string text3 = "Name: ${EName}";

// Data Binding
IExptessionBinder binder = new AdaptiveExptessionBinder(isForce: false);
IExptessionBinder forceBinder = new AdaptiveExptessionBinder(isForce: true);

// output: "Ace"
ExpressionBindResult result1 = forceBinder.BindData(text1, data);
string outText1 = result1.Value;

// output: "ID: 1; Name: Ace"
ExpressionBindResult result2 = forceBinder.BindData(text2, data);
string outText2 = result2.Value;

// output: "Name: "
ExpressionBindResult result3A = forceBinder.BindData(text3, data);
string outText3A = result3A.Value;

// output: "Name: ${EName}"
ExpressionBindResult result3B = binder.BindData(text3, data);
string outText3B = result3B.Value;
```



* **Use Prebuild Function**

```c#
// Using
using ExpressionDataBinder.Binders;

// Data
var data = new {
    Id = 1,
    Name = "John",
    Ages = 20
};

// Expressions
 var text1 = "${Name} is ${if(Ages >= 18, 'adult', 'child')}.";

// Data Binding
IExptessionBinder binder = new AdaptiveExptessionBinder();

// output: "John is adult"
ExpressionBindResult result1 = binder.BindData(text1, data);
string outText1 = result1.Value;
```



* **Use Custom Function**

```c#
// Using
using ExpressionDataBinder.Binders;

// Expressions
 var text1 = "Today is ${datetime.today('yyyy-MM-dd')}.";

// Data Binding
IExptessionBinder binder = new AdaptiveExptessionBinder();

// output: "Today is 2020-03-03"
ExpressionBindResult result1 = binder.BindData(text1, data);
string outText1 = result1.Value;
```



## Custom Functions

> 擴充一些 Adaptive Expression Functions

* **日期時間 (DateTime)**

| Function                                                   | Description                                                  |
| ---------------------------------------------------------- | ------------------------------------------------------------ |
| `datetime.now('{format}'?, '{locale}'?)`                   | 取得目前時間<br />‧ `format`： 日期時間格式 (可選參數)<br />‧ `locale`： 地區格式 (可選參數) |
| `datetime.today('{format}'?, '{locale}'?)`                 | 取得今天的日期時間<br />‧ `format`： 日期時間格式 (可選參數)<br />‧ `locale`： 地區格式 (可選參數) |
| `datetime.date({year}, {month}, {day}, '{format}'?)`       | 取得指定日期<br />‧ `year`： 年<br />‧ `month`： 月<br />‧ `day`： 日<br />‧ `format`： 日期格式 (可選參數) |
| `datetime.time({hours}, {minutes}, {second}, '{format}'?)` | 取得指定時間<br />‧ `hours`： 時<br />‧ `minutes`： 分<br />‧ `second`： 秒<br />‧ `format`： 時間格式 (可選參數) |
| `datetime.diff({datetimeA}, {datetimeB}, {unit})`          | 計算日期差<br />‧ `datetimeA`： 日期時間A<br />‧ `datetimeB`：日期時間B<br />‧ `unit`： 單位<br />　　‧ `Y` (年)、`M` (月)、`D` (天)、`W` (週)<br />　　‧ `h` (小時)、`m` (分鐘)、`s` (秒數)、`ms` (毫秒) |

* **數學運算 (Math)**

| Function                        | Description                                           |
| ------------------------------- | ----------------------------------------------------- |
| `math.abs({number})`            | 取絕對值<br />‧ `number`： 數字                       |
| `math.power({number}, {power})` | 取指數<br />‧ `number`： 數字<br />‧ `power`： 次方數 |
| `math.pi()`                     | 取得圓周率                                            |

* **字串處理 (String)**

* 

| Function                                                     | Description                                                  |
| ------------------------------------------------------------ | ------------------------------------------------------------ |
| `text.exact('{textA}', '{textB}', {ignoreCase}?, {ignoreWidth}?)` | 檢查文字是否相等<br />‧ `textA`： 文字A<br />‧ `textB`： 文字B<br />‧ `ignoreCase`： 是否忽略大小寫 (可選參數)<br />‧ `ignoreWidth`： 是否忽略全形半形(可選參數) |
| `text.isNullOrEmpty('{text}')`                               | 檢查文字是否為 Null 或是 Empty<br />‧ `text`： 文字          |
| `text.isNullOrWhiteSpace('{text}')`                          | 檢查文字是否為 Null、Empty 或是空白<br />‧ `text`： 文字     |
| `text.trim('{text}', '{trimChars}'?)`                        | 去除字串頭尾指定字元集<br />‧ `text`： 文字<br />‧ `trimChars`： 字元集 (可選參數)，預設值：" " |
| `text.trimStart('{text}', '{trimChars}'?)`                   | 去除字首指定字元集<br />‧ `text`： 文字<br />‧ `trimChars`： 字元集 (可選參數)，預設值：" " |
| `text.trimEnd('{text}', '{trimChars}'?)`                     | 去除字尾指定字元集<br />‧ `text`： 文字<br />‧ `trimChars`： 字元集 (可選參數)，預設值：" " |
| `text.trimStartWord('{text}', '{trimWord}', {ignoreCase}?)`  | 去除字首單字<br />‧ `text`： 文字<br />‧ `trimWord`： 單字<br />‧ `ignoreCase`： 是否忽略大小寫 (可選參數) |
| `text.trimEndWord('{text}', '{trimWord}', {ignoreCase}?)`    | 去除字尾單字<br />‧ `text`： 文字<br />‧ `trimWord`： 單字<br />‧ `ignoreCase`： 是否忽略大小寫 (可選參數) |
| `text.toFirstUpper('{text}')`                                | 字串第一個字轉大寫<br />‧ `text`： 文字                      |
| `text.toFirstLower('{text}')`                                | 字串第一個字轉小寫<br />‧ `text`： 文字                      |
| `text.toFullWidth('{text}')`                                 | 字串轉全形<br />‧ `text`： 文字                              |
| `text.toHalfWidth('{text}')`                                 | 字串轉半形<br />‧ `text`： 文字                              |
| `text.toPascalCase('{text}')`                                | 字串轉 Pascal Case<br />‧ `text`： 文字                      |
| `text.toCamelCase('{text}')`                                 | 字串轉 Camel Case<br />‧ `text`： 文字                       |
| `text.toSnakeCase('{text}', {isAllCap}?)`                    | 字串轉 Snake Case<br />‧ `text`： 文字<br />‧ `isAllCap`： 是否全部大寫 |
| `text.toKebabCase('{text}', {isAllCap}?)`                    | 字串轉 Kebab Case<br />‧ `text`： 文字<br />‧ `isAllCap`： 是否全部大寫 |
| `text.truncateWord('{text}', {wordCount})`                   | 截短到指定字數 (英文單字)<br />‧ `text`： 文字<br />‧ `wordCount`： 中文字數、英文單字數 |
| `text.replaceByRegex('{text}', '{pattern}', '{newText}', {ignoreCase}?)` | 使用 Regular expression 取代指定文字<br />‧ `text`： 舊字串<br />‧ `pattern`： Regular expression Pattern<br />‧ `newText`： 新字串<br />‧ `ignoreCase`： 是否忽略大小寫 (可選參數) |
| `text.getRegexGroupValue('{text}', '{pattern}', '{groupName}', {ignoreCase}?)` | 透過 Regular Expression 取得指定 Group 的字串<br />‧ `text`： 文字<br />‧ `pattern`： Regular expression Pattern<br />‧ `groupName`： Regular expression Group Name<br />‧ `ignoreCase`： 是否忽略大小寫 (可選參數) |
| `text.getRegexGroupValue('{text}', '{pattern}', {groupIndex}, {ignoreCase}?)` | 透過 Regular Expression 取得指定 Group 的字串<br />‧ `text`： 文字<br />‧ `pattern`： Regular expression Pattern<br />‧ `groupIndex`： Regular expression Group 索引<br />‧ `ignoreCase`： 是否忽略大小寫 (可選參數) |



