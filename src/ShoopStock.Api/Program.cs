using FluentValidation.AspNetCore;
using Serilog;
using ShoopStock.Api.Installers;
using ShoopStock.Api.Models;
using ShoopStock.Core.Infrastructure.DependencyInjection;
using ShoopStock.Core.Massages;
using ShoopStock.Infrastructure;
using ShoopStock.Infrastructure.Repositories;
using ShoopStock.Infrastructure.Seed.ConfigSeedData;
using ShoopStock.Infrastructure.UnitOfWork;
using ShoopStock.Services;
using ShoopStock.Services.Interfaces;
using ShoopStock.Services.Services;
using Swashbuckle.AspNetCore.SwaggerUI;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((ctx, lc) => lc
    .ReadFrom.Configuration(ctx.Configuration));

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .CreateLogger();

#pragma warning disable CS0618 // Type or member is obsolete
builder.Services
    .AddControllers()
    .AddFluentValidation(conf => conf.RegisterValidatorsFromAssemblyContaining<ProductRequestValidator>())
    .AddJsonOptions(x =>
    {
        x.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });
#pragma warning restore CS0618 // Type or member is obsolete

builder.Services.AddHttpClient();
builder.Services.RegisterAllDependencies();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddServices();

builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<ICategoryRepositoriy, CategoryRepositoriy>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// Add services to the container.
builder.Services.InstallServicesInAssembly(builder.Configuration, builder.Environment);


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

try
{
    Log.Information(LoggMassages.AppStart);
    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (!app.Environment.IsProduction())
    {
        app.UseSwagger();
        app.UseSwaggerUI(options => options.DocExpansion(DocExpansion.None));
    }

    app.UseSerilogRequestLogging();

    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();

    app.SeedAsync(app.Services).Run();

}
catch (Exception e)
{
    Log.Fatal(e, LoggMassages.FailedStart);
}
finally
{
    Log.CloseAndFlush();
}
