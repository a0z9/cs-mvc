using static System.Console;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

//app.MapGet("/", () => "-- MVC training --");

int counter = 0;
app.Run( async (ctx) => 
{
    WriteLine( $"Run starts {++counter}");
    await ctx.Response.WriteAsync("-- MVC --");

}

);

app.Run();
