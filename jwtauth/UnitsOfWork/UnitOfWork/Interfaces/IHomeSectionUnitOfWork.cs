namespace jwtauth;

public interface IHomeSectionUnitOfWork : IBaseSettingsUnitOfWork<HomeSection> 
{
    Task Create(SectionRequest request, string rootPath);
    Task Update(SectionRequest request, string rootPath);
}
