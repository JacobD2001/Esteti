using Esteti.Application.Logic.Abstractions;
using Esteti.Infrastructure.Persistence;
using Esteti.WebApi.Middlewares;
using Serilog;
using Esteti.Application;
using Esteti.Infrastructure.Auth;
using Esteti.WebApi.Application.Auth;

namespace Esteti.WebApi
{
    public class Program
    {
        public static string APP_NAME = "Esteti.WebApi";

        public static void Main(string[] args)
        {
            //logging before the host is built
            Log.Logger = new LoggerConfiguration()
                .Enrich.WithProperty("Application", APP_NAME)
                .Enrich.WithProperty("MachineName", Environment.MachineName)
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .CreateBootstrapLogger();

            var builder = WebApplication.CreateBuilder(args);

            if(builder.Environment.IsDevelopment())
            {
                builder.Configuration.AddJsonFile("appsettings.Development.local.json");
            }

            //logging after the host is built
            builder.Host.UseSerilog((context, services, configuration) => configuration
                .Enrich.WithProperty("Application", APP_NAME)
                .Enrich.WithProperty("MachineName", Environment.MachineName)
                .ReadFrom.Configuration(context.Configuration)
                .ReadFrom.Services(services)
                .Enrich.FromLogContext());

            // Add services to the container.

            builder.Services.AddHttpContextAccessor();
            builder.Services.AddDatabaseCache();
            builder.Services.AddSqlDatabase(builder.Configuration.GetConnectionString("MainDbSql")!);
            builder.Services.AddControllers();
            builder.Services.AddJwtAuth(builder.Configuration);
            builder.Services.AddJwtAuthenticationDataProvider(builder.Configuration);
            builder.Services.AddPasswordManager();

            builder.Services.AddMediatR(c =>
            {
                c.RegisterServicesFromAssemblyContaining(typeof(BaseCommandHandler)); 
            });

            builder.Services.AddApplicationServices();
            builder.Services.AddValidators();

            builder.Services.AddSwaggerGen(o =>
            {
                o.CustomSchemaIds(x =>
                {
                    var name = x.FullName;
                    if (name != null)
                        name = name.Replace("+", "_"); //swagger bug for .net fix

                    return name;
                });
            });

            builder.Services.AddCors();

            var app = builder.Build();

            if(app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseCors(builder => builder
            .WithOrigins(app.Configuration.GetValue<string>("WebAppBaseUrl") ?? "")
            .WithOrigins(app.Configuration.GetSection("AdditionalCorsOrigins").Get<string[]>() ?? new string[0])
            .WithOrigins((Environment.GetEnvironmentVariable("AdditionalCorsOrigins") ?? "").Split(",").Where(h => !string.IsNullOrEmpty(h)).Select(h => h.Trim()).ToArray())
            .AllowAnyHeader()
            .AllowCredentials()
            .AllowAnyMethod());



            app.UseExceptionResultMiddleware();

            // Configure the HTTP request pipeline.

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
