using WebApp9_cookiee_ef.Models.Binders;
using Microsoft.AspNetCore.Authentication.Cookies;
using WebApp9_cookiee_ef.Services;
using Microsoft.EntityFrameworkCore;
using WebApp9_cookiee_ef.Entities;

var builder = WebApplication.CreateBuilder(args);


var conn = builder.Configuration.GetConnectionString("mariadb_local");

builder.Services.AddDbContextPool<UniversityDb>(
    opt => {
        opt.UseMySql(conn, ServerVersion.AutoDetect(conn));
        opt.EnableDetailedErrors();
        opt.EnableSensitiveDataLogging();
        }
    );

builder.Services.AddControllersWithViews(
    opt => opt.ModelBinderProviders.Insert(0,new RolePeopleModelBinderProvider())
    );


builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).
    AddCookie(opt => {
        opt.LoginPath = "/home/login";
        opt.AccessDeniedPath = "/home/denied";
        
    });

builder.Services.AddSingleton<iData, DataService>();
//builder.Services.AddTransient<iData, DataService>();
//builder.Services.AddScoped<iData, DataService>();
//builder.Services.Add<iData, DataService>();



builder.Services.AddAuthorization();




var app = builder.Build();

app.Environment.EnvironmentName = "Prod";

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseStatusCodePages();

app.UseStatusCodePagesWithReExecute("/home/PageError","?id={0}");

app.UseRouting();

app.MapControllerRoute(
    name: "route1",
   // pattern: "{controller=Home}/{action=Index}/{id?}/{name?}");
   pattern: "{controller=Home}/{action=Index}/{id?}/{name:minlength(3)?}");

app.MapControllerRoute(
    name: "api",
   // pattern: "{controller=Home}/{action=Index}/{id?}/{name?}");
   pattern: "api/{controller=Home}/{action=Index}/{id:int?}/{name?}");


app.UseAuthentication();
app.UseAuthorization();

app.Run();
