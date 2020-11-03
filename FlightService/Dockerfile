FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443
EXPOSE 300

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY ["FlightService/FlightService.csproj", "FlightService/"]
COPY ["Business/Business.csproj", "Business/"]
COPY ["DatabaseLayer/DatabaseLayer.csproj", "DatabaseLayer/"]
COPY ["Common/Common.csproj", "Common/"]
RUN dotnet restore "FlightService/FlightService.csproj"
COPY . .
WORKDIR "/src/FlightService"
RUN dotnet build "FlightService.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "FlightService.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "FlightService.dll"]