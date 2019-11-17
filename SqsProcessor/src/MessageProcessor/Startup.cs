using Amazon.Extensions.NETCore.Setup;
using Amazon.SQS;
using MessageProcessor.Features;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace MessageProcessor
{
    public class Startup
    {
        public static ServiceProvider Initialise(IServiceCollection services)
        {
            services.AddOptions();
            services.AddSingleton(Log.Logger);
            services.AddDefaultAWSOptions(new AWSOptions());
            services.AddAWSService<IAmazonSQS>();

            services.AddSingleton<IMessageQueuePublisher, MessageQueuePublisher>();
            services.AddSingleton<MessageHandler>();

            return services.BuildServiceProvider();
        }
    }
}