using ExpressionDataBinder.Evaluators;
using ExpressionDataBinder.Test.TestData;
using Xunit;

namespace ExpressionDataBinder.Test.Evaluators
{
    public class AdaptiveExpressionEvaluatorTest
    {
        [Fact]
        public void TestEvaluate()
        {
            // Arrange
            string expression01 = "addHours(utcNow(), 8, 'yyyy-MM-dd')";
            string expression02 = "(5 * 5) + 100";
            string expression03 = "concat('Hello', ' ', 'World', '!')";
            string expression04 = "first(where(createArray('Test', 'Hello', 'Bye', 'How are you?'), s, length(s) > 4 ))";
            string expression05 = "isMatch('ab', '^[a-z]{1,2}$')";

            // Act
            var evaluator = new AdaptiveExpressionEvaluator();
            var result01 = evaluator.Evaluate(expression01);
            var result02 = evaluator.Evaluate(expression02);
            var result03 = evaluator.Evaluate(expression03);
            var result04 = evaluator.Evaluate(expression04);
            var result05 = evaluator.Evaluate(expression05);

            // Assert
            Assert.Equal(result01.ToString(), DateTime.Today.ToString("yyyy-MM-dd"));
            Assert.Equal(Convert.ToInt32(result02), ((5 * 5) + 100));
            Assert.Equal("Hello World!", result03.ToString());
            Assert.Equal("Hello", result04.ToString());
            Assert.True(Convert.ToBoolean(result05));
        }


        [Fact]
        public void TestEvaluateWithData()
        {
            // Arrange
            var data = new UserProfile()
            {
                Id = "ace",
                Name = "Ace",
                Ages = 18,
                Birth = "1911-01-01",
                Detial = new UserProfileDetial()
                {
                    Email = "abc@no.mail.com",
                    Phone = "0806449",
                    Interests = new List<string>()
                    {
                        "玩大老二",
                        "玩二十一點",
                        "玩抽鬼牌",
                    }
                },
                IsBlocked = false,
                CreateDate = DateTime.Now
            };

            string expression01 = "Name";
            string expression02 = "Detial.Email";
            string expression03 = "length(Detial.Email)";
            string expression04 = "join(Detial.Interests, '、')";

            // Act
            var evaluator = new AdaptiveExpressionEvaluator();
            var result01 = evaluator.Evaluate(expression01, data);
            var result02 = evaluator.Evaluate(expression02, data);
            var result03 = evaluator.Evaluate(expression03, data);
            var result04 = evaluator.Evaluate(expression04, data);

            // Assert
            Assert.Equal(result01.ToString(), data.Name);
            Assert.Equal(result02.ToString(), data.Detial.Email);
            Assert.Equal(Convert.ToInt32(result03), data.Detial.Email.Length);
            Assert.Equal(result04.ToString(), string.Join("、", data.Detial.Interests));
        }


        [Fact]
        public void TestEvaluateWithCustomFunction()
        {
            // Arrange
            string expression01 = "datetime.now('yyyy-MM-dd')";
            string expression02 = "datetime.today('yyyy-MM-dd')";
            string expression03 = "math.pi()";
            string expression04 = "text.trimStart('  ABC  ')";
            string expression05 = "text.trimEnd('  ABC  ')";

            // Act
            var evaluator = new AdaptiveExpressionEvaluator();
            var result01 = evaluator.Evaluate(expression01);
            var result02 = evaluator.Evaluate(expression02);
            var result03 = evaluator.Evaluate(expression03);
            var result04 = evaluator.Evaluate(expression04);
            var result05 = evaluator.Evaluate(expression05);

            // Assert
            Assert.Equal(DateTime.Now.ToString("yyyy-MM-dd"), result01.ToString());
            Assert.Equal(DateTime.Today.ToString("yyyy-MM-dd"), result02.ToString());
            Assert.Equal(Math.PI, result03.ToObject<double>());
            Assert.Equal("  ABC  ".TrimStart(), result04.ToString());
            Assert.Equal("  ABC  ".TrimEnd(), result05.ToString());
        }
    }
}
