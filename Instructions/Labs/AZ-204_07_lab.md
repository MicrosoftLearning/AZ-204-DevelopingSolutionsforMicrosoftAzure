---
lab:
    title: 'Lab: Access resource secrets more securely across services'
    az204Module: 'Module 07: Implement secure cloud solutions'
    az020Module: 'Module 07: Implement secure cloud solutions'
---

# Lab: Access resource secrets more securely across services | Student lab manual

## Lab scenario

Your company has a data-sharing business-to-business (B2B) agreement with another local business in which you're expected to parse a file that's dropped off nightly. To keep things simple, the second company has decided to drop the file as a Microsoft Azure Storage blob every night. You're now tasked with devising a way to access the file and generate a secure URL that any internal system can use to access the blob without exposing the file to the internet. You've decided to use Azure Key Vault to store the credentials for the storage account and Azure Functions to write the code necessary to access the file without storing credentials in plaintext or exposing the file to the internet.

## Objectives

After you complete this lab, you'll be able to:

- Create an Azure Key Vault and store secrets in the key vault.
- Create a system-assigned managed identity for an Azure App Service instance.
- Create a Key Vault access policy for an Azure Active Directory identity or application.
- Use the Azure SDK for .NET to download a blob with an Azure Function.

## Lab setup

- Estimated time: **45 minutes**

## Instructions

### Before you start

#### Sign in to the lab virtual machine

Ensure that you're signed in to your Windows 10 virtual machine (VM) by using the following credentials:

- Username: **Admin**
- Password: **Pa55w.rd**

#### Review the installed applications

Find the taskbar on your Windows 10 desktop. The taskbar contains the icons for the applications that you'll use in this lab:

- Microsoft Edge
- File Explorer
- Windows Terminal
- Visual Studio Code

### Exercise 1: Create Azure resources

#### Task 1: Open the Azure portal

