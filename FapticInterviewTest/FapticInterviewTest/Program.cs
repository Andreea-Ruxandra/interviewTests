using Azure.Core;
using FapticInterviewTest.Endpoints;
using FapticInterviewTest.Models;
using FapticInterviewTest.Services.BitStampService;
using Microsoft.EntityFrameworkCore;
using System;
using System.Runtime.CompilerServices;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("PricesDb");
builder.Services.AddBitstampServices();
builder.Services.AddBitfinexServices();
builder.Services.AddPriceOperationsServices();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddDbContext<PriceDbContext>(options =>
                                              options.UseSqlServer(builder.Configuration.GetConnectionString(connectionString), sqlServerOptionsAction: y => { y.EnableRetryOnFailure(); })) ;
var app = builder.Build();
app.MapGet("/", () => "Hello World!");
app.MapPricesEndpoints();
app.UseHttpsRedirection();
app.Run();
