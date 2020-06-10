---
lab:
    title: 'Lab: Enhancing a web application by using the Azure Content Delivery Network'
    az204Module: 'Module 13: Integrate caching and content delivery within solutions'
---

# Lab: Enhancing a web application by using the Azure Content Delivery Network
# Student lab manual

## Lab scenario

Your marketing organization has been tasked with building a website landing page to host content about an upcoming edX course. While designing the website, your team decided that multimedia videos and image content would be the ideal way to convey your marketing message. The website is already completed and available using a Docker container, and your team also decided that it would like to use a content delivery network (CDN) to improve the performance of the images, the videos, and the website itself. You have been tasked with using Microsoft Azure Content Delivery Network to improve the performance of both standard and streamed content on the website.

## Objectives

After you complete this lab, you will be able to:

-   Register a Microsoft.CDN resource provider.

-   Create Content Delivery Network resources.

-   Create and configure Content Delivery Network endpoints that are bound to various Azure services.

## Lab setup

-   Estimated time: **45 minutes**

## Instructions

### Before you start

#### Sign in to the lab virtual machine

Ensure that you're signed in to your Windows 10 virtual machine (VM) by using the following credentials:
    
-   Username: **Admin**

-   Password: **Pa55w.rd**

#### Review the installed applications

Find the taskbar on your Windows 10 desktop. The taskbar contains the icon for the application that you'll use in this lab:

-   Microsoft Edge

### Exercise 1: Create Azure resources

#### Task 1: Open the Azure portal

