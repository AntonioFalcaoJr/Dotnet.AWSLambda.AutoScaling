using System;
using Amazon;
using Amazon.Runtime;

namespace Dotnet.AWSLambda.AutoScaling.Services.Credentials
{
    public class CredentialService : ICredentialService
    {
        public AWSCredentials GetCredentials()
            => new BasicAWSCredentials(GetAccessKey(), GetSecretKey());

        public RegionEndpoint GetRegionEndpoint()
            => RegionEndpoint.GetBySystemName(GetRegion());

        private static string GetAccessKey()
            => Environment.GetEnvironmentVariable("AWS_ACCESS_KEY_ID");

        private static string GetRegion()
            => Environment.GetEnvironmentVariable("AWS_DEFAULT_REGION");

        private static string GetSecretKey()
            => Environment.GetEnvironmentVariable("AWS_SECRET_ACCESS_KEY");
    }
}