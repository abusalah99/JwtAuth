namespace jwtauth;

public interface ICloud
{
    Task<string> UploadFile(string fileName, byte[] file, string? existingFileId = null);
    Task<string> GetFileUrl(string fileName);
}
