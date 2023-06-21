namespace jwtauth;

public class BackblazeCloud : ICloud
{
    private readonly BackblazeOptions _options;

    private B2Client Client ;
    private B2Options b2Options ;
    public BackblazeCloud(IOptions<BackblazeOptions> options)
    {
        _options = options.Value;

        b2Options = new()
        {
            KeyId = _options.AccountId,
            ApplicationKey = _options.ApplicationKey,
        };

        Client = new(B2Client.Authorize(b2Options));
    }

    public async Task<string> GetFileUrl(string fileName)
    {
        var token = await Client.Files.GetDownloadAuthorization(
            fileNamePrefix: fileName,
            validDurationInSeconds: 86400,
            bucketId:_options.BucketId);

        string bucketUrl = Client.Files.GetFriendlyDownloadUrl(fileName,_options.BucketName);

        string acessUrl = $"{bucketUrl}?Authorization={token.AuthorizationToken}";
        return acessUrl;   
    }

    public async Task<string> UploadFile(string fileName, byte[] file, string? existingFileId)
    {
        if(existingFileId != null) 
            await Client.Files.Delete(existingFileId, fileName);

        var Fileinfo =  await Client.Files.Upload(file, fileName, _options.BucketId); 

        return Fileinfo.FileId;

    }
}
