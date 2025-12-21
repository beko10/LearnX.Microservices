using AutoMapper;
using CatalogService.Application.Features.CourseFeature.DTOs;
using CatalogService.Domain.Entities;

namespace CatalogService.Application.Features.CourseFeature.Mappings;

public class Mapping : Profile
{
    public Mapping()
    {
        CreateMap<Course, CreateCourseCommandDto>()
            .ForMember(dest => dest.Feature, opt => opt.MapFrom(src => src.Feature))
            .ReverseMap()
            .ForMember(dest => dest.Feature, opt => opt.MapFrom(src => src.Feature));

        CreateMap<Course, UpdateCourseCommandDto>()
            .ForMember(dest => dest.Feature, opt => opt.MapFrom(src => src.Feature))
            .ReverseMap()
            .ForMember(dest => dest.Feature, opt => opt.MapFrom(src => src.Feature));

        CreateMap<Course, GetAllCourseQueryDto>()
            .ForMember(dest => dest.Feature, opt => opt.MapFrom(src => src.Feature))
            .ReverseMap()
            .ForMember(dest => dest.Feature, opt => opt.MapFrom(src => src.Feature));

        CreateMap<Course, GetByIdCourseQueryDto>()
            .ForMember(dest => dest.Feature, opt => opt.MapFrom(src => src.Feature))
            .ReverseMap()
            .ForMember(dest => dest.Feature, opt => opt.MapFrom(src => src.Feature));

        // Feature mappings
        CreateMap<Feature, CreateCourseFeatureDto>().ReverseMap();
        CreateMap<Feature, UpdateCourseFeatureDto>().ReverseMap();
        CreateMap<Feature, GetAllCourseFeatureDto>().ReverseMap();
        CreateMap<Feature, GetByIdCourseFeatureDto>().ReverseMap();
    }
}

