# LAB-5 – Interface Patterns in Practice

## Architecture Overview

```
Program.cs (Composition Root)
    ↓
IMenuService
    ↓
StudentService (Business Logic – depends only on interfaces)
    ├── IStudentRepository
    ├── IStudentPrinter
    ├── IAverageStrategy
    └── IStudentValidator
```

---

## Patterns Implemented

### 1. Strategy Pattern – `IStudentPrinter`

**Location:** `Implementations/Printer/`

| Class | Behavior |
|---|---|
| `ConsoleStudentPrinter` | Prints students to console |
| `FileStudentPrinter` | Writes students to a .txt file |
| `JsonStudentPrinter` | Outputs students in JSON format |

**Why useful:** `StudentService` calls `_printer.Print(group)` without knowing HOW the printing happens. You can swap printers in `Program.cs` without touching `StudentService`.

---

### 2. Strategy Pattern – `IAverageStrategy`

**Location:** `Implementations/Strategy/`

| Class | Behavior |
|---|---|
| `SimpleAverageStrategy` | Plain arithmetic average |
| `WeightedAverageStrategy` | Later grades have higher weight |
| `MedianAverageStrategy` | Returns the median grade |

**Why useful:** Different grading policies can be swapped at runtime. `StudentService.CalculateAverage()` works the same regardless of which strategy is injected.

---

### 3. Repository Pattern – `IStudentRepository`

**Location:** `Implementations/Repository/`

| Class | Behavior |
|---|---|
| `MemoryStudentRepository` | Returns hardcoded in-memory data |
| `FileStudentRepository` | Reads student data from a file |
| `ApiStudentRepository` | Simulates fetching from a remote API |

**Why useful:** `StudentService` does not know where students come from. You can switch from memory to file or API without any changes to business logic.

---

### 4. Service Abstraction – `IMenuService`

**Location:** `Implementations/Menu/`

| Class | Behavior |
|---|---|
| `ConsoleMenuService` | Interactive console UI |
| `DebugMenuService` | Runs with debug output for testing |
| `WebMenuSimulationService` | Simulates HTTP request/response cycle |

**Why useful:** User interaction is completely separated from business logic. `StudentService` never deals with UI concerns.

---

### 5. Adapter Pattern – `IStudentValidator`

**Location:** `Implementations/Adapter/`

| Class | Behavior |
|---|---|
| `StudentValidatorAdapter` | Wraps `LegacyStudentValidation` (old system) |
| `StrictStudentValidator` | New direct implementation |

**Why useful:** The legacy system has an incompatible interface (`CheckStudent(name, email)`). The adapter translates it into `IStudentValidator.Validate(Student)`, so `StudentService` can use the old system without knowing about it.

---

## Runtime Switching

All switching happens **only in `Program.cs`**. `StudentService` is never modified.

```csharp
// Swap printer:
IStudentPrinter printer = new JsonStudentPrinter();

// Swap strategy:
IAverageStrategy strategy = new WeightedAverageStrategy();

// Swap repository:
IStudentRepository repository = new FileStudentRepository();
```

---

## Key Rules Followed

- `StudentService` depends **only on interfaces**, never on concrete classes
- All dependencies are injected via **constructor injection**
- No `new` keyword inside services
- `Program.cs` is the only composition root
- Business logic is not in `Main`
