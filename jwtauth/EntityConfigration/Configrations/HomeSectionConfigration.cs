namespace jwtauth;

public class HomeSectionConfigration : BaseConfigration<HomeSection>
{
    public override void Configure(EntityTypeBuilder<HomeSection> builder)
    {
        base.Configure(builder);

        builder.Property(e => e.Image).IsRequired();

        builder.Property(e => e.SectionText).IsRequired();
    }
}