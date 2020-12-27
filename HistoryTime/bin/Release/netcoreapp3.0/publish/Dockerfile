FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build-env
WORKDIR /app
COPY . . 

CMD ASPNETCORE_URLS=http://*:$PORT dotnet HistoryTime.dll