FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY . .
RUN dotnet restore "02-Services/Authentication/Authentication.csproj"
WORKDIR "/src/02-Services/Authentication"
RUN dotnet build "Authentication.csproj" -c Release -o /app/build
RUN dotnet publish "Authentication.csproj" -c Release -o /app/publish /p:UseAppHost=false


FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

COPY --from=build /app/publish .
COPY --from=build /src/deployment-config/appsettings.json .

    
ENTRYPOINT ["dotnet", "Authentication.dll"]
