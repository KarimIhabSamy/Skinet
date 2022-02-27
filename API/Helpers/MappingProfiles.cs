using API.Dtos;
using AutoMapper;
using Core.Entities;

namespace API.Helpers
{

    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Product,ProductToReturnDto>()
            .ForMember(h => h.ProductBrand,o => o.MapFrom(s => s.ProductBrand.Name))
             .ForMember(h => h.ProductType,o => o.MapFrom(s => s.ProductType.Name))
             .ForMember(o => o.PictureUrl,d => d.MapFrom<ProductUrlResolver>());
        }
    }
}