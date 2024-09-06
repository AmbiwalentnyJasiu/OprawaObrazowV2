using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.OData;
using Microsoft.AspNetCore.OData.Routing.Conventions;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;
using OprawaObrazowWebApi;
using OprawaObrazowWebApi.Models;
using OprawaObrazowWebApi.Repositories;
using OprawaObrazowWebApi.Repositories.Interfaces;
using OprawaObrazowWebApi.Services;
using OprawaObrazowWebApi.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<DatabaseContext>(opt =>
    opt.UseSqlServer(builder.Configuration.GetConnectionString("OprawaDb")));

var jwtIssuer = builder.Configuration.GetSection("Jwt:Issuer").Get<string>() ?? throw new KeyNotFoundException("JWT issuer missing in config");
var jwtKey = builder.Configuration.GetSection("Jwt:Key").Get<string>() ?? throw new KeyNotFoundException("JWT key missing in config");

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtIssuer,
            ValidAudience = jwtIssuer,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
        };
    });

//register repositories
builder.Services
    .AddScoped<IUserRepository, UserRepository>()
    .AddScoped<IBaseRepository<Client>, ClientRepository>()
    .AddScoped<IBaseRepository<Delivery>, DeliveryRepository>()
    .AddScoped<IBaseRepository<FramePiece>, FramePieceRepository>()
    .AddScoped<IBaseRepository<Frame>, FrameRepository>()
    .AddScoped<IBaseRepository<Order>, OrderRepository>()
    .AddScoped<IBaseRepository<Supplier>, SupplierRepository>();

//register services
builder.Services
    .AddScoped<IUserService, UserService>()
    .AddScoped<IBaseService<Client>, ClientService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    var db = app.Services.CreateScope().ServiceProvider.GetService<DatabaseContext>();
    db?.Database.EnsureCreated();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
