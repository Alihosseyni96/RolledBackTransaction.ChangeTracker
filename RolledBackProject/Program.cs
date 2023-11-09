using Microsoft.EntityFrameworkCore;
using RolledBackProject.Context;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<RolledBackContext>(x =>
{
    x.UseSqlServer("Data Source=.;Initial Catalog=RolledBackContext;Integrated Security=True;TrustServerCertificate=true");
});
builder.Services.AddScoped<RolledBackContext>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
