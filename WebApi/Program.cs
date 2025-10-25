using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Diagnostics;
using Week4Task4pt2.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

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
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/error");
}

app.UseHttpsRedirection();
app.MapControllers();

app.Map("/error", (HttpContext context, ILogger<Program> logger) =>
{
    var exceptionHandlerFeature = context.Features.Get<IExceptionHandlerPathFeature>();
    var exception = exceptionHandlerFeature?.Error;
    var path = exceptionHandlerFeature?.Path ?? "Неизвестный путь";

    logger.LogError(exception, "Необрабатываемое исключение возникло на {Path}", path);

    return Results.Problem(
            title: "Непредвиденная ошибка.",
            detail: "Повторите попытку позже или свяжитесь с поддержкой.",
            statusCode: 500,
            type: "https://httpstatuses.com/500"
        );
});
app.Run();