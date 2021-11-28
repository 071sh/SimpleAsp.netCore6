using Application;
using Application.Interface.Setting;
using Application.Service.Setting;
using Infrastructure.IdentityConfigs;
using Infrastructure.MappingProfile.Setting;
using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddTransient<IAppSettingService, AppSettingService>();



#region ConnectionString

builder.Services.AddTransient<IDataBaseContext, DataBaseContext>();
string connection =builder.Configuration["ConnectionString:SqlServer"];
builder.Services.AddDbContext<DataBaseContext>(option => option.UseSqlServer(connection));

#endregion

builder.Services.ConfigureApplicationCookie(option =>
{
    option.ExpireTimeSpan = TimeSpan.FromMinutes(10);
    option.LoginPath = "/account/login";
    option.AccessDeniedPath = "/Account/AccessDenied";
    option.SlidingExpiration = true; // اگر کاربر فعالیت داشت 10 دقیقه تمدید میشه و الله خارج میشه
});


builder.Services.AddIdentityConfig(builder.Configuration);
builder.Services.AddAutoMapper(typeof(AppSettingProfile));


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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
