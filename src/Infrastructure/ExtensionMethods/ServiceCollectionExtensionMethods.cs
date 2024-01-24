using Core.DTOs.Company;
using Core.DTOs.Employee;
using Core.DTOs.Identity;
using Core.Identity;
using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using Core.Utilities.Constants;
using FluentValidation;
using Infrastructure.Configuration;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Infrastructure.Services.Validators.AccountValidators;
using Infrastructure.Services.Validators.DtoValidators;
using Infrastructure.Services.Validators.LoginValidators;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;
using System.Text;

namespace Infrastructure.ExtensionMethods;

public static class ServiceCollectionExtensionMethods
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, WebApplicationBuilder builder)
    {
        services
            .ConfigureDatabase(builder.Configuration)
            .AddMemoryCache()
            .AddApplicationServices()
            .AddIdentityConfiguration()
            .AddJwtBearerAuthentication(builder.Configuration)
            .AddAuthorizationConfiguration()
            .AddAutoMapper(Assembly.GetExecutingAssembly())
            .ConfigureSmtpService(builder);

        return services;
    }

    static IServiceCollection ConfigureDatabase(this IServiceCollection services, ConfigurationManager configurationManager)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(configurationManager.GetConnectionString(nameof(ApplicationDbContext))));

        return services;
    }

    static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddTransient<ICompanyRepository, CompanyRepository>();
        services.AddTransient<ICompanyService, CompanyService>();

        services.AddTransient<IEmployeeRepository, EmployeeRepository>();
        services.AddTransient<IEmployeeService, EmployeeService>();

        services.AddTransient<ISuperAdminService, SuperAdminService>();
        services.AddTransient<IEmailService, EmailService>();

        services.AddTransient<IIdentityService, IdentityService>();

        services.AddTransient<IValidator<LoginDto>, LoginValidator>();
        services.AddTransient<IValidator<CreateCompanyDto>, RegisterValidator>();
        services.AddTransient<IValidator<ResetPasswordDto>, ResetPasswordValidator>();
        services.AddTransient<IValidator<CreateNewPasswordDto>, CreateNewPasswordValidator>();
        services.AddTransient<IValidator<ChangePasswordDto>, ChangePasswordValidator>();
        services.AddTransient<IValidator<ChangeEmailDto>, ChangeEmailValidator>();
        services.AddTransient<IValidator<ChangeCompanyStatusDto>, ChangeCompanyStatusValidator>();
        services.AddTransient<IValidator<CreateEmployeeDto>, CreateEmployeeValidator>();
        services.AddTransient<IValidator<Guid>, GuidValidator>();

        services.AddTransient<IJwtService, JwtService>();

        return services;
    }

    static IServiceCollection AddIdentityConfiguration(this IServiceCollection services)
    {
        services.AddIdentity<ApplicationUser, ApplicationRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

        services.Configure<IdentityOptions>(options =>
        {
            // Password settings.
            options.Password.RequireDigit = true;
            options.Password.RequireLowercase = true;
            options.Password.RequireNonAlphanumeric = true;
            options.Password.RequireUppercase = true;
            options.Password.RequiredLength = 6;
            options.Password.RequiredUniqueChars = 1;

            // Lockout settings.
            options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
            options.Lockout.MaxFailedAccessAttempts = 5;
            options.Lockout.AllowedForNewUsers = true;

            // User settings.
            options.User.AllowedUserNameCharacters =
            "abcçdefgğhıijklmnoöpqrşstüuvwxyzABCÇDEFGĞHIİJKLMNOÖPQRSŞTUÜVWXYZ0123456789-._@+ ";
            options.User.RequireUniqueEmail = true;
        });

        services.Configure<DataProtectionTokenProviderOptions>(options =>
            options.TokenLifespan = TimeSpan.FromDays(7));

        return services;
    }

    static IServiceCollection AddJwtBearerAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                configuration.CheckTokenPropertiesForNullability();

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ClockSkew = TimeSpan.Zero,
                    ValidAudience = configuration["Token:Audience"],
                    ValidIssuer = configuration["Token:Issuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Token:SecurityKey"]!))
                };
            });

        return services;
    }

    static IServiceCollection AddAuthorizationConfiguration(this IServiceCollection services)
    {
        services.AddAuthorizationCore(options =>
        {
            options.AddPolicy(CustomRoles.SuperAdmin, builder =>
            {
                builder.RequireRole(CustomRoles.SuperAdmin);
            });
            options.AddPolicy(CustomRoles.CompanyAdmin, builder =>
            {
                builder.RequireRole(CustomRoles.CompanyAdmin);
            });
            options.AddPolicy(CustomRoles.Employee, builder =>
            {
                builder.RequireRole(CustomRoles.Employee);
            });
            options.AddPolicy(CustomRoles.QuestionHost, builder =>
            {
                builder.RequireRole(CustomRoles.QuestionHost);
            });
        });
        services.AddAuthorizationPolicyEvaluator();
        return services;
    }

    static IServiceCollection ConfigureSmtpService(this IServiceCollection services, WebApplicationBuilder builder)
    {
        services.Configure<SmtpSettings>(builder.Configuration.GetSection("SmtpSettings"));
        return services;
    }
}
