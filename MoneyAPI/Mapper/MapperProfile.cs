using System;
using MoneyAPI.Model.External;
using MoneyAPI.Model.Local;
using AutoMapper;

namespace MoneyAPI.Mapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<NBPExchangeRate, NBPExchangeRateDTO>();
            CreateMap<NBPResult, NBPResultDTO>();

        }
    }
}
