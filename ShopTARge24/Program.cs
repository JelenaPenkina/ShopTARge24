using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Options;
using ShopTARge24.ApplicationServices.Services;
using ShopTARge24.Core.Domain;
using ShopTARge24.Core.ServiceInterface;
using ShopTARge24.Data;
using ShopTARge24.Hubs;

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


builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    options.SignIn.RequireConfirmedAccount = true;
    options.Password.RequiredLength = 3;

    options.Tokens.EmailConfirmationTokenProvider = "CutsomEmailConfirmation";
    options.Lockout.MaxFailedAccessAttempts = 3;
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(1);
})
    .AddEntityFrameworkStores<ShopTARge24Context>()
    .AddDefaultTokenProviders()
    .AddTokenProvider<DataProtectorTokenProvider<ApplicationUser>>("CustomEmailConfirmation");
//.AddDefaulUI();

//var configuration = builder.Configuration;

//builder.Services.AddAuthentication().AddGoogle(googleOptions =>
//{
//    googleOptions.ClientId = ""; // configuration["Authentication:Google:ClientId"];
//    googleOptions.ClientSecret = ""; // configuration["Authentication:Google:ClientSecret"];
//});

builder.Services.AddAuthentication()
    .AddGoogle(googleOptions =>
    {
        googleOptions.ClientId = builder.Configuration["Authentication:Google:ClientId"]
            ?? throw new InvalidOperationException("Google ClientId not found.");

        googleOptions.ClientSecret = builder.Configuration["Authentication:Google:ClientSecret"]
            ?? throw new InvalidOperationException("Google ClientSecret not found.");
    });


//builder.Services.AddAuthentication().AddMicrosoftAccount(microsoftOptions =>
//{
//    microsoftOptions.ClientId = builder.Configuration["Authentication:Microsoft:ClientId"];
//    microsoftOptions.ClientSecret = builder.Configuration["Authentication:Microsoft:ClientSecret"];
//});

//builder.Services.AddIdentity<Automobile.Models.Account, IdentityRole>(options =>
//{
//    options.User.RequireUniqueEmail = false;
//})
//    .AddEntityFrameworkStores<Providers.Database.EFProvider.DataContext>()
//    .AddDefaultTokenProviders();

var app = builder.Build();

// aitab vältida iga controllerisse kirjutada, et autoriseerimine vajalik
app.MapControllers().RequireAuthorization();

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

app.UseAuthentication();
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