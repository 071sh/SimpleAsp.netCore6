using Application;
using Application.Interface.Setting;
using Application.Service.Setting;
using Infrastructure.MappingProfile.Setting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Persistence.Contexts;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();



#region ConnectionString

builder.Services.AddTransient<IDataBaseContext, DataBaseContext>();
string connection = builder.Configuration["ConnectionString:SqlServer"];
builder.Services.AddDbContext<DataBaseContext>(option => option.UseSqlServer(connection));

#endregion

builder.Services.AddApiVersioning(Options =>
{
    Options.AssumeDefaultVersionWhenUnspecified = true;
    Options.DefaultApiVersion = new ApiVersion(1, 0);
    Options.ReportApiVersions = true;
});



// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

#region Swagger

builder.Services.AddSwaggerGen(s =>
{
    s.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "API.EndPoint.xml"), true);

    s.SwaggerDoc("v1", new OpenApiInfo { Title = "API.EndPoint", Version = "v1" });
    s.SwaggerDoc("v2", new OpenApiInfo { Title = "API.EndPoint", Version = "v2" });

    s.DocInclusionPredicate((doc, apiDescription) =>
    {
        if (!apiDescription.TryGetMethodInfo(out MethodInfo methodInfo)) return false;

        var version = methodInfo.DeclaringType
            .GetCustomAttributes<ApiVersionAttribute>(true)
            .SelectMany(attr => attr.Versions);

        return version.Any(v => $"v{v.ToString()}" == doc);
    });
});

#endregion

#region Mapper

builder.Services.AddAutoMapper(typeof(AppSettingProfile));

#endregion

builder.Services.AddTransient<IAppSettingService, AppSettingService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        options.SwaggerEndpoint("/swagger/v2/swagger.json", "v2");
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
