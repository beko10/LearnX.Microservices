using DiscountService.API.Common.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MongoDB.Bson;
using MongoDB.EntityFrameworkCore.Extensions;

namespace DiscountService.API.Common.MongoDB;

public class CouponConfiguration : IEntityTypeConfiguration<Coupon>
{
    public void Configure(EntityTypeBuilder<Coupon> builder)
    {
        builder.ToCollection("coupons");

        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id)
            .HasElementName("_id")
            .HasBsonRepresentation(BsonType.ObjectId)
            .IsRequired();

        builder.Property(c => c.Code)
            .HasElementName("code")
            .IsRequired()
            .HasMaxLength(20);

        builder.HasIndex(c => c.Code).IsUnique();

        builder.Property(c => c.Description)
            .HasElementName("description")
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(c => c.DiscountAmount)
            .HasElementName("discountAmount")
            .HasBsonRepresentation(BsonType.Decimal128);

        builder.Property(c => c.DiscountPercentage)
            .HasElementName("discountPercentage");

        builder.Property(c => c.IsPercentage)
            .HasElementName("isPercentage");

        builder.Property(c => c.ValidFrom)
            .HasElementName("validFrom")
            .HasBsonRepresentation(BsonType.DateTime)
            .IsRequired();

        builder.Property(c => c.ValidUntil)
            .HasElementName("validUntil")
            .HasBsonRepresentation(BsonType.DateTime)
            .IsRequired();

        builder.Property(c => c.UsageLimit)
            .HasElementName("usageLimit");

        builder.Property(c => c.UsedCount)
            .HasElementName("usedCount");

        builder.Property(c => c.IsActive)
            .HasElementName("isActive");

        builder.Property(c => c.CreatedAt)
            .HasElementName("createdAt")
            .HasBsonRepresentation(BsonType.DateTime)
            .IsRequired();

        builder.Property(c => c.UpdatedAt)
            .HasElementName("updatedAt")
            .HasBsonRepresentation(BsonType.DateTime);
    }
}
