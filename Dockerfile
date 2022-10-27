#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["chart-integracao-ifood/chart-integracao-ifood.csproj", "chart-integracao-ifood/"]
RUN dotnet restore "chart-integracao-ifood/chart-integracao-ifood.csproj"
COPY . .
WORKDIR "/src/chart-integracao-ifood"
RUN dotnet build "chart-integracao-ifood.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "chart-integracao-ifood.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "chart-integracao-ifood.dll", "--server"]