using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Diagnostics;
using Week4Task4pt2.Infrastructure;
using Week4Task4pt2.Domain.Exceptions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddProblemDetails();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplicationServices();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Week4Task4pt2",
        Version = "v1",
        Description = "Система управления библиотекой"
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "Week4Task4pt2 API v1");
        options.RoutePrefix = "swagger";
    });
}

app.UseExceptionHandler("/error");

app.Map("/error", (HttpContext context) =>
{
    var ex = context.Features.Get<IExceptionHandlerFeature>()?.Error;

    return ex switch
    {
        Week4Task4pt2.Domain.Exceptions.ValidationException vEx => Results.Problem(
            detail: vEx.Message,
            statusCode: vEx.StatusCode,
            title: "Не удалось выполнить проверку."),

        NotFoundException nEx => Results.Problem(
            detail: nEx.Message,
            statusCode: nEx.StatusCode,
            title: "Ресурс не найден."),

        ArgumentException argEx => Results.Problem(
            detail: argEx.Message,
            statusCode: 400,
            title: "Недопустимый аргумент."),

        _ => Results.Problem(
            detail: ex?.Message ?? "Произошла непредвиденная ошибка.",
            statusCode: 500,
            title: "Внутренняя ошибка сервера.")
    };
});

app.UseHttpsRedirection();
app.MapControllers();

app.Run();