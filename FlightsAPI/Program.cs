using Microsoft.EntityFrameworkCore;
using FlightsAPI.Models;

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var connectionString = builder.Configuration.GetConnectionString("Flights");
builder.Services.AddDbContextPool<FlightsContext>(option =>
option.UseSqlServer(connectionString)
);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.WithOrigins("*");
                          policy.AllowAnyMethod();
                          policy.AllowAnyOrigin();
                          policy.AllowAnyHeader();
                      });
});
builder.Services.AddControllers(
options => options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true);
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors(MyAllowSpecificOrigins);
app.UseAuthorization();

app.MapControllers();

app.Run();
