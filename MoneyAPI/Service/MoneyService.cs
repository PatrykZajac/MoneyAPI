using System;
using System.Linq;
using AutoMapper;
using MoneyAPI.Enums;
using MoneyAPI.Exceptions;
using MoneyAPI.Model.Local;
using MoneyAPI.Repository;

namespace MoneyAPI.Service
{
    public class MoneyService : IMoneyService
    {
        private readonly IMoneyRepository repository;
        public MoneyService(IMoneyRepository moneyRepository)
        {
            repository = moneyRepository;
        }

        public APIResult GetExchangeRate(CurrencyEnum currency, DateTime dateFrom, DateTime dateTo)
        {
            if (dateFrom > dateTo)
            {
                throw new DateFromAfterDateToException();
            }

            var dataResult = repository.GetNBPData(currency, dateFrom, dateTo);

            if (dataResult.rates.Count > 0)
            {
                var averageSumList = dataResult.rates.Select(item => (item.ask + item.bid) / 2);
                var averageSum = averageSumList.Sum();
                var average = averageSum / dataResult.rates.Count;
                var sumOfDiffPow = averageSumList.Select(item => (item - average)).Select(item => item * item).Sum();
                var standardDeviation = Convert.ToDecimal(Math.Sqrt(Convert.ToDouble(sumOfDiffPow / dataResult.rates.Count)));

                var result = new APIResult()
                {
                    average_price = Math.Round(average, 4),
                    standard_deviation = Math.Round(standardDeviation, 4)
                };

                return result;
            }
            throw new APIReturnsEmptyDataException();
        }
    }
}
