FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY WebApplication/WebApplication.csproj WebApplication/
RUN dotnet restore WebApplication/WebApplication.csproj
COPY . .
WORKDIR /src/WebApplication
RUN dotnet build WebApplication.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish WebApplication.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "WebApplication.dll"]
