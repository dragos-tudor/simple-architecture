set -e

CONFIGURATION=${1:-Debug}
WORKSPACE_DIR=/workspaces/simple-architecture/
PROJECTS=(
  "Simple.Testing.Models"
  "Simple.Testing.Http"
  "Simple.Shared.Models"
  "Simple.Domain.Queries"
  "Simple.Infrastructure.MailServer"
  "Simple.Infrastructure.JobScheduler"
  "Simple.Infrastructure.MessageQueue"
  "Simple.Infrastructure.MongoDb"
  "Simple.Infrastructure.SqlServer"
  "Simple.Domain.CodeGenerator"
  "Simple.Domain.Models"
  "Simple.Domain.Services"
  "Simple.Api.Endpoints"
  "Simple.Api"
  "Simple.Worker.Services"
  "Simple.Worker"
)

dotnet restore
for PROJECT in ${PROJECTS[@]}; do
  echo "building project $PROJECT ..."
  cd $WORKSPACE_DIR/$PROJECT && dotnet build --no-restore --no-dependencies -c $CONFIGURATION
done
