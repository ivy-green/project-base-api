using Amazon.S3;
using Amazon.SQS;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Polly;
using Polly.Retry;
using ProjectBase.Application.Services.AuthService;
using ProjectBase.Application.Services.Helpers;
using ProjectBase.Application.Services.UserService;
using ProjectBase.Application.UnitOfWork;
using ProjectBase.Domain.Configuration;
using ProjectBase.Domain.Data;
using ProjectBase.Insfracstructure.Services.FileService;
using ProjectBase.Insfracstructure.Services.Jwts;
using ProjectBase.Insfracstructure.Services.Message.SQS;
using System.Text;

namespace ProjectBase
{
    public static class ServiceRegister
    {
        public static IServiceCollection AddServices(this IServiceCollection service)
        {
            service.AddScoped<IUnitOfWork, UnitOfWork>();

            service.AddScoped<IJwtService, JwtService>();
            service.AddScoped<IUserService, UserService>();
            service.AddScoped<IFileService, FileService>();
            service.AddScoped<IJwtService, JwtService>();
            service.AddScoped<IHashService, SHA256HashService>();
            service.AddScoped<IAuthService, AuthService>();

            service.AddScoped<ISqsMessage, SqsMessage>();

            return service;
        }
        public static IServiceCollection AddDbConnectionString(this IServiceCollection service, 
            AppSettingConfiguration setting)
        {
            RetryPolicy retryPolicy = Policy
                .Handle<Exception>()
                .WaitAndRetry(
                    retryCount: 5,
                    sleepDurationProvider: attempt => TimeSpan.FromSeconds(Math.Pow(2, attempt)),
                    onRetry: (exception, delay, retryCount, context) =>
                    {
                        // Log retry attempt
                        Console.WriteLine($"Retry {retryCount} due to: {exception}");
                    });
            service.AddDbContext<AppDBContext>(option =>
            {
                retryPolicy.Execute(() =>
                {
                    option.UseNpgsql(setting.ConnectionStrings.DefaultConnection);
                });
            });

            return service;
        }
        public static IServiceCollection AddJwtAuthenticate(this IServiceCollection service, AppSettingConfiguration setting)
        {
            service.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(options =>
                    {
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuer = false,
                            ValidateAudience = false,
                            ValidateIssuerSigningKey = true,
                            IssuerSigningKey = new SymmetricSecurityKey(
                                Encoding.UTF8.GetBytes(setting.JWTSection.SecretKey)),
                            ClockSkew = TimeSpan.Zero
                        };
                    });

            return service;
        }
        public static IServiceCollection AddAWSService(this IServiceCollection service, AppSettingConfiguration setting)
        {
            service.AddSingleton<IAmazonS3>(sp =>
            {
                var awsOptions = setting.AWSSection;
                var config = new AmazonS3Config
                {
                    ServiceURL = awsOptions.S3Url,
                    ForcePathStyle = true
                };
                return new AmazonS3Client(awsOptions.AccessKey, awsOptions.Secret, config);
            });

            service.AddSingleton<IAmazonSQS>(sp =>
            {
                var awsOptions = setting.AWSSection;
                var config = new AmazonSQSConfig
                {
                    ServiceURL = awsOptions.SQSUrl1,
                };
                return new AmazonSQSClient(awsOptions.AccessKey, awsOptions.Secret, config);
            });

            return service;
        }
    }
    
}
