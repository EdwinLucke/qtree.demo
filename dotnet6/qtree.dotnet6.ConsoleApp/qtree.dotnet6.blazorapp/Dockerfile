#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["qtree.dotnet6.blazorapp/qtree.dotnet6.blazorapp.csproj", "qtree.dotnet6.blazorapp/"]
RUN dotnet restore "qtree.dotnet6.blazorapp/qtree.dotnet6.blazorapp.csproj"
COPY . .
WORKDIR "/src/qtree.dotnet6.blazorapp"
RUN dotnet build "qtree.dotnet6.blazorapp.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "qtree.dotnet6.blazorapp.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "qtree.dotnet6.blazorapp.dll"]