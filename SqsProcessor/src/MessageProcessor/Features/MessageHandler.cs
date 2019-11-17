using System.Linq;
using System.Threading.Tasks;
using Amazon.Lambda.SQSEvents;
using Serilog;

namespace MessageProcessor.Features
{
    public class MessageHandler
    {
        private readonly IMessageQueuePublisher _messageQueuePublisher;
        private readonly ILogger _logger;

        public MessageHandler(IMessageQueuePublisher messageQueuePublisher)
        {
            _messageQueuePublisher = messageQueuePublisher;
            _logger = Log.ForContext<MessageHandler>();
        }
        
        public async Task Execute(SQSEvent sqsEvent)
        {
            var processMessageTasks = sqsEvent.Records.Select(ProcessMessage);
            await Task.WhenAll(processMessageTasks);
            _logger.Information("Processed SQS Event");
        }

        private async Task ProcessMessage(SQSEvent.SQSMessage message)
        {
            _logger.Information("Processing {MessageBody}", message.Body);
            await _messageQueuePublisher.PublishMessage(message.Body);
            _logger.Information("{Method} DONE", nameof(ProcessMessage));
        }
    }
}