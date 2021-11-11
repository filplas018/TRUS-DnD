using TUSDemo.Services;
using tusdotnet;
using tusdotnet.Interfaces;
using tusdotnet.Models;
using tusdotnet.Stores;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();
builder.Services.AddSingleton<FileStorageManager>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseTus(httpContext => 
{
    var uploadPath = Path.Combine(app.Environment.ContentRootPath, "Uploads");
    var fsm = httpContext.RequestServices.GetService<FileStorageManager>();
    var TUSconf = new DefaultTusConfiguration
    {
        Store = new TusDiskStore(uploadPath),
        UrlPath = "/files",
        MaxAllowedUploadSizeInBytes = 100000000,
        Events = new tusdotnet.Models.Configuration.Events
        { 
            OnFileCompleteAsync = async eventContext =>
            {
                ITusFile file = await eventContext.GetFileAsync();
                await fsm.StoreTus(file,eventContext);
            }
        }
    };
    return TUSconf;
});

app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html"); ;

app.Run();
