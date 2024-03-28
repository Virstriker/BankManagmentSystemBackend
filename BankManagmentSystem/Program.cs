using BankManagmentSystem.Business_Logic;
using BankManagmentSystem.Business_Logic.Interfaces;
using BankManagmentSystem.Models;
using DocumentFormat.OpenXml.Drawing.Diagrams;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Description = "Standard Authorization header using th Bearer schema (\"Bearer {token}\")",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    }) ;
    options.OperationFilter<SecurityRequirementsOperationFilter>();
});
builder.Services.AddAutoMapper(typeof(Program).Assembly);
builder.Services.AddTransient<IBankAccountManager, BankAccountManager>();
builder.Services.AddTransient<ITransactionManager, TransactionManager>();
builder.Services.AddTransient<IEmployeeManager, EmployeeManager>();
builder.Services.AddTransient<IAccountManager, AccountManager>();
builder.Services.AddTransient<ILoginManager, LoginManager>();
builder.Services.AddTransient<IGetIntrest, CurrentManager>();
builder.Services.AddTransient<IGetIntrest, SavingsManager>();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
    });
builder.Services.AddDbContext<BankingSystemContext>(options =>
{
    options.UseSqlServer("Server = (localdb)\\MSSQLLocalDB; Database = BankingSystem; Trusted_Connection = True;");
    options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
});
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAnyOrigin",
        builder => builder.AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader());
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAnyOrigin");
app.MapControllers();
app.UseAuthentication();
app.UseAuthorization();

app.Run();
