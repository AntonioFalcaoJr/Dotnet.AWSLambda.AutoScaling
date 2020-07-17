using System.Collections.Generic;

namespace Dotnet.AWSLambda.AutoScaling.Services.Processes
{
    public interface IProcessService
    {
        List<string> GetProcessesName();
    }
}