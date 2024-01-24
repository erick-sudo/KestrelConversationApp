#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
# Expose both HTTP and HTTPS ports
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/WebUI/WebUI.csproj", "src/WebUI/"]
COPY ["src/Infrastructure/Infrastructure.csproj", "src/Infrastructure/"]
COPY ["src/Core/Core.csproj", "src/Core/"]
RUN dotnet restore "./src/WebUI/./WebUI.csproj"
COPY . .
WORKDIR "/src/src/WebUI"
RUN dotnet build "./WebUI.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Generate and Configure HTTPS Certificate
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS generate-certificate
WORKDIR /https

# Create a self-signed certificate for development purposes
RUN openssl req -x509 -nodes -newkey rsa:4096 -keyout conversationapp.key -out conversationapp.crt -days 365 -subj "/CN=localhost"


FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./WebUI.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=true

FROM base AS final
ENV ASPNETCORE_ENVIRONMENT=Development
WORKDIR /app
COPY --from=publish /app/publish .

# Copy the generated certificate from the generate-certificate image
COPY --from=generate-certificate /https/conversationapp.crt /https/conversationapp.crt
COPY --from=generate-certificate /https/conversationapp.key /https/conversationapp.key

ENTRYPOINT ["dotnet", "WebUI.dll", "--urls", "http://0.0.0.0:8080", "https://0.0.0.0:8081"]