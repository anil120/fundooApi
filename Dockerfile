FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY ["SignalIR-Hub/SignalIR-Hub.csproj", "SignalIR-Hub/"]
RUN dotnet restore "SignalIR-Hub/SignalIR-Hub.csproj"
COPY . .
WORKDIR "/src/SignalIR-Hub"
RUN dotnet build "SignalIR-Hub.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "SignalIR-Hub.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "SignalRHub.dll"]
