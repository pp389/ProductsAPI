using Microsoft.EntityFrameworkCore;
using ProductAPI.Data;
using ProductAPI.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<IProductHistoryService, ProductHistoryService>();

builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddSingleton<IForbiddenWordsService, ForbiddenWordsService>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.UseAuthorization();
app.MapControllers();

app.Run();
