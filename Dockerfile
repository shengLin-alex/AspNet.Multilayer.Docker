FROM mcr.microsoft.com/dotnet/sdk:5.0
WORKDIR /app
COPY *.sln .
COPY AspNet.Multilayer.Docker.Helper/. ./AspNet.Multilayer.Docker.Helper/
COPY AspNet.Multilayer.Docker.Repository/. ./AspNet.Multilayer.Docker.Repository/
COPY AspNet.Multilayer.Docker.Web/. ./AspNet.Multilayer.Docker.Web/
RUN dotnet restore --disable-parallel && dotnet build

