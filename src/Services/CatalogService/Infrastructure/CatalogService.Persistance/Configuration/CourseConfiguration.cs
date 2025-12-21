
using CatalogService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MongoDB.Bson;
using MongoDB.EntityFrameworkCore.Extensions;

namespace CatalogService.Infrastructure.Persistence.Configurations;

public class CourseConfiguration : IEntityTypeConfiguration<Course>
{
    public void Configure(EntityTypeBuilder<Course> builder)
    {
        builder.ToCollection("courses");

        // GUID Id -> _id mapping
        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id)
            .HasElementName("_id")
            .HasBsonRepresentation(BsonType.ObjectId)
            .IsRequired();

        
        builder.Property(c => c.Title)
            .HasElementName("title")
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(c => c.Description)
            .HasElementName("description")
            .IsRequired();

        builder.Property(c => c.Price)
            .HasElementName("price")
            .HasBsonRepresentation(BsonType.Decimal128)
            .IsRequired();

        builder.Property(c => c.Picture)
            .HasElementName("picture")
            .IsRequired(false);

        
        builder.Property(c => c.UserId)
            .HasElementName("userId")
            .HasBsonRepresentation(BsonType.String)
            .IsRequired();

        builder.Property(c => c.CategoryId)
            .HasElementName("categoryId")
            .HasBsonRepresentation(BsonType.String)
            .IsRequired();

        builder.Property(c => c.CreatedDate)
            .HasElementName("createdDate")
            .HasBsonRepresentation(BsonType.DateTime)
            .IsRequired();

        builder.Property(c => c.UpdatedDate)
            .HasElementName("updatedDate")
            .HasBsonRepresentation(BsonType.DateTime)
            .IsRequired();


        builder.OwnsOne(c => c.Feature, featureBuilder =>
        {
            featureBuilder.HasElementName("feature"); 

            featureBuilder.Property(f => f.Duration)
                .HasElementName("duration")
                .IsRequired();

           
            featureBuilder.Property(f => f.Rating)
                .HasElementName("rating")
                .HasBsonRepresentation(BsonType.Double)
                .IsRequired();

       
            featureBuilder.Property(f => f.EducatorFullName)
                .HasElementName("educatorFullName")
                .IsRequired()
                .HasMaxLength(100);
        });

        builder.Ignore(c => c.Category);
    }
}