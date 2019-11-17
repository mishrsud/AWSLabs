using System;
using System.Threading.Tasks;
using Amazon.Lambda.Core;
using Amazon.Lambda.Serialization.Json;
using Amazon.Lambda.SQSEvents;
using MessageProcessor.Features;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.Context;
using Serilog.Formatting.Json;

[assembly: LambdaSerializer(typeof(JsonSerializer))]
namespace MessageProcessor
{
    public class Lambda
    {
        private readonly ServiceProvider _serviceProvider;

        public Lambda()
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .Enrich.FromLogContext()
                .Enrich.WithProperty("ApplicationName", "MessageProcessor")
                .Enrich.WithProperty("Environment", GetEnvironment())
                .Enrich.WithProperty("Version", GetVersion())
                .WriteTo.Console(new JsonFormatter())
                .CreateLogger();

            var services = new ServiceCollection();
            _serviceProvider = Startup.Initialise(services);
        }

        public async Task Execute(SQSEvent sqsEvent, ILambdaContext lambdaContext)
        {
            using (LogContext.PushProperty("CorrelationId", lambdaContext.AwsRequestId))
            {
                try
                {
                    Log.Debug("{EventHandler} fired", nameof(Execute));
                    await _serviceProvider.GetRequiredService<MessageHandler>().Execute(sqsEvent);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }
        }

        private string GetEnvironment()
        {
            return Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
        }

        private string GetVersion()
        {
            return typeof(Startup).Assembly.GetName().Version.ToString();
        }
    }
}