---
lab:
    title: 'Lab: Access resource secrets more securely across services'
    az204Module: 'Module 07: Implement secure cloud solutions'
    az020Module: 'Module 07: Implement secure cloud solutions'
    type: 'Answer Key'
---

# Lab: Access resource secrets more securely across services | Student lab answer key

## Microsoft Azure user interface

Given the dynamic nature of Microsoft cloud tools, you might experience Azure user interface (UI) changes after the development of this training content. These changes might cause the lab instructions and lab steps to not match.

Microsoft updates this training course when the community brings needed changes to our attention; however, because cloud updates occur frequently, you might encounter UI changes before this training content updates. **If this occurs, adapt to the changes, and then work through them in the labs as needed.**

## Instructions

### Before you start

#### Sign in to the lab virtual machine

Sign in to your Windows 10 virtual machine (VM) by using the following credentials:

- Username: **Admin**
- Password: **Pa55w.rd**

> **Note**: Instructions to connect to the virtual lab environment will be provided by your instructor.

#### Review the installed applications

Find the taskbar on your Windows 10 desktop. The taskbar contains the icons for the applications that you'll use in this lab:

- Microsoft Edge
- File Explorer
- Windows Terminal
- Visual Studio Code

### Exercise 1: Create Azure resources

#### Task 1: Open the Azure portal

