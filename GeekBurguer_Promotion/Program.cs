using GeekBurguer_Promotion_Infrastructure.Context;
using GeekBurguer_Promotion_Infrastructure.Queue;
using GeekBurguer_Promotion_Infrastructure.Queue.Interface;
using GeekBurguer_Promotion_Infrastructure.Repository.Default;
using GeekBurguer_Promotion_Infrastructure.Repository.Default.Interface;
using GeekBurguer_Promotion_Infrastructure.Repository.Promotion;
using GeekBurguer_Promotion_Infrastructure.Repository.Promotion.Interface;
using GeekBurguer_Promotion_Service.Contracts.SwaggerExclude;
using GeekBurguer_Promotion_Service.Product;
using GeekBurguer_Promotion_Service.Product.Interface;
using GeekBurguer_Promotion_Service.Promotion;
using GeekBurguer_Promotion_Service.Promotion.AutoMapper;
using GeekBurguer_Promotion_Service.Promotion.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "GeekBurger", Version = "v1" });
    c.EnableAnnotations();
    c.SchemaFilter<SwaggerExcludeFilter>();
});

builder.Services.AddCors(options => options.AddDefaultPolicy(builder =>
{
    builder.AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader();
}));

builder.Services.AddDbContext<GeekBurguerContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IServiceBus, ServiceBus>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IPromotionService, PromotionService>();
builder.Services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
builder.Services.AddScoped<IPromotionRepository, PromotionRepository>();
builder.Services.AddAutoMapper(typeof(PromotionMapper));
builder.Services.AddSingleton<IConfiguration>(builder.Configuration);

var app = builder.Build();

using var scope = app.Services.CreateScope();
PopulaDB.IncluiDadosDB(scope.ServiceProvider.GetRequiredService<GeekBurguerContext>());

app.UseCors();
app.UseSwagger();
app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "GeekBurger v1"));

app.UseHttpsRedirection();

app.MapControllers();

app.Run();