using Microsoft.EntityFrameworkCore;
using NZWalks_API.Data;
using NZWalks_API.Mappings;
using NZWalks_API.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//Injected The Database Context
builder.Services.AddDbContext<NZ_Walks_DB_Context>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("NZWalksDBConnectionString")));
// Injected Repository Pattern 
builder.Services.AddScoped<IRegionRepository, SQLRegionRepository>();
builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

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
