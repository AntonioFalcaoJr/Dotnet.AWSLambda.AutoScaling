using System.Collections.Generic;
using Amazon.AutoScaling.Model;

namespace Dotnet.AWSLambda.AutoScaling.Domain
{
    public class Scaling
    {
        public AutoScalingGroup Group { get; set; }
        public List<string> Processes { get; set; }
        public bool Suspend { get; set; }
        public string Tag { get; set; }
    }
}