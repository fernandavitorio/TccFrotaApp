using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TccFrotaApp.Models;

namespace TccFrotaApp.ViewModels.Mappings
{
    public class ViewModelToEntityMappingProfile : Profile
    {
        public ViewModelToEntityMappingProfile()
        {
              CreateMap<RegistrationViewModel,Login>().ForMember(au => au.UserName, map => map.MapFrom(vm => vm.Email));
        }
    }

}