1. On the taskbar, select the **Microsoft Edge** icon.
1. In the open browser window, go to the **Azure portal** (<https://portal.azure.com>).
1. Enter the email address for your Microsoft account, and then select **Next**.
1. Enter the **password** for your Microsoft account, and then select **Sign in**.
    > **Note**: If this is your first time signing in to the Azure portal, you will be offered a tour of the portal. If you prefer to skip the tour, select **Get Started** to begin using the portal.

#### Task 2: Create an Azure Storage account

1. In the Azure portal's navigation pane, select **All services**.
1. From the **All services** blade, select **Storage Accounts**.
1. From the **Storage accounts** blade, find your list of Storage instances.
1. From the **Storage accounts** blade, select **Add**.
1. Find the tabs from the **Create storage account** blade, such as **Basics**.
    > **Note**: Each tab represents a step in the workflow to create a new storage account. You can select **Review + Create** at any time to skip the remaining tabs.
1. From the **Basics** tab, perform the following actions:
    1. Leave the **Subscription** text box set to its default value.
    1. In the **Resource group** section, select **Create new**, enter **ConfidentialStack**, and then select **OK**.
    1. In the **Storage account name** text box, enter **securestor[yourname]**.
    1. In the **Location** drop-down list, select the **(US) East US** region.
    1. In the **Performance** section, select **Standard**.
    1. In the **Account kind** drop-down list, select **StorageV2 (general purpose v2)**.
    1. In the **Replication** drop-down list, select **Locally-redundant storage (LRS)**.
    1. In the **Access tier** section, ensure that **Hot** is selected.
    1. Select **Review + Create**.
1. From the **Review + Create** tab, review the options that you selected during the previous steps.
1. Select **Create** to create the storage account by using your specified configuration.
    > **Note**: Wait for the creation task to complete before you move forward with this lab.
1. In the Azure portal's navigation pane, select **All services**.
1. From the **All services** blade, select **Storage Accounts**.
1. From the **Storage accounts** blade, select the **securestor[yourname]** storage account that you created earlier in this lab.
1. From the **Storage account** blade, find the **Settings** section, and then select the **Access keys** link.
1. From the **Access keys** blade, select any one of the keys and record the value in either of the **Connection string** boxes. You'll use this value later in this lab.
    > **Note**: It doesn't matter which connection string you choose. They are interchangeable.

#### Task 3: Create an Azure Key Vault

1. In the Azure portal's navigation pane, select the **Create a resource** link.
1. From the **New** blade, find the **Search the Marketplace** text box above the list of featured services.
1. In the search box, enter **Vault**, and then select Enter.
1. From the **Marketplace** search results blade, select the **Key Vault** result.
1. From the **Key Vault** blade, select **Create**.
1. Find the tabs from the **Create key vault** blade, such as **Basics**.
    > **Note**: Each tab represents a step in the workflow to create a new key vault. You can select **Review + Create** at any time to skip the remaining tabs.
1. From the **Basics** tab, perform the following actions:
    1. Leave the **Subscription** text box set to its default value.
    1. In the **Resource group** section, select **Use existing**, and then select **ConfidentialStack** in the list.
    1. In the **Key vault name** text box, enter **securevault[yourname]**.
    1. In the **Region** drop-down list, select the **East US** region.
    1. In the **Pricing tier** drop-down list, select **Standard**.
    1. Select **Review + Create**.
1. From the **Review + Create** tab, review the options that you selected during the previous steps.
1. Select **Create** to create the key vault by using your specified configuration.
    > **Note**: Wait for the creation task to complete before you move forward with this lab.

#### Task 4: Create an Azure Functions app

1. In the Azure portal's navigation pane, select the **Create a resource** link.
1. From the **New** blade, find the **Search the Marketplace** text box above the list of featured services.
1. In the search box, enter **Function**, and then select Enter.
1. From the **Marketplace** search results blade, select the **Function App** result.
1. From the **Function App** blade, select **Create**.
1. Find the tabs from the **Function App** blade, such as **Basics**.
    > **Note**: Each tab represents a step in the workflow to create a new function app. You can select **Review + Create** at any time to skip the remaining tabs.
1. From the **Basics** tab, perform the following actions:
    1. Leave the **Subscription** text box set to its default value.
    1. In the **Resource group** section, select **Use existing**, and then select **ConfidentialStack** in the list.
    1. In the **Function app name** text box, enter **securefunc[yourname]**.
    1. In the **Publish** section, select **Code**.
    1. In the **Runtime stack** drop-down list, select **.NET Core**.
    1. In the **Version** drop-down list, select **3.1**.
    1. In the **Region** drop-down list, select the **East US** region.
    1. Select **Next: Hosting**.
1. From the **Hosting** tab, perform the following actions:
    1. In the **Operating System** section, select **Linux**.
    1. In the **Storage account** drop-down list, select the **securestor[yourname]** storage account that you created earlier in this lab.
    1. In the **Plan type** drop-down list, select the **Consumption (Serverless)** option.
    1. Select **Review + Create**.
1. From the **Review + Create** tab, review the options that you selected during the previous steps.
1. Select **Create** to create the function app by using your specified configuration.
    > **Note**: Wait for the creation task to complete before you move forward with this lab.

> **Review**: In this exercise, you created all the resources that you'll use for this lab.

### Exercise 2: Configure secrets and identities

#### Task 1: Configure a system-assigned managed service identity

1. In the Azure portal's navigation pane, select the **Resource groups** link.
1. From the **Resource groups** blade, find and then select the **ConfidentialStack** resource group that you created earlier in this lab.
1. From the **ConfidentialStack** blade, select the **securefunc[yourname]** function app that you created earlier in this lab.
1. From the **App Service** blade, select the **Identity** option from the **Settings** section.
1. From the **Identity** pane, find the **System assigned** tab, and then perform the following actions:
    1. In the **Status** section, select **On**, and then select **Save**.
    1. In the confirmation dialog box, select **Yes**.
    > **Note**: Wait for the system-assigned managed identity to be created before you move forward with this lab.

#### Task 2: Create a Key Vault secret

1. In the Azure portal's navigation pane, select the **Resource groups** link.
1. From the **Resource groups** blade, find and then select the **ConfidentialStack** resource group that you created earlier in this lab.
1. From the **ConfidentialStack** blade, select the **securevault[yourname]** key vault that you created earlier in this lab.
1. From the **Key Vault** blade, select the **Secrets** link in the **Settings** section.
1. In the **Secrets** pane, select **Generate/Import**.
1. From the **Create a secret** blade, perform the following actions:
    1. In the **Upload options** drop-down list, select **Manual**.
    1. In the **Name** text box, enter **storagecredentials**.
    1. In the **Value** text box, enter the storage account connection string that you recorded earlier in this lab.
    1. Leave the **Content Type** text box set to its default value.
    1. Leave the **Set activation date** text box set to its default value.
    1. Leave the **Set expiration date** text box set to its default value.
    1. In the **Enabled** section, select **Yes**, and then select **Create**.
    > **Note**: Wait for the secret to be created before you move forward with this lab.
1. Return to the Secrets pane, and then select the **storagecredentials** item in the list.
1. In the Versions pane, select the latest version of the **storagecredentials** secret.
1. In the Secret Version pane, perform the following actions:
    1. Find the metadata for the latest version of the secret.
    1. Select **Show secret value** to find the value of the secret.
    1. Record the value of the **Secret Identifier** text box because you'll use this later in the lab.
    > **Note**: You are recording the value of the **Secret Identifier** text box, not the **Secret Value** text box.

#### Task 3: Configure a Key Vault access policy

1. In the Azure portal's navigation pane, select the **Resource groups** link.
1. From the **Resource groups** blade, find and then select the **ConfidentialStack** resource group that you created earlier in this lab.
1. From the **ConfidentialStack** blade, select the **securevault[yourname]** key vault that you created earlier in this lab.
1. From the **Key Vault** blade, select the **Access policies** link in the **Settings** section.
1. In the Access policies pane, select **Add Access Policy**.
1. From the **Add access policy** blade, perform the following actions:
    1. Select the **Select principal** link.
    1. From the **Principal** blade, find and then select the service principal named **securefunc[yourname]**, and then select **Select**.
        > **Note**: The system-assigned managed identity you created earlier in this lab will have the same name as the Azure Function resource.
    1. Leave the **Key permissions** list set to its default value.
    1. In the **Secret permissions** drop-down list, select the **GET** permission.
    1. Leave the **Certificate permissions** list set to its default value.
    1. Leave the **Authorized application** text box set to its default value.
    1. Select **Add**.
1. Back in the Access policies pane, select **Save**.
    > **Note**: Wait for your changes to the access policies to save before you move forward with this lab.

#### Task 4: Create a Key Vault-derived application setting

1. In the Azure portal's navigation pane, select the **Resource groups** link.
1. From the **Resource groups** blade, find and then select the **ConfidentialStack** resource group that you created earlier in this lab.
1. From the **ConfidentialStack** blade, select the **securefunc[yourname]** function app that you created earlier in this lab.
1. From the **App Service** blade, select the **Configuration** option from the **Settings** section.
1. From the **Configuration** pane, perform the following actions:
    1. Select the **Application settings** tab, and then select **New application setting**.
    1. In the **Add/Edit application setting** pop-up window, in the **Name** text box, enter **StorageConnectionString**.
    1. In the **Value** text box, construct a value by using the following syntax: ``@Microsoft.KeyVault(SecretUri=*Secret Identifier*)``
        > **Note**: You'll need to build a reference to your ***Secret Identifier*** by using the above syntax. For example, if your secret identifier is ``https://securevaultstudent.vault.azure.net/secrets/storagecredentials/17b41386df3e4191b92f089f5efb4cbf``, your value would be ``@Microsoft.KeyVault(SecretUri=https://securevaultstudent.vault.azure.net/secrets/storagecredentials/17b41386df3e4191b92f089f5efb4cbf)``.
    1. Leave the **deployment slot setting** text box set to its default value.
    1. Select **OK** to close the pop-up window and return to the **Configuration** section.
    1. Select **Save** from the blade to save your settings.  
    1. In the **Save Changes** confirmation pop-up dialog box, select **Continue**.
    > **Note**: Wait for your application settings to save before you move forward with the lab.

> **Review**: In this exercise, you created a system-assigned managed service identity for your function app and then gave that identity the appropriate permissions to get the value of a secret in your key vault. Finally, you created a secret that you referenced within your function app's configuration settings.

### Exercise 3: Build an Azure Functions app

#### Task 1: Initialize a function project

1. On the taskbar, select the **Windows Terminal** icon.
1. Enter the following command, and then select Enter to change the current directory to the **Allfiles (F):\\Allfiles\\Labs\\07\\Starter\\func** empty directory:

    ```powershell
    cd F:\Allfiles\Labs\07\Starter\func
    ```

1. At the open command prompt, enter the following command, and then select Enter to use the **Azure Functions Core Tools** to create a new local Functions project in the current directory using the **dotnet** runtime:

    ```powershell
    func init --worker-runtime dotnet
    ```

    > **Note**: You can review the documentation to [create a new project][azure-functions-core-tools-new-project] using the **Azure Functions Core Tools**.
1. Enter the following command, and then select Enter to **build** the .NET Core 3.1 project:

    ```powershell
    dotnet build
    ```

#### Task 2: Create an HTTP-triggered function

1. Still in the open command prompt, enter the following command, and then select Enter to use the **Azure Functions Core Tools** to create a new function named **FileParser** using the **HTTP trigger** template:

    ```powershell
    func new --template "HTTP trigger" --name "FileParser"
    ```

    > **Note**: You can review the documentation to [create a new function][azure-functions-core-tools-new-function] using the **Azure Functions Core Tools**.
1. Close the currently running **Windows Terminal** application.

#### Task 3: Configure and read an application setting

1. On the **Start** screen, select the **Visual Studio Code** tile.
1. From the **File** menu, select **Open Folder**.
1. In the **File Explorer** window that opens, browse to **Allfiles (F):\\Allfiles\\Labs\\07\\Starter\\func**, and then select **Select Folder**.
1. In the Explorer pane of the **Visual Studio Code** window, open the **local.settings.json** file.
1. Observe the current value of the **Values** object:

    ```json
    "Values": {
        "AzureWebJobsStorage": "UseDevelopmentStorage=true",
        "FUNCTIONS_WORKER_RUNTIME": "dotnet"
    }
    ```

1. Update the value of the **Values** object by adding a new setting named **StorageConnectionString** and setting it to a string value of **[TEST VALUE]**:

    ```json
    "Values": {
        "AzureWebJobsStorage": "UseDevelopmentStorage=true",
        "FUNCTIONS_WORKER_RUNTIME": "dotnet",
        "StorageConnectionString": "[TEST VALUE]"
    }
    ```

1. The **local.settings.json** file should now include:

    ```json
    {
        "IsEncrypted": false,
        "Values": {
            "AzureWebJobsStorage": "UseDevelopmentStorage=true",
            "FUNCTIONS_WORKER_RUNTIME": "dotnet",
            "StorageConnectionString": "[TEST VALUE]"
        }
    }
    ```

1. In the Explorer pane of the **Visual Studio Code** window, open the **FileParser.cs** file.
1. In the code editor, observe the example implementation:

    ```csharp
    using System;
    using System.IO;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Azure.WebJobs;
    using Microsoft.Azure.WebJobs.Extensions.Http;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Logging;
    using Newtonsoft.Json;

    namespace func
    {
        public static class FileParser
        {
            [FunctionName("FileParser")]
            public static async Task<IActionResult> Run(
                [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
                ILogger log)
            {
                log.LogInformation("C# HTTP trigger function processed a request.");

                string name = req.Query["name"];

                string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                dynamic data = JsonConvert.DeserializeObject(requestBody);
                name = name ?? data?.name;

                string responseMessage = string.IsNullOrEmpty(name)
                    ? "This HTTP triggered function executed successfully. Pass a name in the query string or in the request body for a personalized response."
                    : $"Hello, {name}. This HTTP triggered function executed successfully.";

                return new OkObjectResult(responseMessage);
            }
        }
    }
    ```

1. Delete all of the content within the **FileParser.cs** file.
1. Add the following lines of code to add **using directives** for the **Microsoft.AspNetCore.Mvc**, **Microsoft.Azure.WebJobs**, **Microsoft.AspNetCore.Http**, **System**, and **System.Threading.Tasks** namespaces:

    ```csharp
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Azure.WebJobs;
    using Microsoft.AspNetCore.Http;
    using System;
    using System.Threading.Tasks;
    ```

1. Create a new **public static** class named **FileParser**:

    ```csharp
    public static class FileParser
    { }
    ```

1. Observe the **FileParser.cs** file again, which should now include:

    ```csharp
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Azure.WebJobs;
    using Microsoft.AspNetCore.Http;
    using System;
    using System.Threading.Tasks;

    public static class FileParser
    { }
    ```

1. Within the **FileParser** class, add the following code block to create a new **public static** *asynchronous* method named **Run** that returns a variable of type **Task\<IActionResult\>** and that also takes in a variable of type **HttpRequest** named *request*:

    ```csharp
    public static async Task<IActionResult> Run(
        HttpRequest request)
    { }
    ```

1. Add the following code to append an attribute to the **Run** method of type **FunctionNameAttribute** that has its **name** parameter set to a value of **FileParser**:

    ```csharp
    [FunctionName("FileParser")]
    public static async Task<IActionResult> Run(
        HttpRequest request)
    { }
    ```

1. Add the following code to append an attribute to the **request** paramter of type **HttpTriggerAttribute** that has its **methods** parameter array set to a single value of **GET**:

    ```csharp
    [FunctionName("FileParser")]
    public static async Task<IActionResult> Run(
        [HttpTrigger("GET")] HttpRequest request)
    { }
    ```

1. Observe the **FileParser.cs** file again, which should now include:

    ```csharp
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Azure.WebJobs;
    using Microsoft.AspNetCore.Http;
    using System;
    using System.Threading.Tasks;

    public static class FileParser
    {
        [FunctionName("FileParser")]
        public static async Task<IActionResult> Run(
            [HttpTrigger("GET")] HttpRequest request)
        { }
    }
    ```

1. In the **Run** method, enter the following line of code to retrieve the value of the **StorageConnectionString** application setting by using the **Environment.GetEnvironmentVariable** method and storing the result in a **string** variable named **connectionString**:

    ```csharp
    string connectionString = Environment.GetEnvironmentVariable("StorageConnectionString");
    ```

1. Enter the following line of code to return the value of the **connectionString** variable as the HTTP response:

    ```csharp
    return new OkObjectResult(connectionString);
    ```

1. Observe the **FileParser.cs** file again, which should now include:

    ```csharp
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Azure.WebJobs;
    using Microsoft.AspNetCore.Http;
    using System;
    using System.Threading.Tasks;

    public static class FileParser
    {
        [FunctionName("FileParser")]
        public static async Task<IActionResult> Run(
            [HttpTrigger("GET")] HttpRequest request)
        {
            string connectionString = Environment.GetEnvironmentVariable("StorageConnectionString");
            return new OkObjectResult(connectionString);
        }
    }
    ```

1. Select **Save** to sae your changes to the **Echo.cs** file.

#### Task 4: Validate the local function

1. On the taskbar, select the **Windows Terminal** icon.
1. Enter the following command, and then select Enter to change the current directory to the **Allfiles (F):\\Allfiles\\Labs\\07\\Starter\\func** empty directory:

    ```powershell
    cd F:\Allfiles\Labs\07\Starter\func
    ```

1. At the open command prompt, enter the following command, and then select Enter to run the function app project:

    ```powershell
    func start --build
    ```

    > **Note**: You can review the documentation to [start the function app project locally][azure-functions-core-tools-start-function] using the **Azure Functions Core Tools**.
1. On the taskbar, select the **Windows Terminal** icon again to open a new instance of the **Windows Terminal** application.
1. When you receive the open command prompt, enter the following command, and then select Enter to start the **httprepl** tool setting the base Uniform Resource Identifier (URI) to ``http://localhost:7071``:

    ```powershell
    httprepl http://localhost:7071
    ```

    > **Note**: An error message is displayed by the httprepl tool. This message occurs because the tool is searching for a Swagger definition file to use to "traverse" the API. Because your functio projectp does not produce a Swagger definition file, you'll need to traverse the API manually.
1. When you receive the tool prompt, enter the following command, and then select Enter to browse to the relative **api** directory:

    ```powershell
    cd api
    ```

1. Enter the following command, and then select Enter to browse to the relative **fileparser** directory:

    ```powershell
    cd fileparser
    ```

1. Enter the following command, and then select Enter to run the **get** command:

    ```powershell
    get
    ```

1. Observe the **[TEST VALUE]** value of the **StorageConnectionString** being returned as the result of the HTTP request:

    ```powershell
    HTTP/1.1 200 OK
    Content-Type: text/plain; charset=utf-8
    Date: Tue, 01 Sep 2020 23:35:39 GMT
    Server: Kestrel
    Transfer-Encoding: chunked

    [TEST VALUE]

    ```

1. Enter the following command, and then select Enter to exit the **httprepl** application:

    ```powershell
    exit
    ```

1. Close all currently running instances of the **Windows Terminal** application.

#### Task 5: Deploy using the Azure Functions Core Tools

1. On the taskbar, select the **Windows Terminal** icon.
1. Enter the following command, and then select Enter to change the current directory to the **Allfiles (F):\\Allfiles\\Labs\\07\\Starter\\func** empty directory:

    ```powershell
    cd F:\Allfiles\Labs\07\Starter\func
    ```

1. At the open command prompt, enter the following command, and then select Enter to log in to the Azure Command-Line Interface (CLI):

    ```powershell
    az login
    ```

1. In the **Microsoft Edge** browser window, perform the following actions:
    1. Enter the email address for your Microsoft account, and then select **Next**.
    1. Enter the password for your Microsoft account, and then select **Sign in**.
1. Return to the currently open **Windows Terminal** window. Wait for the sign-in process to finish.
1. Enter the following command, and then select Enter to publish the function app project:

    ```powershell
    func azure functionapp publish <function-app-name>
    ```

    > **Note**: As an example, if your **Function App name** is **securefuncstudent**, your command would be ``func azure functionapp publish securefuncstudent``. You can review the documentation to [publish the local function app project][azure-functions-core-tools-publish-azure] using the **Azure Functions Core Tools**.
1. Wait for the deployment to finalize before you move forward with the lab.
1. Close the currently running **Windows Terminal** application.

#### Task 6: Test the Key Vault-derived application setting

1. On the taskbar, select the **Microsoft Edge** icon.
1. In the open browser window, go to the Azure portal (<https://portal.azure.com>).
1. In the Azure portal's navigation pane, select the **Resource groups** link.
1. On the **Resource groups** blade, find and then select the **Serverless** resource group that you created earlier in this lab.
1. On the **Serverless** blade, select the **securefunc[yourname]** function app that you created earlier in this lab.
1. From the **App Service** blade, select the **Functions** option from the **Functions** section.
1. In the **Functions** pane, select the the existing **FileParser** function.
1. In the **Function** blade, select the **Code + Test** option from the **Developer** section.
1. In the function editor, select **Test/Run**.
1. In the pop-up dialog box that appears, perform the following actions:
    - In the **HTTP method** list, select **GET**.
1. Select **Run** to test the function.
1. Observe the results of the test run. The result should be your Azure Storage connection string.

> **Review**: In this exercise, you used a service identity to read the value of a secret stored in Key Vault and returned that value as the result of a function app.

### Exercise 4: Access Azure Blob Storage data

#### Task 1: Upload a sample storage blob

1. In the Azure portal's navigation pane, select the **Resource groups** link.
1. From the **Resource groups** blade, find and then select the **ConfidentialStack** resource group that you created earlier in this lab.
1. From the **ConfidentialStack** blade, select the **securestor[yourname]** storage account that you created earlier in this lab.
1. From the **Storage account** blade, select the **Containers** link in the **Blob service** section.
1. In the **Containers** section, select **+ Container**.
1. In the **New container** pop-up window, perform the following actions:
    1. In the **Name** text box, enter **drop**.
    1. In the **Public access level** drop-down list, select **Blob (anonymous read access for blobs only)**, and then select **OK**.
1. Return to the **Containers** section, and then select the newly created **drop** container.
1. From the **Container** blade, select **Upload**.
1. In the **Upload blob** pop-up window, perform the following actions:
    1. In the **Files** section, select the **Folder** icon.
    1. In the **File Explorer** window, browse to **Allfiles (F):\\Allfiles\\Labs\\07\\Starter**, select the **records.json** file, and then select **Open**.
    1. Ensure that **Overwrite if files already exist** is selected, and then select **Upload**.  
    > **Note**: Wait for the blob to upload before you continue with this lab.
1. Return to the **Container** blade, and then select the **records.json** blob in the list of blobs.
1. From the **Blob** blade, find the blob metadata, and then copy the URL for the blob.
1. On the taskbar, right-click the **Microsoft Edge** icon or activate the shortcut menu, and then select **New window**.
1. In the new browser window, go to the URL that you copied for the blob.
1. The JavaScript Object Notation (JSON) contents of the blob should now display. Close the browser window with the JSON contents.
1. Return to the browser window with the Azure portal, and then close the **Blob** blade.
1. Return to the **Container** blade, and then select **Change access level policy**.
1. In the **Change access level** pop-up window, perform the following actions:
    1. In the **Public access level** drop-down list, select **Private (no anonymous access)**.
    1. Select **OK**.
1. On the taskbar, right-click the **Microsoft Edge** icon or activate the shortcut menu, and then select **New window**.
1. In the new browser window, go to the URL that you copied for the blob.
1. An error message indicating that the resource wasn't found should now display.
    > **Note**: If the error message doesn't display, your browser might have cached the file. Press Ctrl+F5 to refresh the page until the error message displays.

#### Task 2: Pull and configure the Azure SDK for .NET

1. On the taskbar, select the **Windows Terminal** icon.
1. Enter the following command, and then select Enter to change the current directory to the **Allfiles (F):\\Allfiles\\Labs\\07\\Starter\\func** empty directory:

    ```powershell
    cd F:\Allfiles\Labs\07\Starter\func
    ```

1. At the open command prompt, enter the following command, and then select Enter to add version **12.6.0** of the **Azure.Storage.Blobs** package from NuGet:

    ```powershell
    dotnet add package Azure.Storage.Blobs --version 12.6.0
    ```

    > **Note**: The [Azure.Storage.Blobs](https://www.nuget.org/packages/Azure.Storage.Blobs/12.6.0) NuGet package references the subset of the Azure SDK for .NET required to write code for Azure Blob Storage.
1. Close the currently running **Windows Terminal** application.
1. On the **Start** screen, select the **Visual Studio Code** tile.
1. From the **File** menu, select **Open Folder**.
1. In the **File Explorer** window that opens, browse to **Allfiles (F):\\Allfiles\\Labs\\07\\Starter\\func**, and then select **Select Folder**.
1. In the Explorer pane of the **Visual Studio Code** window, open the **FileParser.cs** file.
1. Add a **using directive** for the **Azure.Storage.Blobs** namespace:

    ```csharp
    using Azure.Storage.Blobs;
    ```

1. Observe the **FileParser.cs** file, which should now include:

    ```csharp
    using Azure.Storage.Blobs;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Azure.WebJobs;
    using Microsoft.AspNetCore.Http;
    using System;
    using System.Threading.Tasks;

    public static class FileParser
    {
        [FunctionName("FileParser")]
        public static async Task<IActionResult> Run(
            [HttpTrigger("GET")] HttpRequest request)
        {
            string connectionString = Environment.GetEnvironmentVariable("StorageConnectionString");
            return new OkObjectResult(connectionString);
        }
    }
    ```

#### Task 3: Write Azure Blob Storage code using the Azure SDK for .NET

1. Within the **Run** method of the **FileParser** class, delete the following line of code:

    ```csharp
    return new OkObjectResult(connectionString);
    ```

1. Still within the **Run** method, add the following code block to create a new instance of the **BlobClient** class by passing in your *connectionString* variable, a  ``"drop"`` string value, and a ``"records.json"`` string value to the constructor:

    ```csharp
    BlobClient blob = new BlobClient(connectionString, "drop", "records.json");
    ```

1. Still within the **Run** method, add the following code block to use the **BlobClient.DownloadAsync** method to download the contents of the referenced blob asynchronously and store the result in a variable named *response*:

    ```csharp
    var response = await blob.DownloadAsync();
    ```

1. Still within the **Run** method, add the following code block to return the value of the various content stored in the *content* variable by using the **FileStreamResult** class constructor:

    ```csharp
    return new FileStreamResult(response?.Value?.Content, response?.Value?.ContentType);
    ```

1. Observe the **FileParser.cs** file again, which should now include:

    ```csharp
    using Azure.Storage.Blobs;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Azure.WebJobs;
    using Microsoft.AspNetCore.Http;
    using System;
    using System.Threading.Tasks;

    public static class FileParser
    {
        [FunctionName("FileParser")]
        public static async Task<IActionResult> Run(
            [HttpTrigger("GET")] HttpRequest request)
        {
            string connectionString = Environment.GetEnvironmentVariable("StorageConnectionString");
            BlobClient blob = new BlobClient(connectionString, "drop", "records.json");
            var response = await blob.DownloadAsync();
            return new FileStreamResult(response?.Value?.Content, response?.Value?.ContentType);
        }
    }
    ```

1. Select **Save** to save your changes to the **Echo.cs** file.

#### Task 4: Deploy and validate the Azure Functions app

1. On the taskbar, select the **Windows Terminal** icon.
1. Enter the following command, and then select Enter to change the current directory to the **Allfiles (F):\\Allfiles\\Labs\\07\\Starter\\func** empty directory:

    ```powershell
    cd F:\Allfiles\Labs\07\Starter\func
    ```

1. At the open command prompt, enter the following command, and then select Enter to log in to the Azure CLI:

    ```powershell
    az login
    ```

1. In the **Microsoft Edge** browser window, perform the following actions:
    1. Enter the email address for your Microsoft account, and then select **Next**.
    1. Enter the password for your Microsoft account, and then select **Sign in**.
1. Return to the currently open **Windows Terminal** window. Wait for the sign-in process to finish.
1. Enter the following command, and then select Enter to publish the function app project again:

    ```powershell
    func azure functionapp publish <function-app-name>
    ```

    > **Note**: As an example, if your **Function App name** is **securefuncstudent**, your command would be ``func azure functionapp publish securefuncstudent``. You can review the documentation to [publish the local function app project][azure-functions-core-tools-publish-azure] using the **Azure Functions Core Tools**.
1. Wait for the deployment to finalize before you move forward with the lab.
1. Close the currently running **Windows Terminal** application.
1. On the taskbar, select the **Microsoft Edge** icon.
1. In the open browser window, go to the Azure portal (<https://portal.azure.com>).
1. In the Azure portal's navigation pane, select the **Resource groups** link.
1. On the **Resource groups** blade, find and then select the **Serverless** resource group that you created earlier in this lab.
1. On the **Serverless** blade, select the **securefunc[yourname]** function app that you created earlier in this lab.
1. From the **App Service** blade, select the **Functions** option from the **Functions** section.
1. In the **Functions** pane, select the the existing **FileParser** function.
1. In the **Function** blade, select the **Code + Test** option from the **Developer** section.
1. In the function editor, select **Test/Run**.
1. In the pop-up dialog box that appears, perform the following actions:
    - In the **HTTP method** list, select **GET**.
1. Select **Run** to test the function.
1. Observe the results of the test run. The output will contain the content of the **$/drop/records.json** blob stored in your Azure Storage account.

> **Review**: In this exercise, you used C\# code to access a storage account, and then downloaded the contents of a blob.

### Exercise 5: Clean up your subscription

#### Task 1: Open Azure Cloud Shell and list resource groups

1. In the Azure portal's navigation pane, select the **Cloud Shell** icon to open a new shell instance.

    > **Note**: The **Cloud Shell** icon is represented by a greater than sign (\>) and underscore character (\_).

1. If this is your first time opening Cloud Shell using your subscription, you can use the **Welcome to Azure Cloud Shell Wizard** to configure Cloud Shell for first-time usage. Perform the following actions in the wizard:

    1. A dialog box prompts you to create a new storage account to begin using the shell. Accept the default settings, and then select **Create storage**.

    > **Note**: Wait for Cloud Shell to finish its initial setup procedures before moving forward with the lab. If Cloud Shell configuration options don't display, this is most likely because you are using an existing subscription with this course's labs. The labs are written with the presumption that you are using a new subscription.

#### Task 2: Delete a resource group

1. When you receive the command prompt, enter the following command, and then select Enter to delete the **ConfidentialStack** resource group:

    ```powershell
    az group delete --name ConfidentialStack --no-wait --yes
    ```

1. Close the Cloud Shell pane in the portal.

#### Task 3: Close the active application

1. Close the currently running Microsoft Edge application.

> **Review**: In this exercise, you cleaned up your subscription by removing the resource groups used in this lab.
