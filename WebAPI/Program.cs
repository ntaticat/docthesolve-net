using System.Reflection;
using System.Text;
using Application.Hubs;
using Application.Queries;
using Application.Queries.Ticket;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Persistence;

const string allowClientPolicy = "_allow_frontend_vue";

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DocTheSolveNetContext>();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters
{
    ValidateIssuer = false,
    ValidateAudience = false,
    ValidateLifetime = true,
    ValidateIssuerSigningKey = true,
    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWTKey"])),
    ClockSkew = TimeSpan.Zero
});

var identityBuilder = builder.Services.AddIdentityCore<Domain.ApplicationUser>();
new IdentityBuilder(identityBuilder.UserType, identityBuilder.Services)
    .AddEntityFrameworkStores<DocTheSolveNetContext>()
    .AddSignInManager<SignInManager<Domain.ApplicationUser>>()
    .AddDefaultTokenProviders();

builder.Services.AddSignalR();
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: allowClientPolicy, policy =>
    {
        policy.WithOrigins("https://localhost:8080")
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
    
        policy.WithOrigins("http://localhost:8080")
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
            
        policy.WithOrigins("https://localhost:4200")
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
    
        policy.WithOrigins("http://localhost:4200")
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
    });
});
builder.Services.AddMediatR(typeof(GetTicketsQuery.Handler).GetTypeInfo().Assembly);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.UseCors(allowClientPolicy);
app.MapHub<IncidenceHub>("/incidenceHub");

app.Run();
