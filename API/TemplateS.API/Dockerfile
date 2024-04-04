FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
WORKDIR /app
EXPOSE 80

COPY . ./
RUN dotnet restore
RUN dotnet publish -c Release -o out

# Copy wait-for-it.sh into our image
COPY wait-for-it.sh wait-for-it.sh
# Make it executable, in Linux
RUN chmod +x wait-for-it.sh

FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app

COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "TemplateS.API.dll"]