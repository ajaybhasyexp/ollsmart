FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS base
WORKDIR /app
EXPOSE $PORT

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src
COPY ["src/ollsmart.csproj", "src/"]
RUN dotnet restore "src/ollsmart.csproj"

# Setup NodeJs
RUN apt-get update && \
    apt-get install -y wget && \
    apt-get install -y gnupg2 && \
    wget -qO- https://deb.nodesource.com/setup_10.x | bash - && \
    apt-get install -y build-essential nodejs
COPY . .
WORKDIR "/src/src"
RUN dotnet build "ollsmart.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ollsmart.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
# ENTRYPOINT ["dotnet", "OllsMart.dll"]
CMD ASPNETCORE_URLS=http://*:$PORT dotnet OllsMart.dll
