namespace jwtauth;

public class SectionRequest
{
    public string Name { get; set; }
    public string SectionText { get; set; }
    public IFormFile Image { get; set; }
}