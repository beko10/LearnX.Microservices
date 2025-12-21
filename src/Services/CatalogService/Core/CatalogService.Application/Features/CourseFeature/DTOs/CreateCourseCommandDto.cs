namespace CatalogService.Application.Features.CourseFeature.DTOs;

public class CreateCourseCommandDto
{
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public decimal Price { get; set; }
    public string? Picture { get; set; }
    public Guid UserId { get; set; }
    public Guid CategoryId { get; set; }
    public CreateCourseFeatureDto Feature { get; set; } = null!;
}

public class CreateCourseFeatureDto
{
    public string Duration { get; set; } = null!;
    public float Rating { get; set; }
    public string EducatorFullName { get; set; } = null!;
}

