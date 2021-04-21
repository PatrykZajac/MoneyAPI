using System;
using MoneyAPI.Exceptions;
using MoneyAPI.Model.Local;
using MoneyAPI.Repository;
using MoneyAPI.Service;
using Moq;
using Xunit;

namespace MonetAPITest.Service
{
    public class MoneySericeTest
    {
        private readonly MoneyService _service;
        private readonly Mock<IMoneyRepository> _moneyRepositoryMock;
        public MoneySericeTest()
        {
            _moneyRepositoryMock = new Mock<IMoneyRepository>();
            _service = new MoneyService(_moneyRepositoryMock.Object);

        }
        [Fact]
        public void GetExchangeRate_Return_Expected_Result_Type()
        {
            _moneyRepositoryMock.Setup(i => i.GetNBPData(It.IsAny<MoneyAPI.Enums.CurrencyEnum>(), It.IsAny<DateTime>(), It.IsAny<DateTime>())).Returns(new NBPResultDTO()
            {
                rates = new System.Collections.Generic.List<NBPExchangeRateDTO>()
                {
                    new NBPExchangeRateDTO()
                    {
                        ask = 5m,
                        bid = 5m
                    }
                }
            });

            var result = _service.GetExchangeRate(MoneyAPI.Enums.CurrencyEnum.CHF, DateTime.Now, DateTime.Now);

            Assert.IsType<APIResult>(result);
        }

        [Fact]
        public void GetExcengaRate_Throws_Exception()
        {

            _moneyRepositoryMock.Setup(i => i.GetNBPData(It.IsAny<MoneyAPI.Enums.CurrencyEnum>(), It.IsAny<DateTime>(), It.IsAny<DateTime>())).Returns(new NBPResultDTO());

            var ex = Assert.Throws<DateFromAfterDateToException>(() => _service.GetExchangeRate(MoneyAPI.Enums.CurrencyEnum.CHF, DateTime.Now.AddDays(1), DateTime.Now));
        }

        [Fact]
        public void GetExcengaRate_Throws_Exception_NoData()
        {

            _moneyRepositoryMock.Setup(i => i.GetNBPData(It.IsAny<MoneyAPI.Enums.CurrencyEnum>(), It.IsAny<DateTime>(), It.IsAny<DateTime>())).Returns(new NBPResultDTO()
            {
                rates = new System.Collections.Generic.List<NBPExchangeRateDTO>()
            });

            var ex = Assert.Throws<APIReturnsEmptyDataException>(() => _service.GetExchangeRate(MoneyAPI.Enums.CurrencyEnum.CHF, DateTime.Now, DateTime.Now));
        }

        [Fact]
        public void GetExcengaRate_Return_Expected_Result()
        {
            var expectedResult = new NBPResultDTO()
            {
                code = "",
                currency = "EUR",
                rates = new System.Collections.Generic.List<NBPExchangeRateDTO>(),
                table = "C"
            };
            expectedResult.rates.Add(new NBPExchangeRateDTO()
            {
                ask = 5m,
                bid = 5m,
                effectiveDate = new DateTime(),
                no = "1"
            });

            expectedResult.rates.Add(new NBPExchangeRateDTO()
            {
                ask = 4m,
                bid = 4m,
                effectiveDate = new DateTime(),
                no = "2"
            });
            _moneyRepositoryMock.Setup(i => i.GetNBPData(It.IsAny<MoneyAPI.Enums.CurrencyEnum>(), It.IsAny<DateTime>(), It.IsAny<DateTime>())).Returns(expectedResult);

            var result = _service.GetExchangeRate(MoneyAPI.Enums.CurrencyEnum.CHF, DateTime.Now, DateTime.Now);

            Assert.Equal(4.5m, result.average_price);
            Assert.Equal(0.5m, result.standard_deviation);
        }

        [Fact]
        public void GetExcengaRate_Is_Function_Used_For_Valid_Data()
        {
            var count = 0;
            _moneyRepositoryMock.Setup(i => i.GetNBPData(It.IsAny<MoneyAPI.Enums.CurrencyEnum>(), It.IsAny<DateTime>(), It.IsAny<DateTime>())).Callback(() => count++).Returns(new NBPResultDTO()
            {
                rates = new System.Collections.Generic.List<NBPExchangeRateDTO>()
                {
                    new NBPExchangeRateDTO()
                    {
                        ask = 5m,
                        bid = 5m
                    }
                }
            });
            var result = _service.GetExchangeRate(MoneyAPI.Enums.CurrencyEnum.CHF, DateTime.Now, DateTime.Now);
            _moneyRepositoryMock.Verify(i => i.GetNBPData(It.IsAny<MoneyAPI.Enums.CurrencyEnum>(), It.IsAny<DateTime>(), It.IsAny<DateTime>()), Times.Exactly(1), "Moq.MockException: Invocation was unexpectedly performed " + count + " times, not 1 time: i => i.GetExchangeRate()");
        }

        [Fact]
        public void GetMoneyData_Is_Function_Used_For_Not_Valid_Data_Empty_Result()
        {
            var count = 0;
            _moneyRepositoryMock.Setup(i => i.GetNBPData(It.IsAny<MoneyAPI.Enums.CurrencyEnum>(), It.IsAny<DateTime>(), It.IsAny<DateTime>())).Callback(() => count++).Returns(new NBPResultDTO()
            {
                rates = new System.Collections.Generic.List<NBPExchangeRateDTO>()
            });
            var ex = Assert.Throws<APIReturnsEmptyDataException>(() => _service.GetExchangeRate(MoneyAPI.Enums.CurrencyEnum.CHF, DateTime.Now, DateTime.Now));
            _moneyRepositoryMock.Verify(i => i.GetNBPData(It.IsAny<MoneyAPI.Enums.CurrencyEnum>(), It.IsAny<DateTime>(), It.IsAny<DateTime>()), Times.Exactly(1), "Moq.MockException: Invocation was unexpectedly performed " + count + " times, not 0 time: i => i.GetExchangeRate()");
        }

        [Fact]
        public void GetMoneyData_Is_Function_Used_For_Not_Valid_Data_Invalid_Date()
        {
            var count = 0;
            _moneyRepositoryMock.Setup(i => i.GetNBPData(It.IsAny<MoneyAPI.Enums.CurrencyEnum>(), It.IsAny<DateTime>(), It.IsAny<DateTime>())).Callback(() => count++).Returns(new NBPResultDTO()
            {
                rates = new System.Collections.Generic.List<NBPExchangeRateDTO>()
            });
            var ex = Assert.Throws<DateFromAfterDateToException>(() => _service.GetExchangeRate(MoneyAPI.Enums.CurrencyEnum.CHF, DateTime.Now.AddDays(1), DateTime.Now));
            _moneyRepositoryMock.Verify(i => i.GetNBPData(It.IsAny<MoneyAPI.Enums.CurrencyEnum>(), It.IsAny<DateTime>(), It.IsAny<DateTime>()), Times.Exactly(0), "Moq.MockException: Invocation was unexpectedly performed " + count + " times, not 0 time: i => i.GetExchangeRate()");
        }
    }
}
