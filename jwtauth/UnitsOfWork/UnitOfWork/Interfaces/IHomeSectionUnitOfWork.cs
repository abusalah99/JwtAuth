namespace jwtauth;

public interface IHomeSectionUnitOfWork : IBaseSettingsUnitOfWork<HomeSection> 
{
    Task Create(SectionRequest request);
    Task Update(SectionRequest request);
    Task<IEnumerable<HomeSectionResponse>> ReadSectionsResponse();
}
