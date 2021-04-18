using System;
using MoneyAPI.Enums;
using MoneyAPI.Model.Local;

namespace MoneyAPI.Repository
{
    public interface IMoneyRepository
    {
        public NBPResultDTO GetNBPData(CurrencyEnum currency, DateTime dateFrom, DateTime dateTo);
    }
}
