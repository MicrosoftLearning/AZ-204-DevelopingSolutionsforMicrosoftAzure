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

This exercise should take approximately **XX** minutes to complete.

## Before you start

Before you begin, make sure you have the following requirements in place:

* An Azure subscription. If you don't already have one, you can [sign up for one](https://azure.microsoft.com/).

<!-- * [Visual Studio Code](https://code.visualstudio.com/) on one of the [supported platforms](https://code.visualstudio.com/docs/supporting/requirements#_platforms).

* [.NET 8](https://dotnet.microsoft.com/en-us/download/dotnet/8.0) is the target framework.

* [C# Dev Kit](https://marketplace.visualstudio.com/items?itemName=ms-dotnettools.csdevkit) for Visual Studio Code.
  
*  The latest [Azure CLI](https://learn.microsoft.com/cli/azure/install-azure-cli) tools, installed locally.
 -->

## Create an Azure Cosmos DB account

In this section of exercise you create a resource group and Azure Cosmos DB account. You also record the endpoint for the account and retrieve an access key.

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

It's time to start adding the packages and code to the project.

### Add packages and using statements

1. Open the terminal in Visual Studio Code and use the following command to add the **Microsoft.Azure.Cosmos** package to the project.

    ```dotnetcli
    dotnet add package Microsoft.Azure.Cosmos --version 3.*
    dotnet add package Newtonsoft.Json --version 13.*
    ```

1. Delete any existing code in the *Program.cs* file and add the following  statements.

    ```csharp
    using Microsoft.Azure.Cosmos;
    ```

### Add code to connect to an Azure Cosmos DB account

1. Add the following code snippet after the **using** statements. The code snippet adds constants and variables into the class and adds some error checking. Be sure to replace the placeholder values for **EndpointUri** and **PrimaryKey** following the directions in the code comments.

    ```csharp
    public class Program
    {
        // Replace <documentEndpoint> with the information created earlier
        private static readonly string EndpointUri = "<documentEndpoint>";

        // Set variable to the Primary Key from earlier.
        private static readonly string PrimaryKey = "<your primary key>";

        // The Cosmos client instance
        private CosmosClient cosmosClient;

        // The database we will create
        private Database database;

        // The container we will create.
        private Container container;

        // The names of the database and container we will create
        private string databaseId = "myDatabase";
        private string containerId = "myContainer";

        public static async Task Main(string[] args)
        {
            try
            {
                Console.WriteLine("Beginning operations...\n");
                Program p = new Program();
                await p.CosmosAsync();
    
            }
            catch (CosmosException de)
            {
                Exception baseException = de.GetBaseException();
                Console.WriteLine("{0} error occurred: {1}", de.StatusCode, de);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: {0}", e);
            }
            finally
            {
                Console.WriteLine("End of program, press any key to exit.");
                Console.ReadKey();
            }
        }
        //The sample code below gets added below this line
    }
    ```

1. Below the `Main` method, add a new asynchronous task called `CosmosAsync`, which instantiates our new `CosmosClient` and adds code to call the methods you add later to create a database and a container.

    ```csharp
    public async Task CosmosAsync()
    {
        // Create a new instance of the Cosmos Client
        this.cosmosClient = new CosmosClient(EndpointUri, PrimaryKey);

        // Runs the CreateDatabaseAsync method
        await this.CreateDatabaseAsync();

        // Run the CreateContainerAsync method
        await this.CreateContainerAsync();
    }
    ```

## Create a database

Copy and paste the `CreateDatabaseAsync` method after the  `CosmosAsync` method. `CreateDatabaseAsync` creates a new database with ID `az204Database` if it doesn't already exist.

```csharp
private async Task CreateDatabaseAsync()
{
    // Create a new database using the cosmosClient
    this.database = await this.cosmosClient.CreateDatabaseIfNotExistsAsync(databaseId);
    Console.WriteLine("Created Database: {0}\n", this.database.Id);
}
 ```

## Create a container

Copy and paste the `CreateContainerAsync` method below the `CreateDatabaseAsync` method.

```csharp
private async Task CreateContainerAsync()
{
    // Create a new container
    this.container = await this.database.CreateContainerIfNotExistsAsync(containerId, "/LastName");
    Console.WriteLine("Created Container: {0}\n", this.container.Id);
}
```

## Run the application

1. Save your work and, in a terminal in Visual Studio Code, check for any errors by running the `dotnet build` command. Run the `dotnet run` command if the build is successful. The console displays the following messages.

    ```
    Beginning operations...
    
    Created Database: myDatabase
    
    Created Container: myContainer
    
    End of program, press any key to exit.
    ```

1. Verify the results by opening the Azure portal, navigating to your Azure Cosmos DB resource, and use the **Data Explorer** to view the database and container.

## Clean up Azure resources

Now that you finished the exercise, you should delete the cloud resources you created to avoid unnecessary resource usage.

```
az group delete --name rg-cosmos --no-wait --yes
```

