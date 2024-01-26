using Azure;
using Azure.Messaging.EventGrid;
using System;
using System.Threading.Tasks;    
public class Program
{
    private const string topicEndpoint = "";
    /* Update the topicEndpoint string constant by setting its value to the Topic
       Endpoint of the Event Grid topic that you recorded previously in this lab. */
    private const string topicKey = "";   
    /* Update the topicKey string constant by setting its value to the Key of the Event Grid topic that you recorded previously in this lab. */     
public static async Task Main(string[] args)
{   
    /* To create a new variable named "endpoint" of type "Uri", 
       using the "topicEndpoint" string constant as a constructor parameter */
    Uri endpoint = new Uri(topicEndpoint);

    /* To create a new variable named "credential" of type "AzureKeyCredential",
       use the "topicKey" string constant as a constructor parameter. */
    AzureKeyCredential credential = new AzureKeyCredential(topicKey);

    /* To create a new variable named "client" of type "EventGridPublisherClient", 
       using the "endpoint" and "credential" variables as constructor parameters */
    EventGridPublisherClient client = new EventGridPublisherClient(endpoint, credential);

    /* To create a new variable named "firstEvent" of type "EventGridEvent",
       and populate that variable with sample data */        
    EventGridEvent firstEvent = new EventGridEvent(
        subject: $"New Employee: Alba Sutton",
        eventType: "Employees.Registration.New",
        dataVersion: "1.0",
        data: new
        {
            FullName = "Alba Sutton",
            Address = "4567 Pine Avenue, Edison, WA 97202"
        }
    );

    /* To create a new variable named "secondEvent" of type "EventGridEvent",
       and populate that variable with sample data */
    EventGridEvent secondEvent = new EventGridEvent(
        subject: $"New Employee: Alexandre Doyon",
        eventType: "Employees.Registration.New",
        dataVersion: "1.0",
        data: new
        {
            FullName = "Alexandre Doyon",
            Address = "456 College Street, Bow, WA 98107"
        }
    );

    /* To asynchronously invoke the "EventGridPublisherClient.SendEventAsync"
       method using the "firstEvent" variable as a parameter */
    await client.SendEventAsync(firstEvent);
    Console.WriteLine("First event published");

    /* To asynchronously invoke the "EventGridPublisherClient.SendEventAsync"
       method using the "secondEvent" variable as a parameter */
    await client.SendEventAsync(secondEvent);
    Console.WriteLine("Second event published");
}
}