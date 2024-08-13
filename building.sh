set -e

CONFIGURATION=${1:-Debug}
WORKSPACE_DIR=/workspaces/simple-architecture/
PROJECTS=(
  "Simple.Domain.CodeGenerator"
  "Simple.Domain.Models"
  "Simple.Shared.Testing"
  "Simple.Domain.Services"
  "Simple.Infrastructure.EmailServer"
  "Simple.Infrastructure.JobScheduler"
  "Simple.Infrastructure.Mediator"
  "Simple.Infrastructure.MongoDb"
  "Simple.Infrastructure.Queue"
  "Simple.Infrastructure.SqlServer"
  "Simple.Domain.Integrations"
  "Simple.Infrastructure.Integrations"
  "Simple.Api"
  "Simple.Worker"
)

dotnet restore
for PROJECT in ${PROJECTS[@]}; do
  echo "building project $PROJECT ..."
  cd $WORKSPACE_DIR/$PROJECT && dotnet build --no-restore --no-dependencies -c $CONFIGURATION
done
