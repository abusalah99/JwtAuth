namespace jwtauth;

public static class DependencyInjectionService
{
    public static void AddDependencyInjectionService(this IServiceCollection services)
    {
        services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
        services.AddScoped(typeof(IBaseRepositiorySettings<>), typeof(BaseRepositiorySettings<>));
        services.AddScoped(typeof(IBaseUnitOfWork<>), typeof(BaseUnitOfWork<>));
        services.AddScoped(typeof(IBaseSettingsUnitOfWork<>), typeof(BaseSettingsUnitOfWork<>));

        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IUserUnitOfWork, UserUnitOfWork>();

        services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();

        services.AddScoped<IHomeSectionRepository, HomeSectionRepository>();
        services.AddScoped<IHomeSectionUnitOfWork, HomeSectionUnitOfWork>();

        services.AddScoped<IRecordResultRepository, RecordResultRepository>();
        services.AddScoped<IRecordResultUnitOfWork, RecordResultUnitOfWork>();

        services.AddScoped<IStatusUnitOfWork, StatusUnitOfWork>();

        services.AddSingleton<IPythonScriptExcutor, PythonScriptExcutor>();

        services.AddSingleton<IJwtProvider, JwtProvider>();

        services.AddSingleton<IFileSaver, FileSaver>();

        services.AddSingleton<ISmsSender, TwilioSmsSender>();
        services.AddSingleton<IMailSender, GmailSmtpMailSender>();

        services.AddSingleton<IOTPGenrator, SixRandomDigitOTPGenrator>();

        services.AddSingleton<IImageConverter, ImageConverter>();

        services.AddTransient<GlobalErrorHandlerMiddleware>();
        services.AddTransient<TransactionRollbackMiddleware>();
        services.AddTransient<CorsMiddleware>();
        services.AddTransient<RefreshTokenValidator>();
    }
}
