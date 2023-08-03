FROM mcr.microsoft.com/dotnet/sdk:7.0 as base
WORKDIR /app
COPY INKIPER.csproj .
RUN dotnet restore
COPY . .
RUN dotnet publish -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:7.0
ENV ASPNETCORE_URLS=http://+5000
WORKDIR /app
COPY --from=base /app/publish .

ENTRYPOINT ["dotnet", "INKIPER.dll"]