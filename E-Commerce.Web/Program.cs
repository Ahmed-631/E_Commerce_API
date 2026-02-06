using E_Commerce.Domain.Contracts;
using E_Commerce.Infrastructure.Service;
using E_Commerce.Service.DependencyInjection;
using E_Commerce.Web.Handlers;
using ECommerce.Persistance.DependencyInjection;
using ECommerce.Persistance.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text;

internal class Program
{
    private static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddCors(options => 
        {
            options.AddPolicy("DevelopmentPolicy", builder => 
            {
                builder.AllowAnyHeader()
                .AllowAnyOrigin()
                .AllowAnyMethod();
            });

            options.AddPolicy("Production", builder => 
            {
                builder.WithHeaders("")
                .WithOrigins("")
                .WithMethods("");
            });

        });

        builder.Services.AddControllers();

        builder.Services.AddEndpointsApiExplorer();

        builder.Services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "E-Commerce API", Version = "v1" });

            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = @"JWT Authorization header using the Bearer scheme. \r\n\r\n 
                      Enter 'Bearer' [space] and then your token in the text input below.
                      \r\n\r\nExample: 'Bearer 12345abcdef'",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Scheme = "Bearer",
                BearerFormat = "JWT"
            });
        });

        builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

        builder.Services.AddPersistanceServices(builder.Configuration)
            .AddApplicationServices()
            .AddInfrastructureServices();

        builder.Services.Configure<JWTOptions>(builder.Configuration.GetSection(JWTOptions.SectionName));

        builder.Services.AddExceptionHandler<ExceptionHandler>();

        builder.Services.AddProblemDetails();

        builder.Services.Configure<ApiBehaviorOptions>(opt =>
        {
            opt.InvalidModelStateResponseFactory = actionContext =>
            {
                var error = actionContext.ModelState.Where(x => x.Value!.Errors.Any())
                .ToDictionary(x => x.Key, y => y.Value!.Errors.Select(e => e.ErrorMessage).ToArray());

                var problem = new ProblemDetails
                {
                    Title = "VAlidation Error!",
                    Detail = "One or more validation error occurs!",
                    Status = StatusCodes.Status400BadRequest,
                    Extensions = { { "errors", error } }
                };
                return new BadRequestObjectResult(problem);
            };
        });

        builder.Services.AddAuthentication(options => 
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
            .AddJwtBearer(options => 
            {
                var jwt = builder.Configuration.GetSection(JWTOptions.SectionName).Get<JWTOptions>();
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateLifetime = true,
                    ValidateAudience = true,
                    ValidateIssuer = true,
                    ValidateIssuerSigningKey = true,
                    
                    ValidAudience = jwt.Audience,
                    ValidIssuer = jwt.Issuer,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.Key))
                };
            });

        var app = builder.Build();

        using var scope = app.Services.CreateScope();

        var initializer = scope.ServiceProvider.GetRequiredService<IDbInitializer>();

        await initializer.InitializeAsync();
        await initializer.InitializeAuthDbAsync();

        ///using (var scope = app.Services.CreateScope())
        ///{
        ///    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        ///    await db.Database.MigrateAsync();
        ///}
        ///app.Use(async (context, next) =>
        ///{
        ///    try
        ///    {
        ///        await next.Invoke(context);
        ///    }
        ///    catch (Exception ex) 
        ///    {
        ///        context.Response.StatusCode = StatusCodes.Status500InternalServerError;
        ///        await context.Response.WriteAsJsonAsync(new
        ///        {
        ///            StatusCode = StatusCodes.Status500InternalServerError,
        ///            ex.Message
        ///        });
        ///    }
        ///});

        //app.UseMiddleware<GlobalExceptionHandler>();

        //app.UseCustomExceptionHandler();

        app.UseExceptionHandler();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(c => 
            {
                c.DisplayRequestDuration();
                c.EnableFilter();
            
            });
        }

        app.UseCors("DevelopmentPolicy");

        app.UseStaticFiles();

        app.UseHttpsRedirection();

        app.UseAuthentication();

        app.UseAuthorization();

        app.MapControllers();

        //app.UseResponseCaching();

        app.Run();
    }
}