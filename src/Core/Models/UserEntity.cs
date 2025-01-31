using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Core.Models
{
    public class UserEntity: BaseEntity
    {
        public required string Name { get; set; }
        public required string Password { get; set; }
    }

    internal class UserConfiguration: BaseEntityConfiguration<UserEntity>
    {
        public override void Configure(EntityTypeBuilder<UserEntity> builder)
        {
            base.Configure(builder);
        }
    }
}
