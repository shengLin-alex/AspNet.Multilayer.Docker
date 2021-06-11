FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY *.sln .
COPY AspNet.Multilayer.Docker.Helper/*.csproj ./AspNet.Multilayer.Docker.Helper/
COPY AspNet.Multilayer.Docker.Repository/*.csproj ./AspNet.Multilayer.Docker.Repository/
COPY AspNet.Multilayer.Docker.Web/*.csproj ./AspNet.Multilayer.Docker.Web/
RUN dotnet restore

COPY AspNet.Multilayer.Docker.Helper/. ./AspNet.Multilayer.Docker.Helper/
COPY AspNet.Multilayer.Docker.Repository/. ./AspNet.Multilayer.Docker.Repository/
COPY AspNet.Multilayer.Docker.Web/. ./AspNet.Multilayer.Docker.Web/
WORKDIR /src/AspNet.Multilayer.Docker.Web
RUN dotnet build && dotnet publish -c release -o /app --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:5.0
WORKDIR /app
COPY --from=build /app ./
ENTRYPOINT [ "dotnet", "AspNet.Multilayer.Docker.Web.dll" ]
