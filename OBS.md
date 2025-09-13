
### Running projects tests
```shell
cd <project_path>
dotnet restore
dotnet build --no-restore
dotnet run --no-build --no-restore-- --config-file ../.testconfig.json --no-progress // mstest v2
```

### Observations
- greenmail server sometimes fail to start smtp service. greemail server container needs to be restarted.
- start containers error "current system boot ID differs from cached boot ID".
  - delete `/run/libpod/alive` old system boot id and restart containers `podman restart -a` twice.
  - logs unavailable to get current system boot id [`journalctl --list -boots`] and to replace the old one `/run/libpod/alive`.
- set `dotnetAcquisitionExtension.sharedExistingDotnetPath: "/usr/bin/dotnet"` .NET Install Tool settings to use installed .Net Runtime (and skip loading other .Net Runtime version). usefull on offline scenarios.
