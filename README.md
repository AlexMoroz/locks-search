# Local setup

Prerequisites:
===
```
docker version
node -v
dotnet -v
```

Add containers:
===
```
docker run -e 'ACCEPT_EULA=Y' -e 'SA_PASSWORD=P@ssw0rd' -p 1433:1433 -d mcr.microsoft.com/mssql/server:2019-latest
docker run -p 6379:6379 redislabs/redisearch:latest
```

Build & Run:
===
```
dotnet build;
dotnet run --project LocksSearch;
```

Go to: http://localhost:5001