using System;
using System.Threading.Tasks;
using Amazon.SQS;
using Amazon.SQS.Model;
using Serilog;

namespace MessageProcessor.Features
{
    public interface IMessageQueuePublisher
    {
        Task PublishMessage<T>(T message);
    }
    
    public class MessageQueuePublisher : IMessageQueuePublisher
    {
        private readonly IAmazonSQS _sqsClient;
        private readonly ILogger _logger;
        private readonly string _queueUrl;

        public MessageQueuePublisher(IAmazonSQS sqsClient)
        {
            _sqsClient = sqsClient;
            _logger = Log.ForContext<MessageQueuePublisher>();
            _queueUrl = Environment.GetEnvironmentVariable("QUEUE_URL");
        }
        
        public async Task PublishMessage<T>(T message)
        {
            var sendMessageRequest = new SendMessageRequest
            {
                QueueUrl = _queueUrl,
                DelaySeconds = 20,
                MessageBody = message.ToString()
            };

            var sendMessageResponse = await _sqsClient.SendMessageAsync(sendMessageRequest);
            _logger.Information("Message sent, response: {@Response}", sendMessageResponse);
        }
    }
}