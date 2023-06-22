using Microsoft.VisualBasic;

namespace jwtauth;

public class HomeSectionUnitOfWork : BaseSettingsUnitOfWork<HomeSection>, IHomeSectionUnitOfWork
{
    private readonly IImageConverter _converter;
    private readonly ICloud _cloud;
    public HomeSectionUnitOfWork(IHomeSectionRepository repository,
        IImageConverter converter , ICloud cloud) : base(repository)
    {
        _converter = converter;
        _cloud = cloud; 
    }


    public async Task Create(SectionRequest request)
    {
        await CheckSectionNameIsUniqueAsync(request.Name);

        if (request.Image == null)
            throw new ArgumentException("Image was not supplied");

        byte[] image = await _converter.ConvertImage(request.Image);

        HomeSection homeSection = new()
        {
            Name = request.Name,
            SectionText = request.SectionText,
            ImageId = await _cloud.UploadFile($"{request.Name}.jpg", image)
        };

        await Create(homeSection);
    }

    public async Task<IEnumerable<HomeSectionResponse>> ReadSectionsResponse()
    {
        List<HomeSectionResponse> responses = new();

        var SectionsFromDb = await Read();

        foreach(HomeSection section in SectionsFromDb)
        {
            HomeSectionResponse response = new();

            response.Id = section.Id;
            response.SectionText = section.SectionText;
            response.Name = section.Name;
            response.ImageUrl = await _cloud.GetFileUrl($"{section.Name}.jpg");

            responses.Add(response);
        }

        return responses;
    }

    public async Task Update(SectionRequest request)
    {
        HomeSection sectionFromDb = await Read(request.Id);

        if (sectionFromDb == null)
            throw new ArgumentException("Section not found");

        if (request.Name != sectionFromDb.Name)
            await CheckSectionNameIsUniqueAsync(request.Name);

        sectionFromDb.Name = request.Name;
        sectionFromDb.SectionText = request.SectionText;

        if (request.Image != null)
        { 
           byte[] image =  await _converter.ConvertImage(request.Image);

            sectionFromDb.ImageId =
                 await _cloud.UploadFile($"{sectionFromDb.Name}.jpg", image, sectionFromDb.ImageId);
        }

        await Update(sectionFromDb);
    }

    private async Task CheckSectionNameIsUniqueAsync(string name)
    {
        var sectionsFromDb = await Search(name);

        if (sectionsFromDb.Any())
            throw new ArgumentException("This name is already used");
    }
}
