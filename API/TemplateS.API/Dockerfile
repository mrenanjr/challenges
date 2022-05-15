FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
WORKDIR /app

COPY . ./
RUN dotnet restore
RUN dotnet publish -c Release -o out

COPY wait-for-it.sh wait-for-it.sh 
RUN chmod +x wait-for-it.sh

FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
EXPOSE 5000
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "TemplateS.API.dll"]