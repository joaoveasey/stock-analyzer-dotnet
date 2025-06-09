using DotNetEnv;
using Microsoft.OpenApi.Models;
using StockAnalyzer.WebApi.Services;
using StockAnalyzer.WebApi.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

Env.Load();

builder.Services.AddControllers();

// Add services to the container.
builder.Services.AddHttpClient<IChatGptService, ChatGptService>();
builder.Services.AddHttpClient<IDeepSeekService, DeepSeekService>();
builder.Services.AddScoped<IBrapiService, BrapiService>();
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://brapi.dev/api/") });


// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "StockAnalyzer API", Version = "v1" });
});

var app = builder.Build();

app.UseRouting();
app.MapControllers();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.Run();