using System.Text.Json.Serialization;
using Leonardo;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<FibonacciDataContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddTransient<Fibonacci>();

var app = builder.Build();

app.MapGet("/", () => "Hello World 2!");

app.MapGet("/Fibonacci", async (Fibonacci fibonacci, ILogger<Program> logger) =>
{
    logger.LogInformation("Application Name : {ApplicationName}", "Leonardo");
    var result = await fibonacci.RunAsync(["42", "43"]);
    return Results.Json(result, FibonacciContext.Default.ListFibonacciResult);
});

app.Run();

[JsonSerializable(typeof(List<FibonacciResult>))]
internal partial class FibonacciContext : JsonSerializerContext
{
}