set -e

WORKSPACE_DIR=/workspaces/simple-architecture/
PROJECTS=(
  "Simple.Infrastructure.JobScheduler"
  "Simple.Infrastructure.MailServer"
  "Simple.Infrastructure.MessageQueue"
  "Simple.Infrastructure.MongoDb"
  "Simple.Infrastructure.SqlServer"
  "Simple.Domain.Services"
  "Simple.Api.Endpoints"
  # "Simple.Api"
  # "Simple.Worker.Jobs"
  # "Simple.Worker"
)

./building.sh Debug
for PROJECT in ${PROJECTS[@]}; do
  echo "testing project $PROJECT ..."
  cd $WORKSPACE_DIR/$PROJECT && dotnet run --no-build --no-restore -- --settings ../.runsettings
done
