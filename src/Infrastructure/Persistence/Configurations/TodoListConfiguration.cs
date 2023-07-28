using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Todo_App.Domain.Entities;

namespace Todo_App.Infrastructure.Persistence.Configurations;

public class TodoListConfiguration : IEntityTypeConfiguration<TodoList>
{
    public void Configure(EntityTypeBuilder<TodoList> builder)
    {
        builder.Property(t => t.Title)
            .HasMaxLength(200)
            .IsRequired();

        builder
            .OwnsOne(b => b.Colour);

        builder.Property(list => list.Active)
            .HasDefaultValue(true);

        builder.HasQueryFilter(list => list.Active);
    }
}
