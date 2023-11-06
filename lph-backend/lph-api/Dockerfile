FROM mcr.microsoft.com/dotnet/sdk:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["lph-api.csproj", "."]
RUN dotnet restore "lph-api.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "lph-api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "lph-api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "lph-api.dll"]

ENV ASPNETCORE_URLS=http://+:80
