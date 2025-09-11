set -e

CONFIGURATION=${1:-Debug}
WORKSPACE_DIR=/workspaces/simple-architecture/
PROJECTS=(
  "Simple.Shared.Models"
  "Simple.Testing.Models"
  "Simple.Testing.Http"
  "Simple.Domain.Queries"
  "Simple.Infrastructure.JobScheduler"
  "Simple.Infrastructure.MailServer"
  "Simple.Infrastructure.MessageQueue"
  "Simple.Infrastructure.MongoDb"
  "Simple.Infrastructure.SqlServer"
  "Simple.Domain.CodeGenerator"
  "Simple.Domain.Models"
  "Simple.Domain.Services"
  "Simple.Api.Endpoints"
  # "Simple.Api"
  # "Simple.Worker.Jobs"
  # "Simple.Worker"
)

dotnet restore
for PROJECT in ${PROJECTS[@]}; do
  echo "building project $PROJECT ..."
  cd $WORKSPACE_DIR/$PROJECT && dotnet build --no-restore --no-dependencies -c $CONFIGURATION
done
