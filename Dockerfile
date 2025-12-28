# STAGE 1: Build the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy the solution and project files to restore dependencies
COPY ["CodeFirst.sln", "./"]
COPY ["CodeFirst.ConsoleUI/CodeFirst.ConsoleUI.csproj", "CodeFirst.ConsoleUI/"]
COPY ["CodeFirst.DataAccess/CodeFirst.DataAccess.csproj", "CodeFirst.DataAccess/"]
COPY ["CodeFirst.Models/CodeFirst.Models.csproj", "CodeFirst.Models/"]
COPY ["CodeFirst.DataAccess.Tests/CodeFirst.DataAccess.Tests.csproj", "CodeFirst.DataAccess.Tests/"]

RUN dotnet restore

# Copy the rest of the source code and build
COPY . .
WORKDIR "/src/CodeFirst.ConsoleUI"
RUN dotnet build -c Release -o /app/build

# STAGE 2: Publish
FROM build AS publish
RUN dotnet publish -c Release -o /app/publish

# STAGE 3: Final Runtime Image
FROM mcr.microsoft.com/dotnet/runtime:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Set the entry point for the Console App
ENTRYPOINT ["dotnet", "CodeFirst.ConsoleUI.dll"]
