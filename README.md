
## Simple architecture [wip]

### Simple architecture design
- **simple architecture** an alternative to [clean-architecture](https://blog.cleancoder.com/uncle-bob/2012/08/13/the-clean-architecture.html).
- modules dependencies: **high-level modules** --> **middle-level modules** [optional] --> **low-level modules** [modules pyramid].
- *high-level modules* **integrate** all functionalities from *low-level modules* and *mid-level modules*.
- *middle-level modules* **relies only** on *low-level modules* having low complexity than *high-level modules*.
- *low-level modules* **are independently** of each other ["parallel" modules].
- *low-level modules* **could share** some *low-level modules* [ex. models].
- *low-level modules* should be **most of** all modules [pyramid base].
- ex:
  - *high-level modules*: Simple.Web.Api.
  - *low-level modules*: Simple.Domain.Services, Simple.Infrastructure.*, Simple.Shared.*.

### Simple-clean architectures parallels
- *the dependency rule* - dependencies simply not exists [excepting *domain services module* callback funcs].
- *entities* - rich entities replaced with simple data bags [no behaviours].
- *use cases* = *domain services module*.
- *interface adapters* - reside into *high-level modules*.
- *frameworks and drivers* - reside into:
  - *low-level modules* [databases drivers].
  - *high-level modules* [web frameworks].

### Remarks
- something similar already exists how else [IODA architecture](https://ccd-akademie.de/en/clean-architecture-vs-onion-architecture-vs-hexagonale-architektur/).

### Credits
Based idea comes somehow from:
- F# team top-bottom [dependency order](https://fsharpforfunandprofit.com/posts/recipe-part3/#how-not-to-do-it).
- Mark Seemann's mindset changing hidden gems:
  - [impureim sandwitch](https://blog.ploeh.dk/2020/03/02/impureim-sandwich/).
  - [dependency rejection](https://blog.ploeh.dk/2017/01/27/from-dependency-injection-to-dependency-rejection/).


*SIMPLE ALWAYS MEANS SIMPLE*