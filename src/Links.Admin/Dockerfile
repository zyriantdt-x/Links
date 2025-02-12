FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY Links.Admin.csproj .
RUN dotnet restore Links.Admin.csproj
COPY . .
RUN dotnet build Links.Admin.csproj -c Release -o /app/build

FROM build AS publish
RUN dotnet publish Links.Admin.csproj -c Release -o /app/publish

FROM nginx:alpine AS final
WORKDIR /usr/share/nginx/html
COPY --from=publish /app/publish/wwwroot .
COPY nginx.conf /etc/nginx/nginx.conf