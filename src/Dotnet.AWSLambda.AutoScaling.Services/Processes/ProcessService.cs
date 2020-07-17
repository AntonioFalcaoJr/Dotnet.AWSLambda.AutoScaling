using System.Collections.Generic;
using System.Linq;
using Amazon.AutoScaling.Model;

namespace Dotnet.AWSLambda.AutoScaling.Services.Processes
{
    public class ProcessService : IProcessService
    {
        private static ProcessType LaunchProcessType => new ProcessType {ProcessName = "Launch"};
        private static ProcessType ScheduledActionsProcessType => new ProcessType {ProcessName = "ScheduledActions"};
        private static ProcessType TerminateProcessType => new ProcessType {ProcessName = "Terminate"};

        public List<string> GetProcessesName()
            => GetProcessesTypes().Select(x => x.ProcessName).ToList();

        private static IEnumerable<ProcessType> GetProcessesTypes()
            => new List<ProcessType>
            {
                LaunchProcessType,
                TerminateProcessType,
                ScheduledActionsProcessType
            };
    }
}