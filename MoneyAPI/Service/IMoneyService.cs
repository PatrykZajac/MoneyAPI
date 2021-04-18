using System;
using MoneyAPI.Enums;
using MoneyAPI.Model.Local;

namespace MoneyAPI.Service
{
    public interface IMoneyService
    {
        public APIResult GetExchangeRate(CurrencyEnum currency, DateTime dateFrom, DateTime dateTo);
    }
}
