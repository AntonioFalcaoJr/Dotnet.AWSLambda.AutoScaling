using Dotnet.AWSLambda.AutoScaling.Application.Managers;
using Microsoft.Extensions.DependencyInjection;

namespace Dotnet.AWSLambda.AutoScaling.Application.Extensions.DependencyInjection
{
    public static class ApplicationDependencyInjection
    {
        public static IServiceCollection AddManager(this IServiceCollection services)
            => services.AddScoped<IAutoScalingManager, AutoScalingManager>();
    }
}