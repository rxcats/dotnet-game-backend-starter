using MessagePack.AspNetCoreMvcFormatter;
using MessagePack.Resolvers;
using Microsoft.EntityFrameworkCore;
using RxCats.GameApi.Conf;
using RxCats.GameApi.Extensions;
using RxCats.GameApi.Filter;
using RxCats.GameApi.Options;
using RxCats.GameApi.Provider;
using RxCats.GameApi.Provider.Impl;
using RxCats.GameApi.Service;
using RxCats.GameApi.Service.Impl;

var builder = WebApplication.CreateBuilder(args);

if (builder.Environment.IsDevelopment())
{
    builder.WebHost.ConfigureLogging(ZLoggerConfiguration.ApplyConsoleOptions);
}
else
{
    builder.WebHost.ConfigureLogging(ZLoggerConfiguration.ApplyFullOptions);
}

builder.WebHost.ConfigureServices(services =>
{
    services.Configure<GameOptions>(builder.Configuration.GetSection(nameof(GameOptions)));

    services.AddCors(options =>
    {
        options.AddDefaultPolicy(policyBuilder =>
        {
            policyBuilder.WithMethods("OPTIONS", "GET", "POST", "PUT", "DELETE");
            policyBuilder.AllowAnyHeader();
            policyBuilder.AllowAnyOrigin();
        });
    });

    services.AddControllers().AddMvcOptions(options =>
    {
        options.OutputFormatters.Add(new MessagePackOutputFormatter(ContractlessStandardResolver.Options));
        options.InputFormatters.Add(new MessagePackInputFormatter(ContractlessStandardResolver.Options));
        options.Filters.Add(typeof(GlobalExceptionFilter));
    });

    var databaseOptions = new DatabaseOptions();
    builder.Configuration.GetSection(nameof(DatabaseOptions))
        .Bind(databaseOptions);

    services.AddPooledDbContextFactory<GameDatabaseContext>(options =>
    {
        var connectionString = @$"Server={databaseOptions.Server};
                    Port={databaseOptions.Port};
                    Uid={databaseOptions.User};
                    Pwd={databaseOptions.Password};
                    Database={databaseOptions.Database};";

        options
            .UseMySql(connectionString, new MySqlServerVersion(databaseOptions.Version))
            .EnableSensitiveDataLogging()
            .EnableDetailedErrors()
            .LogTo(Console.WriteLine);
    });

    services.AddScoped<ValidateSession>();

    services.AddSingleton<IAuthService, AuthService>();
    services.AddSingleton<IAccessTokenValidateProvider, FirebaseProvider>();
});

var app = builder.Build();
app.UseCors();
app.UseJsonApiLogging();
app.UseMessagePackApiLogging();
app.MapControllers();
app.Run();