using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using N5.User.Application.Behaviours;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace N5.User.Application;

/// <summary>
/// dependency inyection
/// </summary>
[ExcludeFromCodeCoverage]
public static class DependencyInjection
{
    /// <summary>
    /// Add Application
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(Assembly.GetExecutingAssembly()); //Mediator
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(CustomManageBehaviour<,>));
        services.AddAutoMapper(Assembly.GetExecutingAssembly()); //Automapper
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly()); //Validations.

        return services;
    }
}