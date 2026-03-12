FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["AnnapurnaEnterprises.Api.csproj", "./"]
RUN dotnet restore "AnnapurnaEnterprises.Api.csproj"

COPY . .
RUN dotnet publish "AnnapurnaEnterprises.Api.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/publish .

ENV ASPNETCORE_URLS=http://+:10000
EXPOSE 10000

ENTRYPOINT ["dotnet", "AnnapurnaEnterprises.Api.dll"]