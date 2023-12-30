using AutoMapper;
using Electro_goods_API.Models.DTO;
using Electro_goods_API.Models.Entities;
using Microsoft.CodeAnalysis.Options;

namespace Electro_goods_API.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //CreateMap<Product, ProductDTO>()
            //    .ForMember(destenation => destenation.Name, option => option.MapFrom(source => language == "uk"? source.NameUK : source.Name))
            //    .ForMember(destenation => destenation.Description, option => option.MapFrom(source => language == "uk"? source.DescriptionUK : source.Description));
        }
    }
}
