using Sukt.Identity.Domain.Aggregates.Users;
using Sukt.Identity.Domain.Shared.Users;

namespace Sukt.Identity.EntityFrameworkCore.EntityConfigurations
{
    public class IdentityUserConfiguration : AggregateRootMappingConfiguration<IdentityUser, string>
    {
        public override void Map(EntityTypeBuilder<IdentityUser> b)
        {
            b.HasKey(o => o.Id);
            b.Property(o => o.UserName).HasMaxLength(50).IsRequired();
            b.Property(o => o.NormalizedUserName).HasMaxLength(50);
            b.Property(o => o.NormalizedEmail).IsRequired();
            b.Property(o => o.PhoneNumberConfirmed).IsRequired().HasDefaultValue(true);
            b.Property(o => o.TwoFactorEnabled).HasDefaultValue(false);
            b.Property(o => o.LockoutEnabled).HasDefaultValue(false);
            b.Property(o => o.AccessFailedCount).HasDefaultValue(0);
            b.Property(o => o.ConcurrencyStamp).IsConcurrencyToken();
            b.HasMany(x=>x.Roles).WithOne().HasForeignKey(o => o.UserId);
            b.HasMany(x => x.Logins).WithOne().HasForeignKey(o => o.UserId);
            b.HasMany(x => x.Claims).WithOne().HasForeignKey(o => o.UserId);
            b.HasMany(x => x.Tokens).WithOne().HasForeignKey(o => o.UserId);
            b.ToTable($"{SuktIdentityDbProperties.DbTablePrefix}users");
        }
    }
}
