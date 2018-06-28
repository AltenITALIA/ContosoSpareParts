FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY SpareParts.Vehicle.Api/SpareParts.Vehicle.Api.csproj SpareParts.Vehicle.Api/
COPY SpareParts.Vehicle.Cqrs/SpareParts.Vehicle.Cqrs.csproj SpareParts.Vehicle.Cqrs/
COPY SpareParts.Cqrs/SpareParts.Cqrs.csproj SpareParts.Cqrs/
COPY SpareParts.Common/SpareParts.Common.csproj SpareParts.Common/
COPY SpareParts.Vehicle.EntityFramework/SpareParts.Vehicle.EntityFramework.csproj SpareParts.Vehicle.EntityFramework/
COPY SpareParts.Vehicle.ReadModel/SpareParts.Vehicle.ReadModel.csproj SpareParts.Vehicle.ReadModel/
COPY SpareParts.EntityFramework/SpareParts.EntityFramework.csproj SpareParts.EntityFramework/
COPY SpareParts.Repository/SpareParts.Repository.csproj SpareParts.Repository/
COPY SpareParts.DataAccessObject/SpareParts.DataAccessObject.csproj SpareParts.DataAccessObject/
COPY SpareParts.Vehicle.DomainModel/SpareParts.Vehicle.DomainModel.csproj SpareParts.Vehicle.DomainModel/
COPY SpareParts.Vehicle.Cqrs.Handlers/SpareParts.Vehicle.Cqrs.Handlers.csproj SpareParts.Vehicle.Cqrs.Handlers/
RUN dotnet restore SpareParts.Vehicle.Api/SpareParts.Vehicle.Api.csproj
COPY . .
WORKDIR /src/SpareParts.Vehicle.Api
RUN dotnet build SpareParts.Vehicle.Api.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish SpareParts.Vehicle.Api.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "SpareParts.Vehicle.Api.dll"]
