using AutoMapper;
using Supermarket.API.Extensions;
using Supermarket.API.Models;
using Supermarket.API.Resource;

namespace Supermarket.API.Mapping
{
    public class ModelToResourceProfile : Profile
    {
        public ModelToResourceProfile() 
        {
            CreateMap<Category, CategoryResource>();
            CreateMap<Products, ProductResource>()
                .ForMember(dest => dest.UnitOfMeasurement,
                opt => opt.MapFrom(src => src.unitOfMeasurement.ToDescriptionString()));
        }
    }
}
