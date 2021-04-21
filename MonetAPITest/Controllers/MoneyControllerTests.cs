using System;
using Microsoft.AspNetCore.Mvc;
using MoneyAPI.Controllers;
using MoneyAPI.Model.Local;
using MoneyAPI.Service;
using Moq;
using Xunit;

namespace MonetAPITest.Controllers
{
    public class MoneyControllerTest
    {
        private readonly MoneyController _controller;
        private readonly Mock<IMoneyService> _moneyServiceMock;

        public MoneyControllerTest()
        {
            _moneyServiceMock = new Mock<IMoneyService>();
            _controller = new MoneyController(_moneyServiceMock.Object);
        }

        [Fact]
        public void GetMoneyData_Return_Expected_Result_Type()
        {
            _moneyServiceMock.Setup(i => i.GetExchangeRate(It.IsAny<MoneyAPI.Enums.CurrencyEnum>(), It.IsAny<DateTime>(), It.IsAny<DateTime>())).Returns(new APIResult());

            var result = _controller.GetMoneyData("EUR", "2021-01-01", "2021-01-02");

            Assert.IsType<ActionResult<APIResult>>(result);

        }

        [Fact]
        public void GetMoneyData_Return_Expected_Result()
        {
            var expectedResult = new APIResult()
            {
                average_price = 4.5m,
                standard_deviation = 0.125m
            };
            _moneyServiceMock.Setup(i => i.GetExchangeRate(It.IsAny<MoneyAPI.Enums.CurrencyEnum>(), It.IsAny<DateTime>(), It.IsAny<DateTime>())).Returns(expectedResult);

            var result = _controller.GetMoneyData("EUR", "2021-01-01", "2021-01-02");

            Assert.Equal(expectedResult.average_price, result.Value.average_price);
            Assert.Equal(expectedResult.standard_deviation, result.Value.standard_deviation);
        }

        [Fact]
        public void GetMoneyData_Is_Function_Used_For_Valid_Data()
        {
            var count = 0;
            _moneyServiceMock.Setup(i => i.GetExchangeRate(It.IsAny<MoneyAPI.Enums.CurrencyEnum>(), It.IsAny<DateTime>(), It.IsAny<DateTime>())).Callback(() => count++).Returns(new APIResult());
            var result = _controller.GetMoneyData("EUR", "2021-01-01", "2021-01-02");
            _moneyServiceMock.Verify(i => i.GetExchangeRate(It.IsAny<MoneyAPI.Enums.CurrencyEnum>(), It.IsAny<DateTime>(), It.IsAny<DateTime>()), Times.Exactly(1), "Moq.MockException: Invocation was unexpectedly performed " + count + " times, not 1 time: i => i.GetExchangeRate()");
            //Assert.Equal(1, count);
        }

        [Fact]
        public void GetMoneyData_Is_Function_Used_For_Not_Valid_Data()
        {
            var count = 0;
            _moneyServiceMock.Setup(i => i.GetExchangeRate(It.IsAny<MoneyAPI.Enums.CurrencyEnum>(), It.IsAny<DateTime>(), It.IsAny<DateTime>())).Callback(() => count++).Returns(new APIResult());
            var result = _controller.GetMoneyData("asd", "asd", "asd");
            _moneyServiceMock.Verify(i => i.GetExchangeRate(It.IsAny<MoneyAPI.Enums.CurrencyEnum>(), It.IsAny<DateTime>(), It.IsAny<DateTime>()), Times.Exactly(0), "Moq.MockException: Invocation was unexpectedly performed " + count + " times, not 0 time: i => i.GetExchangeRate()");
            //Assert.Equal(0, count);
        }

    }
}
