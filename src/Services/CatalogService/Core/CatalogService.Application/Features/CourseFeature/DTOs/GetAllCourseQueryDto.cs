namespace CatalogService.Application.Features.CourseFeature.DTOs;

public class GetAllCourseQueryDto
{
    public string Title { get; set; } = default!;
    public string Description { get; set; } = default!;
    public decimal Price { get; set; }
    public string? Picture { get; set; }
    public Guid UserId { get; set; }
    public Guid CategoryId { get; set; }
    public GetAllCourseFeatureDto Feature { get; set; } = default!;
}

public class GetAllCourseFeatureDto
{
    public string Duration { get; set; } = default!;
    public float Rating { get; set; }
    public string EducatorFullName { get; set; } = default!;
}

