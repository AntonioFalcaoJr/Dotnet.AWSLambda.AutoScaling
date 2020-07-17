using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Dotnet.AWSLambda.AutoScaling.Application.Managers
{
    internal interface IAutoScalingManager
    {
        Task ManageAsync(JObject input);
    }
}