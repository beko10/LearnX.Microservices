namespace CatalogService.Application.Features.CourseFeature.DTOs;

public class GetByIdCourseQueryDto
{
    public string Title { get; set; } = default!;
    public string Description { get; set; } = default!;
    public decimal Price { get; set; }
    public string? Picture { get; set; }
    public string UserId { get; set; }
    public string CategoryId { get; set; }
    public GetByIdCourseFeatureDto Feature { get; set; } = default!;
}

public class GetByIdCourseFeatureDto
{
    public string Duration { get; set; } = default!;
    public float Rating { get; set; }
    public string EducatorFullName { get; set; } = default!;
}

