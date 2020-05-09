FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS build
WORKDIR /src
COPY ["Rigel.API/Rigel.API.csproj", "Rigel.API/"]
RUN dotnet restore "Rigel.API/Rigel.API.csproj"
COPY . .
WORKDIR "/src/Rigel.API"
RUN dotnet build "Rigel.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Rigel.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Rigel.API.dll"]