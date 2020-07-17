using System.Threading.Tasks;
using Dotnet.AWSLambda.AutoScaling.Domain;

namespace Dotnet.AWSLambda.AutoScaling.Services
{
    public interface IAutoScalingService
    {
        Task ResumeProcessesAsync(Scaling scaling);
        Task SuspendProcessesAsync(Scaling scaling);
    }
}