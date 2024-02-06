using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.Json;
using System.Text.Json.Serialization;
using SimpleBookCatalog.Api.Exceptions;
using SimpleBookCatalog.Application.Services;
using SimpleBookCatalog.Infrastructure.Context;
using SimpleBookCatalog.Infrastructure.Services;
using SimpleBookCatalog.Application.Repositories;
using SimpleBookCatalog.Infrastructure.Repositories;

//var builder = WebApplication.CreateSlimBuilder();
var builder = WebApplication.CreateBuilder();

// Add services to the container.

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.WebHost.UseKestrelHttpsConfiguration();

builder.Services.Configure<JsonOptions>(opt => opt.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
builder.Services.AddDbContext<SimpleBookCatalogDbContext>(opt =>
{
    //opt.UseSqlServer(builder.Configuration.GetConnectionString("SbcDB_FreeHostAsp"));
    opt.UseSqlServer(builder.Configuration.GetConnectionString("SbcDbSharkAsp"));
});

builder.Services.AddScoped<IServiceManager, ServiceManager>();
builder.Services.AddScoped<IRepositoryWrapper, RepositoryWrapper>();

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("BlazorApp", builder =>
    {
        //builder.AllowAnyHeader().AllowAnyMethod().AllowCredentials().AllowAnyOrigin();
        builder.WithOrigins("http://www.dev24.ovh").AllowAnyHeader().AllowAnyMethod().AllowCredentials();
        builder.WithOrigins("http://dev24.ovh").AllowAnyHeader().AllowAnyMethod().AllowCredentials();
        builder.WithOrigins("https://localhost:7085").AllowAnyHeader().AllowAnyMethod().AllowCredentials();
        builder.WithOrigins("http://localhost:5085").AllowAnyHeader().AllowAnyMethod().AllowCredentials();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseExceptionHandler(_ => { });

app.UseHttpsRedirection();

app.MapControllers();

app.UseCors("BlazorApp");

app.Run();
