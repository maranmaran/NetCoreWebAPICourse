using AutoMapper;
using PokemonAPI.BusinessLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tests.BusinessLayer
{
    public static class TestHelper
    {
        public static IMapper GetMapper()
        {
            //auto mapper configuration
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<Mappings>();
            });
            var mapper = mockMapper.CreateMapper();

            return mapper;
        }
    }
}
