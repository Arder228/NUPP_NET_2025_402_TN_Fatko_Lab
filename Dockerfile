FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["ComputerSystem.Common/ComputerSystem.Common.csproj", "ComputerSystem.Common/"]
COPY ["ComputerSystem.Infrastructure/ComputerSystem.Infrastructure.csproj", "ComputerSystem.Infrastructure/"]
COPY ["ComputerSystem.REST/ComputerSystem.REST.csproj", "ComputerSystem.REST/"]
RUN dotnet restore "ComputerSystem.REST/ComputerSystem.REST.csproj"

COPY . .
WORKDIR "/src/ComputerSystem.REST"
RUN dotnet build -c Release --no-restore
RUN dotnet publish -c Release -o /app/publish --no-build

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/publish .
ENV ASPNETCORE_URLS=http://*:8080
EXPOSE 8080
ENTRYPOINT ["dotnet", "ComputerSystem.REST.dll"]