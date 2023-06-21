namespace jwtauth;

public class HomeSectionConfigration : BaseConfigration<HomeSection>
{
    public override void Configure(EntityTypeBuilder<HomeSection> builder)
    {
        base.Configure(builder);

        builder.Property(e => e.SectionText).IsRequired();

        builder.Property(e => e.ImageId).IsRequired();  

        builder.HasIndex(e => e.Name).IsUnique();
    }
}