1.  Sign in to the Azure portal (<https://portal.azure.com>).

1.  If this is your first time signing in to the Azure portal, you'll notice a dialog box offering a tour of the portal. Select **Get Started** to skip the tour.

#### Task 2: Create a Storage account

1.  Create a new storage account with the following details:
    
    -	New resource group: **MarketingContent**

    -	Name: **contenthost*[yourname]***

    -	Location: **East US**

    -	Performance: **Standard**

    -	Account kind: **StorageV2 (general purpose v2)**

    -	Replication: **Read-access geo-redundant storage (RA-GRS)**

    -	Access tier: **Hot**

    > **Note**: Wait for Azure to finish creating the storage account before you move forward with the lab. You'll receive a notification when the account is created.
   
#### Task 3: Create a web app by using Azure App Service

1.  Create a new web app with the following details:

    -   Existing resource group: **MarketingContent**
    
    -   Name: **landingpage*[yourname]***

    -   Publish: **Docker Container**

    -	Operating system: **Linux**

    -	Region: **East US**

    -	New App Service plan: **MarketingPlan**
    
    -	SKU and size: **Premium V2 P1v2**

    -	Docker options: **Single Container**

    -	Image source: **Docker Hub**

    -   Access type: **Public**

    -   Image and tag: **microsoftlearning/edx-html-landing-page:latest**
  
    > **Note**: Wait for Azure to finish creating the web app before you move forward with the lab. You'll receive a notification when the app is created.

1.  Access the **landingpage*[yourname]*** web app that you created earlier in this lab.

1.  In the **Settings** section, go to the **Properties** section, and then record the value of the **URL** text box. You'll use this value later in the lab.

#### Review

In this exercise, you created an Azure Storage account and an Azure Web App that you'll use later in this lab.

### Exercise 2: Configure Content Delivery Network and endpoints

#### Task 1: Open Azure Cloud Shell

1.  Open a new Cloud Shell instance in the Azure portal.

1.  If Cloud Shell isn't already configured, configure the shell for Bash by using the default settings.

1.  At the **Cloud Shell** command prompt in the portal, use the **az** command with the **\-\-version** flag to get the version of the Azure Command-Line Interface (Azure CLI) tool.

#### Task 2: Register the Microsoft.CDN provider

1.  Use the **az** command with the **\-\-help** flag to find a list of subgroups and commands at the root level of the Azure CLI.

1.  Use the **az provider** command with the **\-\-help** flag to get a list of commands available for resource providers.

1.  Use the **az provider list** command to list all currently registered providers.

1.  Use the **az provider list** command again with the **\-\-query "[].namespace"** flag to list just the namespaces of the currently registered providers.

1.  Observe the list of currently registered providers. The **Microsoft.CDN** provider isn't currently in the list of providers.

1.  Use the **az provider register** command with the **\-\-help** flag to get the required flags to register a new provider.

1.  Use the **az provider register** command to register a namespace with your current subscription by using the following setting:

    -   Namespace: **Microsoft.CDN**

1.  Close the Cloud Shell pane.

#### Task 3: Create a Content Delivery Network profile

1.  Create a new Content Delivery Network profile with the following details:
    
    -   Name: **contentdeliverynetwork**

    -   Existing resource group: **MarketingContent**

    -	Resource group location: **East US**

    -	Pricing tier: **Standard Akamai**

    -   Create a new CDN endpoint now: **No**
  
    > **Note**: Wait for Azure to finish creating the CDN profile before you move forward with the lab. You'll receive a notification when the app is created.

#### Task 4: Configure Storage containers

1.  Access the **contenthost*[yourname]*** storage account that you created earlier in this lab.

1.  Select the **Containers** link in the **Blob service** section, and then create a new container with the following settings:
    
    -	Name: **media**

    -	Public access level: **Blob (anonymous read access for blobs only)**
    
1.  Create a new container with the following settings:
    
    -	Name: **video**

    -	Public access level: **Blob (anonymous read access for blobs only)**

1.  Observe the updated list of containers.

#### Task 5: Create Content Delivery Network endpoints

1.  Access the **contentdeliverynetwork** CDN profile that you created earlier in this lab, and then select **+ Endpoint**.

1.  Add a new endpoint with the following properties:

    -   Name: **cdnmedia*[yourname]***

    -   Origin type: **Storage**

    -   Origin hostname: **contenthost*[yourname]*.blob.core.windows.net** (the Storage account that you created earlier in this lab)

    -   Origin path: **/media**

    -   Optimized for: **General web delivery**

    > **Note**: Wait for Azure to finish creating the CDN endpoint before you move forward with the lab. You'll receive a notification when the account is created.

1.  Add a new endpoint with the following properties:

    -   Name: **cdnvideo*[yourname]***

    -   Origin type: **Storage**

    -   Origin hostname: **contenthost*[yourname]*.blob.core.windows.net** (the Storage account that you created earlier in this lab)

    -   Origin path: **/video**

    -   Optimized for: **Video on demand media streaming**

    > **Note**: Wait for Azure to finish creating the CDN endpoint before you move forward with the lab. You'll receive a notification when the account is created.

1.  Add a new endpoint with the following properties:

    -   Name: **cdnweb*[yourname]***

    -   Origin type: **Web App**

    -   Origin hostname: **landingpage*[yourname]*.azurewebsites.net** (the Web App that you created earlier in this lab)

    -   Optimized for: **General web delivery**

    > **Note**: Wait for Azure to finish creating the CDN endpoint before you move forward with the lab. You'll receive a notification when the account is created.

#### Review

In this exercise, you registered the resource provider for Content Delivery Network and then used the provider to create both CDN profile and endpoint resources.

### Exercise 3: Upload and configure static web content

#### Task 1: Observe the landing page

1.  Access the **landingpage*[yourname]*** web app that you created earlier in this lab.

1.  Go to the URL for the **landingpage*[yourname]*** web app.

1.  Observe the error message displayed on the screen. The website won't work until you configure the specified settings to reference multimedia content.

1.  Return to the Azure portal.

#### Task 2: Upload Storage blobs

1.  Access the **contenthost*[yourname]*** storage account that you created earlier in this lab.

1.  Select the **Containers** link in the **Blob service** section, and then select the **media** container.

1.  Upload the following files from the **Allfiles (F): \\Allfiles\\Labs\\13\\Starter** folder on your lab VM:

    -   **campus.jpg**
    
    -   **conference.jpg**
    
    -   **poster.jpg**

    > **Note**: We recommend that you enable the **Overwrite if files already exist** option.

1.  Record the value in the **URL** text box. You will use this value later in the lab.

1.  Back in the **Containers** section, select the **video** container.

1.  Upload the **welcome.mp4** file from the **Allfiles (F): \\Allfiles\\Labs\\13\\Starter** folder on your lab VM.

    > **Note**: We recommend that you enable the **Overwrite if files already exist** option.

1.  Record the value in the **URL** text box. You will use this value later in the lab.

#### Task 3: Configure Web App settings

1.  Access the **landingpage*[yourname]*** web app that you created earlier in this lab.

1.  In the **Settings** section, go to the **Configuration** section.

1.  Find and access the **Application Settings** tab in the **Configuration** section.

1.  Create a new application setting by using the following details:
    
    -	Name: **CDNMediaEndpoint**

    -	Value: The **URI** value of the **media** container in the **contenthost*[yourname]*** storage account that you recorded earlier in this lab.
    
    -	Deployment slot setting: **Not selected**

1.  Create a new application setting by using the following details:
    
    -	Name: **CDNVideoEndpoint**

    -	Value: The **URI** value of the **video** container in the **contenthost*[yourname]*** storage account that you recorded earlier in this lab.
    
    -	Deployment slot setting: **Not selected**

1.  Save your changes to the application settings.

#### Task 4: Validate the corrected landing page

1.  Access the **landingpage*[yourname]*** web app that you created earlier in this lab.

1.  **Restart** the currently running Web App.

    > **Note**: Wait for the restart operation to complete before you move forward with the lab. You'll receive a notification when the operation is done.

1.  Go to the URL for the **landingpage*[yourname]*** web app.

1.  Observe the updated website rendering multimedia content of various types.

1.  Return to the Azure portal.

#### Review

In this exercise, you uploaded multimedia content as blobs to Storage containers and then updated your Web App to point directly to the storage blobs.

### Exercise 4: Use Content Delivery Network endpoints

#### Task 1: Retrieve endpoint Uniform Resource Identifiers (URIs)

1.  Access the **contentdeliverynetwork** CDN profile that you created earlier in this lab.

1.  Select the **cdnmedia*[yourname]*** endpoint.

1.  Copy the value of the **Endpoint hostname** text box. You will use this value later in the lab. 

1.  Select the **cdnvideo*[yourname]*** endpoint.

1.  Copy the value of the **Endpoint hostname** text box. You will use this value later in the lab. 

#### Task 2: Test multimedia content by using the CDN

1.  Construct a URL for the **campus.jpg** resource by combining the **Endpoint hostname** URL from the **cdnmedia*[yourname]*** endpoint that you copied earlier in the lab with a relative path of **/campus.jpg**.

    > **Note**: For example, if your **Endpoint hostname** URL is **https://cdnmediastudent.azureedge.net/**, your newly constructed URL would be **https://cdnmediastudent.azureedge.net/campus.jpg**.

1.  Construct a URL for the **conference.jpg** resource by combining the **Endpoint hostname** URL from the **cdnmedia*[yourname]*** endpoint that you copied earlier in the lab with a relative path of **/conference.jpg**.

    > **Note**: For example, if your **Endpoint hostname** URL is **https://cdnmediastudent.azureedge.net/**, your newly constructed URL would be **https://cdnmediastudent.azureedge.net/conference.jpg**.

1.  Construct a URL for the **poster.jpg** resource by combining the **Endpoint hostname** URL from the **cdnmedia*[yourname]*** endpoint that you copied earlier in the lab with a relative path of **/poster.jpg**.

    > **Note**: For example, if your **Endpoint hostname** URL is **https://cdnmediastudent.azureedge.net/**, your newly constructed URL would be **https://cdnmediastudent.azureedge.net/poster.jpg**.

1.  Construct a URL for the **welcome.mp4** resource by combining the **Endpoint hostname** URL from the **cdnvideo*[yourname]*** endpoint that you copied earlier in the lab with a relative path of **/welcome.mp4**.

    > **Note**: For example, if your **Endpoint hostname** URL is **https://cdnvideostudent.azureedge.net/**, your newly constructed URL would be **https://cdnvideostudent.azureedge.net/welcome.mp4**.

1.  Open a new **Microsoft Edge** browser window.

1.  In the new browser window, go to the URL that you constructed for the **campus.jpg** media resource, and then verify that the resource was successfully found.

    > **Note**: If the content isn't available yet, the CDN endpoint is still initializing. This initialization process can take anywhere from 5 to 15 minutes.

1.  Go to the URL that you constructed for the **conference.jpg** media resource, and then verify that the resource was successfully found.

1.  Go to the URL that you constructed for the **poster.jpg** media resource, and then verify that the resource was successfully found.

1.  Go to the URL that you constructed for the **welcome.mp4** video resource, and then verify that the resource was successfully found.

1.  Close the browser window that you created in this task.

#### Task 3: Update the Web App settings

1.  Access the **landingpage*[yourname]*** web app that you created earlier in this lab.

1.  In the **Settings** section, go to the **Configuration** section.

1.  Find and access the **Application Settings** tab in the **Configuration** section.

1.  Update the **CDNMediaEndpoint** application setting by changing its value to the **Endpoint hostname** URL from the **cdnmedia*[yourname]*** endpoint that you copied earlier in the lab.

1.  Update the **CDNVideoEndpoint** application setting by changing its value to the **Endpoint hostname** URL from the **cdnvideo*[yourname]*** endpoint that you copied earlier in the lab.

1.  Save your changes to the application settings.

1.  Restart the currently running Web App.

    > **Note**: Wait for the restart operation to complete before you move forward with the lab. You'll receive a notification when the operation is done.

#### Task 4: Test the web content

1.  Access the **contentdeliverynetwork** CDN profile that you created earlier in this lab.

1.  Select the **cdnweb*[yourname]*** endpoint.

1.  Copy the value of the **Endpoint hostname** text box. You will use this value later in the lab.

1.  Open a new **Microsoft Edge** browser window.

1.  In the new browser window, go to the **Endpoint hostname** URL for the **cdnweb*[yourname]*** endpoint.

1.  Observe the website and multimedia content that are all served by using Content Delivery Network.

#### Review

In this exercise, you updated your Web App to use Content Delivery Network to serve multimedia content and to serve the web application itself.

### Exercise 5: Clean up your subscription 

#### Task 1: Open Azure Cloud Shell and list resource groups

1.  In the portal, select the **Cloud Shell** icon to open a new shell instance.

1.  If Cloud Shell isn't already configured, configure the shell for Bash by using the default settings.

#### Task 2: Delete a resource group

1.  At the command prompt, enter the following command, and then select Enter to delete the **MarketingContent** resource group:

    ```
    az group delete --name MarketingContent --no-wait --yes
    ```
    
1.  Close the Cloud Shell pane in the portal.

#### Task 3: Close the active application

1.     the currently running Microsoft Edge application.

#### Review

In this exercise, you cleaned up your subscription by removing the resource group used in this lab.
