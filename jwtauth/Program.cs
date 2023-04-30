using jwtauth;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
 
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddDbContext<ApplicationDbContext>(
    options => options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection"))
               .EnableDetailedErrors()
               .EnableSensitiveDataLogging()
               .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));

builder.Services.AddOptions();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer();

builder.Services.ConfigureOptions<JwtAccessOptionsSetup>();
builder.Services.ConfigureOptions<JwtRefreshOptionsSetup>();
builder.Services.ConfigureOptions<JwtBearerOptionsSetup>();

builder.Services.ConfigureOptions<GmailSmtpOptionsSetup>();
builder.Services.ConfigureOptions<TwilioOptionsSetup>();

builder.Services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
builder.Services.AddScoped(typeof(IBaseRepositiorySettings<>), typeof(BaseRepositiorySettings<>));
builder.Services.AddScoped(typeof(IBaseUnitOfWork<>), typeof(BaseUnitOfWork<>));
builder.Services.AddScoped(typeof(IBaseSettingsUnitOfWork<>), typeof(BaseSettingsUnitOfWork<>));

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserUnitOfWork, UserUnitOfWork>();

builder.Services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();

builder.Services.AddScoped<IHomeSectionRepository, HomeSectionRepository>();
builder.Services.AddScoped<IHomeSectionUnitOfWork, HomeSectionUnitOfWork>();

builder.Services.AddScoped<IRecordResultRepository, RecordResultRepository>();
builder.Services.AddScoped<IRecordResultUnitOfWork, RecordResultUnitOfWork>();

builder.Services.AddSingleton<IPythonScriptExcutor, PythonScriptExcutor>();

builder.Services.AddSingleton<IJwtProvider, JwtProvider>();

builder.Services.AddSingleton<IFileSaver, FileSaver>();

builder.Services.AddSingleton<ISmsSender, TwilioSmsSender>();
builder.Services.AddSingleton<IMailSender, GmailSmtpMailSender>();

builder.Services.AddSingleton<IOTPGenrator, SixRandomDigitOTPGenrator>();

builder.Services.AddTransient<GlobalErrorHandlerMiddleware>();
builder.Services.AddTransient<RefreshTokenValidator>();

var app = builder.Build();

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseMiddleware<GlobalErrorHandlerMiddleware>();

app.MapControllers();

app.UseCors("AllowAll");

app.Run();
