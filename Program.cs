// Can use builder to add services
// https://learn.microsoft.com/en-us/aspnet/core/fundamentals/static-files?view=aspnetcore-7.0

using Microsoft.Extensions.DependencyInjection;
using SimpleStoreFront.Services;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.UseStaticWebAssets();

builder.Services.AddTransient<IMailService, NullMailService>();

builder.Services.AddControllersWithViews()
    .AddRazorRuntimeCompilation();

builder.Services.AddRazorPages();

var app = builder.Build();

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
