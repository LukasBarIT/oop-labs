# CW-1 — OOP C# Refaktoringas

**Vardas Pavardė** | Data: 2026-05-20

---

## Atliktos užduotys

- [x] Task 1 — Refaktoringas į sluoksnius
- [x] Task 2 — LINQ + non-LINQ versijos (meniu 7, 8, 9)
- [x] Task 3 — Runtime argumentai + StubStudentRepository
- [x] Drills — visos 5 LINQ_Drills užbaigtos

---

## Projekto struktūra

```
CW1After/
├── Program.cs                        ← composition root, apdoroja args
├── Models/
│   ├── Student.cs
│   └── Group.cs
├── Interfaces/
│   └── IStudentRepository.cs         ← pagrindinis interfeisas
├── Services/
│   ├── MemoryStudentRepository.cs    ← įprasti 5 studentai
│   ├── StubStudentRepository.cs      ← Task 3: 1 test studentas
│   ├── AverageCalculator.cs          ← vidurkio formulė (DRY)
│   ├── StudentValidator.cs
│   ├── StudentService.cs             ← meniu 1–6 logika
│   └── ReportService.cs              ← Task 2: meniu 7, 8, 9 (LINQ + non-LINQ)
├── UI/
│   └── ConsoleMenu.cs                ← visi Console.Write/ReadLine tik čia
└── LINQ_Drills.cs                    ← 5 drills (LINQ + Plain versijos)
```

---

## Kaip paleisti

```bash
# Įprastas paleidimas (MemoryStudentRepository)
dotnet run

# Su stub duomenimis (StubStudentRepository)
dotnet run -- --stub
```

### Tikėtami rezultatai

| Komanda | Rezultatas |
|---|---|
| `dotnet run` | `[INFO] Using MemoryStudentRepository (default).` — 5 studentai |
| `dotnet run -- --stub` | `[INFO] Using StubStudentRepository (--stub).` — tik `[999] Test Student` |
| Meniu `1` su `--stub` | `[999] Test Student (TEST) email=test@test.lt avg=10.00` |

---

## Dizaino sprendimai

**SRP** — kiekviena klasė turi vieną atsakomybę: `AverageCalculator` tik skaičiuoja vidurkį, `StudentValidator` tik validuoja, `ConsoleMenu` tik rodo ekraną.

**DRY** — vidurkio formulė vienoje vietoje (`AverageCalculator`), naudojama ir `StudentService`, ir `ReportService`.

**Konstruktoriaus injekcija** — `StudentService` gauna `IStudentRepository`, nežino ar tai `Memory` ar `Stub`.

**Task 2** — meniu 7, 8, 9 kiekvienas turi dvi versijas `ReportService` klasėje: su LINQ ir be LINQ (tik `for`/`foreach`/`if`/`List.Sort`).

**Task 3** — `Program.cs` skaito `args`, parenka repository, visas likęs kodas nesikeičia.
