namespace WebApi.UserApi;

// Microsoft.
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

// WebApi.UserApi.
using Data;

// Serilog.
using Serilog;
using WebApi.UserApi.Services;

public class Program
{
    public static void Main(string[] args)
    {
        //Log.Logger = new LoggerConfiguration()
        //    .ReadFrom
        //    .Configuration(Configuration)
        //    .CreateLogger();
        
        var builder = WebApplication.CreateBuilder(args);

        // Add Serilog to the container.
        builder.Host.UseSerilog((context, configuration) => configuration
            .WriteTo.Console()
            .ReadFrom.Configuration(context.Configuration), true);

        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddDbContext<UserContext>(options =>
    //builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options
    .UseSqlServer(Configuration.GetConnectionString("DefaultConnection"))
#if DEBUG
        .EnableSensitiveDataLogging(true)
#endif
    );

        builder.Services.AddRouting(options => options.LowercaseUrls =  true);

        // Add Scoped objects.
        builder.Services.AddScoped<IUserService, UserService>();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }

    /// <summary>
    /// 
    /// </summary>
    public static IConfiguration Configuration { get; } = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
#if DEBUG
        .AddJsonFile($"appsettings.Development.json", optional: true)
#endif
        .Build();
}
