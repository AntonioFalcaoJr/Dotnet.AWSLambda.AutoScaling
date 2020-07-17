using System.Linq;
using System.Threading.Tasks;
using Dotnet.AWSLambda.AutoScaling.Domain;
using Dotnet.AWSLambda.AutoScaling.Services;
using Newtonsoft.Json.Linq;

namespace Dotnet.AWSLambda.AutoScaling.Application.Managers
{
    public class AutoScalingManager : IAutoScalingManager
    {
        private readonly IAutoScalingService _autoScalingService;

        public AutoScalingManager(IAutoScalingService autoScalingService)
        {
            _autoScalingService = autoScalingService;
        }

        public async Task ManageAsync(JObject input)
        {
            var autoScaling = input.ToObject<Domain.AutoScaling>();
            await Task.WhenAll(autoScaling.Scalings.Select(ManageProcessesAsync));
        }

        private Task ManageProcessesAsync(Scaling scaling)
            => scaling.Suspend
                ? _autoScalingService.SuspendProcessesAsync(scaling)
                : _autoScalingService.ResumeProcessesAsync(scaling);
    }
}