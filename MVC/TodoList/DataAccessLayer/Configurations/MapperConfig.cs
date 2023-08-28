using AutoMapper;
using DataAccessLayer.DTO;
using DataAccessLayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Configurations
{
    public class MapperConfig:Profile
    {
        public MapperConfig() 
        {
            CreateMap<Objective, CreateEditTaskDTO>().ReverseMap();
        }
    }
}
