using Infrastructure.Data.Contexts;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Presentation.Extensions.Middlewares;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen(c =>
{
    //Tagit hjälp av AI för att sätta upp API key Authentication för swagger
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Web API", Version = "v1" });
    c.AddSecurityDefinition("ApiKey", new OpenApiSecurityScheme
    {
        Description = "API Key authentication",
        Name = "X-API-KEY",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "ApiKey"
                }
            },
            Array.Empty<string>()
        }
    });
});

builder.Services.AddDbContext<DataContext>(x => x.UseSqlServer(builder.Configuration.GetConnectionString("AlphaDB")));
builder.Services.AddScoped<IClientRepository, ClientRepository>();
builder.Services.AddScoped<IStatusRepository, StatusRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IProjectRepository, ProjectRepository>();

builder.Services.AddScoped<IClientService, ClientService>();
builder.Services.AddScoped<IStatusService, StatusService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IProjectService, ProjectService>();



var app = builder.Build();
app.MapOpenApi();
app.UseSwagger();
app.UseSwaggerUI(x =>
{
    x.SwaggerEndpoint("/swagger/v1/swagger.json", "Web API");

    x.ConfigObject.AdditionalItems["persistAuthorization"] = "true";
});

app.UseRewriter(new RewriteOptions().AddRedirect("^$", "swagger"));
app.UseHttpsRedirection();

app.UseCors(x => x.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod());
app.UseMiddleware<DefaultApiKeyMiddleware>();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
