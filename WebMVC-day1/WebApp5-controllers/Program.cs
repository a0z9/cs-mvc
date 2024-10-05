var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
//builder.Services.AddControllersWithViews();

var app = builder.Build();

//app.MapGet("/", () => "Hello World!");


app.MapControllerRoute("default",
    "{controller=Home}/{action=Index}"
    );


app.Run();
