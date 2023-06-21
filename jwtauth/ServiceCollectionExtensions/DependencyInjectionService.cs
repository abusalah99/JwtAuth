namespace jwtauth;

public static class DependencyInjectionService
{
    public static void AddDependencyInjectionService(this IServiceCollection services)
    {
        services.AddSingleton(typeof(IBaseRepository<>), typeof(BaseRepository<>));
        services.AddSingleton(typeof(IBaseRepositiorySettings<>), typeof(BaseRepositiorySettings<>));
        services.AddSingleton(typeof(IBaseUnitOfWork<>), typeof(BaseUnitOfWork<>));
        services.AddSingleton(typeof(IBaseSettingsUnitOfWork<>), typeof(BaseSettingsUnitOfWork<>));

        services.AddSingleton<IPythonScriptExcutor, PythonScriptExcutor>();

        services.AddSingleton<IJwtProvider, JwtProvider>();

        services.AddSingleton<IFileSaver, FileSaver>();

        services.AddSingleton<ICloud, BackblazeCloud>();

        services.AddSingleton<ISmsSender, TwilioSmsSender>();
        services.AddSingleton<IMailSender, GmailSmtpMailSender>();

        services.AddSingleton<IOTPGenrator, SixRandomDigitOTPGenrator>();

        services.AddSingleton<IImageConverter, ImageConverter>();

        services.AddSingleton<RefreshTokenValidator>();

        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IUserUnitOfWork, UserUnitOfWork>();

        services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();

        services.AddScoped<IHomeSectionRepository, HomeSectionRepository>();
        services.AddScoped<IHomeSectionUnitOfWork, HomeSectionUnitOfWork>();

        services.AddScoped<IRecordResultRepository, RecordResultRepository>();
        services.AddScoped<IRecordResultUnitOfWork, RecordResultUnitOfWork>();

        services.AddScoped<IStatusUnitOfWork, StatusUnitOfWork>();

        services.AddTransient<GlobalErrorHandlerMiddleware>();
        services.AddTransient<TransactionRollbackMiddleware>();
        services.AddTransient<CorsMiddleware>();
    }
}
