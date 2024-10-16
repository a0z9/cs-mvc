using WebApp9_cookiee_ef.Models.Binders;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//builder.Services.AddControllersWithViews();
builder.Services.AddControllersWithViews(
    opt => opt.ModelBinderProviders.Insert(0,new RolePeopleModelBinderProvider())
    );


var app = builder.Build();

app.Environment.EnvironmentName = "Prod";

// Configure the HTTP request pipeline.
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

app.UseAuthorization();

app.MapControllerRoute(
    name: "route1",
   // pattern: "{controller=Home}/{action=Index}/{id?}/{name?}");
   pattern: "{controller=Home}/{action=Index}/{id?}/{name:minlength(3)?}");

app.MapControllerRoute(
    name: "api",
   // pattern: "{controller=Home}/{action=Index}/{id?}/{name?}");
   pattern: "api/{controller=Home}/{action=Index}/{id:int?}/{name?}");

app.Run();
