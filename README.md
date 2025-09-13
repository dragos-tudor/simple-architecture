
# Simple architecture
Build independent [*low-level*] modules and integrate them on top [*high-level*] modules.

### Simple architecture design
- *simple architecture*: an alternative to [clean-architecture](https://blog.cleancoder.com/uncle-bob/2012/08/13/the-clean-architecture.html).
- modules dependencies: *high-level modules* --> *low-level modules* [modules pyramid].
- *low-level modules* *completely independent* ["parallel" modules].
- *low-level modules* *could share* some modules [ex. *simple.shared.models*].
- *low-level modules* should be *most of* all modules [pyramid base].
- *low-level modules* should be seen as *lego pieces* and *high-level modules* as *lego structures*.
- *high-level modules* *integrate* all *low-level modules*.
- *fractal design* could be applied at any level:
  - server projects [simple projects](/).
  - front-end modules [react-like library](https://github.com/dragos-tudor/frontend-rendering).
  - front-end components [identity app](https://github.com/dragos-tudor/identity-app/frontend-components).
  - functions [aspnet-like security library](https://github.com/dragos-tudor/backend-security).
- *simple architecture* aka *pyramid architecture*.

### Simple architecture implementation
- *high-level modules*: simple.api.endpoints, simple.worker.jobs [sharing simple.messaging.handlers].
- *low-level modules*: simple.domain.models, simple.domain.queries, simple.domain.services, simple.infrastructure.mailserver, simple.infrastructure.jobscheduler, simple.infrastructure.messagequeue, simple.infrastructure.mongodb, simple.infrastructure.sqlserver, [sharing simple.shared.models].

### Simple vs clean architecture parallels
- *the dependency rule* != no dependencies [even callback funcs could be seen as a form of dependency].
- *entities* != simple data bags [simple.domain.models].
- *use cases* == *simple.domain.services*.
- *interface adapters* == *high-level modules*.
- *frameworks and drivers*:
  - *low-level modules* [databases drivers].
  - *high-level modules* [web frameworks].
- *pyramid* vs *stack* => *simple* vs *complex* => *unknown* [yet] vs *highly-appreciated* [?!].

### Simple vs clean architecture dependencies
- [clean-architecture implementation](https://github.com/ardalis/CleanArchitecture/tree/main/src):
  - *clean.architecture.core* project depends on *Mediatr* package [?!].
  - *clean.architecture.usecases* project depends on *clean.architecture.core* project and *EntityFrameworkCore* package [?!].
  - *clean.architecture.infrastructure* project depends on *clean.architecture.usecases* project [?!].
  - *clean.architecture.web* project depends on *clean.architecture.infrastructure* project [ok].
- [other clean-architecture implementation](https://github.com/jasontaylordev/CleanArchitecture/tree/main/src):
  - *domain* project depends on *Mediatr* package [?!].
  - *application* project depends on *domain* project and *EntityFrameworkCore* package [?!].
  - *infrastructure* project depends on *application* project [?!].
  - *web* project depends on *infrastructure* project [ok].
- [simple-architecture implementation](/):
  - *simple.domain.\**, *simple.infrastructure.\** completely independent projects [sharing *simple.shared.models* project].
  - *simple.api*, *simple.worker* projects independent of each other, depend on all above projects [sharing *simple.messaging.handlers*].

### Remarks
- similar architecture already exists [how else :)] [IODA architecture](https://ccd-akademie.de/en/clean-architecture-vs-onion-architecture-vs-hexagonale-architektur/).
- *operations* = *low-level modules* funcs [ex. database funcs].
- *integrations* = *high-level modules* funcs [ex. webapi endpoints].

### Credits
- Eric Evans: autonomous bounded contexts [DDD](https://www.amazon.com/Domain-Driven-Design-Tackling-Complexity-Software/dp/0321125215).
- F# team top-bottom [dependency order](https://fsharpforfunandprofit.com/posts/recipe-part3/#how-not-to-do-it).
- Mark Seemann's mindset changing hidden gems:
  - [impureim sandwitch](https://blog.ploeh.dk/2020/03/02/impureim-sandwich/).
  - [dependency rejection](https://blog.ploeh.dk/2017/01/27/from-dependency-injection-to-dependency-rejection/).

*SIMPLE ALWAYS MEANS SIMPLE*
