FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443
EXPOSE 160

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY ["AirlineService/AirlineService.csproj", "AirlineService/"]
COPY ["Business/Business.csproj", "Business/"]
COPY ["DatabaseLayer/DatabaseLayer.csproj", "DatabaseLayer/"]
COPY ["Common/Common.csproj", "Common/"]
RUN dotnet restore "AirlineService/AirlineService.csproj"
COPY . .
WORKDIR "/src/AirlineService"
RUN dotnet build "AirlineService.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "AirlineService.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "AirlineService.dll"]