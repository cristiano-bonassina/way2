FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS build
WORKDIR /src
COPY ["Way2.Web/Way2.Presentation.WebApi.csproj", "Way2.Web/"]
COPY ["Way2.Domain.Entities/Way2.Domain.Entities.csproj", "Way2.Domain.Entities/"]
COPY ["Way2.Domain.Entities.Abstractions/Way2.Domain.Entities.Abstractions.csproj", "Way2.Domain.Entities.Abstractions/"]
COPY ["Way2.Common/Way2.Common.csproj", "Way2.Common/"]
COPY ["Way2.Application.UseCases.Abstractions/Way2.Application.UseCases.Abstractions.csproj", "Way2.Application.UseCases.Abstractions/"]
COPY ["Way2.Application.Models/Way2.Application.Models.csproj", "Way2.Application.Models/"]
COPY ["Way2.Infrastructure.DependencyResolution/Way2.Infrastructure.DependencyResolution.csproj", "Way2.Infrastructure.DependencyResolution/"]
COPY ["Way2.Application.Validations/Way2.Application.Validations.csproj", "Way2.Application.Validations/"]
COPY ["Way2.Application.Validations.Abstractions/Way2.Application.Validations.Abstractions.csproj", "Way2.Application.Validations.Abstractions/"]
COPY ["Way2.Application.Events/Way2.Application.Events.csproj", "Way2.Application.Events/"]
COPY ["Way2.Infrastructure.Persistence/Way2.Infrastructure.Persistence.csproj", "Way2.Infrastructure.Persistence/"]
COPY ["Way2.Application.Services/Way2.Application.Services.csproj", "Way2.Application.Services/"]
COPY ["Way2.Application.Repositories.Abstractions/Way2.Application.Repositories.Abstractions.csproj", "Way2.Application.Repositories.Abstractions/"]
COPY ["Way2.Application.UseCases/Way2.Application.UseCases.csproj", "Way2.Application.UseCases/"]
RUN dotnet restore "Way2.Web/Way2.Presentation.WebApi.csproj"
COPY . .
WORKDIR "/src/Way2.Web"
RUN dotnet build "Way2.Presentation.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Way2.Presentation.WebApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Way2.Presentation.WebApi.dll"]