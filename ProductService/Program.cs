using Microsoft.EntityFrameworkCore;
using ProductService.Data;
using ProductService.Repository;
using ProductService.Services;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.UseUrls("https://*:10611");
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(typeof(Program).Assembly);

builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductExtentionService, ProductExtentionService>();

var conn = builder.Configuration.GetConnectionString("Default");
builder.Services.AddDbContext<ProductContext>(opt =>
{
    opt.UseSqlServer(conn);
});


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
