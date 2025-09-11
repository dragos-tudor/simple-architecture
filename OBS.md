
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
- .NET Install Tool settings `dotnetAcquisitionExtension.sharedExistingDotnetPath: "/usr/bin/dotnet"` will use installed .Net Runtime (and skip loading other .Net Runtime version).
- start containers error "current system boot ID differs from cached boot ID". solutions:
  - delete `/run/libpod` and `/run/containers/storage` folder and re-run podman.
  - if journal logs would were available the current system boot id [`journalctl --list -boots`] would replaced the old system boot id from `/run/libpod/alive`.
