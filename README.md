
## Simple architecture [wip]

### Simple architecture design
- **simple architecture** an alternative to [clean-architecture](https://blog.cleancoder.com/uncle-bob/2012/08/13/the-clean-architecture.html).
- modules dependencies: **high-level module** --> **middle-level modules** [optional] --> **low-level modules** [modules pyramid].
- *high-level module* **integrate** all functionalities from *low-level modules* and *mid-level modules*.
- *middle-level modules* **relies only** on *low-level module* and have low complexity than *high-level modules*.
- *low-level modules* **are independently** of each other ["parallel" modules].
- *low-level and middle-level modules* funcs should **not have callbacks** [other dependecy form].
- *low-level modules* should be **most of** all modules [pyramid base].
- *low-level modules* **could share** some *support modules* [ex. models/sharing *low-level modules*].
- *sky-level modules* should **consume** *high-level modules* [ex. webapi, ui, services]. *sky-level modules* **not part** of simple architecture.

### Simple-clean architectures differences
- *the dependency rule* - dependencies simply not exists.
- *entities* - rich entities replaced with simple data bags [no behaviours].
- *use cases* = *high-level module*.
- *interface adapters* - reside into *sky-level modules*.
- *frameworks and drivers* - reside into:
  - *middle-level modules* [databases drivers].
  - *sky-level modules* [web frameworks].

### Remarks
- something similar already exists how else [IODA architecture](https://ccd-akademie.de/en/clean-architecture-vs-onion-architecture-vs-hexagonale-architektur/).

### Credits
Based idea comes somehow from:
- F# team top-bottom [dependency order](https://fsharpforfunandprofit.com/posts/recipe-part3/#how-not-to-do-it).
- Mark Seemann's mindset changing hidden gems:
  - [impureim sandwitch](https://blog.ploeh.dk/2020/03/02/impureim-sandwich/).
  - [dependency rejection](https://blog.ploeh.dk/2017/01/27/from-dependency-injection-to-dependency-rejection/).