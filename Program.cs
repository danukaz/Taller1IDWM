using System.Security.Claims;
using System.Text;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

using Serilog;

using Taller.Src.Data;
using Taller.Src.Helpers;
using Taller.Src.Interfaces;
using Taller.Src.Middlewares;
using Taller.Src.Models;
using Taller.Src.Repositories;
using Taller.Src.Services;

Log.Logger = new LoggerConfiguration()

    .CreateLogger();
try
{
    Log.Information("starting server.");

    var builder = WebApplication.CreateBuilder(args);
    builder.Services.AddControllers();
    builder.Services.AddDbContext<StoreContext>(options =>
        options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

    builder.Services.AddTransient<ExceptionMiddleware>();
    builder.Services.AddScoped<IProductRepository, ProductRepository>();
    builder.Services.AddScoped<IUserRepository, UserRepository>();
    builder.Services.AddScoped<ITokenServices, TokenService>();
    builder.Services.AddScoped<IShippingAddressRepository, ShippingAddressRepository>();
    builder.Services.AddScoped<IOrderRepository, OrderRepository>();
    builder.Services.AddScoped<IBasketRepository, BasketRepository>();
    builder.Services.AddScoped<IPhotoService, PhotoService>();
    builder.Services.AddScoped<UnitOfWork>();
    builder.Services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(options =>
    {
        options.SerializerOptions.Converters.Add(new DateOnlyJsonConverter());
    });
    builder.Services.AddIdentity<User, IdentityRole>(opt =>
    {
        opt.User.RequireUniqueEmail = true;
        opt.Password.RequireNonAlphanumeric = false;
        opt.Password.RequiredLength = 6;
        opt.SignIn.RequireConfirmedEmail = false;
    })
    .AddEntityFrameworkStores<StoreContext>();
    builder.Services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        })
    .AddJwtBearer(opt =>
    {
        opt.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidateAudience = true,
            ValidAudience = builder.Configuration["Jwt:Audience"],
            ValidateIssuerSigningKey = true,
            ValidateLifetime = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:SignInKey"]!)),
            RoleClaimType = ClaimTypes.Role
        };
    });

    builder.Host.UseSerilog((context, services, configuration) => configuration
            .ReadFrom.Configuration(context.Configuration)
            .Enrich.FromLogContext()
            .Enrich.WithThreadId()
            .Enrich.WithMachineName());


    var corsSettings = builder.Configuration.GetSection("CorsSettings");
    var origins = corsSettings["AllowedOrigins"]?.Split(';')
              ?? ["http://localhost:3000"];
    var methods = corsSettings["AllowedMethods"]?.Split(',')
              ?? ["GET", "POST", "PUT", "DELETE", "PATCH", "OPTIONS"];

    builder.Services.AddCors(options =>
    {
        options.AddPolicy("EcommercePolicy", policy =>
        {
            policy.WithOrigins(origins)
                .WithMethods(methods)
                .WithHeaders("Content-Type", "Authorization")
                .AllowCredentials();
        });
    });


    var app = builder.Build();
    app.UseMiddleware<ExceptionMiddleware>();
    app.UseRouting();
    app.UseCors("EcommercePolicy");
    app.UseAuthentication();
    app.UseAuthorization();
    await DbInitializer.InitDb(app);
    app.MapControllers();
    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "server terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}