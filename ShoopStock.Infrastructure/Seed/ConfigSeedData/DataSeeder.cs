using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ShoopStock.Infrastructure.Seed.ConfigSeedData;

public static class DataSeeder
{
    public static WebApplication SeedAsync(this WebApplication app, IServiceProvider services)
    {
        var context = services.CreateScope().ServiceProvider.GetRequiredService<ApplicationDbContext>();
        context.Database.Migrate();

        return app;
    }
}
