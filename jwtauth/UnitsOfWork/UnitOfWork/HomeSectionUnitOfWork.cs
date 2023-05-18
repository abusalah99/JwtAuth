namespace jwtauth;

public class HomeSectionUnitOfWork : BaseSettingsUnitOfWork<HomeSection>, IHomeSectionUnitOfWork
{
    private readonly IImageConverter _converter;
    public HomeSectionUnitOfWork(IHomeSectionRepository repository,
        IImageConverter converter) : base(repository) => _converter = converter;

    public async Task Create(SectionRequest request)
    {
        var sectionFromDb = await Search(request.Name);

        if (sectionFromDb.Any())
            throw new ArgumentException("This name is already used");

        if (request.Image == null)
            throw new ArgumentException("Image was not supplied");

           string extension =  Path.GetExtension(request.Image.FileName);

        byte[] image = await _converter.ConvertImage(request.Image);

        HomeSection homeSection = new()
        {
            Name = request.Name,
            Image = image,
            SectionText = request.SectionText,
            Extention = extension
        };

        await Create(homeSection);
    }

    public async Task Update(SectionRequest request)
    {
        HomeSection sectionFromDb = await Read(request.Id);

        if (sectionFromDb == null) 
            throw new ArgumentException("Section not found");

        if (request.Image != null)
        {
            string extension = Path.GetExtension(request.Image.FileName);

           byte[] image =  await _converter.ConvertImage(request.Image);

            sectionFromDb.Image = image;
            sectionFromDb.Extention = extension;    
        }
        sectionFromDb.Name = request.Name;
        sectionFromDb.SectionText = request.SectionText;
     
        await Update(sectionFromDb);
    }
}