1. Sign in to the Azure portal (<https://portal.azure.com>).
1. If this is your first time signing in to the Azure portal, a dialog box offering a tour of the portal displays. If you prefer to skip the tour, select **Get Started** to skip the tour.

#### Task 2: Create an Azure Storage account

1. Create a new storage account with the following details:
    - New resource group: **ConfidentialStack**
    - Name: **securestor[yourname]**
    - Location: **East US**
    - Performance: **Standard**
    - Account kind: **StorageV2 (general purpose v2)**
    - Replication: **Locally-redundant storage (LRS)**
    - Access tier: **Hot**
    > **Note**: Wait for Azure to finish creating the storage account before you move forward with the lab. You'll receive a notification when the account is created.
1. Open the **Access Keys** blade of your newly created storage account instance.
1. Record the value in the **Connection string** text box. You'll use this value later in this lab.

#### Task 3: Create an Azure key vault

1. Create a new key vault with the following details:
    - Existing resource group: **ConfidentialStack**
    - Name: **securevault[yourname]**
    - Region: **East US**
    - Pricing tier: **Standard**
    > **Note**: Wait for Azure to finish creating the key vault before you move forward with the lab. You'll receive a notification when the vault is created.

#### Task 4: Create an Azure Functions app

1. Create a new function app with the following details:
    - Existing resource group: **ConfidentialStack**
    - App name: **securefunc[yourname]**
    - Publish: **Code**
    - Runtime Stack: **.NET Core**
    - Version: **3.1**
    - Region: **East US**
    - Operating system: **Linux**
    - Storage account: **securestor[yourname]**
    - Plan: **Consumption (Serverless)**
    - Enable Application Insights: **Yes**

    > **Note**: Wait for Azure to finish creating the function app before you move forward with the lab. You'll receive a notification when the app is created.

> **Review**: In this exercise, you created all the resources that you'll use for this lab.

### Exercise 2: Configure secrets and identities

#### Task 1: Configure a system-assigned managed service identity

1. Access the **securefunc[yourname]** function app that you created earlier in this lab.
1. Browse to the **Identity** option from the **Settings** section.
1. Enable the system-assigned managed identity, and then save your changes.

#### Task 2: Create a Key Vault secret

1. Access the **securevault[yourname]** key vault that you created earlier in this lab.
1. Select the **Secrets** link in the **Settings** section.
1. Create a new secret with the following settings:
    - Name: **storagecredentials**
    - Value: ***Storage connection string***
    - Enabled: **Yes**
    > **Note**: Use the storage account connection string that you recorded earlier in this lab for the value of this secret.
1. Select through the secret to find the metadata for its latest version.
1. Record the value of the **Secret Identifier** text box because you'll use this later in the lab.

#### Task 3: Configure a Key Vault access policy

1. Access the **securevault[yourname]** key vault that you created earlier in this lab.
1. Browse to the **Access Policies** link in the **Settings** section.
1. Create a new access policy with the following settings:
    - Principal: **securefunc[yourname]**
        > **Note**: The system-assigned managed identity you created earlier in this lab will have the same name as the Azure Functions resource.
    - Key permissions: **None**
    - Secret permissions: **GET**
    - Certificate permissions: **None**
    - Authorized application: **None**
1. Save your changes to the list of **Access Policies**.

#### Task 4: Create a Key Vault-derived application setting

1. Access the **securefunc[yourname]** function app that you created earlier in this lab.
1. Browse to the **Configuration** option from the **Settings** section.
1. Create a new application setting by using the following details:
    - Name: **StorageConnectionString**
    - Value: **@Microsoft.KeyVault(SecretUri=*Secret Identifier*)**
    - Deployment slot setting: **Not selected**
    > **Note**: You'll need to build a reference to your ***Secret Identifier*** by using the previous syntax. For example, if your ***Secret Identifier*** is ``https://securevaultstudent.vault.azure.net/secrets/storagecredentials/17b41386df3e4191b92f089f5efb4cbf``, your value would be ``@Microsoft.KeyVault(SecretUri=https://securevaultstudent.vault.azure.net/secrets/storagecredentials/17b41386df3e4191b92f089f5efb4cbf)``.
1. Save your changes to the application settings.

> **Review**: In this exercise, you created a system-assigned managed service identity for your function app and then gave that identity the appropriate permissions to get the value of a secret in your key vault. Finally, you created a secret that you referenced within your function app's configuration settings.

### Exercise 3: Build an Azure Functions app

#### Task 1: Initialize a function project

1. Open the **Windows Terminal** application.
1. Change the current directory to the **Allfiles (F):\\Allfiles\\Labs\\07\\Starter\\func** empty directory:

    ```powershell
    cd F:\Allfiles\Labs\07\Starter\func
    ```

1. Use the **Azure Functions Core Tools** to create a new local Azure Functions project with the following details:
    - worker runtime: **dotnet**

    ```powershell
    func init --worker-runtime dotnet
    ```

    > **Note**: You can review the documentation to [create a new project][azure-functions-core-tools-new-project] using the **Azure Functions Core Tools**.
1. **Build** the .NET Core 3.1 project:

    ```powershell
    dotnet build
    ```

#### Task 2: Create an HTTP-triggered function

1. Still in the open command prompt,create a new function with the following details:
    - template: **HTTP trigger**
    - name: **FileParser**

    ```powershell
    func new --template "HTTP trigger" --name "FileParser"
    ```

    > **Note**: You can review the documentation to [create a new function][azure-functions-core-tools-new-function] using the **Azure Functions Core Tools**.
1. Close the currently running **Windows Terminal** application.

#### Task 3: Configure and read an application setting

1. Open **Visual Studio Code**.
1. Using **Visual Studio Code**, open the solution folder found at **Allfiles (F):\\Allfiles\\Labs\\07\\Starter\\func**.
1. Open the **local.settings.json** file.
1. Update the value of the **Values** object by adding a new setting named **StorageConnectionString** and setting it to a string value of **[TEST VALUE]**:

    ```json
    "Values": {
        "AzureWebJobsStorage": "UseDevelopmentStorage=true",
        "FUNCTIONS_WORKER_RUNTIME": "dotnet",
        "StorageConnectionString": "[TEST VALUE]"
    }
    ```

1. Open the **FileParser.cs** file.
1. In the code editor, delete all the code within the **FileParser.cs** file.
1. Add **using directives** for the **Microsoft.AspNetCore.Mvc**, **Microsoft.Azure.WebJobs**, **Microsoft.AspNetCore.Http**, **System**, and **System.Threading.Tasks** namespaces:

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

1. Within the **FileParser** class, create a new **public static** *asynchronous* method named **Run** that returns a variable of type **Task\<IActionResult\>** and that also takes in a variable of type **HttpRequest** named *request*:

    ```csharp
    public static async Task<IActionResult> Run(
        HttpRequest request)
    { }
    ```

1. Append an attribute to the **Run** method of type **FunctionNameAttribute** that has its **name** parameter set to a value of **FileParser**:

    ```csharp
    [FunctionName("FileParser")]
    public static async Task<IActionResult> Run(
        HttpRequest request)
    { }
    ```

1. Append an attribute to the **request** parameter of type **HttpTriggerAttribute** that has its **methods** parameter array set to a single value of **GET**:

    ```csharp
    [FunctionName("FileParser")]
    public static async Task<IActionResult> Run(
        [HttpTrigger("GET")] HttpRequest request)
    { }
    ```

1. Within the **Run** method, retrieve the value of the **StorageConnectionString** application setting by using the **Environment.GetEnvironmentVariable** method and storing the result in a **string** variable named **connectionString**:

    ```csharp
    string connectionString = Environment.GetEnvironmentVariable("StorageConnectionString");
    ```

1. Finally, return the value of the **connectionString** variable as the HTTP response:

    ```csharp
    return new OkObjectResult(connectionString);
    ```

1. **Save** the **Echo.cs** file.

#### Task 4: Validate the local function

1. Open the **Windows Terminal** application.
1. Change the current directory to the **Allfiles (F):\\Allfiles\\Labs\\07\\Starter\\func** project directory.
1. Start the function app project:

    ```powershell
    func start --build
    ```

    > **Note**: You can review the documentation to [start the function app project locally][azure-functions-core-tools-start-function] using the **Azure Functions Core Tools**.
1. Open a new instance of the **Windows Terminal** application.
1. Start the **httprepl** tool, and then set the base Uniform Resource Identifier (URI) to ``http://localhost:7071``:

    ```powershell
    httprepl http://localhost:7071
    ```

    > **Note**: An error message is displayed by the httprepl tool. This message occurs because the tool is searching for a Swagger definition file to use to "traverse" the API. Because your functio projectp does not produce a Swagger definition file, you'll need to traverse the API manually.
1. When you receive the tool prompt, browse to the relative **api/fileparser** directory:

    ```powershell
    cd api
    cd fileparser
    ```

1. Run the **get** command:

    ```powershell
    get
    ```

1. Observe the **[TEST VALUE]** value of the **StorageConnectionString** being returned as the result of the HTTP request.

1. Exit the **httprepl** application:

    ```powershell
    exit
    ```

1. Close all currently running instances of the **Windows Terminal** application.

#### Task 5: Deploy using the Azure Functions Core Tools

1. Open the **Windows Terminal** application.
1. Change the current directory to the **Allfiles (F):\\Allfiles\\Labs\\07\\Starter\\func** project directory.
1. Log in to the Azure Command-Line Interface (CLI) by using your Azure credentials:

    ```powershell
    az login
    ```

1. Publish the function app project:

    ```powershell
    func azure functionapp publish <function-app-name>
    ```

    > **Note**: For example, if your **Function App name** is **securefuncstudent**, your command would be ``func azure functionapp publish securefuncstudent``. You can review the documentation to [publish the local function app project][azure-functions-core-tools-publish-azure] using the **Azure Functions Core Tools**.
1. Wait for the deployment to finalize before you move forward with the lab.
1. Close the currently running **Windows Terminal** application.

#### Task 6: Test the Key Vault-derived application setting

1. Sign in to the Azure portal (<https://portal.azure.com>).
1. Access the **securefunc[yourname]** function app that you created earlier in this lab.
1. From the **App Service** blade, locate and open the **Functions** section, and then locate and open the **FileParser** function.
1. In the **Function** blade, select the **Code + Test** option from the **Developer** section.
1. In the function editor, select **Test/Run**.
1. In the pop-up dialog that appears, perform the following actions:
    - In the **HTTP method** list, select **GET**.
1. Select **Run** to test the function.
1. Observe the result of the test run. The result should be your Azure Storage connection string.

> **Review**: In this exercise, you used a service identity to read the value of a secret stored in Key Vault and returned that value as the result of a function app.

### Exercise 4: Access Azure Blob Storage data

#### Task 1: Upload a sample Storage blob

1. Access the **securestor[yourname]** storage account that you created earlier in this lab.
1. Select the **Containers** link in the **Blob service** section, and then create a new container with the following settings:
    - Name: **drop**
    - Public access level: **Blob (anonymous read access for blobs only)**
1. Browse to the new **drop** container, and then select **Upload** to upload the **records.json** file in the **Allfiles (F): \\Allfiles\\Labs\\07\\Starter** folder on your lab VM.
    > **Note:** You should enable the **Overwrite if files already exist** option.
1. Find the metadata for the **records.json** blob by selecting the blob entry in the list of blobs.
1. Using a new browser tab, go to to the URL for the blob, and then find the blob’s contents.
1. Update the container’s access level by changing the **Public access level** to **Private (no anonymous access)**.
1. Using a new browser window or tab, go to to the URL for the blob, and then find the blob’s contents. You should receive an error message indicating that the resource wasn't found.
    > **Note**: If you don't receive the error message, your browser might have cached the file. Refresh the page until you receive the error message.

#### Task 2: Pull and configure the Azure SDK for .NET

1. Open the **Windows Terminal** application.
1. Change the current directory to the **Allfiles (F):\\Allfiles\\Labs\\07\\Starter\\func** project directory.
1. When you receive the open command prompt, add version **12.6.0** of the **Azure.Storage.Blobs** package from NuGet:

    ```powershell
    dotnet add package Azure.Storage.Blobs --version 12.6.0
    ```

    > **Note**: The [Azure.Storage.Blobs](https://www.nuget.org/packages/Azure.Storage.Blobs/12.6.0) NuGet package references the subset of the Azure SDK for .NET required to write code for Azure Blob Storage.
1. Close the currently running **Windows Terminal** application.
1. Using **Visual Studio Code**, open the solution folder found at **Allfiles (F):\\Allfiles\\Labs\\07\\Starter\\func**.
1. Open the **FileParser.cs** file.
1. Add a **using directive** for the **Azure.Storage.Blobs** namespace:

    ```csharp
    using Azure.Storage.Blobs;
    ```

#### Task 3: Write Azure Blob Storage code using the Azure SDK for .NET

1. Within the **Run** method of the **FileParser** class, delete the following line of code:

    ```csharp
    return new OkObjectResult(connectionString);
    ```

1. Still within the **Run** method, create a new instance of the **BlobClient** class by passing in your *connectionString* variable, a  ``"drop"`` string value, and a ``"records.json"`` string value to the constructor:

    ```csharp
    BlobClient blob = new BlobClient(connectionString, "drop", "records.json");
    ```

1. Still within the **Run** method, use the **BlobClient.DownloadAsync** method to download the contents of the referenced blob asynchronously and store the result in a variable named *response*:

    ```csharp
    var response = await blob.DownloadAsync();
    ```

1. Still within the **Run** method, return the value of the various content stored in the *content* variable by using the **FileStreamResult** class constructor:

    ```csharp
    return new FileStreamResult(response?.Value?.Content, response?.Value?.ContentType);
    ```

1. **Save** the **Echo.cs** file.

#### Task 4: Deploy and validate the Azure Functions app

1. Open the **Windows Terminal** application.
1. Change the current directory to the **Allfiles (F):\\Allfiles\\Labs\\07\\Starter\\func** project directory.
1. Log in to the Azure CLI by using your Azure credentials:

    ```powershell
    az login
    ```

1. Publish the function app project again:

    ```powershell
    func azure functionapp publish <function-app-name>
    ```

    > **Note**: As an example, if your **Function App name** is **securefuncstudent**, your command would be ``func azure functionapp publish securefuncstudent``. You can review the documentation to [publish the local function app project][azure-functions-core-tools-publish-azure] using the **Azure Functions Core Tools**.
1. Wait for the deployment to finalize before you move forward with the lab.
1. Close the currently running **Windows Terminal** application.
1. Sign in to the Azure portal (<https://portal.azure.com>).
1. Access the **securefunc[yourname]** function app that you created earlier in this lab.
1. From the **App Service** blade, locate and open the **Functions** section, then locate and open the **FileParser** function.
1. In the **Function** blade, select the **Code + Test** option from the **Developer** section.
1. In the function editor, select **Test/Run**.
1. In the pop-up dialog that appears, perform the following actions:
    - In the **HTTP method** list, select **GET**.
1. Select **Run** to test the function.
1. Observe the results of the test run. The output will contain the content of the **$/drop/records.json** blob stored in your Azure Storage account.

> **Review**: In this exercise, you used C\# code to access a storage account and then downloaded the contents of a blob.

### Exercise 5: Clean up your subscription

#### Task 1: Open Azure Cloud Shell and list resource groups

1. In the portal, select the **Cloud Shell** icon to open a new shell instance.

1. If **Cloud Shell** isn't already configured, configure the shell for Bash by using the default settings.

#### Task 2: Delete a resource group

1. Enter the following command, and then select Enter to delete the **ConfidentialStack** resource group:

    ```powershell
    az group delete --name ConfidentialStack --no-wait --yes
    ```

1. Close the Cloud Shell pane from the portal.

#### Task 3: Close the active application

1. Close the currently running Microsoft Edge application.

> **Review**: In this exercise, you cleaned up your subscription by removing the resource groups that were used in this lab.
