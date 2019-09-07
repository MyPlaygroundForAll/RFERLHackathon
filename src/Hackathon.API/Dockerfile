FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["src/Hackathon.API/Hackathon.API.csproj", "src/Hackathon.API/"]
COPY ["src/Hackathon/Hackathon.csproj", "src/Hackathon/"]
COPY ["src/RFERL.Modules.Framework.WebAPI.Abstractions/RFERL.Modules.Framework.WebAPI.Abstractions.csproj", "src/RFERL.Modules.Framework.WebAPI.Abstractions/"]
COPY ["src/RFERL.Modules.Framework.Common/RFERL.Modules.Framework.Common.csproj", "src/RFERL.Modules.Framework.Common/"]
RUN dotnet restore "src/Hackathon.API/Hackathon.API.csproj"
COPY . .
WORKDIR "/src/src/Hackathon.API"
RUN dotnet build "Hackathon.API.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Hackathon.API.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Hackathon.API.dll"]
