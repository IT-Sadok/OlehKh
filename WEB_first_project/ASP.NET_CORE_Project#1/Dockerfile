# Використання базового .NET SDK для збірки
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /app

# Копіювання та відновлення залежностей
COPY *.csproj ./
RUN dotnet restore

# Копіювання решти файлів та публікація застосунку
COPY . ./
RUN dotnet publish -c Release -o out

# Використання .NET Runtime для виконання
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build-env /app/out .

# Відкриття порту та запуск застосунку
EXPOSE 80
ENTRYPOINT ["dotnet", "ASP.NET_CORE_Project_1.dll"]
