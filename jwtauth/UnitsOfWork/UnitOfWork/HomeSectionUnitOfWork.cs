using System.Collections;

namespace jwtauth;

public class HomeSectionUnitOfWork : BaseSettingsUnitOfWork<HomeSection>, IHomeSectionUnitOfWork
{
    private readonly IFileSaver _fileSaver;
    public HomeSectionUnitOfWork(IHomeSectionRepository repository,
        ILogger<HomeSectionUnitOfWork> logger, IFileSaver fileSaver)
        : base(repository, logger) => _fileSaver = fileSaver;

    public async Task Create(SectionRequest request, string rootPath)
    {
        var sectionFromDb = await Search(request.Name);

        if (sectionFromDb.Any())
            throw new ArgumentException("This name is already used");

        if (request.Image == null)
            throw new ArgumentException("Image was not supplied");

        string path = rootPath + @"\Resources\Images\" + request.Name 
            + Path.GetExtension(request.Image.FileName);

        await _fileSaver.Save(request.Image, path);

        HomeSection homeSection = new()
        {
            Name = request.Name,
            IamgePath = path,
            SectionText = request.SectionText,
        };

        await Create(homeSection);
    }

    public async Task Update(SectionRequest request, string rootPath)
    {
        HomeSection sectionFromDb = await Read(request.Id);

        if (sectionFromDb == null) 
            throw new ArgumentException("Section not found");

        if (request.Image != null)
        {
            File.Delete(sectionFromDb.IamgePath);

            string path = rootPath + @"\Resources\Images\" + request.Name
                + Path.GetExtension(request.Image.FileName);

            await _fileSaver.Save(request.Image, path);

            sectionFromDb.IamgePath = path;
        }
        sectionFromDb.Name = request.Name;
        sectionFromDb.SectionText = request.SectionText;
     
        await Update(sectionFromDb);
    }
    public override async Task Delete(Guid id)
    {
        HomeSection SectionFromDb = await Read(id);

        if (SectionFromDb == null)
            throw new ArithmeticException("Home section not found");

        await Task.Run(() => File.Delete(SectionFromDb.IamgePath));

        await base.Delete(id);
    }
}
