using System;
namespace MoneyAPI.Model.Local
{
    public class NBPExchangeRateDTO
    {
        public string no { get; set; }
        public DateTime effectiveDate { get; set; }
        public decimal bid { get; set; }
        public decimal ask { get; set; }
        public NBPExchangeRateDTO()
        {
        }
    }
}
