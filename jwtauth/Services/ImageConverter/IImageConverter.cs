namespace jwtauth;

public interface IImageConverter
{
    Task<byte[]> ConvertImage(IFormFile image); 
}
