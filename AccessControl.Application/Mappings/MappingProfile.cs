using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccessControl.Application.ViewModels;
using AccessControl.Domain.Entities;
using AutoMapper;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AccessControl.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserDetailsVM>().ReverseMap();

        }
    }
}
