using ExpressionDataBinder.Binders;
using ExpressionDataBinder.Test.TestData;
using Xunit;

namespace ExpressionDataBinder.Test.Binders
{
    public class AdaptiveExptessionBinderTest
    {
        [Fact]
        public void TestBindData()
        {
            // Arrange
            var data = GetTestData();
            var binder = new AdaptiveExptessionBinder(false);
            var forceBinder = new AdaptiveExptessionBinder(true);

            var message01 = "${Name}";
            var message02 = "ID: ${Id}; Name: ${Name}";
            var message03 = "Name: ${EName}";

            // Act
            var result01 = forceBinder.BindData(message01, data);
            var result02 = forceBinder.BindData(message02, data);
            var result03 = forceBinder.BindData(message03, data);
            var result04 = binder.BindData(message03, data);

            // Assert
            Assert.True(result01.IsSuccess);
            Assert.Equal(data.Name, result01.Value.ToString());

            Assert.True(result02.IsSuccess);
            Assert.Equal($"ID: {data.Id}; Name: {data.Name}", result02.Value.ToString());

            Assert.Equal("Name: ", result03.Value.ToString());

            Assert.False(result04.IsSuccess);
            Assert.Equal("Name: ${EName}", result04.Value.ToString());
        }

        [Fact]
        public void TestBindExpression()
        {
            // Arrange
            var data = GetTestData();
            var binder = new AdaptiveExptessionBinder();

            var message01 = "${if(Ages >= 18, 'adult', 'child')}";
            var message02 = "今天是 ${datetime.today('yyyy年MM月dd日')}";

            // Act

            var result01 = binder.BindData(message01, data);
            var result02 = binder.BindData(message02, data);

            // Assert
            Assert.True(result01.IsSuccess);
            Assert.Equal("adult", result01.Value.ToString());

            Assert.True(result02.IsSuccess);
            Assert.Equal($"今天是 {DateTime.Now.ToString("yyyy年MM月dd日")}", result02.Value.ToString());
        }


        private UserProfile GetTestData()
        {
            var data = new UserProfile()
            {
                Id = "ace",
                Name = "Ace",
                Ages = 20,
                Birth = "1911-01-01",
                Detial = new UserProfileDetial()
                {
                    Email = "abc@no.mail.com",
                    Phone = "0806449"
                },
                IsBlocked = false,
                CreateDate = DateTime.Now
            };
            return data;
        }
    }
}
