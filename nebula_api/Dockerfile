# podman build -t nebula-container .
# Use the official image as a parent image.
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env

# Set the working directory.
WORKDIR /app

# Copy everything into the working directory.
COPY . ./

RUN dotnet restore
RUN dotnet build

# RUN dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0

WORKDIR /app
COPY --from=build-env /app/out .

EXPOSE 5290

# Start the application.
# ENTRYPOINT ["dotnet", "nebula_api.dll"]