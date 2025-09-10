
### About tests
- MSTest v2 runner should be used to run tests [even v1 version is supported].

### Running projects tests
```shell
cd <project_path>
dotnet restore
dotnet build --no-restore
dotnet run --no-build --no-restore-- --config-file ../.testconfig.json --no-progress
```

### Known issues
- greenmail server sometimes fail to start smtp service. greemail server container needs to be restarted.
