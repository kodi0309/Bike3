using Srv_Config;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Configure MongoDB
var connectionString = builder.Configuration.GetConnectionString("MongoDbConnection");
if (string.IsNullOrEmpty(connectionString))
{
    throw new InvalidOperationException("MongoDB connection string is not configured.");
}

await DbInitializer.InitDbAsync(connectionString);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Urls.Add("http://localhost:7003");

app.Run();
