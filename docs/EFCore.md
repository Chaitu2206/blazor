### EF Core

Tools `dotnet tool install --global dotnet-ef`

```add-migration Initial -OutputDir 'Data/Migrations' -Project Zindagi.Infra```
```remove-migration -Project Zindagi.Infra```
```update-database -Project Zindagi.Infra```
