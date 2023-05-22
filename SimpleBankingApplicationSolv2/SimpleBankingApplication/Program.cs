using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.InMemory;

using SimpleBankingApplication.Models;
using SimpleBankingApplication.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddDbContext<DataContext>(options =>
    options.UseInMemoryDatabase("InMemoryDatabase"));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Seed the in-memory database
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    //var dbContext = services.GetRequiredService<DataContext>();

}

app.MapControllers();

app.Run();
