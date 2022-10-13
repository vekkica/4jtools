using _4JTools.AutoMapperMapping;
using _4JTools.Exceptions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.Configure<AutoMapperOptions>(builder.Configuration.GetSection("AutoMapperOptions"));

var app = builder.Build();

app.UseExceptionHandling();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var weatherForecasts = new List<WeatherForecast>()
{
    new WeatherForecast
    {
        Id = 1,
        Description = "Freezing"
    },
    new WeatherForecast
    {
        Id = 2,
        Description = "Bracing"
    },
        new WeatherForecast
    {
        Id = 3,
        Description = "Chilly"
    },
    new WeatherForecast
    {
        Id = 4,
        Description = "Cool"
    },
        new WeatherForecast
    {
        Id = 5,
        Description = "Mild"
    },
    new WeatherForecast
    {
        Id = 6,
        Description = "Warm"
    }
};

app.MapGet("/weatherforecast", () =>
{
    return weatherForecasts;
})
.WithName("GetWeatherForecast");

app.MapGet("/weatherforecast/{id}", (int id) =>
{
    var forecast = weatherForecasts.FirstOrDefault(x => x.Id == id);

    if (forecast is null)
    {
        throw new EntityNotFoundException();
    }

    return forecast;
})
.WithName("GetWeatherForecastById");

app.Run();

internal record WeatherForecast()
{
    public int Id { get; set; }
    public string Description { get; set; } = string.Empty;
}

internal static class ApplicationBuilderExtensions
{
    public static IApplicationBuilder UseExceptionHandling(this IApplicationBuilder app)
    {
        app.UseExceptionHandler(new ExceptionHandlerOptions
        {
            AllowStatusCode404Response = true,
            ExceptionHandler = c => GlobalExceptionHandling.HandleAsync(c)
        });
        return app;
    }
}