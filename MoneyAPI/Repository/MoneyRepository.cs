using System;
using AutoMapper;
using MoneyAPI.Enums;
using MoneyAPI.Model.External;
using MoneyAPI.Model.Local;
using RestSharp;

namespace MoneyAPI.Repository
{
    public class MoneyRepository : IMoneyRepository
    {
        private RestClient client;
        private readonly IMapper _mapper;
        public MoneyRepository(IMapper mapper)
        {
            client = new RestClient("http://api.nbp.pl/");
            _mapper = mapper;
        }

        public NBPResultDTO GetNBPData(CurrencyEnum currency, DateTime dateFrom, DateTime dateTo)
        {
            var request = new RestRequest(string.Format("api/exchangerates/rates/c/{0}/{1:yyyy-MM-dd}/{2:yyyy-MM-dd}/?format=json", currency.ToString().ToLower(), dateFrom, dateTo), Method.GET);
            var result = client.Execute<NBPResult>(request).Data;
            return _mapper.Map<NBPResultDTO>(result);
        }
    }
}
