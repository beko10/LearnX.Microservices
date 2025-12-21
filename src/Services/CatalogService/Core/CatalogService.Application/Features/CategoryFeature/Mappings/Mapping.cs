using AutoMapper;
using CatalogService.Application.Features.CategoryFeature.DTOs;
using CatalogService.Domain.Entities;

namespace CatalogService.Application.Features.CategoryFeature.Mappings;
public class Mapping : Profile
{
    public Mapping()
    {
        CreateMap<Category, CreateCategoryCommandDto>().ReverseMap();

        CreateMap<Category, UpdateCategoryCommandDto>().ReverseMap();

        CreateMap<Category, GetAllCategoryQueryDto>().ReverseMap();

        CreateMap<Category, GetByIdCategoryQueryDto>().ReverseMap();

    }
}
