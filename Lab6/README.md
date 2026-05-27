# LAB-6 — Containers in Object-Oriented Architecture (C#)

## How to run

```
dotnet run
```

## Project structure

```
Lab6/
├── Models/
│   └── Student.cs                   — data class
├── Interfaces/
│   ├── IStudentRepository.cs
│   ├── IStudentPrinter.cs
│   ├── IAverageStrategy.cs
│   ├── IStudentValidator.cs
│   └── IMenuService.cs
├── Repositories/
│   └── MemoryStudentRepository.cs   — Task 1
├── Printers/
│   ├── ConsoleStudentPrinter.cs     — Task 2
│   ├── FileStudentPrinter.cs        — Task 2
│   └── JsonStudentPrinter.cs        — Task 2
├── Strategies/
│   └── AverageStrategies.cs         — Task 3 (Simple, Weighted, Median)
├── Validators/
│   └── StudentValidator.cs          — Task 4
├── Services/
│   ├── StudentService.cs            — Task 5
│   └── MenuService.cs
└── Program.cs                       — clean, no business logic
```

## How containers are used

`MemoryStudentRepository` holds a **private** `List<Student> _students`.
It is never exposed directly — callers can only use `Add()`, `Remove()`,
`GetById()`, and `GetAll()` (which returns a copy, so external code cannot
mutate the internal list).

## Patterns extended for collections

| Component | Change |
|---|---|
| `IStudentRepository` | `GetAll()` returns `List<Student>` |
| `IStudentPrinter` | `PrintStudents(List<Student>)` |
| `IAverageStrategy` | `Calculate(List<Student>)` |
| `IStudentValidator` | `ValidateAll(List<Student>)` added |

## Design decisions

- **Constructor injection** everywhere — `StudentService` receives all
  dependencies through its constructor, making it testable and decoupled.
- **Private container** — the `List<Student>` inside the repository is
  `private readonly`. No public property exposes it.
- **Return copy in `GetAll()`** — prevents callers from accidentally
  modifying the internal list via the reference.
- **Strategy pattern** — the average algorithm is swappable at the
  composition root (`Program.cs`) without touching any other class.
- **Separation of concerns** — printing, validation, calculation, and
  persistence are each in their own class with a single responsibility.
