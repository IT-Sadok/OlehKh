
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /app


COPY ASP.NET_CORE_Project_1/ASP.NET_CORE_Project_1.csproj ./ASP.NET_CORE_Project_1/
RUN dotnet restore ./ASP.NET_CORE_Project_1/ASP.NET_CORE_Project_1.csproj

COPY . ./
RUN dotnet publish ./ASP.NET_CORE_Project_1/ASP.NET_CORE_Project_1.csproj -c Release -o out


FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build-env /app/out .

EXPOSE 8080
ENTRYPOINT ["dotnet", "ASP.NET_CORE_Project_1.dll"]
