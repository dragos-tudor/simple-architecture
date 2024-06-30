
# Simple architecture
Build independent [*low-level*] modules and integrate them on top [*high-level*] modules.

### Simple architecture design
- **simple architecture**: an alternative to [clean-architecture](https://blog.cleancoder.com/uncle-bob/2012/08/13/the-clean-architecture.html).
- modules dependencies: **high-level modules** --> **middle-level modules** [optional] --> **low-level modules** [modules pyramid].
- *high-level modules* **integrate** all *low-level modules* and *mid-level modules*: domain integrations [here](/Simple.Domain.Integrations/ContactEndpoints/Creating.Sql.cs), [here](/Simple.Domain.Integrations/ContactEndpoints/Creating.Mongo.cs), and infrastructure integrations [here](/Simple.Infrastructure.Integrations/Integrations/Integrating.cs).
- *middle-level modules* **relies** on *low-level modules* having lower complexity than *high-level modules*.
- *low-level modules* **completely independent** ["parallel" modules].
- *low-level modules* **could share** some modules [ex. *simple.domain.models*].
- *low-level modules* should be **most of** all modules [pyramid base].
- *low-level modules* should be seen as **lego pieces** and *high-level modules* as **lego structures**.
- **fractal design** could be applied at any level:
  - server projects [simple projects](/).
  - front-end modules [react-like library](https://github.com/dragos-tudor/frontend-rendering).
  - front-end pages [security sample project](https://github.com/dragos-tudor/backend-security/tree/main/Security.Sample/frontend-components).
  - functions [aspnet-like security library](https://github.com/dragos-tudor/backend-security).
- **simple architecture** aka **pyramid architecture**.

### Simple architecture implementation
- *high-level modules*: simple.domain.integrations, simple.infrastructure.integrations.
- *low-level modules*: simple.domain.services, simple.infrastructure.emailserver, simple.infrastructure.jobscheduler, simple.infrastructure.mediator, simple.infrastructure.mongodb, simple.infrastructure.queue, simple.infrastructure.sqlserver, simple.domain.models [shared].

### Simple vs clean architectures parallels
- *the dependency rule* != no dependencies [even callback funcs could be seen as a form of dependency].
- *entities* != simple data bags [*low-level module* simple.domain.models].
- *use cases* = *simple.domain.services*.
- *interface adapters* = *high-level modules*.
- *frameworks and drivers* =
  - *low-level modules* [databases drivers].
  - *high-level modules* [web frameworks].
- *pyramid* vs *stack* => *simple* vs *complex* -> *unknown* [yet] vs *highly-appreciated* [?!].

### Simple vs clean architectures dependencies
- [clean-architecture implementation](https://github.com/ardalis/CleanArchitecture/tree/main/src):
  - *clean.architecture.core* project depend on *Mediatr* package [?!].
  - *clean.architecture.usecases* project depend on *clean.architecture.core* project and *EntityFrameworkCore* package [?!].
  - *clean.architecture.infrastructure* project depend on *clean.architecture.usecases* project [?!].
  - *clean.architecture.web* project depend on *clean.architecture.infrastructure* project [ok].
- [other clean-architecture implementation](https://github.com/jasontaylordev/CleanArchitecture/tree/main/src):
  - *domain* project depend on *Mediatr* package [?!].
  - *application* project depend on *domain* project and *EntityFrameworkCore* package [?!].
  - *infrastructure* project depend on *application* project [?!].
  - *web* project depend on *infrastructure* project [ok].
- [simple-architecture implementation](/):
  - *simple.domain.services*, *simple.infrastructure.** completely independent projects [share *simple.domain.models* project].
  - *simple.domain.integrations* and *simple.infrastructure.integrations* projects independent of each other, depends on all above projects [top of the pyramid].

### Remarks
- similar architecture already exists [how else :)] [IODA architecture](https://ccd-akademie.de/en/clean-architecture-vs-onion-architecture-vs-hexagonale-architektur/).
- *operations* = *low-level modules* funcs [ex. database funcs].
- *integrations* = *high-level modules* funcs [ex. webapi endpoints].

### Credits
Based idea comes from:
- F# team top-bottom [dependency order](https://fsharpforfunandprofit.com/posts/recipe-part3/#how-not-to-do-it).
- Mark Seemann's mindset changing hidden gems:
  - [impureim sandwitch](https://blog.ploeh.dk/2020/03/02/impureim-sandwich/).
  - [dependency rejection](https://blog.ploeh.dk/2017/01/27/from-dependency-injection-to-dependency-rejection/).
- Dragos Tudor: each functionality == one function/controller ex. [create contact](/Simple.Domain.Services/CreateContact/Creating.cs).

*SIMPLE ALWAYS MEANS SIMPLE*