using System;
using System.Threading.Tasks;
using Azure.Messaging.ServiceBus;
namespace MessageReader
{
    class Program
    {   
        /* The `<serviceBus-connection-string>` placeholder represents
           the connection string to the target Azure Service Bus namespace */
        static string serviceBusConnectionString = "<serviceBus-connection-string>";
        
        /* To create a string constant named "queueName" with a value
           of "messagequeue", matching the name of the Service Bus queue.*/
        static string queueName = "messagequeue";
        static ServiceBusClient client= default;

        /* Create a ServiceBusProcessor that will be used to process messages from the queue */
        static ServiceBusProcessor processor = default;


        static async Task MessageHandler(ProcessMessageEventArgs args)
        {   
            /* To create a static async "MessageHandler" task that displays 
               the body of messages in the queue as they are being processed 
               and deletes them after the processing completes */

            string body = args.Message.Body.ToString();
            Console.WriteLine($"Received: {body}");
            await args.CompleteMessageAsync(args.Message);
        }
        static Task ErrorHandler(ProcessErrorEventArgs args)
        {   
            /* To create a static async "ErrorHandler" task that manages 
               any exceptions encountered during message processing */
            Console.WriteLine(args.Exception.ToString());
            return Task.CompletedTask;
        }

        static async Task Main()
        {   
            /* To initialize "client" of type "ServiceBusClient" that will provide 
               connectivity to the Service Bus namespace and "processor" that will
               be responsible for processing of messages */
            client = new ServiceBusClient(serviceBusConnectionString);
            processor = client.CreateProcessor(queueName, new ServiceBusProcessorOptions());
            try
            {   
                /* To create a try block, which first implements a message and error
                    processing handler, initiates message processing, and stops
                    processing following a user input */
                processor.ProcessMessageAsync += MessageHandler;
                processor.ProcessErrorAsync += ErrorHandler;

                await processor.StartProcessingAsync();
                Console.WriteLine("Wait for a minute and then press any key to end the processing");
                Console.ReadKey();

                Console.WriteLine("\nStopping the receiver...");
                await processor.StopProcessingAsync();
                Console.WriteLine("Stopped receiving messages");
            }
            finally
            {
               /* To create a finally block that asynchronously disposes of the "processor"
                   and "client" objects, releasing any network and unmanaged resources */
                await processor.DisposeAsync();
                await client.DisposeAsync();
            }
        }
    }
}