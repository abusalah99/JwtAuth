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

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer();

builder.Services.ConfigureOptions<JwtAccessOptionsSetup>();
builder.Services.ConfigureOptions<JwtRefreshOptionsSetup>();
builder.Services.ConfigureOptions<JwtBearerOptionsSetup>();

builder.Services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
builder.Services.AddScoped(typeof(IBaseRepositiorySettings<>), typeof(BaseRepositiorySettings<>));
builder.Services.AddScoped(typeof(IBaseUnitOfWork<>), typeof(BaseUnitOfWork<>));
builder.Services.AddScoped(typeof(IBaseSettingsUnitOfWork<>), typeof(BaseSettingsUnitOfWork<>));

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserUnitOfWork, UserUnitOfWork>();
builder.Services.AddScoped<IJwtProvider, JwtProvider>();

builder.Services.AddTransient<GlobalErrorHandlerMiddleware>();
builder.Services.AddTransient<RefreshTokenValidator>();

var app = builder.Build();

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseMiddleware<GlobalErrorHandlerMiddleware>();

app.MapControllers();

app.Run();
