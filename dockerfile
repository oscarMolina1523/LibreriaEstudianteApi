FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src

COPY LibreriaEstudianteAPISolution.sln ./
COPY LibreriaEstudianteEndPoint/LibreriaEstudianteEndPoint.csproj LibreriaEstudianteEndPoint/
COPY 1.CapaModelo/1.CapaModelo.csproj 1.CapaModelo/
COPY 2.CapaDatos/2.CapaDatos.csproj 2.CapaDatos/

COPY 1.CapaModelo/. 1.CapaModelo/
COPY 2.CapaDatos/. 2.CapaDatos/
COPY LibreriaEstudianteEndPoint/. LibreriaEstudianteEndPoint/

RUN dotnet restore "LibreriaEstudianteAPISolution.sln"
RUN dotnet build "LibreriaEstudianteAPISolution.sln" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "LibreriaEstudianteAPISolution.sln" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish ./
ENTRYPOINT ["dotnet", "LibreriaEstudianteEndPoint.dll"]
