using LanchoneteApi.Pedidos.Application;
using LanchoneteApi.Pedidos.Application.Mappings;
using LanchoneteApi.Pedidos.Infrastructure.Messaging;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// Services
builder.Services.AddScoped<PedidoService, PedidoService>();
builder.Services.AddScoped<ProcessamentoPedidoService, ProcessamentoPedidoService>();
builder.Services.AddScoped<ConsumoPedidoService, ConsumoPedidoService>();

// Cache 
builder.Services.AddMemoryCache();

// AutoMapper
//builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddAutoMapper(typeof(ServiceMapping));

//Swagger 
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.EnableAnnotations();
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
