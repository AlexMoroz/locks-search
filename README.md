# Local setup

Prerequisites:
===
```
docker version
node -v
dotnet -v
```

Containers:
===
```
docker run -e 'ACCEPT_EULA=Y' -e 'SA_PASSWORD=P@ssw0rd' -p 1433:1433 -d mcr.microsoft.com/mssql/server:2019-latest
docker run -p 6379:6379 redislabs/redisearch:latest
```

