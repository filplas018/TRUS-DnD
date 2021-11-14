using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using TUSDemo.Data;
using TUSDemo.Services;
using tusdotnet;
using tusdotnet.Interfaces;
using tusdotnet.Models;
using tusdotnet.Models.Configuration;
using tusdotnet.Stores;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<FileStorageManager>();

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
    var uploadPath = Path.Combine(app.Environment.ContentRootPath, "Files");
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
                await fsm.StoreTus(file, eventContext);
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