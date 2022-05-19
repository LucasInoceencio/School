#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/School.Api/School.Api.csproj", "School.Api/"]
COPY ["src/School.Infrastructure/School.Infrastructure.csproj", "src/School.Infrastructure/"]
COPY ["src/School.Application/School.Application.csproj", "src/School.Application/"]
COPY ["src/School.Domain/School.Domain.csproj", "src/School.Domain/"]
RUN dotnet restore "School.Api/School.Api.csproj"
COPY . .
WORKDIR "/src/src/School.Api"
RUN dotnet build "School.Api.csproj" -c Release -o /app/build
RUN dotnet tool install --global dotnet-ef

FROM build AS publish
RUN dotnet publish "School.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "School.Api.dll"]