---
lab:
    title: 'Lab: Implement task processing logic by using Azure Functions'
    az204Module: 'Module 02: Implement Azure Functions'
    az020Module: 'Module 02: Implement Azure Functions'
    type: 'Answer Key'
---

# Lab: Implement task processing logic by using Azure Functions | Student lab answer key

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
1. In the open browser window, go to the Azure portal (<https://portal.azure.com>).
1. Enter the email address for your Microsoft account, and then select **Next**.
1. Enter the password for your Microsoft account, and then select **Sign in**.
    > **Note**: If this is your first time signing in to the Azure portal, you'll be offered a tour of the portal. If you prefer to skip the tour, select **Get Started** to begin using the portal.

#### Task 2: Create an Azure Storage account

1. In the Azure portal's navigation pane, select **All services**.
1. On the **All services** blade, select **Storage Accounts**.
1. On the **Storage accounts** blade, get your list of storage account instances.
1. On the **Storage accounts** blade, select **Add**.
1. On the **Create storage account** blade, observe the tabs on the blade, such as **Basics**, **Tags**, and **Review + Create**.
    > **Note**: Each tab represents a step in the workflow to create a new storage account. You can select **Review + Create** at any time to skip the remaining tabs.
1. Select the **Basics** tab, and then in the tab area, perform the following actions:
    1. Leave the **Subscription** text box set to its default value.
    1. In the **Resource group** section, select **Create new**, enter **Serverless**, and then select **OK**.
    1. In the **Storage account name** text box, enter **funcstor[yourname]**.
    1. In the **Location** list, select the **(US) East US** region.
    1. In the **Performance** section, select **Standard**.
    1. In the **Account kind** list, select **StorageV2 (general purpose v2)**.
    1. In the **Replication** list, select **Locally-redundant storage (LRS)**.
    1. In the **Access tier (default)** section, ensure that **Hot** is selected.
    1. Select **Review + Create**.
1. On the **Review + Create** tab, review the options that you specified in the previous steps.
1. Select **Create** to create the storage account by using your specified configuration.
    > **Note**: On the **Deployment** blade, wait for the creation task to complete before moving forward with this lab.
1. In the Azure portal's navigation pane, select **All services**.
1. On the **All services** blade, select **Storage Accounts**.
1. On the **Storage accounts** blade, select the **funcstor[yourname]** storage account instance.
1. From the **Storage account** blade, find the **Settings** section, and then select **Access keys**.
1. From the **Access keys** blade, select any one of the keys, and then record the value of either of the **Connection string** boxes.
    > **Note**: You'll use this value later in the lab. It doesn't matter which connection string you choose. They are interchangeable.

#### Task 3: Create a Function app

1. In the Azure portal's navigation pane, select the **Create a resource** link.
1. From the **New** blade, find the **Search the Marketplace** text box.
1. In the search box, enter **Function**, and then select Enter.
1. On the **Everything** search results blade, select the **Function App** result.
1. On the **Function App** blade, select **Create**.
1. Find the tabs on the **Function App** blade, such as **Basics**.
    > **Note**: Each tab represents a step in the workflow to create a new function app. You can select **Review + Create** at any time to skip the remaining tabs.
1. On the **Basics** tab, perform the following actions:
    1. Leave the **Subscription** text box set to its default value.
    1. In the **Resource group** section, select **Use existing**, and then select **Serverless** in the list.
    1. In the **Function app name** text box, enter **funclogic[yourname]**.
    1. In the **Publish** section, select **Code**.
    1. In the **Runtime stack** drop-down list, select **.NET Core**.
    1. In the **Version** drop-down list, select **3.1**.
    1. In the **Region** drop-down list, select the **East US** region.
    1. Select **Next: Hosting**.
1. On the **Hosting** tab, perform the following actions:
    1. In the **Operating System** section, select **Linux**.
    1. In the **Storage account** drop-down list, select the **funcstor[yourname]** storage account that you created earlier in this lab.
    1. In the **Plan type** drop-down list, select the **Consumption** option.
    1. Select **Review + Create**.
1. On the **Review + Create** tab, review the options that you selected during the previous steps.
1. Select **Create** to create the function app by using your specified configuration.
    > **Note**: Wait for the creation task to complete before you move forward with this lab.

> **Review**: In this exercise, you created all the resources that you'll use for this lab.

### Exercise 2: Configure a local Azure Functions project

#### Task 1: Initialize a function project

1. On the taskbar, select the **Windows Terminal** icon.
1. Enter the following command, and then select Enter to change the current directory to the **Allfiles (F):\\Allfiles\\Labs\\02\\Starter\\func** empty directory:

    ```powershell
    cd F:\Allfiles\Labs\02\Starter\func
    ```

1. When you receive the open command prompt, enter the following command, and then select Enter to use the **Azure Functions Core Tools** to create a new local Azure Functions project in the current directory using the **dotnet** runtime:

    ```powershell
    func init --worker-runtime dotnet
    ```

    > **Note**: You can review the documentation to [create a new project][azure-functions-core-tools-new-project] using the **Azure Functions Core Tools**.
1. Close the currently running **Windows Terminal** application.

#### Task 2: Configure connection string

1. On the **Start** screen, select the **Visual Studio Code** tile.
1. From the **File** menu, select **Open Folder**.
1. In the **File Explorer** window that opens, browse to **Allfiles (F):\\Allfiles\\Labs\\02\\Starter\\func**, and then select **Select Folder**.
1. In the Explorer pane of the **Visual Studio Code** window, open the **local.settings.json** file.
1. Observe the current value of the **AzureWebJobsStorage** setting:

    ```json
    "AzureWebJobsStorage": "UseDevelopmentStorage=true",
    ```

1. Update the value of the **AzureWebJobsStorage** by setting it to the **connection string** of the storage account that you recorded earlier in this lab.

#### Task 3: Build and validate a project

1. On the taskbar, select the **Windows Terminal** icon.
1. Enter the following command, and then select Enter to change the current directory to the **Allfiles (F):\\Allfiles\\Labs\\02\\Starter\\func** empty directory:

    ```powershell
    cd F:\Allfiles\Labs\02\Starter\func
    ```

1. When you receive the open command prompt, enter the following command, and then select Enter to **build** the .NET Core 3.1 project:

    ```powershell
    dotnet build
    ```

> **Review**: In this exercise, you created a local project that you'll use for Azure Functions development.

### Exercise 3: Create a function that's triggered by an HTTP request

#### Task 1: Create an HTTP-triggered function

1. On the taskbar, select the **Windows Terminal** icon.
1. Enter the following command, and then select Enter to change the current directory to the **Allfiles (F):\\Allfiles\\Labs\\02\\Starter\\func** empty directory:

    ```powershell
    cd F:\Allfiles\Labs\02\Starter\func
    ```

1. When you receive the open command prompt, enter the following command, and then select Enter to use the **Azure Functions Core Tools** to create a new function named **Echo** using the **HTTP trigger** template:

    ```powershell
    func new --template "HTTP trigger" --name "Echo"
    ```

    > **Note**: You can review the documentation to [create a new function][azure-functions-core-tools-new-function] using the **Azure Functions Core Tools**.
1. Close the currently running **Windows Terminal** application.

#### Task 2: Write HTTP-triggered function code

1. On the **Start** screen, select the **Visual Studio Code** tile.
1. From the **File** menu, select **Open Folder**.
1. In the **File Explorer** window that opens, browse to **Allfiles (F):\\Allfiles\\Labs\\02\\Starter\\func**, and then select **Select Folder**.
1. In the Explorer pane of the **Visual Studio Code** window, open the **Echo.cs** file.
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
        public static class Echo
        {
            [FunctionName("Echo")]
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

1. Delete all the content within the **Echo.cs** file.
1. Add the following lines of code to add **using directives** for the **Microsoft.AspNetCore.Mvc**, **Microsoft.Azure.WebJobs**, **Microsoft.AspNetCore.Http**, and **Microsoft.Extensions.Logging** namespaces:

    ```csharp
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Azure.WebJobs;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Logging;
    ```

1. Create a new **public static** class named **Echo**:

    ```csharp
    public static class Echo
    { }
    ```

1. Observe the **Echo.cs** file again, which should now include:

    ```csharp
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Azure.WebJobs;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Logging;

    public static class Echo
    { }
    ```

1. Within the **Echo** class, add the following code block to create a new **public static** method named **Run** that returns a variable of type **IActionResult** and that also takes in variables of type **HttpRequest** and **ILogger** as parameters named *request* and *logger*:

    ```csharp
    public static IActionResult Run(
        HttpRequest request,
        ILogger logger)
    { }
    ```

1. Add the following code to append an attribute to the **Run** method of type **FunctionNameAttribute** that has its **name** parameter set to a value of **Echo**:

    ```csharp
    [FunctionName("Echo")]
    public static IActionResult Run(
        HttpRequest request,
        ILogger logger)
    { }
    ```

1. Add the following code to append an attribute to the **request** parameter of type **HttpTriggerAttribute** that has its **methods** parameter array set to a single value of **POST**:

    ```csharp
    [FunctionName("Echo")]
    public static IActionResult Run(
        [HttpTrigger("POST")] HttpRequest request,
        ILogger logger)
    { }
    ```

1. Observe the **Echo.cs** file again, which should now include:

    ```csharp
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Azure.WebJobs;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Logging;

    public static class Echo
    {
        [FunctionName("Echo")]
        public static IActionResult Run(
            [HttpTrigger("POST")] HttpRequest request,
            ILogger logger)
        { }
    }
    ```

1. In the **Run** method, enter the following line of code to log a fixed message:

    ```csharp
    logger.LogInformation("Received a request");
    ```

1. Enter the following line of code to echo the body of the HTTP request as the HTTP response:

    ```csharp
    return new OkObjectResult(request.Body);
    ```

1. Observe the **Echo.cs** file again, which should now include:

    ```csharp
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Azure.WebJobs;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Logging;

    public static class Echo
    {
        [FunctionName("Echo")]
        public static IActionResult Run(
            [HttpTrigger("POST")] HttpRequest request,
            ILogger logger)
        {
            logger.LogInformation("Received a request");
            return new OkObjectResult(request.Body);
        }
    }
    ```

1. Select **Save** to save your changes to the **Echo.cs** file.

#### Task 3: Test the HTTP-triggered function by using httprepl

1. On the taskbar, select the **Windows Terminal** icon.
1. Enter the following command, and then select Enter to change the current directory to the **Allfiles (F):\\Allfiles\\Labs\\02\\Starter\\func** empty directory:

    ```powershell
    cd F:\Allfiles\Labs\02\Starter\func
    ```

1. When you receive the open command prompt, enter the following command, and then select Enter to run the function app project:

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

1. Enter the following command, and then select Enter to browse to the relative **echo** directory:

    ```powershell
    cd echo
    ```

1. Enter the following command, and then select Enter to run the **post** command sending in an HTTP request body set to a numeric value of **3** by using the **\-\-content** option:

    ```powershell
    post --content 3
    ```

1. Enter the following command, and then select Enter to run the **post** command sending in an HTTP request body set to a numeric value of **5** by using the **\-\-content** option:

    ```powershell
    post --content 5
    ```

1. Enter the following command, and then select Enter to run the **post** command sending in an HTTP request body set to a string value of **Hello** by using the **\-\-content** option:

    ```powershell
    post --content "Hello"
    ```

1. Enter the following command, and then select Enter to run the **post** command sending in an HTTP request body set to a JavaScript Object Notation (JSON) value of **{"msg": "Successful"}** by using the **\-\-content** option:

    ```powershell
    post --content "{"msg": "Successful"}"
    ```

1. Enter the following command, and then select Enter to exit the **httprepl** application:

    ```powershell
    exit
    ```

1. Close all currently running instances of the **Windows Terminal** application.

> **Review**: In this exercise, you created a basic function that echoes the content sent via an HTTP POST request.

### Exercise 4: Create a function that triggers on a schedule

#### Task 1: Create a schedule-triggered function

1. On the taskbar, select the **Windows Terminal** icon.
1. Enter the following command, and then select Enter to change the current directory to the **Allfiles (F):\\Allfiles\\Labs\\02\\Starter\\func** empty directory:

    ```powershell
    cd F:\Allfiles\Labs\02\Starter\func
    ```

1. When you receive the open command prompt, enter the following command, and then select Enter to use the **Azure Functions Core Tools** to create a new function named **Recurring** using the **Timer trigger** template:

    ```powershell
    func new --template "Timer trigger" --name "Recurring"
    ```

    > **Note**: You can review the documentation to [create a new function][azure-functions-core-tools-new-function] using the **Azure Functions Core Tools**.
1. Close the currently running **Windows Terminal** application.

#### Task 2: Observe function code

1. On the **Start** screen, select the **Visual Studio Code** tile.
1. From the **File** menu, select **Open Folder**.
1. In the **File Explorer** window that opens, browse to **Allfiles (F):\\Allfiles\\Labs\\02\\Starter\\func**, and then select **Select Folder**.
1. In the Explorer pane of the **Visual Studio Code** window, open the **Recurring.cs** file.
1. In the code editor, observe the implementation:

    ```csharp
    using System;
    using Microsoft.Azure.WebJobs;
    using Microsoft.Azure.WebJobs.Host;
    using Microsoft.Extensions.Logging;

    namespace func
    {
        public static class Recurring
        {
            [FunctionName("Recurring")]
            public static void Run([TimerTrigger("0 */5 * * * *")]TimerInfo myTimer, ILogger log)
            {
                log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");
            }
        }
    }
    ```

#### Task 3: Observe function runs

1. On the taskbar, select the **Windows Terminal** icon.
1. Enter the following command, and then select Enter to change the current directory to the **Allfiles (F):\\Allfiles\\Labs\\02\\Starter\\func** empty directory:

    ```powershell
    cd F:\Allfiles\Labs\02\Starter\func
    ```

1. When you receive the open command prompt, enter the following command, and then select Enter to run the function app project:

    ```powershell
    func start --build
    ```

    > **Note**: You can review the documentation to [start the function app project locally][azure-functions-core-tools-start-function] using the **Azure Functions Core Tools**.
1. Observe the function run that occurs about every five minutes. Each function run should render a simple message to the log.
1. Close the currently running **Windows Terminal** application.

#### Task 4: Update the function integration configuration

1. On the **Start** screen, select the **Visual Studio Code** tile.
1. From the **File** menu, select **Open Folder**.
1. In the **File Explorer** window that opens, browse to **Allfiles (F):\\Allfiles\\Labs\\02\\Starter\\func**, and then select **Select Folder**.
1. In the Explorer pane of the **Visual Studio Code** window, open the **Recurring.cs** file.
1. In the code editor, observe the existing **Run** method signature:

    ```csharp
    [FunctionName("Recurring")]
    public static void Run([TimerTrigger("0 */5 * * * *")]TimerInfo myTimer, ILogger log)
    ```

1. Update the **Run** method signature code block to change the schedule to execute once every **30 seconds**:

    ```csharp
    [FunctionName("Recurring")]
    public static void Run([TimerTrigger("*/30 * * * * *")]TimerInfo myTimer, ILogger log)
    ```

1. Select **Save** to save your changes to the **Recurring.cs** file.

#### Task 5: Observe function runs

1. On the taskbar, select the **Windows Terminal** icon.
1. Enter the following command, and then select Enter to change the current directory to the **Allfiles (F):\\Allfiles\\Labs\\02\\Starter\\func** empty directory:

    ```powershell
    cd F:\Allfiles\Labs\02\Starter\func
    ```

1. When you receive the open command prompt, enter the following command, and then select Enter to run the function app project:

    ```powershell
    func start --build
    ```

    > **Note**: You can review the documentation to [start the function app project locally][azure-functions-core-tools-start-function] using the **Azure Functions Core Tools**.
1. Observe the function run that occurs about every 30 seconds. Each function run should render a simple message to the log.
1. Close the currently running **Windows Terminal** application.

> **Review**: In this exercise, you created a function that runs automatically based on a fixed schedule.

### Exercise 5: Create a function that integrates with other services

#### Task 1: Upload sample content to Azure Blob Storage

1. In the Azure portal's navigation pane, select the **Resource groups** link.
1. On the **Resource groups** blade, find and then select the **Serverless** resource group that you created earlier in this lab.
1. On the **Serverless** blade, select the **funcstor[yourname]** storage account that you created earlier in this lab.
1. On the **Storage account** blade, select the **Containers** link in the **Blob service** section.
1. In the **Containers** section, select **+ Container**.
1. In the **New container** pop-up window, perform the following actions:
    1. In the **Name** text box, enter **content**.
    1. In the **Public access level** drop-down list, select **Private (no anonymous access)**.
    1. Select **OK**.
1. Return to the **Containers** section, and then select the recently created **content** container.
1. On the **Container** blade, select **Upload**.
1. In the **Upload blob** window, perform the following actions:
    1. In the **Files** section, select the **Folder** icon.
    1. In the **File Explorer** window, browse to **Allfiles (F):\\Allfiles\\Labs\\02\\Starter**, select the **settings.json** file, and then select **Open**.
    1. Ensure that the **Overwrite if files already exist** check box is selected, and then select **Upload**.
      > **Note**: Wait for the blob to upload before you continue with this lab.

#### Task 2: Create a Blob-triggered function

1. On the taskbar, select the **Windows Terminal** icon.
1. Enter the following command, and then select Enter to change the current directory to the **Allfiles (F):\\Allfiles\\Labs\\02\\Starter\\func** empty directory:

    ```powershell
    cd F:\Allfiles\Labs\02\Starter\func
    ```

1. When you receive the open command prompt, enter the following command, and then select Enter to use the **Azure Functions Core Tools** to create a new function named **GetSettingInfo** using the **Blob trigger** template:

    ```powershell
    func new --template "Blob trigger" --name "GetSettingInfo"
    ```

    > **Note**: You can review the documentation to [create a new function][azure-functions-core-tools-new-function] using the **Azure Functions Core Tools**.
1. Close the currently running **Windows Terminal** application.

#### Task 3: Write Blob-inputted function code

1. On the **Start** screen, select the **Visual Studio Code** tile.
1. From the **File** menu, select **Open Folder**.
1. In the **File Explorer** window that opens, browse to **Allfiles (F):\\Allfiles\\Labs\\02\\Starter\\func**, and then select **Select Folder**.
1. In the Explorer pane of the **Visual Studio Code** window, open the **GetSettingInfo.cs** file.
1. In the code editor, observe the example implementation:

    ```csharp
    using System;
    using System.IO;
    using Microsoft.Azure.WebJobs;
    using Microsoft.Azure.WebJobs.Host;
    using Microsoft.Extensions.Logging;

    namespace func
    {
        public static class GetSettingInfo
        {
            [FunctionName("GetSettingInfo")]
            public static void Run([BlobTrigger("samples-workitems/{name}", Connection = "")]Stream myBlob, string name, ILogger log)
            {
                log.LogInformation($"C# Blob trigger function Processed blob\n Name:{name} \n Size: {myBlob.Length} Bytes");
            }
        }
    }
    ```

1. Delete all the content within the **GetSettingInfo.cs** file.

1. Add the following lines of code to add **using directives** for the **Microsoft.AspNetCore.Http**, **Microsoft.AspNetCore.Mvc**, and **Microsoft.Azure.WebJobs** namespaces:

    ```csharp
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Azure.WebJobs;
    ```

1. Create a new **public static** class named **GetSettingInfo**:

    ```csharp
    public static class GetSettingInfo
    { }
    ```

1. Observe the **GetSettingInfo.cs** file again, which should now include:

    ```csharp
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Azure.WebJobs;

    public static class GetSettingInfo
    { }
    ```

1. Within the **GetSettingInfo** class, add the following code block to create a new **public static** expression-bodied method named **Run** that returns a variable of type **IActionResult** and that also takes in variables of type **HttpRequest** and **string** as parameters named *request* and *json*:

    ```csharp
    public static IActionResult Run(
        HttpRequest request,
        string json)
        => null;
    ```

    > **Note**: You are only temporarily setting the return value to **null**.

1. Add the following code to append an attribute to the **Run** method of type **FunctionNameAttribute** that has its **name** parameter set to a value of **GetSettingInfo**:

    ```csharp
    [FunctionName("GetSettingInfo")]
    public static IActionResult Run(
        HttpRequest request,
        string json)
        => null;
    ```

1. Add the following code to append an attribute to the **request** parameter of type **HttpTriggerAttribute** that has its **methods** parameter array set to a single value of **GET**:

    ```csharp
    [FunctionName("GetSettingInfo")]
    public static IActionResult Run(
        [HttpTrigger("GET")] HttpRequest request,
        string json)
        => null;
    ```

1. Add the following code to append an attribute to the **json** parameter of type **BlobAttribute** that has its **blobPath** parameter set to a value of **content/settings.json**:

    ```csharp
    [FunctionName("GetSettingInfo")]
    public static IActionResult Run(
        [HttpTrigger("GET")] HttpRequest request,
        [Blob("content/settings.json")] string json)
        => null;
    ```

1. Add the following code to update the **Run** expression-bodied method to return a new instance of the **OkObjectResult** class passing in the value of the **json** method parameter as the sole constructor parameter:

    ```csharp
    [FunctionName("GetSettingInfo")]
    public static IActionResult Run(
        [HttpTrigger("GET")] HttpRequest request,
        [Blob("content/settings.json")] string json)
        => new OkObjectResult(json);
    ```

1. Observe the **GetSettingInfo.cs** file again, which should now include:

    ```csharp
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Azure.WebJobs;

    public static class GetSettingInfo
    {
        [FunctionName("GetSettingInfo")]
        public static IActionResult Run(
            [HttpTrigger("GET")] HttpRequest request,
            [Blob("content/settings.json")] string json)
            => new OkObjectResult(json);
    }
    ```

1. Select **Save** to save your changes to the **GetSettingInfo.cs** file.

#### Task 4: Test the Blob-inputted function by using httprepl

1. On the taskbar, select the **Windows Terminal** icon.
1. Enter the following command, and then select Enter to change the current directory to the **Allfiles (F):\\Allfiles\\Labs\\02\\Starter\\func** empty directory:

    ```powershell
    cd F:\Allfiles\Labs\02\Starter\func
    ```

1. When you receive the open command prompt, enter the following command, and then select Enter to run the function app project:

    ```powershell
    func start --build
    ```

    > **Note**: You can review the documentation to [start the function app project locally][azure-functions-core-tools-start-function] using the **Azure Functions Core Tools**.
1. On the taskbar, select the **Windows Terminal** icon again to open a new instance of the **Windows Terminal** application.
1. When you receive the open command prompt, enter the following command, and then select Enter to start the **httprepl** tool setting the base Uniform Resource Identifier (URI) to ``http://localhost:7071``:

    ```powershell
    httprepl http://localhost:7071
    ```

    > **Note**: An error message is displayed by the httprepl tool. This message occurs because the tool is searching for a Swagger definition file to use to "traverse" the API. Because your functio project does not produce a Swagger definition file, you'll need to traverse the API manually.
1. When you receive the tool prompt, enter the following command, and then select Enter to browse to the relative **api** endpoint:

    ```powershell
    cd api
    ```

1. Enter the following command, and then select Enter to browse to the relative **getsettinginfo** endpoint:

    ```powershell
    cd getsettinginfo
    ```

1. Enter the following command, and then select Enter to run the **get** command for the current endpoint:

    ```powershell
    get
    ```

1. Observe the JSON content of the response from the function app, which should now include:

    ```json
    {
        "version": "0.2.4",
        "root": "/usr/libexec/mews_principal/",
        "device": {
            "id": "21e46d2b2b926cba031a23c6919"
        },
        "notifications": {
            "email": "joseph.price@contoso.com",
            "phone": "(425) 555-0162 x4151"
        }
    }
    ```

1. Enter the following command, and then select Enter to exit the **httprepl** application:

    ```powershell
    exit
    ```

1. Close all currently running instances of the **Windows Terminal** application.

> **Review**: In this exercise, you created a function that returns the content of a JSON file in Storage.

### Exercise 6: Deploy a local function project to an Azure Functions app

#### Task 1: Deploy using the Azure Functions Core Tools

1. On the taskbar, select the **Windows Terminal** icon.
1. Enter the following command, and then select Enter to change the current directory to the **Allfiles (F):\\Allfiles\\Labs\\02\\Starter\\func** empty directory:

    ```powershell
    cd F:\Allfiles\Labs\02\Starter\func
    ```

1. When you receive the open command prompt, enter the following command, and then select Enter to login to the Azure Command-Line Interface (CLI):

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

    > **Note**: For example, if your **Function App name** is **funclogicstudent**, your command would be ``func azure functionapp publish funclogicstudent``. You can review the documentation to [publish the local function app project][azure-functions-core-tools-publish-azure] using the **Azure Functions Core Tools**.
1. Wait for the deployment to finalize before you move forward with the lab.
1. Close the currently running **Windows Terminal** application.

#### Task 2: Validate deployment

1. On the taskbar, select the **Microsoft Edge** icon.
1. In the open browser window, go to the Azure portal (<https://portal.azure.com>).
1. In the Azure portal's navigation pane, select the **Resource groups** link.
1. On the **Resource groups** blade, find and then select the **Serverless** resource group that you created earlier in this lab.
1. On the **Serverless** blade, select the **funclogic[yourname]** function app that you created earlier in this lab.
1. From the **App Service** blade, select the **Functions** option from the **Functions** section.
1. In the **Functions** pane, select the the existing **GetSettingInfo** function.
1. In the **Function** blade, select the **Code + Test** option from the **Developer** section.
1. In the function editor, select **Test/Run**.
1. In the popup dialog that appears, perform the following actions:
    - In the **HTTP method** list, select **GET**.
1. Select **Run** to test the function.
1. Observe the results of the test run. The JSON content should now include:

    ```json
    {
        "version": "0.2.4",
        "root": "/usr/libexec/mews_principal/",
        "device": {
            "id": "21e46d2b2b926cba031a23c6919"
        },
        "notifications": {
            "email": "joseph.price@contoso.com",
            "phone": "(425) 555-0162 x4151"
        }
    }
    ```

> **Review**: In this exercise, you deployed a local function project to Azure Functions and validated that the functions work in Azure.

### Exercise 7: Clean up your subscription

#### Task 1: Open Azure Cloud Shell and list resource groups

1. In the Azure portal's navigation pane, select the **Cloud Shell** icon to open a new shell instance.
    > **Note**: The **Cloud Shell** icon is represented by a greater than sign (\>) and underscore character (\_).
1. If this is your first time opening Cloud Shell using your subscription, you can use the **Welcome to Azure Cloud Shell Wizard** to configure Cloud Shell for first-time usage. Perform the following actions in the wizard:
    1. A dialog box prompts you to create a new storage account to begin using the shell. Accept the default settings, and then select **Create storage**.
    > **Note**: Wait for Cloud Shell to finish its initial setup procedures before moving forward with the lab. If Cloud Shell configuration options don't display, this is most likely because you're using an existing subscription with this course's labs. The labs are written with the presumption that you're using a new subscription.

#### Task 2: Delete a resource group

1. When you receive the command prompt, enter the following command, and then select Enter to delete the **Serverless** resource group:

    ```powershell
    az group delete --name Serverless --no-wait --yes
    ```

1. Close the Cloud Shell pane in the portal.

#### Task 3: Close the active application

1. Close the currently running Microsoft Edge application.

> **Review**: In this exercise, you cleaned up your subscription by removing the resource group that was used in this lab.

[azure-functions-core-tools-new-function]: https://docs.microsoft.com/azure/azure-functions/functions-run-local#create-func
[azure-functions-core-tools-new-project]: https://docs.microsoft.com/azure/azure-functions/functions-run-local#create-a-local-functions-project
[azure-functions-core-tools-start-function]: https://docs.microsoft.com/azure/azure-functions/functions-run-local#start
[azure-functions-core-tools-publish-azure]: https://docs.microsoft.com/azure/azure-functions/functions-run-local#publish
