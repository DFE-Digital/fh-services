FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
RUN wget https://github.com/moparisthebest/static-curl/releases/latest/download/curl-amd64 -O /opt/curl && chmod +x /opt/curl
WORKDIR /app

COPY . ./
WORKDIR /app/src/ui/connect-ui
RUN dotnet restore
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:8.0
COPY --from=build-env /opt/curl /usr/bin/curl
WORKDIR /app
COPY --from=build-env /app/src/ui/connect-ui/out .
ENTRYPOINT ["dotnet", "FamilyHubs.Referral.Web.dll"]
