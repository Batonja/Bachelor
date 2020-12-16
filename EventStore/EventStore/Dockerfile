FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY ["EventStore/EventStore.csproj", "EventStore/"]
RUN dotnet restore "EventStore/EventStore.csproj"
COPY . .
WORKDIR "/src/EventStore"
RUN dotnet build "EventStore.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "EventStore.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "EventStore.dll"]