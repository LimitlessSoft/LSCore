﻿using Lamar;
using LSCore.Domain.Middlewares;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

namespace LSCore.Framework
{
    public class LSCoreBaseApiStartup : LSCoreBaseStartup
    {
        private readonly bool _addAuthentication;
        private readonly bool _useCustomAuthorizationPolicy;
        private readonly bool _apiKeyAuthentication;

        public Func<IApplicationBuilder, IApplicationBuilder>? AfterAuthenticationMiddleware { get; set; } = null;

        public LSCoreBaseApiStartup(string projectName, bool addAuthentication = true, bool useCustomAuthorizationPolicy = false, bool apiKeyAuthentication = false) : base(projectName)
        {
            _addAuthentication = addAuthentication;
            _useCustomAuthorizationPolicy = useCustomAuthorizationPolicy;
            _apiKeyAuthentication = apiKeyAuthentication;
        }

        public override void ConfigureServices(IServiceCollection services)
        {
            base.ConfigureServices(services);
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen((options) =>
            {
                if (_addAuthentication)
                {
                    var jwtSecurityScheme = new OpenApiSecurityScheme
                    {
                        BearerFormat = "JWT",
                        Name = "JWT Authentication",
                        In = ParameterLocation.Header,
                        Type = SecuritySchemeType.Http,
                        Scheme = JwtBearerDefaults.AuthenticationScheme,
                        Description = "Put **_ONLY_** your JWT Bearer token on textbox below!",

                        Reference = new OpenApiReference
                        {
                            Id = JwtBearerDefaults.AuthenticationScheme,
                            Type = ReferenceType.SecurityScheme
                        }
                    };

                    options.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);

                    options.AddSecurityRequirement(new OpenApiSecurityRequirement
                    {
                        { jwtSecurityScheme, Array.Empty<string>() }
                    });

                }
            });
            services.AddControllers();
            if (_addAuthentication)
            {
                services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidIssuer = ConfigurationRoot["JWT_ISSUER"],
                        ValidAudience = ConfigurationRoot["JWT_AUDIENCE"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(ConfigurationRoot["JWT_KEY"]!)),
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = false,
                        ValidateIssuerSigningKey = true
                    };
                });

                if(!_useCustomAuthorizationPolicy)
                    services.AddAuthorization();
            }
            services.AddHttpLogging(logging =>
            {
                logging.LoggingFields = Microsoft.AspNetCore.HttpLogging.HttpLoggingFields.All;
                logging.RequestBodyLogLimit = 4096;
                logging.ResponseBodyLogLimit = 4096;
            });
        }

        public override void ConfigureContainer(ServiceRegistry services)
        {
            base.ConfigureContainer(services);
        }

        public override void Configure(IApplicationBuilder applicationBuilder, IServiceProvider serviceProvider)
        {
            if (!_addAuthentication && AfterAuthenticationMiddleware != null)
                throw new Exception($"You must enable authentication if you want to use {nameof(AfterAuthenticationMiddleware)}!");
            
            base.Configure(applicationBuilder, serviceProvider);

            applicationBuilder.UseHttpLogging();

            applicationBuilder.UseRouting();

            applicationBuilder.UseSwagger();
            applicationBuilder.UseSwaggerUI();

            //applicationBuilder.UseHttpsRedirection();
            if (_apiKeyAuthentication)
                applicationBuilder.UseMiddleware<LSCoreApiKeyAuthorizationMiddleware>();

            if (_addAuthentication)
            {
                applicationBuilder.UseAuthentication();
                applicationBuilder.UseAuthorization();

                if(AfterAuthenticationMiddleware != null)
                    AfterAuthenticationMiddleware(applicationBuilder);

            }

            applicationBuilder.UseEndpoints((routes) =>
            {
                routes.MapControllers();
            });
        }
    }
}
