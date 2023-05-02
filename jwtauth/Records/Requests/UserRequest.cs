namespace jwtauth;

public class UserRequest
{
    public required string Email { get; set; }
    public string? Password { get; set; }
    public int Age { get; set; }
    public string? FristName { get; set; }
    public string? LastName { get; set; }
    public string? Phone { get; set; }
    public IFormFile? UserImage { get; set; }
    public string? ImageExtention { get; set; }  
}
