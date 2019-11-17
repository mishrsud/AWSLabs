FROM mcr.microsoft.com/dotnet/core/sdk:2.1 AS build

WORKDIR /app
COPY ./src/MessageProcessor/*.csproj ./MessageProcessor/

WORKDIR /app/MessageProcessor
RUN dotnet restore

WORKDIR /app
COPY src/MessageProcessor/. ./MessageProcessor

WORKDIR /app/MessageProcessor
RUN dotnet publish -c Release -o out/

WORKDIR /app/MessageProcessor.Tests
COPY src/MessageProcessor.Tests/. .

WORKDIR /app/MessageProcessor.Tests
CMD ["dotnet", "test"]