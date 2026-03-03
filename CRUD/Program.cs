using CRUD.Interfaces;
using CRUD.Services;
using CRUD.Storage;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IProductStorage, ProductInMemoryStorage>();
builder.Services.AddScoped<IProductService, ProductService>();

builder.Services.AddControllers();

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