FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["DeliveryDemoService.csproj", "./"]
RUN dotnet restore "DeliveryDemoService.csproj"
COPY . .
WORKDIR "/src/"
RUN dotnet build "DeliveryDemoService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DeliveryDemoService.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DeliveryDemoService.dll"]
