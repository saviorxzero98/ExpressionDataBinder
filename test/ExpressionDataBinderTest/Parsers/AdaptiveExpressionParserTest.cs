using ExpressionDataBinder.Parsers;
using Xunit;

namespace ExpressionDataBinder.Test.Parsers
{
    public class AdaptiveExpressionParserTest
    {
        [Fact]
        public void TestMatch()
        {
            // Arrange
            var textA = "Dear ${UserName}";
            var textB = "Dear ${LastName} ${FirstName}";
            var textC = "Today is ${addHours(utcNow(), 8, 'yyyy-MM-dd')}";
            var textD = "{\"type\": \"AdaptiveCard\",\"version\": \"1.0\",\"body\": [{\"type\": \"TextBlock\",\"text\": \"Hello World! ${isMatch('ab', '^[a-z]{1,2}$')} ${string({ \"name\": \"Sophie Owen\"})}\"}]}";
            var textE = "Dear ${ UserName }";
            var textF = "{\"type\": \"AdaptiveCard\",\"version\": \"1.0\",\"body\": [{\"type\": \"TextBlock\",\"text\": \"Hello World! ${formatDateTime('2019-12-10T09:00:00', DateFormat)}\"}]}";
            var textG = "{\"type\": \"AdaptiveCard\",\"version\": \"1.0\",\"body\": [{\"type\": \"TextBlock\",\"text\": \"Hello World! ${json('{\"fullName\": \"Sophia Owen\"}')}\"}]}";
            var textH = "{\"Data\": \"${string(''{abc')}\", \"Data2\": \"${string(\\\"'abc}\\\")}\" }";

            // Act
            var parser = new AdaptiveExpressionParser();
            var resultA = parser.Match(textA);
            var resultB = parser.Match(textB);
            var resultC = parser.Match(textC);
            var resultD = parser.Match(textD);
            var resultE = parser.Match(textE);
            var resultF = parser.Match(textF);
            var resultG = parser.Match(textG);
            var resultH = parser.Match(textH);

            // Assert
            Assert.Single(resultA);
            Assert.Equal("UserName", resultA.FirstOrDefault().Expression);
            Assert.Equal("${UserName}", resultA.FirstOrDefault().MatchedToken);


            Assert.Equal(2, resultB.Count);

            Assert.Single(resultC);
            Assert.Equal("addHours(utcNow(), 8, 'yyyy-MM-dd')", resultC.FirstOrDefault().Expression);
            Assert.Equal("${addHours(utcNow(), 8, 'yyyy-MM-dd')}", resultC.FirstOrDefault().MatchedToken);

            Assert.Equal(2, resultD.Count);
            Assert.Equal("isMatch('ab', '^[a-z]{1,2}$')", resultD.FirstOrDefault().Expression);
            Assert.Equal("${isMatch('ab', '^[a-z]{1,2}$')}", resultD.FirstOrDefault().MatchedToken);

            Assert.Single(resultE);
            Assert.Equal("UserName", resultE.FirstOrDefault().Expression);
            Assert.Equal("${ UserName }", resultE.FirstOrDefault().MatchedToken);

            Assert.Single(resultF);
            Assert.Equal("formatDateTime('2019-12-10T09:00:00', DateFormat)", resultF.FirstOrDefault().Expression);
            Assert.Equal("${formatDateTime('2019-12-10T09:00:00', DateFormat)}", resultF.FirstOrDefault().MatchedToken);

            Assert.Single(resultG);
            Assert.Equal("json('{\"fullName\": \"Sophia Owen\"}')", resultG.FirstOrDefault().Expression);
            Assert.Equal("${json('{\"fullName\": \"Sophia Owen\"}')}", resultG.FirstOrDefault().MatchedToken);

            Assert.Equal(2, resultH.Count);
            Assert.Equal("string(''{abc')", resultH[0].Expression);
            Assert.Equal("${string(''{abc')}", resultH[0].MatchedToken);
            Assert.Equal("string(\\\"'abc}\\\")", resultH[1].Expression);
            Assert.Equal("${string(\\\"'abc}\\\")}", resultH[1].MatchedToken);
        }

        [Fact]
        public void TestMatchNoMatch()
        {
            // Arrange
            var textA = "Dear {UserName}";
            var textB = "Dear {{UserName}}";
            var textC = "Dear ${{{$.UserName}}}";
            var textD = "Dear ${{{=> DateTime.Now.ToString() ;}}}";

            // Act
            var parser = new AdaptiveExpressionParser();
            var resultA = parser.Match(textA);
            var resultB = parser.Match(textB);
            var resultC = parser.Match(textC);
            var resultD = parser.Match(textD);

            // Assert
            Assert.Empty(resultA);
            Assert.Empty(resultB);
            Assert.Single(resultC);
            Assert.Single(resultD);
        }

        [Fact]
        public void TestIsMatch()
        {
            // Arrange
            var text = "${addHours(utcNow(), 8, 'yyyy-MM-dd')}";

            // Act
            var parser = new AdaptiveExpressionParser();
            var result = parser.IsMatch(text);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void TestIsMatchNoMatch()
        {
            // Arrange
            var text = "{addHours(utcNow(), 8, 'yyyy-MM-dd')}";

            // Act
            var parser = new AdaptiveExpressionParser();
            var result = parser.IsMatch(text);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void TestIsMatchWithEmptyString()
        {
            // Arrange
            var text = string.Empty;

            // Act
            var parser = new AdaptiveExpressionParser();
            var result = parser.IsMatch(text);

            // Assert
            Assert.False(result);
        }
    }
}
