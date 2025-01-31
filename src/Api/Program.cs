using Api;
using Api.Extensions;
using Core;
using Infrastructure.Converters;
using Microsoft.EntityFrameworkCore;

using System.Text.Json;
using System.Text.Json.Serialization;

var corsPolicyName = "DevelopCorsPolicy";
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSwagger(builder.Configuration);
builder.Services.AddServices(builder.Configuration);

builder.Services.AddControllers(o => {
    o.AllowEmptyInputInBodyModelBinding = true;    
}).AddJsonOptions(options =>
{
    options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
    options.JsonSerializerOptions.PropertyNameCaseInsensitive = false;    
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    options.JsonSerializerOptions.Converters.Add(new DateOnlyJsonConverter("yyyy.MM.dd"));
}); ;

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var origins = builder.Configuration.GetSection("CorsPolicy:Origins").Get<string[]>()!;
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: corsPolicyName,
        builder =>
        {
            builder
                .WithOrigins(origins)
                .WithMethods("GET", "POST", "PUT", "OPTIONS")
                .AllowCredentials()
                .AllowAnyHeader();
        });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

if (app.Environment.IsDevelopment())
{
    using (var scope = app.Services.CreateScope())
    {
        var db = scope.ServiceProvider.GetRequiredService<WadaDbContext>();
        db.Database.Migrate();
    }
}

if (app.Environment.IsDevelopment())
{
    app.UseCors(corsPolicyName);
}

app.UseHttpsRedirection();

app.UseExceptionHandler();

app.UseAuthorization();

app.MapControllers();

app.Run();
