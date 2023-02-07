var builder = WebApplication.CreateBuilder(args);

// Can use builder to add services
// https://learn.microsoft.com/en-us/aspnet/core/fundamentals/static-files?view=aspnetcore-7.0

var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

//app.MapGet("/", () => "Hello World!");

app.Run();
