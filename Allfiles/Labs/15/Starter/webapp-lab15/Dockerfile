FROM mcr.microsoft.com/windows/nanoserver:1809  
WORKDIR /app  
COPY . .  
CMD [ "dotnet", "webapp-lab15.dll" ]  


   FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
   WORKDIR /app
   EXPOSE 5151

   ENV ASPNETCORE_URLS=http://+:5151

   FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
   ARG configuration=Release
   WORKDIR /src
   COPY ["webapp-lab15.csproj", "./"]
   RUN dotnet restore "webapp-lab15.csproj"
   COPY . .
   WORKDIR "/src/."
   RUN dotnet build "webapp-lab15.csproj" -c $configuration -o /app/build

   FROM build AS publish
   ARG configuration=Release
   RUN dotnet publish "webapp-lab15.csproj" -c $configuration -o /app/publish /p:UseAppHost=false

   FROM base AS final
   WORKDIR /app
   COPY --from=publish /app/publish .
   ENTRYPOINT ["dotnet", "webapp-lab15.dll"]