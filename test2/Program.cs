using Microsoft.EntityFrameworkCore;
using test2.Data;
using test2.Controllers;
using test2.Model;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Register the DbContext with SQL Server (replace with your actual connection string)
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add other services like controllers and swagger
builder.Services.AddControllers();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Name API",
        Version = "v1"
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Name API v1");
    });
}

app.UseRouting();
app.MapControllers();
app.Run();
