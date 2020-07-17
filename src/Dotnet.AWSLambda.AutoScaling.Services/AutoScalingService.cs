using System.Linq;
using System.Threading.Tasks;
using Amazon.AutoScaling;
using Amazon.AutoScaling.Model;
using Dotnet.AWSLambda.AutoScaling.Domain;
using Dotnet.AWSLambda.AutoScaling.Services.Credentials;
using Dotnet.AWSLambda.AutoScaling.Services.Processes;

namespace Dotnet.AWSLambda.AutoScaling.Services
{
    public class AutoScalingService : IAutoScalingService
    {
        private readonly ICredentialService _credentialService;
        private readonly IProcessService _processService;

        public AutoScalingService(ICredentialService credentialService, IProcessService processService)
        {
            _credentialService = credentialService;
            _processService = processService;
        }

        public async Task SuspendProcessesAsync(Scaling scaling)
        {
            await SetScalingGroupAsync(scaling);
            if (scaling.Group is null) return;
            await OnSuspendProcessesAsync(scaling);
        }

        public async Task ResumeProcessesAsync(Scaling scaling)
        {
            await SetScalingGroupAsync(scaling);
            if (scaling.Group is null) return;
            await OnResumeProcessesAsync(scaling);
        }

        private static AutoScalingGroup GetAutoScalingGroupByTagDescriptionValue(DescribeAutoScalingGroupsResponse reponse, string tagValue)
            => reponse.AutoScalingGroups.FirstOrDefault(scalingGroup
                => scalingGroup.Tags.Any(tagDescription => tagDescription.Value.Equals(tagValue)));

        private AmazonAutoScalingClient GetClient()
            => new AmazonAutoScalingClient(_credentialService.GetCredentials(), _credentialService.GetRegionEndpoint());

        private async Task<DescribeAutoScalingGroupsResponse> GetDescribeAutoScalingGroupsAsync()
        {
            using var client = GetClient();
            return await client.DescribeAutoScalingGroupsAsync();
        }

        private dynamic GetProcessesRequest<T>(Scaling scaling)
            where T : AmazonAutoScalingRequest, new()
        {
            dynamic request = new T();
            request.AutoScalingGroupName = scaling.Group.AutoScalingGroupName;
            request.ScalingProcesses = scaling.Processes ?? _processService.GetProcessesName();
            return request;
        }

        private async Task OnResumeProcessesAsync(Scaling scaling)
        {
            var resumeProcessesRequest = GetProcessesRequest<ResumeProcessesRequest>(scaling);
            using var client = GetClient();
            await client.ResumeProcessesAsync(resumeProcessesRequest);
        }

        private async Task OnSuspendProcessesAsync(Scaling scaling)
        {
            var suspendProcessesRequest = GetProcessesRequest<SuspendProcessesRequest>(scaling);
            using var client = GetClient();
            await client.SuspendProcessesAsync(suspendProcessesRequest);
        }

        private async Task SetScalingGroupAsync(Scaling scaling)
        {
            var describeAutoScalingGroupsResponse = await GetDescribeAutoScalingGroupsAsync();
            scaling.Group = GetAutoScalingGroupByTagDescriptionValue(describeAutoScalingGroupsResponse, scaling.Tag);
        }
    }
}