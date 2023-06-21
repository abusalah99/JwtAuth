namespace jwtauth;

public class BackblazeOptionsSetup : OptionSetup<BackblazeOptions>
{
    public BackblazeOptionsSetup(IConfiguration configuration, string sectionName = "Backblaze")
    : base(configuration, sectionName) { }
}
