using System;
namespace MoneyAPI.Model.External
{
    public class NBPExchangeRate
    {
        public string no { get; set; }
        public DateTime effectiveDate { get; set; }
        public decimal bid { get; set; }
        public decimal ask { get; set; }
        public NBPExchangeRate()
        {
        }
    }
}
