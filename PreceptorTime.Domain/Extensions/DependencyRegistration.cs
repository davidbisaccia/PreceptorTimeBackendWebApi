using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using PreceptorTime.Domain.Mapper;
using PreceptorTime.Domain.Services;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace PreceptorTime.Domain.Extensions
{
    public static class DependencyRegistration
    {
        public static IServiceCollection AddMappers(this IServiceCollection services)
        {
            services
                .AddSingleton<IReportMapper, ReportMapper>()
                .AddSingleton<ITimeEntryMapper, TimeEntryMapper>()
                .AddSingleton<IUserInfoMapper, UserInfoMapper>()
                .AddSingleton<IUserMapper, UserMapper>();

            return services;
        }

        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services
                .AddScoped<Services.IAuthorizationService, AuthorizationService>()
                .AddScoped<IReportsService, ReportsService>()
                .AddScoped<ITimeEntryService, TimeEntryService>()
                .AddScoped<IUserService, UserService>();

            return services;
        }

        public static IMvcBuilder AddValidation(this IMvcBuilder builder)
        {
            builder
                .AddFluentValidation(configuration =>
                    configuration.RegisterValidatorsFromAssembly
                        (Assembly.GetExecutingAssembly()));

            return builder;
        }
    }
}
