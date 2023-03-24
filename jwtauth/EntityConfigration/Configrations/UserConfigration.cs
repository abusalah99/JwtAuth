namespace jwtauth;

public class UserConfigration : BaseConfigrationSettings<User> 
{
    public override void Configure(EntityTypeBuilder<User> builder)
    {
       base.Configure(builder);

        builder.Property(e => e.Email).IsRequired();
        builder.HasIndex(e => e.Email).IsUnique();

        builder.Property(e => e.Password).IsRequired();

        builder.Property(e => e.Role).HasDefaultValue("User").ValueGeneratedOnAdd();

        builder.HasOne(e => e.Token).WithOne(e => e.User).HasForeignKey<RefreshToken>(e=>e.UserId);
    }
}
