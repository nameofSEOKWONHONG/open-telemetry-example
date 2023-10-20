using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddOpenTelemetry()
    .WithTracing(config =>
    {
        config.SetResourceBuilder(ResourceBuilder.CreateDefault()
                .AddService("WeatherService")
            )
            .AddSource("WeatherTracer");
            
        config.AddAspNetCoreInstrumentation();
        config.AddConsoleExporter();
        // config.AddOtlpExporter(o =>
        // {
        //     o.Endpoint = new Uri("http://localhost:4317");
        // });
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