namespace jwtauth;

public class UserResponse : BaseEntity
{
    public string Email { get; set; }
    public int Age { get; set; }
    public string? FristName { get; set; }
    public string? LastName { get; set; }
    public string? Phone { get; set; }
    public string? ImageUrl { get; set;}
}

