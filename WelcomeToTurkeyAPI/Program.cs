using Microsoft.EntityFrameworkCore;
using WelcomeToTurkeyAPI.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<WTTDbContext>(option => option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddCors(option => option.AddPolicy("all", pb => { pb.AllowAnyOrigin().AllowAnyHeader(); }));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("all");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
