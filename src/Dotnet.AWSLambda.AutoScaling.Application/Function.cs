using System.Threading.Tasks;
using Amazon.Lambda.Core;
using Amazon.Lambda.Serialization.Json;
using Dotnet.AWSLambda.AutoScaling.Application.Extensions.DependencyInjection;
using Dotnet.AWSLambda.AutoScaling.Application.Managers;
using Dotnet.AWSLambda.AutoScaling.Services.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Linq;

[assembly: LambdaSerializer(typeof(JsonSerializer))]

namespace Dotnet.AWSLambda.AutoScaling.Application
{
    public static class Function
    {
        public static async Task HandleAsync(JObject input)
        {
            dynamic json = input;
            input = json.body is null
                ? input : JObject.Parse(json.body.ToString());

            await new ServiceCollection()
               .AddManager()
               .AddServices()
               .BuildServiceProvider()
               .GetService<IAutoScalingManager>()
               .ManageAsync(input);
        }
    }
}