### Opening repository [docker-compose free]
- *docker remote api*:
  - for ubuntu host open port 2375 [http] [how to](https://gist.githubusercontent.com/styblope/dc55e0ad2a9848f2cc3307d4819d819f/raw/9e76020d6b72d10351eb9583c606a09d68aba070/docker-api-port.md).
  - for windows host port 2375 seems to be opened by default [unverified].
- *docker network*:
  - run command ```docker network create --driver=bridge architecture-network``` to create neccessary network.
- *.devcontainer/.devcontainer.json*:
  - for mac and windows replace container env "DOCKER_HOST_GATEWAY" with "host.docker.internal" [unverified].

### About tests
- MSTest v2 runner should be used to run tests [even v1 version is supported].
- first run should take some time to:
  - pull SqlServer, MongoDb and GreenMail images from docker hub.
  - create SQLServer, MongoDb and GreenMail containers.
  - start SqlServer, MongoDb replcate set [3] and GreenMail servers.
- possible issues:
  - first run execution timeout. should run test command again.
  - greenmail server sometimes fail to start smtp service. greemail server container needs to be restarted.

### Running tests
```shell
dotnet restore
dotnet build --no-restore
dotnet run --no-build --no-restore <project_path> -- --settings ../.runsettings
```
