using ContactManager.Application.Validators;
using ContactManager.Infrastructure;
using ContactManager.Application;
using FluentValidation;
using FluentValidation.AspNetCore;
using DotNetEnv.Configuration;


DotNetEnv.Env.Load();
var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddDotNetEnv();

builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddInfrastructureServices(builder.Configuration);


builder.Services.AddControllers();
builder.Services.AddFluentValidationAutoValidation()
    .AddValidatorsFromAssemblyContaining<AuthLoginRequestValidator>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();






var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
