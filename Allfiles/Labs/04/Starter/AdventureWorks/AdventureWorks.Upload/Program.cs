namespace AdventureWorks.Upload
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Text.Json;
    using System.IO;
    using Microsoft.Azure.Cosmos;

    public class Program
    {
        private const string EndpointUrl = "";
        private const string AuthorizationKey = "";
        private const string DatabaseName = "Retail";
        private const string ContainerName = "Online";
        private const string PartitionKey = "";
        private const string JsonFilePath = "";

        static private int amountToInsert;
        static List<Model> models;

        static async Task Main(string[] args)
        {
            try
            {
                // <CreateClient>
                CosmosClient cosmosClient = new CosmosClient(EndpointUrl, AuthorizationKey, new CosmosClientOptions() { AllowBulkExecution = true });
                // </CreateClient>

                // <Initialize>
                Console.WriteLine($"Creating a database if not already exists...");
                Database database = await cosmosClient.CreateDatabaseIfNotExistsAsync(Program.DatabaseName);

                // Configure indexing policy to exclude all attributes to maximize RU/s usage
                Console.WriteLine($"Creating a container if not already exists...");
                await database.DefineContainer(Program.ContainerName, PartitionKey)
                        .WithIndexingPolicy()
                            .WithIndexingMode(IndexingMode.Consistent)
                            .WithIncludedPaths()
                                .Attach()
                            .WithExcludedPaths()
                                .Path("/*")
                                .Attach()
                        .Attach()
                    .CreateAsync();
                // </Initialize>

                using (StreamReader reader = new StreamReader(File.OpenRead(JsonFilePath)))
                {
                    string json = await reader.ReadToEndAsync();
                    models = JsonSerializer.Deserialize<List<Model>>(json);
                    amountToInsert = models.Count;
                }

                // Prepare items for insertion
                Console.WriteLine($"Preparing {amountToInsert} items to insert...");

                // Create the list of Tasks
                Console.WriteLine($"Starting...");
                Stopwatch stopwatch = Stopwatch.StartNew();
                // <ConcurrentTasks>
                Container container = database.GetContainer(ContainerName);

                List<Task> tasks = new List<Task>(amountToInsert);
                foreach (Model model in models)
                {
                    tasks.Add(container.CreateItemAsync(model, new PartitionKey(model.Category))
                        .ContinueWith(itemResponse =>
                        {
                            if (!itemResponse.IsCompletedSuccessfully)
                            {
                                AggregateException innerExceptions = itemResponse.Exception.Flatten();
                                if (innerExceptions.InnerExceptions.FirstOrDefault(innerEx => innerEx is CosmosException) is CosmosException cosmosException)
                                {
                                    Console.WriteLine($"Received {cosmosException.StatusCode} ({cosmosException.Message}).");
                                }
                                else
                                {
                                    Console.WriteLine($"Exception {innerExceptions.InnerExceptions.FirstOrDefault()}.");
                                }
                            }
                        }));
                }

                // Wait until all are done
                await Task.WhenAll(tasks);
                // </ConcurrentTasks>
                stopwatch.Stop();

                Console.WriteLine($"Finished writing {amountToInsert} items in {stopwatch.Elapsed}.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        public class Model
        {
            public string id { get; set; }
            public string Name { get; set; }
            public string Category { get; set; }
            public string Description { get; set; }
            public string Photo { get; set; }
            public IList<Product> Products { get; set; }
        }

        public class Product
        {
            public string id { get; set; }
            public string Name { get; set; }
            public string Number { get; set; }
            public string Category { get; set; }
            public string Color { get; set; }
            public string Size { get; set; }
            public decimal? Weight { get; set; }
            public decimal ListPrice { get; set; }
            public string Photo { get; set; }
        }
    }
}
