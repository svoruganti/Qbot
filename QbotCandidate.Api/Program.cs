using Microsoft.OpenApi.Models;
using QbotCandidate.Api.Feature.Booking;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "Candidate Booking" });
});
builder.Services.AddTransient<IBookingClient, BookingClient>();
builder.Services.AddHttpClient();
builder.Services.Configure<BookingClientSettings>(c => c.Url = builder.Configuration["BookingApiBaseUrl"]);
var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();
app.Run();