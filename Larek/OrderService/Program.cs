using OrderService.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using OrderService.Interfaces;
using OrderService.Services;

var builder = WebApplication.CreateBuilder(args);

var optionsBuilder = new DbContextOptionsBuilder<OrderContext>();

builder.Services.AddControllers();
builder.Services.AddHttpClient();
builder.Services.AddScoped<ICatalogService, CatalogService>();
builder.Services.AddHttpClient<ICatalogService, CatalogService>(
			client => client.BaseAddress = new Uri(builder.Configuration["CatalogUrl"] ?? string.Empty));
builder.Services.AddDbContext<OrderContext>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
