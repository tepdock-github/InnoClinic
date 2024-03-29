#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 5010
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["AuthorizationService/AuthorizationService.csproj", "AuthorizationService/"]
COPY ["Authorization.Data/Authorization.Data.csproj", "Authorization.Data/"]
COPY ["Authorization.Domain/Authorization.Domain.csproj", "Authorization.Domain/"]
COPY ["EmailService/EmailService.csproj", "EmailService/"]
RUN dotnet restore "AuthorizationService/AuthorizationService.csproj"
COPY . .
WORKDIR "/src/AuthorizationService"
RUN dotnet build "AuthorizationService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AuthorizationService.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AuthorizationService.dll"]