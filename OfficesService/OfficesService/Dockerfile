#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["OfficesService/OfficesService.csproj", "OfficesService/"]
COPY ["OfficesService.Data/OfficesService.Data.csproj", "OfficesService.Data/"]
COPY ["OfficesService.Domain/OfficesService.Domain.csproj", "OfficesService.Domain/"]
RUN dotnet restore "OfficesService/OfficesService.csproj"
COPY . .
WORKDIR "/src/OfficesService"
RUN dotnet build "OfficesService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "OfficesService.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "OfficesService.dll"]