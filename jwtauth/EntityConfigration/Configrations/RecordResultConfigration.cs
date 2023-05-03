namespace jwtauth;

public class RecordResultConfigration : BaseConfigration<RecordResult>
{
    public override void Configure(EntityTypeBuilder<RecordResult> builder)
    {
        base.Configure(builder);

        builder.Property(e => e.Feedback).HasMaxLength(128);

        builder.HasOne(e=>e.User).WithMany().HasForeignKey(e=>e.UserId);
    }
}
