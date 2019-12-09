using AutoMapper;
using Caphyon.Business.Data.Interfaces;
using Caphyon.Models;
using System.Collections.Generic;

namespace Caphyon
{
    public static class MappingProfile
    {
        public static void Config()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMissingTypeMaps = true;
                // create map
                cfg.CreateMap<ToDoViewModel, IToDo>();
                cfg.CreateMap<IToDo, ToDoViewModel>();

            });
            Mapper.AssertConfigurationIsValid();
        }
    }
}