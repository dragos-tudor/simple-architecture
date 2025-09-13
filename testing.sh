set -e

WORKSPACE_DIR=/workspaces/simple-architecture/
PROJECTS=(
  "Simple.Domain.Services"
  "Simple.Infrastructure.JobScheduler"
  "Simple.Infrastructure.MailServer"
  "Simple.Infrastructure.MessageQueue"
  "Simple.Infrastructure.MongoDb"
  "Simple.Infrastructure.SqlServer"
  "Simple.Messaging.Handlers"
  "Simple.Api.Endpoints"
  "Simple.Api.Tests"
  # "Simple.Worker.Tests"
)

./building.sh Debug
for PROJECT in ${PROJECTS[@]}; do
  echo "testing project $PROJECT ..."
  cd $WORKSPACE_DIR/$PROJECT && dotnet run --no-build --no-restore -- --config-file ../.testconfig.json
done
