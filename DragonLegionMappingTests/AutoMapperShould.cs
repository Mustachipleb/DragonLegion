using System;
using AutoMapper;
using DragonLegion;
using Xunit;

namespace DragonLegionMappingTests
{
    public class AutoMapperShould
    {
        [Fact]
        public void MapModelsCorrectly()
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<ViewModelProfile>();
            });
            
            configuration.AssertConfigurationIsValid();
        }
    }
}
