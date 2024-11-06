using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TodoList.Models.Entities;

namespace TodoList.DataAccess.Configurations;

public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.ToTable("Categories");
        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id).HasColumnName("Id");
        builder.Property(c => c.Name).HasColumnName("Name");
        builder.Property(c => c.CreatedDate).HasColumnName("CreatedDate");
        builder.Property(c => c.UpdatedDate).HasColumnName("UpdatedDate");

        builder.HasMany(c => c.Todos).WithOne(t => t.Category).HasForeignKey(t => t.CategoryId);
    }
}