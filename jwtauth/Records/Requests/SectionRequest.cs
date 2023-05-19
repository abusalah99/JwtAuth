namespace jwtauth;

public record SectionRequest
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public required string SectionText { get; set; }
    public IFormFile? Image { get; set; }
}