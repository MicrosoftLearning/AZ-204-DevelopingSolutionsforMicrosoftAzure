using Azure.Storage;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using System;
using System.Threading.Tasks;    
public class Program
{
    //Update the blobServiceEndpoint value that you recorded previously in this lab.        
    private const string blobServiceEndpoint = "";

    //Update the storageAccountName value that you recorded previously in this lab.
    private const string storageAccountName = "";

    //Update the storageAccountKey value that you recorded previously in this lab.
    private const string storageAccountKey = "";    


    //The following code to create a new asynchronous Main method
    public static async Task Main(string[] args)
    { 
    //The following line of code to create a new instance of the StorageSharedKeyCredential class by using the storageAccountName and storageAccountKey constants as constructor parameters
    StorageSharedKeyCredential accountCredentials = new StorageSharedKeyCredential(storageAccountName, storageAccountKey);

    //The following line of code to create a new instance of the BlobServiceClient class by using the blobServiceEndpoint constant and the accountCredentials variable as constructor parameters
    BlobServiceClient serviceClient = new BlobServiceClient(new Uri(blobServiceEndpoint), accountCredentials);

    //The following line of code to invoke the GetAccountInfoAsync method of the BlobServiceClient class to retrieve account metadata from the service
    AccountInfo info = await serviceClient.GetAccountInfoAsync();

    //Render a welcome message
    await Console.Out.WriteLineAsync($"Connected to Azure Storage Account");

    //Render the storage account's name
    await Console.Out.WriteLineAsync($"Account name:\t{storageAccountName}");

    //Render the type of storage account
    await Console.Out.WriteLineAsync($"Account kind:\t{info?.AccountKind}");

    //Render the currently selected stock keeping unit (SKU) for the storage account
    await Console.Out.WriteLineAsync($"Account sku:\t{info?.SkuName}");

    await EnumerateContainersAsync(serviceClient);

    string existingContainerName = "raster-graphics";
    await EnumerateBlobsAsync(serviceClient, existingContainerName);

    string newContainerName = "vector-graphics";
    BlobContainerClient containerClient = await GetContainerAsync(serviceClient, newContainerName);

    string uploadedBlobName = "graph.svg";
    BlobClient blobClient = await GetBlobAsync(containerClient, uploadedBlobName);
    await Console.Out.WriteLineAsync($"Blob Url:\t{blobClient.Uri}");
    }

    private static async Task EnumerateContainersAsync(BlobServiceClient client)
{   
    /*Create an asynchronous foreach loop that iterates over the results of 
        an invocation of the GetBlobContainersAsync method of the BlobServiceClient class. */    
    await foreach (BlobContainerItem container in client.GetBlobContainersAsync())
    {   
        //Print the name of each container
        await Console.Out.WriteLineAsync($"Container:\t{container.Name}");
    }
}

private static async Task EnumerateBlobsAsync(BlobServiceClient client, string containerName)
{   
    /* Get a new instance of the BlobContainerClient class by using the
       GetBlobContainerClient method of the BlobServiceClient class, 
       passing in the containerName parameter */   
    BlobContainerClient container = client.GetBlobContainerClient(containerName);

    /* Render the name of the container that will be enumerated */
    await Console.Out.WriteLineAsync($"Searching:\t{container.Name}");

    /* Create an asynchronous foreach loop that iterates over the results of
        an invocation of the GetBlobsAsync method of the BlobContainerClient class */
    await foreach (BlobItem blob in container.GetBlobsAsync())
    {     
        //Print the name of each blob    
        await Console.Out.WriteLineAsync($"Existing Blob:\t{blob.Name}");
    }
}
private static async Task<BlobContainerClient> GetContainerAsync(BlobServiceClient client, string containerName)
{   
    /* Get a new instance of the BlobContainerClient class by using the
        GetBlobContainerClient method of the BlobServiceClient class,
        passing in the containerName parameter */   
    BlobContainerClient container = client.GetBlobContainerClient(containerName);

    /* Invoke the CreateIfNotExistsAsync method of the BlobContainerClient class */
    await container.CreateIfNotExistsAsync(PublicAccessType.Blob);

    /* Render the name of the container that was potentially created */
    await Console.Out.WriteLineAsync($"New Container:\t{container.Name}");

    /* Return the container as the result of the GetContainerAsync */        
    return container;
}

private static async Task<BlobClient> GetBlobAsync(BlobContainerClient client, string blobName)
{      
    BlobClient blob = client.GetBlobClient(blobName);
    bool exists = await blob.ExistsAsync();
    if (!exists)
    {
        await Console.Out.WriteLineAsync($"Blob {blob.Name} not found!");
        
    }
    else
        await Console.Out.WriteLineAsync($"Blob Found, URI:\t{blob.Uri}");
    return blob;
}
}
