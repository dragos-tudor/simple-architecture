
## Simple architecture [wip]

### Simple architecture design
- **simple architecture**: an alternative to [clean-architecture](https://blog.cleancoder.com/uncle-bob/2012/08/13/the-clean-architecture.html).
- modules dependencies: **high-level modules** --> **middle-level modules** [optional] --> **low-level modules** [modules pyramid].
- *high-level modules* **integrate** all *low-level modules* and *mid-level modules*.
- *middle-level modules* **relies** on *low-level modules* having lower complexity than *high-level modules*.
- *low-level modules* **completely independent** ["parallel" modules].
- *low-level modules* **could share** some *low-level modules* [ex. *simple.shared.models* project].
- *low-level modules* should be **most of** all modules [pyramid base].
- *low-level modules* could be seen as **lego pieces** and *high-level modules* as **lego structures**.
- **fractals design** could be applied at any level:
  - server projects [simple projects](/).
  - front-end modules [react-like library](https://github.com/dragos-tudor/frontend-rendering).
  - front-end pages [security sample project](https://github.com/dragos-tudor/backend-security/tree/main/Security.Sample/frontend-components).
  - functions [aspnet-like security library](https://github.com/dragos-tudor/backend-security).
- **simple architecture** aka **pyramid architecture**.

### Simple architecture implementation
- *high-level modules*: simple.web.endpoints.
- *low-level modules*: simple.domain.services, simple.infrastructure.\*, simple.shared.\*.

### Simple vs clean architectures parallels
- *the dependency rule* - no dependencies.
- *entities* - rich entities replaced with simple data bags [no behaviours] in *low-level module* simple.shared.models.
- *use cases* = *domain services module*.
- *interface adapters* - reside into *high-level modules*.
- *frameworks and drivers* - reside into:
  - *low-level modules* [databases drivers].
  - *high-level modules* [web frameworks].

### Simple vs clean architectures dependencies
- [clean-architecture implementation](https://github.com/ardalis/CleanArchitecture/tree/main/src):
  - *clean.architecture.core* project depend on *mediator* [no-comments].
  - *clean.architecture.usecases* project depend on *clean.architecture.core* project.
  - *clean.architecture.infrastructure* project depend on *clean.architecture.usecases* project [no-comments].
  - *clean.architecture.web* project depend on *clean.architecture.infrastructure* project.
- [simple-architecture implementation](/):
  - *simple.domain.services*, *simple.infrastructure.** completely independent projects [share *simple.shared.models* project].
  - *simple.webapi* project depend on all above projects.
- *pyramid* vs *stack* => *simple* vs *complicated* -> *unknown* [yet] vs *highly-appreciated* [no-comments].

### Remarks
- something similar already exists how else [IODA architecture](https://ccd-akademie.de/en/clean-architecture-vs-onion-architecture-vs-hexagonale-architektur/).

### Credits
Based idea comes from:
- F# team top-bottom [dependency order](https://fsharpforfunandprofit.com/posts/recipe-part3/#how-not-to-do-it).
- Mark Seemann's mindset changing hidden gems:
  - [impureim sandwitch](https://blog.ploeh.dk/2020/03/02/impureim-sandwich/).
  - [dependency rejection](https://blog.ploeh.dk/2017/01/27/from-dependency-injection-to-dependency-rejection/).
- Dragos Tudor: each functionality == one *high-level* integration function [aka functionality controller].

*SIMPLE ALWAYS MEANS SIMPLE*