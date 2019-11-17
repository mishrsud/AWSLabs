using System;
using System.Threading.Tasks;
using Amazon.Lambda.Core;
using Amazon.Lambda.SQSEvents;

namespace MessageProcessor.Tests
{
    public class MockLambdaHost : IDisposable
    {
        public async Task Execute(SQSEvent sqsEvent)
        {
            Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "Development");
            var lambda = new Lambda();
            await lambda.Execute(sqsEvent, new MockLambdaContext());
        }
        
        private class MockLambdaContext : ILambdaContext
        {
            public string AwsRequestId => Guid.NewGuid().ToString();
            public IClientContext ClientContext { get; }
            public string FunctionName { get; }
            public string FunctionVersion { get; }
            public ICognitoIdentity Identity { get; }
            public string InvokedFunctionArn { get; }
            public ILambdaLogger Logger { get; }
            public string LogGroupName { get; }
            public string LogStreamName { get; }
            public int MemoryLimitInMB { get; }
            public TimeSpan RemainingTime { get; }
        }
        
        public void Dispose()
        {
        }
    }
}