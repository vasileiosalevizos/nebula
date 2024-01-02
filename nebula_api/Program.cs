using GraphQL;
using GraphQL.MicrosoftDI;
using GraphQL.Server;
using GraphQL.Types;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(
    options =>
    {
        options.SwaggerDoc("v1", new() { Title = "Nebula API", Version = "v1", Description = "Documentation of Nebula" });
    }
);

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", builder =>
    {
        builder.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

// GraphQL services
// builder.Services.AddGraphQL(options =>
// {
//     // options.
//     // options.EnableMetrics = builder.Environment.IsDevelopment();
// })
// .AddSystemTextJson()
// .AddGraphTypes(typeof(WeatherForecastQuery));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseGraphQLPlayground();
}

app.UseHttpsRedirection();

// GraphQL middleware
app.UseGraphQL<ISchema>();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast")
.WithOpenApi();

app.MapPost("/getforecast", (WeatherForecast forecast) =>
{
    return forecast;
})
.WithName("getforecast")
.WithOpenApi();

app.Run();

// GraphQL types and query
public class WeatherForecastQuery : ObjectGraphType
{
    public WeatherForecastQuery()
    {
        Field<ListGraphType<WeatherForecastType>>(
            "weatherForecasts",
            resolve: context =>
            {
                // You can integrate your data fetching logic here
                // For demo purposes, we're returning static data
                return new List<WeatherForecast>
                {
                    new WeatherForecast(DateOnly.FromDateTime(DateTime.Now), 25, "Sunny"),
                    new WeatherForecast(DateOnly.FromDateTime(DateTime.Now.AddDays(1)), 20, "Cloudy")
                };
            }
        );
    }
}

public class WeatherForecastType : ObjectGraphType<WeatherForecast>
{
    public WeatherForecastType()
    {
        Field(x => x.Date);
        Field(x => x.TemperatureC);
        Field(x => x.Summary, nullable: true);
        Field(x => x.TemperatureF);
    }
}

// WeatherForecast record
public record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}

