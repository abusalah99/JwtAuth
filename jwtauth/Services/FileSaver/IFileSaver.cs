namespace jwtauth;

public interface IFileSaver 
{
    public Task Save(IFormFile file, string FilePath);
}
