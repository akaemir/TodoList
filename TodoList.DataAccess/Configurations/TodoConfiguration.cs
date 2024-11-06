using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TodoList.Models.Entities;

namespace TodoList.DataAccess.Configurations;

public class TodoConfiguration : IEntityTypeConfiguration<Todo>
{
    public void Configure(EntityTypeBuilder<Todo> builder)
    {
        builder.ToTable("Todos").HasKey(t => t.Id);
        builder.Property(t => t.Id).HasColumnName("Id");
        builder.Property(t => t.Title).HasColumnName("Title");
        builder.Property(t => t.Description).HasColumnName("Description");
        builder.Property(t => t.Completed).HasColumnName("Completed");
        builder.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
        builder.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(t => t.UserId).HasColumnName("UserId");

        builder.HasOne(t => t.User).WithMany(u => u.Todos).HasForeignKey(t => t.UserId)
            .OnDelete(DeleteBehavior.NoAction);
        builder.HasOne(x => x.Category).WithMany(x => x.Todos).HasForeignKey(x => x.CategoryId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.Navigation(x => x.User).AutoInclude();
        builder.Navigation(x => x.Category).AutoInclude();
    }
}