namespace jwtauth;

public class HomeSection : BaseEntitySettings
{
    public required byte[] Image { get; set; }
    public required string Extention { get; set; }  
    public required string SectionText { get; set; } 
    
}
