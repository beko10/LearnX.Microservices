
using CatalogService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MongoDB.Bson;
using MongoDB.EntityFrameworkCore.Extensions;

namespace CatalogService.Infrastructure.Persistence.Configurations;

public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.ToCollection("categories");

        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id)
            .HasElementName("_id")
            .HasBsonRepresentation(BsonType.ObjectId)
            .IsRequired();

        builder.Property(c => c.Name)
            .HasElementName("name")
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(c => c.CreatedDate)
            .HasElementName("createdDate")
            .HasBsonRepresentation(BsonType.DateTime)
            .IsRequired();

        builder.Property(c => c.UpdatedDate)
            .HasElementName("updatedDate")
            .HasBsonRepresentation(BsonType.DateTime)
            .IsRequired();


        builder.Ignore(c => c.Courses);
    }
}