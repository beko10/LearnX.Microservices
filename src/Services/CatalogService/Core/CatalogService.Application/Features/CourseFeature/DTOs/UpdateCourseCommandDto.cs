namespace CatalogService.Application.Features.CourseFeature.DTOs;

public class UpdateCourseCommandDto
{
    public string Id { get; set; } = default!;
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public decimal Price { get; set; }
    public string? Picture { get; set; }
    public Guid UserId { get; set; }
    public Guid CategoryId { get; set; }
    public UpdateCourseFeatureDto Feature { get; set; } = null!;
}

public class UpdateCourseFeatureDto
{
    public string Duration { get; set; } = null!;
    public float Rating { get; set; }
    public string EducatorFullName { get; set; } = null!;
}

