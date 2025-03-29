using Infrastructure.Data.Contexts;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();

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
app.UseSwaggerUI(x => x.SwaggerEndpoint("/swagger/v1/swagger.json", "Web API"));

app.UseRewriter(new RewriteOptions().AddRedirect("^$", "swagger"));
app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors(x => x.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod());
app.MapControllers();

app.Run();
