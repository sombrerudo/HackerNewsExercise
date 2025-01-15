using HackerNewsApiExercise.Models.Clients;
using HackerNewsApiExercise.Models.Clients.Contracts;
using Polly;
using Polly.Extensions.Http;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHttpClient<IHackerNewsService, HackerNewsService>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration["HackerRankBaseUrl"] ?? "https://hacker-news.firebaseio.com/v0/");
})
.AddPolicyHandler(GetRetryPolicy());

builder.Services.AddMemoryCache();

static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy() => HttpPolicyExtensions
        .HandleTransientHttpError()
        .OrResult(msg => msg.StatusCode == System.Net.HttpStatusCode.NotFound)
        .WaitAndRetryAsync(6, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));


var app = builder.Build();



// Configure the HTTP request pipeline.

app.UseSwagger();

app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

