FROM microsoft/aspnetcore:2.0-nanoserver-1709 AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/aspnetcore-build:2.0-nanoserver-1709 AS build
WORKDIR /src
COPY GolfGear/GolfGear.csproj GolfGear/
RUN dotnet restore GolfGear/GolfGear.csproj
COPY . .
WORKDIR /src/GolfGear
RUN dotnet build GolfGear.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish GolfGear.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "GolfGear.dll"]
