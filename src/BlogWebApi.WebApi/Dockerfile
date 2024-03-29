#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /tmp
COPY ["src/BlogWebApi.WebApi/BlogWebApi.WebApi.csproj", "src/BlogWebApi.WebApi/"]
COPY ["src/BlogWebApi.Application/BlogWebApi.Application.csproj", "src/BlogWebApi.Application/"]
COPY ["src/BlogWebApi.Domain/BlogWebApi.Domain.csproj", "src/BlogWebApi.Domain/"]
COPY ["src/BlogWebApi.Infrastructure/BlogWebApi.Infrastructure.csproj", "src/BlogWebApi.Infrastructure/"]
RUN dotnet restore "src/BlogWebApi.WebApi/BlogWebApi.WebApi.csproj"

COPY . .
WORKDIR "/tmp/src/BlogWebApi.WebApi"
RUN dotnet build "BlogWebApi.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BlogWebApi.WebApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BlogWebApi.WebApi.dll"]