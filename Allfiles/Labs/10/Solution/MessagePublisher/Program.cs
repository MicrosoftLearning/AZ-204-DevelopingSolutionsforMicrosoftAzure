using System;
using System.Threading.Tasks;
using Azure.Messaging.ServiceBus;

namespace MessageProcessor
{
    class Program
    {
        private const string connectionString = "";
        private const string queueName = "messagequeue";
        static ServiceBusClient client;
        static ServiceBusSender sender;
        private const int numOfMessages = 3;

        public static async Task Main(string[] args)
        {
            client = new ServiceBusClient(connectionString);
            sender = client.CreateSender(queueName);

            using ServiceBusMessageBatch messageBatch = await sender.CreateMessageBatchAsync();

            for (int i = 1; i <= numOfMessages; i++)
            {
                if (!messageBatch.TryAddMessage(new ServiceBusMessage($"Message {i}")))
                {
                    throw new Exception($"The message {i} is too large to fit in the batch.");
                }
            }

            try
            {
                await sender.SendMessagesAsync(messageBatch);
                Console.WriteLine($"A batch of {numOfMessages} messages has been published to the queue.");
            }
            finally
            {
                await sender.DisposeAsync();
                await client.DisposeAsync();
            }
        }
    }
}