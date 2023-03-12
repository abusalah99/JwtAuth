namespace jwtauth;

public class UserConfigration : BaseConfigrationSettings<User> 
{
    public override void Configure(EntityTypeBuilder<User> builder)
    {
       base.Configure(builder);

        builder.Property( e=> e.Email).IsRequired();
        builder.HasAlternateKey(e => e.Email);

        builder.Property(e=>e.Password).IsRequired();

        builder.Property(e => e.Role).HasDefaultValue("User").ValueGeneratedOnAdd();

        builder.Property(e => e.Token).HasMaxLength(128);
    }
}
