using Microsoft.Azure.Cosmos;

namespace CosmosExercise
{
    // This class represents a product in the Cosmos DB container
    public class Product
    {
        public string? id { get; set; }
        public string? name { get; set; }
        public string? description { get; set; }
    }

    public class Program
    {
        // Cosmos DB account URL - replace with your actual Cosmos DB account URL
        static string cosmosDbAccountUrl = "<documentEndpoint>";

        // Cosmos DB account key - replace with your actual Cosmos DB account key
        static string accountKey = "<primaryKey>";

        // Name of the database to create or use
        static string databaseName = "myDatabase";

        // Name of the container to create or use
        static string containerName = "myContainer";

        public static async Task Main(string[] args)
        {
            // Create the Cosmos DB client using the account URL and key
            CosmosClient client = new(
                accountEndpoint: cosmosDbAccountUrl,
                authKeyOrResourceToken: accountKey
            );

            try
            {
                // Create a database if it doesn't already exist
                Microsoft.Azure.Cosmos.Database database = await client.CreateDatabaseIfNotExistsAsync(databaseName);
                Console.WriteLine($"Created or retrieved database: {database.Id}");

                // Create a container with a specified partition key
                Container container = await database.CreateContainerIfNotExistsAsync(
                    id: containerName,
                    partitionKeyPath: "/id", 
                    throughput: 400
                );
                Console.WriteLine($"Created or retrieved container: {container.Id}");

                // Define a typed item (Product) to add to the container
                Product newItem = new Product
                {
                    id = Guid.NewGuid().ToString(), // Generate a unique ID for the product
                    name = "Sample Item",
                    description = "This is a sample item in my Azure Cosmos DB exercise."
                };

                // Add the item to the container
                // The partition key ensures the item is stored in the correct partition
                ItemResponse<Product> createResponse = await container.CreateItemAsync(
                    item: newItem,
                    partitionKey: new PartitionKey(newItem.id)
                );

                // Log the ID of the created item and the request charge (RU/s) for the operation
                Console.WriteLine($"Created item: {newItem.id}");
                Console.WriteLine($"Request charge: {createResponse.RequestCharge} RUs");
            }
            catch (CosmosException ex)
            {
                // Handle Cosmos DB-specific exceptions
                // Log the status code and error message for debugging
                Console.WriteLine($"Cosmos DB Error: {ex.StatusCode} - {ex.Message}");
            }
            catch (Exception ex)
            {
                // Handle general exceptions
                // Log the error message for debugging
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}
