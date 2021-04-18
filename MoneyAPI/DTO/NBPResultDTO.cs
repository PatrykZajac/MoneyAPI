using System;
using System.Collections.Generic;

namespace MoneyAPI.Model.Local
{
    public class NBPResultDTO
    {
        public string table { get; set; }
        public string currency { get; set; }
        public string code { get; set; }
        public List<NBPExchangeRateDTO> rates { get; set; }

        public NBPResultDTO()
        {
        }
    }
}
