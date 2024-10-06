using Microsoft.EntityFrameworkCore;
using SearchService.DataContext;
using SearchService.Repository;
using SearchService.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.WebHost.UseUrls("https://*:10603");

var conn = builder.Configuration.GetConnectionString("Default");
builder.Services.AddDbContext<BarcodeContext>(opt =>
{
    opt.UseSqlServer(conn);
});

builder.Services.AddScoped<ISearchRepository, SearchRepository>();
builder.Services.AddScoped<IProductService, ProductService>();

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
