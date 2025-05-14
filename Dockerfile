FROM mcr.microsoft.com/dotnet/sdk:9.0-alpine AS build-env
WORKDIR /app

COPY . .
RUN cd Payment.API
RUN dotnet restore --disable-parallel
RUN dotnet publish -c Release -o out --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:9.0-alpine
WORKDIR /app
COPY --from=build-env /app/out .

ENTRYPOINT ["dotnet", "Payment.API.dll"]