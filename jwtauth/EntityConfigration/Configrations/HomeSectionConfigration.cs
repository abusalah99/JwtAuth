﻿namespace jwtauth;

public class HomeSectionConfigration : BaseConfigration<HomeSection>
{
    public override void Configure(EntityTypeBuilder<HomeSection> builder)
    {
        base.Configure(builder);

        builder.Property(e => e.IamgePath).IsRequired();
        builder.Property(e => e.SectionText).IsRequired();
        builder.HasIndex(e => e.Name).IsUnique();
    }
}