using ShopTARge24.Data;
using Microsoft.EntityFrameworkCore;
using ShopTARge24.Core.ServiceInterface;
using ShopTARge24.ApplicationServices.Services;
using Microsoft.Extensions.FileProviders;
using ShopTARge24.Hubs;
using Microsoft.AspNetCore.Identity;
using ShopTARge24.Core.Domain;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
// Add SignalR in our project
builder.Services.AddSignalR();

builder.Services.AddScoped<ISpaceshipServices, SpaceshipServices>();
builder.Services.AddScoped<IKindergartenServices, KindergartenServices>();
builder.Services.AddScoped<IFileServices, FileServices>();
builder.Services.AddScoped<IRealEstateServices, RealEstateServices>();
builder.Services.AddScoped<IWeatherForecastServices, WeatherForecastServices>();
builder.Services.AddScoped<OpenWeatherServices>();
builder.Services.AddScoped<IEmailServices, EmailServices>();

builder.Services.AddHttpClient();
builder.Services.AddHttpClient<IChuckNorrisServices, ChuckNorrisServices>();
builder.Services.AddHttpClient<ICocktailServices, CocktailServices>();

builder.Services.AddDbContext<ShopTARge24Context>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services.AddIdentity<ApplicationUser, IdentityRole>();

//builder.Services.AddIdentity<Automobile.Models.Account, IdentityRole>(options =>
//{
//    options.User.RequireUniqueEmail = false;
//})
//    .AddEntityFrameworkStores<Providers.Database.EFProvider.DataContext>()
//    .AddDefaultTokenProviders();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
        Path.Combine(builder.Environment.ContentRootPath, "multipleFileUpload")),
    RequestPath = "/multipleFileUpload"
});

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.MapHub<UserHub>("/hubs/userCount");
app.MapHub<ChatHub>("/hubs/chat");

app.Run();