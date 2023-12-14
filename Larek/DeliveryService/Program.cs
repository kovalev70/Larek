using DeliveryService.Data;
using DeliveryService.Interfaces;
using DeliveryService.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var optionsBuilder = new DbContextOptionsBuilder<DeliveryContext>();

builder.Services.AddControllers();
builder.Services.AddHttpClient();

builder.Services.AddScoped<ICatalogService, CatalogService>();
builder.Services.AddHttpClient<ICatalogService, CatalogService>(
			client => client.BaseAddress = new Uri(builder.Configuration["CatalogUrl"] ?? string.Empty));

builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddHttpClient<IOrderService, OrderService>(
			client => client.BaseAddress = new Uri(builder.Configuration["OrderUrl"] ?? string.Empty));

builder.Services.AddDbContext<DeliveryContext>();
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
