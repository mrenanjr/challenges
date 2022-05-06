#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["./TemplateS.API/TemplateS.API/TemplateS.API.csproj", "src/TemplateS.API/"]
COPY ["./TemplateS.API/TemplateS.Application/TemplateS.Application.csproj", "src/TemplateS.Application/"]
COPY ["./TemplateS.API/TemplateS.Domain/TemplateS.Domain.csproj", "src/TemplateS.Domain/"]
COPY ["./TemplateS.API/TemplateS.Infra.Data/TemplateS.Infra.Data.csproj", "src/TemplateS.Infra.Data/"]
COPY ["./TemplateS.API/TemplateS.Infra.CrossCutting.IoC/TemplateS.Infra.CrossCutting.IoC.csproj", "src/TemplateS.Infra.CrossCutting.IoC/"]
COPY ["./TemplateS.API/TemplateS.Infra.CrossCutting.Swagger/TemplateS.Infra.CrossCutting.Swagger.csproj", "src/TemplateS.Infra.CrossCutting.Swagger/"]
COPY ["./TemplateS.API/TemplateS.Infra.CrossCutting.ExceptionHandler/TemplateS.Infra.CrossCutting.ExceptionHandler.csproj", "src/TemplateS.Infra.CrossCutting.ExceptionHandler/"]
RUN dotnet restore "src/TemplateS.API/TemplateS.API.csproj"
COPY . .
WORKDIR "/src/TemplateS.API/"
RUN dotnet build -c Release -o /app/build

FROM build AS publish
RUN dotnet publish -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
RUN ls -l
ENTRYPOINT ["dotnet", "TemplateS.API.dll"]
