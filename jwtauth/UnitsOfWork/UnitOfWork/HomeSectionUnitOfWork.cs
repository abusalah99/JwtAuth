namespace jwtauth;

public class HomeSectionUnitOfWork : BaseSettingsUnitOfWork<HomeSection>, IHomeSectionUnitOfWork
{
    public HomeSectionUnitOfWork(IHomeSectionRepository repository,
        ILogger<BaseSettingsUnitOfWork<HomeSection>> logger) : base(repository, logger) { }
}