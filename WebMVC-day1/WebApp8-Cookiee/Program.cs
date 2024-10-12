using WebApp8_cookiee.Models.Binders;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews(
    opt => opt.ModelBinderProviders.Insert(0,new RolePeopleModelBinderProvider())
    );


builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).
    AddCookie(opt => {
        opt.LoginPath = "/home/login";
        opt.AccessDeniedPath = "/home/denied";
        
    });

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
