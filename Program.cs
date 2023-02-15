// Can use builder to add services
// https://learn.microsoft.com/en-us/aspnet/core/fundamentals/static-files?view=aspnetcore-7.0

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SimpleStoreFront.Data;
using SimpleStoreFront.Data.Entities;
using SimpleStoreFront.Services;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();
builder.Logging.AddConsole();

builder.WebHost.UseStaticWebAssets();

builder.Services.AddTransient<StoreSeeder>();

builder.Services.AddTransient<IMailService, NullMailService>();

builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

builder.Services.AddControllersWithViews();

builder.Services.AddRazorPages()
    .AddRazorRuntimeCompilation()
    .AddNewtonsoftJson(cfg => cfg.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

builder.Services.AddScoped<IStoreFrontRepository, StoreFrontRepository>();

builder.Services.AddMvc();

builder.Host.ConfigureAppConfiguration(AddConfiguration);
void AddConfiguration(HostBuilderContext ctx, IConfigurationBuilder bldr)
{
    bldr.Sources.Clear();
    bldr.SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("config.json")
        .AddEnvironmentVariables();
}

var stringConnection = builder.Configuration.GetConnectionString("StoreFrontContextDb");
builder.Services.AddDbContext<StoreFrontContext>(cfg =>
{
    cfg.UseSqlServer(stringConnection);
});

builder.Services.AddIdentity<StoreUser, IdentityRole>(cfg =>
{
    cfg.User.RequireUniqueEmail = true;
})
    .AddEntityFrameworkStores<StoreFrontContext>();

var app = builder.Build();

var scopeFactory = app.Services.GetService<IServiceScopeFactory>();
using (var scope = scopeFactory.CreateScope())
{
    var seeder = scope.ServiceProvider.GetService<StoreSeeder>();
    seeder.SeedAsync().Wait();
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

app.UseAuthentication();
app.UseAuthorization();

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
//scope.ServiceProvider.GetRequiredService<StoreFrontContext>().Database.Migrate();