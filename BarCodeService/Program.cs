using BarCodeService.DataContext;
using BarCodeService.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.WebHost.UseUrls("https://*:10602");
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var conn = builder.Configuration.GetConnectionString("Default");
builder.Services.AddDbContext<BarcodeDataContext>(opt =>
{
    opt.UseSqlServer(conn);
});

builder.Services.AddScoped<IBarcodeRepository, BarcodeRepository>();

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
