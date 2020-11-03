FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443
EXPOSE 8080

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY ["UserService2/UserService2.csproj", "UserService2/"]
COPY ["Business/BusinessLayer.csproj", "Business/"]
COPY ["DatabaseLayer/DatabaseLayer.csproj", "DatabaseLayer/"]
COPY ["Common/Common.csproj", "Common/"]
RUN dotnet restore "UserService2/UserService2.csproj"
COPY . .
WORKDIR "/src/UserService2"
RUN dotnet build "UserService2.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "UserService2.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "UserService2.dll"]