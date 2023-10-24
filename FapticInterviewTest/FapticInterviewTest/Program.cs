using Azure.Core;
using FapticInterviewTest.Endpoints;
using FapticInterviewTest.Services.BitStampService;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddBitstampServices();
builder.Services.AddBitfinexServices();
builder.Services.AddPriceOperationsServices();
builder.Services.AddEndpointsApiExplorer();
var app = builder.Build();
app.MapGet("/", () => "Hello World!");
app.MapPricesEndpoints();
app.UseHttpsRedirection();
app.Run();
