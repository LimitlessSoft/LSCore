﻿using FluentValidation;
using Lamar;
using LSCore.Contracts;
using LSCore.Contracts.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace TD.Core.Framework
{
    public class LSCoreBaseStartup : ILSCoreBaseStartup
    {
        public static IContainer? Container { get; set; }
        public IConfigurationRoot ConfigurationRoot { get; set; }
        public string ProjectName { get; set; }

        public LSCoreBaseStartup(string projectName)
        {
            ProjectName = projectName;

            var builder = new ConfigurationBuilder();

            builder.AddJsonFile("appsettings.json", true);

            ConfigurationRoot = builder.Build();
        }

        public virtual void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton(provider => ConfigurationRoot);
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        }

        public virtual void ConfigureContainer(ServiceRegistry services)
        {
            services.Scan(s =>
            {
                s.AssembliesAndExecutablesFromApplicationBaseDirectory(x =>
                    x.GetName().Name.StartsWith(ProjectName) ||
                    x.GetName().Name.StartsWith("TD.Core")
                );
                s.TheCallingAssembly();
                s.WithDefaultConventions();
                s.ConnectImplementationsToTypesClosing(typeof(IValidator<>));
                s.ConnectImplementationsToTypesClosing(typeof(ILSCoreMap<,>));
                s.ConnectImplementationsToTypesClosing(typeof(ILSCoreDtoMapper<,>));
            });

            ConfigureIoC(services);
        }

        public virtual void ConfigureIoC(ServiceRegistry services)
        {
            LSCore.Domain.LSCoreDomainConstants.Container = new Container(services);
        }

        public virtual void Configure(IApplicationBuilder applicationBuilder, IServiceProvider serviceProvider)
        {

        }
    }
}
