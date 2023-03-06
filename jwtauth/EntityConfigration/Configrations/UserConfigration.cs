namespace jwtauth;
public class UserConfigration : IEntityTypeConfiguration<User>
{
    public  void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id).HasValueGenerator<GuidValueGenerator>();

        builder.Property(e => e.Name).IsRequired();
        builder.Property(e => e.Name).HasMaxLength(10);

        builder.Property( e=> e.Email).IsRequired();
        builder.HasAlternateKey(e => e.Email);

        builder.Property(e=>e.Password).IsRequired();

        builder.Property(e => e.Role).HasDefaultValue("User").ValueGeneratedOnAdd();
    }

}
