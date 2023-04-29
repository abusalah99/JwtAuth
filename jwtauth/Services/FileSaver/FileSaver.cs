namespace jwtauth;

public class FileSaver : IFileSaver
{
    public async Task Save(IFormFile file, string filePath)
    {
        Stream fileStream = new FileStream(filePath, FileMode.Create);

        await file.CopyToAsync(fileStream);
        fileStream.Close();
    }
}
