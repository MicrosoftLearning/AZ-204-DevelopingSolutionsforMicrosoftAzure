---
lab:
    title: 'Create Azure Cosmos DB resources with for NoSQL using .NET'
    description: 'Learn how to create database and container resources in Azure Cosmos DB with the Microsoft .NET SDK v3.'
---

# Create Azure Cosmos DB resources with for NoSQL using .NET

In this exercise, you create a console app that creates a container, database, and an item in Azure Cosmos DB.

Tasks performed in this exercise:

* Create an Azure Cosmos DB account
* Create the console app
* Run the console app and verify results

This exercise should take approximately **30** minutes to complete.

## Before you start

Before you begin, make sure you have the following requirements in place:

* An Azure subscription. If you don't already have one, you can [sign up for one](https://azure.microsoft.com/).

## Create an Azure Cosmos DB account

In this section of exercise you create a resource group and Azure Cosmos DB account. You also record the endpoint, and access key for the account.

1. In your browser navigate to the Azure portal [https://portal.azure.com](https://portal.azure.com); signing in with your Azure credentials if prompted.

1. Use the **[\>_]** button to the right of the search bar at the top of the page to create a new cloud shell in the Azure portal, selecting a ***Bash*** environment. The cloud shell provides a command line interface in a pane at the bottom of the Azure portal.

    > **Note**: If you have previously created a cloud shell that uses a *PowerShell* environment, switch it to ***Bash***.

1. In the cloud shell toolbar, in the **Settings** menu, select **Go to Classic version** (this is required to use the code editor).

1. Create a resource group for the resources needed for this exercise. Replace **\<myResourceGroup>** with a name you want to use for the resource group. You can replace **useast** with a region near you if needed. 

    ```
    az group create --location useast --name <myResourceGroup>
    ```

1. Run the following command to create the Azure Cosmos DB account. Replace **\<myCosmosDBacct>** with a *unique* name to identify your Azure Cosmos DB account. Replace **\<myResourceGroup>** with the name you chose earlier.

    >**Note:** The account name can only contain lowercase letters, numbers, and the hyphen (-) character. It must be between 3-31 characters in length. *This command takes a few minutes to complete.*

    ```
    az cosmosdb create -n <myCosmosDBacct> -g <myResourceGroup>
    ```

1.  Run the following command to retrieve the **documentEndpoint** for the Azure Cosmos DB account. Record the endpoint from the command results, it's needed later in the exercise. Replace **\<myCosmosDBacct>** and **\<myResourceGroup>** with the names you created.

    ```bash
    az cosmosdb show -n <myCosmosDBacct> -g <myResourceGroup> --query "documentEndpoint" --output tsv
    ```

1. Retrieve the primary key for the account by  using the following command. Record the **primaryMasterKey** from the command results for use in the code. Replace **\<myCosmosDBacct>** and **\<myResourceGroup>** with the names you created.

     ```
    # Retrieve the primary key
    az cosmosdb keys list -n <myCosmosDBacct> -g <myResourceGroup>
    ```

## Create the console application

Now that the needed resources are deployed to Azure the next step is to set up the console application using the same terminal window in Visual Studio Code.

1. Create a folder for the project and change in to the folder.

    ```bash
    mkdir cosmosdb
    cd cosmosdb
    ```

1. Create the .NET console app.

    ```dotnetcli
    dotnet new console --framework net8.0
    ```

1. Run the following commands to add the **Microsoft.Azure.Cosmos** package to the project, and also the supporting **Newtonsoft.Json** package.

    ```dotnetcli
    dotnet add package Microsoft.Azure.Cosmos --version 3.*
    dotnet add package Newtonsoft.Json --version 13.*
    ```

Now it's time to replace the template code in the **Program.cs** file using the editor in the cloud shell.

### Add the starting code for the project

1. Run the following command in the cloud shell to begin editing the application.

    ```bash
    code Program.cs
    ```

1. Replace any existing code with the following  following code snippet after the **using** statements. Be sure to replace the placeholder values for **\<documentEndpoint>** and **\<primaryKey>** following the directions in the code comments.

    The code provides the overall structure of the app, and some necessary elements. Review the comments in the code, you add code in areas specified in the instructions. 

    ```csharp
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
    
    
                try
                {
                    // Create a database if it doesn't already exist
    
    
                    // Create a container with a specified partition key
    
    
                    // Define a typed item (Product) to add to the container
    
    
                    // Add the item to the container
                    // The partition key ensures the item is stored in the correct partition
    
    
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
    ```

### Add code to create the client and perform operations 

In this section of the exercise you add code in specified areas of the projects to create the: client, database, container, and add a sample item to the container.

1. Add the following code in the space after the **// Create the Cosmos DB client using the account URL and key** comment. This code defines the client used to connect to your Azure Cosmos DB account.

    ```csharp
    CosmosClient client = new(
        accountEndpoint: cosmosDbAccountUrl,
        authKeyOrResourceToken: accountKey
    );
    ```

    >Note: It's a best practice to use the **DefaultAzureCredential** from the *Azure Identity* library. This can require some additional configuration requirments in Azure depending on how your subscription is set up. 

1. Add the following code in the space after the **// Create a database if it doesn't already exist** comment. 

    ```csharp
    Microsoft.Azure.Cosmos.Database database = await client.CreateDatabaseIfNotExistsAsync(databaseName);
    Console.WriteLine($"Created or retrieved database: {database.Id}");
    ```

1. Add the following code in the space after the **// Create a container with a specified partition key** comment. 

    ```csharp
    Microsoft.Azure.Cosmos.Database database = await client.CreateDatabaseIfNotExistsAsync(databaseName);
    Console.WriteLine($"Created or retrieved database: {database.Id}");
    ```

1. Add the following code in the space after the **// Define a typed item (Product) to add to the container** comment. This defines the item that's added to the container.

    ```csharp
    Product newItem = new Product
    {
        id = Guid.NewGuid().ToString(), // Generate a unique ID for the product
        name = "Sample Item",
        description = "This is a sample item in my Azure Cosmos DB exercise."
    };
    ```

1. Add the following code in the space after the **// Add the item to the container** comment. 

    ```csharp
    ItemResponse<Product> createResponse = await container.CreateItemAsync(
        item: newItem,
        partitionKey: new PartitionKey(newItem.id)
    );
    ```

1. Now that the code is complete, save your progress use **Ctrl + S** to save the file, and **Ctrl + Q** to exit the editor.

1. Run the following command in the cloud shell to test for any errors in the project. If you do see errors, open the *Program.cs* file in the editor and check for missing code or pasting errors.

    ```bash
    dotnet build
    ```

Now that the project is finished it's time to run the application and verify the results in the Azure portal.

## Run the application and verify results

1. Run the `dotnet run` command if in the cloud shell. The output should be something similar to the following example.

    ```
    Created or retrieved database: myDatabase
    Created or retrieved container: myContainer
    Created item: c549c3fa-054d-40db-a42b-c05deabbc4a6
    Request charge: 6.29 RUs
    ```

1. In the Azure portal, navigate to the Azure Cosmos DB resource you created earlier. Select **Data Explorer** in the left navigation. In **Data Explorer**, select **myDatabase** and then expand **myContainer**. You can view the item you created by selecting **Items**.

    ![Screenshot showing the location of Items in the Data Explorer.](./media/09/cosmos-data-explorer.png)

## Clean up resources

Now that you finished the exercise, you should delete the cloud resources you created to avoid unnecessary resource usage.

1. Navigate to the resource group you created and view the contents of the resources used in this exercise.
1. On the toolbar, select **Delete resource group**.
1. Enter the resource group name and confirm that you want to delete it.
