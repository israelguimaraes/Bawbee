#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Bawbee.API/Bawbee.API.csproj", "Bawbee.API/"]
COPY ["Bawbee.Application/Bawbee.Application.csproj", "Bawbee.Application/"]
COPY ["Bawbee.Application.Query/Bawbee.Application.Query.csproj", "Bawbee.Application.Query/"]
COPY ["Bawbee.Domain.Core/Bawbee.Domain.Core.csproj", "Bawbee.Domain.Core/"]
COPY ["Bawbee.Domain/Bawbee.Domain.csproj", "Bawbee.Domain/"]
COPY ["Bawbee.Infra.CrossCutting.Extensions/Bawbee.Infra.CrossCutting.Extensions.csproj", "Bawbee.Infra.CrossCutting.Extensions/"]
COPY ["Bawbee.Infra.CrossCutting.Common/Bawbee.Infra.CrossCutting.Common.csproj", "Bawbee.Infra.CrossCutting.Common/"]
COPY ["Bawbee.Application.Command/Bawbee.Application.Command.csproj", "Bawbee.Application.Command/"]
COPY ["Bawbee.Infra.CrossCutting.IoC/Bawbee.Infra.CrossCutting.IoC.csproj", "Bawbee.Infra.CrossCutting.IoC/"]
COPY ["Bawbee.Infra.CrossCutting.Bus/Bawbee.Infra.CrossCutting.Bus.csproj", "Bawbee.Infra.CrossCutting.Bus/"]
COPY ["Bawbee.Infra.Data/Bawbee.Infra.Data.csproj", "Bawbee.Infra.Data/"]
RUN dotnet restore "Bawbee.API/Bawbee.API.csproj"
COPY . .
WORKDIR "/src/Bawbee.API"
RUN dotnet build "Bawbee.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Bawbee.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Bawbee.API.dll"]
