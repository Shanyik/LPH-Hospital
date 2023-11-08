FROM mcr.microsoft.com/dotnet/sdk:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["lphh-api.csproj", "."]
RUN dotnet restore "lphh-api.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "lphh-api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "lphh-api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "lphh-api.dll"]

ENV ASPNETCORE_URLS=http://+:80
