using AutoMapper;
using Supermarket.API.Models;
using Supermarket.API.Resource;

namespace Supermarket.API.Mapping
{
    public class ModelToResourceProfile : Profile
    {
        public ModelToResourceProfile() 
        {
            CreateMap<Category, CategoryResource>();
        }
    }
}
