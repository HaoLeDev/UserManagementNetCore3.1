using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using User.Models.Models;
using User.ViewModels.ViewModels;

namespace UserManagerNetCore.Infrastructure.Extensions
{
    public class AutoMapperProfile: AutoMapper.Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Category,CategoryViewModel>();
            CreateMap<Customer, CustomerViewModel>();
        }
    }
}
