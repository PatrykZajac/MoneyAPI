using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MoneyAPI.Enums;
using MoneyAPI.Model.Local;
using MoneyAPI.Service;

namespace MoneyAPI.Controllers
{
    [ApiController]
    [Route("/")]
    public class MoneyController : ControllerBase
    {
        private readonly IMoneyService service;
        public MoneyController(IMoneyService moneyService)
        {
            service = moneyService;
        }

        [HttpGet("{currencyString}/{dateFromString}/{dateToString}")]
        public ActionResult<APIResult> GetMoneyData(string currencyString, string dateFromString, string dateToString)
        {
            try
            {
                var currency = Enum.Parse<CurrencyEnum>(currencyString);
                DateTime.TryParse(dateFromString, out var dateFrom);
                DateTime.TryParse(dateToString, out var dateTo);
                return service.GetExchangeRate(currency, dateFrom, dateTo);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
