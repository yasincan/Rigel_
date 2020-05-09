FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS build
WORKDIR /src
COPY ["Rigel.Web/Rigel.Web.csproj", "Rigel.Web/"]
RUN dotnet restore "Rigel.Web/Rigel.Web.csproj"
COPY . .
WORKDIR "/src/Rigel.Web"
RUN dotnet build "Rigel.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Rigel.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Rigel.Web.dll"]