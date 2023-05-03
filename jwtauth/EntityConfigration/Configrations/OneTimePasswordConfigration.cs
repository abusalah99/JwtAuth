namespace jwtauth;

public class OneTimePasswordConfigration : BaseConfigration<OneTimePassword>
{
    public override void Configure(EntityTypeBuilder<OneTimePassword> builder)
    {
        base.Configure(builder);

        builder.Property(e => e.Value).IsRequired().HasMaxLength(6);

        builder.Property(e => e.CreatedAt).IsRequired();

        builder.Property(e => e.ExpireAt).IsRequired();
    }
}
