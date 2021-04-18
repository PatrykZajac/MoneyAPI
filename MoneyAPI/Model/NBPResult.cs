using System;
using System.Collections.Generic;

namespace MoneyAPI.Model.External
{
    public class NBPResult
    {
        public string table { get; set; }
        public string currency { get; set; }
        public string code { get; set; }
        public List<NBPExchangeRate> rates { get; set; }

        public NBPResult()
        {
        }
    }
}
