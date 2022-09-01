## Add Migrations
```sh
cd Smartlog.TransportExchange.Domain 
dotnet ef --startup-project ../Tmusic/ migrations add InitialCreate --context MainDbContext
```

## Update Migrations
```sh
cd Smartlog.TransportExchange.Domain 
dotnet ef --startup-project ../Tmusic/ database update --context MainDbContext
```
## RollBack Migration
```sh
dotnet ef --startup-project ../Tmusic/ database update <name_migration> --context MainDbContext
```

## Remove Migration
```sh
dotnet ef migrations remove
```