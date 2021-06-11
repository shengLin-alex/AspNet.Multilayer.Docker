# AspNet.Multilayer.Docker

> This project is built using [ASP.NET Core 5.0](https://docs.microsoft.com/zh-tw/dotnet/core/dotnet-five).  
> The target is learning ASP.NET core with Dependency injection([Autofac](https://autofac.org/)) using repository pattern and technique of Docker.


## Build Setup

Clone this project by
``` bash
git clone https://github.com/shengLin-alex/AspNet.Multilayer.Docker.git
```

Go to web project folder
``` bash
cd ./AspNet.Multilayer.Docker/AspNet.Multilayer.Docker.Web
```

Then docker-compose up

``` bash
docker compose up
```

Rebuild netcore service
``` bash
docker compose build netcore

docker compose up
```

## .env example
```
HOST_PORT=8001

# The persistent database data folder must be put outside of solution folder
POSTGRES_HOST_DIR=../../db_data/postgres_data
POSTGRES_DB=develop
POSTGRES_USER=dockeradmin
POSTGRES_PASSWORD=dockeradmin
POSTGRES_PORT=5432

# The persistent database data folder must be put outside of solution folder
SQLSERVER_HOST_DIR=../../db_data/sqlserver_data
SQLSERVER_PORT=1433
SQLSERVER_PASSWORD=dockERadmin_1
```


## Simple Test with RESTful API

POST /api/test/user, with body:
```json
{
  "name": "John Doe"
}
```

GET /api/test/users, the response will show:
```json
[
  {
    "id": 1,
    "name": "John Doe"
  }
]
```
