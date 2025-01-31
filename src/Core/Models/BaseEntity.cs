using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Core.Models
{
    public abstract class BaseEntity
	{
		public long Id { get; init; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public bool IsDeleted { get; set; }
    }

    internal abstract class BaseEntityConfiguration<T> : IEntityTypeConfiguration<T> where T : BaseEntity
    {
        public virtual void Configure(EntityTypeBuilder<T> builder)
        {
            builder
                   .HasQueryFilter(e => !e.IsDeleted);

            builder
                .HasKey(_=>_.Id);

            builder
                .Property(e => e.CreatedAt)
                .IsRequired()
                .HasDefaultValueSql("now()");

            builder
                .Property(e => e.UpdatedAt)
                .IsRequired()
                .HasDefaultValueSql("now()");
        }
    }
}
