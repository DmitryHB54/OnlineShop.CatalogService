using OnlineShop.CatalogService.Application;
using OnlineShop.CatalogService.Infrastructure;
using OnlineShop.CatalogService.WebApi.ExceptionHandling;
using OnlineShop.CatalogService.WebApi.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddProblemDetails();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>(); // глобальный обработчик исключений

builder.Services.AddAuthentication(builder.Configuration);

builder.Services.AddControllers();
builder.Services.AddSwaggerConfiguration();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseExceptionHandler(); // middleware который ловит исключения и вызывает зарегистрированные обработчики

app.UseSwaggerDocumantation(app.Environment);

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
public partial class Program() {}