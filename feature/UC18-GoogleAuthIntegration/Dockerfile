# -------- BUILD STAGE --------
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy full solution
COPY . .

# Restore dependencies
RUN dotnet restore QuantityMeasurementApi/QuantityMeasurementApi.csproj

# Build & publish
RUN dotnet publish QuantityMeasurementApi/QuantityMeasurementApi.csproj -c Release -o /app/publish

# -------- RUNTIME STAGE --------
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

# Copy published output
COPY --from=build /app/publish .

# Render uses dynamic port
ENV ASPNETCORE_URLS=http://+:10000

# Expose port
EXPOSE 10000

# Run app
ENTRYPOINT ["dotnet", "QuantityMeasurementApi.dll"]