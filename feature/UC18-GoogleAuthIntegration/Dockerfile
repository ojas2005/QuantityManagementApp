# -------- BUILD STAGE --------
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy everything
COPY . .

# Move into API folder
WORKDIR /src/QuantityMeasurementApi

# Restore dependencies
RUN dotnet restore

# Publish app
RUN dotnet publish -c Release -o /app/publish

# -------- RUNTIME STAGE --------
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

COPY --from=build /app/publish .

ENV ASPNETCORE_URLS=http://+:10000

EXPOSE 10000

ENTRYPOINT ["dotnet", "QuantityMeasurementApi.dll"]