using Dotnet.AWSLambda.AutoScaling.Services.Credentials;
using Dotnet.AWSLambda.AutoScaling.Services.Processes;
using Microsoft.Extensions.DependencyInjection;

namespace Dotnet.AWSLambda.AutoScaling.Services.Extensions.DependencyInjection
{
    public static class ServicesDependencyInjection
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
            => services.AddScoped<IAutoScalingService, AutoScalingService>()
               .AddScoped<ICredentialService, CredentialService>()
               .AddScoped<IProcessService, ProcessService>();
    }
}