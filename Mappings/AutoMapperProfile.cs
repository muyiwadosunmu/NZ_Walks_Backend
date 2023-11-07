using AutoMapper;
using NZWalks_API.Models.Domain;
using NZWalks_API.Models.Domain.DTOs;
using NZWalks_API.Models.DTOs;

namespace NZWalks_API.Mappings
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            // Because they are both same we do not need to define member properties explicitly
            CreateMap<Region, RegionDTO>().ReverseMap();
            CreateMap<AddRegionReqDto, Region>().ReverseMap();
            CreateMap<UpdateRegionReqDto, Region>().ReverseMap();

        }
    }
}