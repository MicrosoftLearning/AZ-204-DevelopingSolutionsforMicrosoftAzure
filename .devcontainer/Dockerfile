FROM ubuntu:18.04

# Install pre-reqs

RUN apt-get update
RUN apt-get -y install --no-install-recommends apt-transport-https curl ca-certificates lsb-release gnupg2 
RUN curl -sSL https://packages.microsoft.com/config/ubuntu/18.04/prod.list | tee /etc/apt/sources.list.d/microsoft-prod.list
RUN curl -sSL https://packages.microsoft.com/keys/microsoft.asc | apt-key add -
RUN apt-get update

# Install PowerShell 7

RUN apt-get install -y powershell

# Install Azure PowerShell

RUN pwsh -Command Set-PSRepository -Name PSGallery -InstallationPolicy Trusted
RUN pwsh -Command Install-Module -Name Az -AcceptLicense -AllowClobber -Scope AllUsers

# Install Azure CLI

RUN apt-get install -y azure-cli

# Install Azure Functions Core Tools

RUN apt-get install -y azure-functions-core-tools-3

# Install .NET Core 3.1

RUN apt-get install -y dotnet-sdk-3.1

# Install .NET templates

RUN dotnet new --install Preview.CSharp.Templates::1.0.6