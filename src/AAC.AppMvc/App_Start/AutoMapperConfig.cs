using System;
using System.Linq;
using System.Reflection;
using AutoMapper;
using AAC.AppMvc.ViewModels;
using AAC.Business.Models.Products;
using AAC.Business.Models.Providers;

namespace AAC.AppMvc
{
    public class AutoMapperConfig
    {
        public static MapperConfiguration GetMapperConfiguration()
        {
            var profiles = Assembly.GetExecutingAssembly()
                .GetTypes()
                .Where(x => typeof(Profile).IsAssignableFrom(x));

            return new MapperConfiguration(cgf =>
            {
                foreach (var profile in profiles)
                {
                    cgf.AddProfile(Activator.CreateInstance(profile) as Profile);
                }
            });
        }
    }

    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Provider, ProviderViewModel>().ReverseMap();
            CreateMap<Address, AddressViewModel>().ReverseMap();
            CreateMap<Product, ProductViewModel>().ReverseMap();
        }
    }
}