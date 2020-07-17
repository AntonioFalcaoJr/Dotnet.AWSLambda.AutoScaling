using Amazon;
using Amazon.Runtime;

namespace Dotnet.AWSLambda.AutoScaling.Services.Credentials
{
    public interface ICredentialService
    {
        AWSCredentials GetCredentials();
        RegionEndpoint GetRegionEndpoint();
    }
}