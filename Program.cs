// Can use builder to add services
// https://learn.microsoft.com/en-us/aspnet/core/fundamentals/static-files?view=aspnetcore-7.0

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SimpleStoreFront.Data;
using SimpleStoreFront.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<StoreFrontContext>(cfg =>
{
    cfg.UseSqlServer();
});

builder.WebHost.UseStaticWebAssets();

builder.Services.AddTransient<StoreSeeder>();

builder.Services.AddTransient<IMailService, NullMailService>();

builder.Services.AddControllersWithViews();

builder.Services.AddRazorPages()
    .AddRazorRuntimeCompilation();

builder.Host.ConfigureAppConfiguration(AddConfiguration);

void AddConfiguration(HostBuilderContext ctx, IConfigurationBuilder bldr)
{
    bldr.Sources.Clear();
    bldr.SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("config.json")
        .AddEnvironmentVariables();
}

var app = builder.Build();

var scopeFactory = app.Services.GetService<IServiceScopeFactory>();

using (var scope = scopeFactory.CreateScope())
{
    var seeder = scope.ServiceProvider.GetService<StoreSeeder>();
    //scope.ServiceProvider.GetRequiredService<StoreFrontContext>().Database.Migrate();
    seeder.Seed();
}

if (builder.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/error");
}

app.UseStaticFiles();

app.UseRouting();

app.UseEndpoints(cfg =>
{
    cfg.MapRazorPages();

    cfg.MapControllerRoute("Default",
        "/{controller}/{action}/{id?}",
        new { controller = "App" , action = "Index" });
});


app.Run();




//static void RunSeeding(IWebHost Host)
//{
//    var seeder = app.Services.GetService<StoreSeeder>();
//    seeder.Seed();
//}
