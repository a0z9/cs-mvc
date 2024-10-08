var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

//app.Environment.EnvironmentName = "test";

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
   pattern: "api/{controller=Home}/{action=Index}/{i:int?}/{name?}");

app.Run();
