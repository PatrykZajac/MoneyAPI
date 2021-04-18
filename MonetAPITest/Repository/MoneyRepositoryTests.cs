using System;
using MoneyAPI.Enums;
using MoneyAPI.Model.Local;
using MoneyAPI.Repository;
using Xunit;

namespace MonetAPITest.Repository
{
    public class MoneyRepositoryTests
    {
        private readonly IMoneyRepository _moneyRepository;

        private class FakeMoneyRepository : IMoneyRepository
        {
            public NBPResultDTO GetNBPData(CurrencyEnum currency, DateTime dateFrom, DateTime dateTo)
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
                return expectedResult;
            }
        }
        public MoneyRepositoryTests()
        {
            _moneyRepository = new FakeMoneyRepository();
        }

        [Fact]
        public void Repository_Return_Expected_Type()
        {
            var result = _moneyRepository.GetNBPData(CurrencyEnum.CHF, DateTime.Now, DateTime.Now);

            Assert.IsType<NBPResultDTO>(result);
        }
    }
}
