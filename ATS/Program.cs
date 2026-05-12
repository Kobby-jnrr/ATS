using ATS.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var connectionstring = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<ATSdbcontext>(options =>
    options.UseNpgsql(connectionstring));

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();
