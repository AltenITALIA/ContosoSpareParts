FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY SpareParts.Part.Api/SpareParts.Part.Api.csproj SpareParts.Part.Api/
COPY SpareParts.Part.EntityFramework/SpareParts.Part.EntityFramework.csproj SpareParts.Part.EntityFramework/
COPY SpareParts.Part.ReadModel/SpareParts.Part.ReadModel.csproj SpareParts.Part.ReadModel/
COPY SpareParts.EntityFramework/SpareParts.EntityFramework.csproj SpareParts.EntityFramework/
COPY SpareParts.Repository/SpareParts.Repository.csproj SpareParts.Repository/
COPY SpareParts.DataAccessObject/SpareParts.DataAccessObject.csproj SpareParts.DataAccessObject/
COPY SpareParts.Part.DomainModel/SpareParts.Part.DomainModel.csproj SpareParts.Part.DomainModel/
COPY SpareParts.Part.Cqrs.Handlers/SpareParts.Part.Cqrs.Handlers.csproj SpareParts.Part.Cqrs.Handlers/
COPY SpareParts.Part.Cqrs/SpareParts.Part.Cqrs.csproj SpareParts.Part.Cqrs/
COPY SpareParts.Cqrs/SpareParts.Cqrs.csproj SpareParts.Cqrs/
COPY SpareParts.Common/SpareParts.Common.csproj SpareParts.Common/
RUN dotnet restore SpareParts.Part.Api/SpareParts.Part.Api.csproj
COPY . .
WORKDIR /src/SpareParts.Part.Api
RUN dotnet build SpareParts.Part.Api.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish SpareParts.Part.Api.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "SpareParts.Part.Api.dll"]
