set -e

WORKSPACE_DIR=/workspaces/simple-architecture/
PROJECTS=(
  "Simple.Domain.Services"
  "Simple.Infrastructure.EmailServer"
  "Simple.Infrastructure.Mediator"
  "Simple.Infrastructure.MongoDb"
  "Simple.Infrastructure.Queue"
  "Simple.Infrastructure.SqlServer"
  "Simple.Domain.Integrations"
  "Simple.Infrastructure.Integrations"
  "Simple.Api"
  "Simple.Worker"
)

./building.sh Debug
for PROJECT in ${PROJECTS[@]}; do
  echo "testing project $PROJECT ..."
  cd $WORKSPACE_DIR/$PROJECT && dotnet run --no-build --no-restore -- --settings ../.runsettings
done
