FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["ArticleAPI/ArticleAPI.csproj", "ArticleAPI/"]
COPY ["ArticleAPI.Data/ArticleAPI.Data.csproj", "ArticleAPI.Data/"]
COPY ["Common/Common.csproj", "Common/"]
RUN dotnet restore "ArticleAPI/ArticleAPI.csproj"
COPY . .
WORKDIR "/src/ArticleAPI"
RUN dotnet build "ArticleAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ArticleAPI.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ArticleAPI.dll"]