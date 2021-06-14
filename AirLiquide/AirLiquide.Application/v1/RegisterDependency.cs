using AirLiquide.Application.v1.ControllerMessage;
using AirLiquide.Application.v1.ControllerMessage.Validator;
using AirLiquide.Repository;
using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AirLiquide.Application.v1
{
    public static class RegisterDependency
    {
        public static void RegisterApplication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());

            services.AddTransient<IValidator<CreateCustomerRequest>, CreateCustomerRequestValidator>();
            services.AddTransient<IValidator<UpdateCustomerRequest>, UpdateCustomerRequestValidator>();
            services.AddTransient<IValidator<DeleteCustomerRequest>, DeleteCustomerRequestValidator>();
            services.AddTransient<IValidator<GetCustomerByIdRequest>, GetCustomerByIdRequestValidator>();

            services.RegisterRepository(configuration);


        }

        public static void RegisterValidators(this IMvcBuilder builder)
        {
            /*
            builder.AddFluentValidation(v =>
            {
                v.DisableDataAnnotationsValidation = false;
                v.RegisterValidatorsFromAssemblyContaining<CreateCustomerRequestValidator>();
            });
            */
            //builder.AddFluentValidation();
        }
    }
}
