# LAB-7 — Saugykla ir API (C#)

## Paleidimas

```bash
dotnet run
```

> Docker neprivalo veikti — programa naudoja demo duomenis atmintyje.
> Norint naudoti realų API: `Program.cs` pakeisti `naudotiApi = true`.

## Projekto struktūra

```
Lab7/
├── Models/
│   ├── Student.cs          — studento modelis (su pažymiais)
│   ├── Group.cs            — grupės modelis
│   └── Faculty.cs          — fakulteto modelis
├── DTOs/
│   ├── ApiStudentDto.cs    — JSON struktūra iš API
│   ├── ApiGroupDto.cs
│   └── ApiFacultyDto.cs
├── Interfaces/             — visi interfeisai (5 vnt.)
├── Legacy/
│   └── LegacyStudentValidation.cs  — senas kodas (nekeičiamas)
├── Implementations/
│   ├── Adapter/
│   │   └── StudentValidatorAdapter.cs   — ADAPTER šablonas
│   ├── Repository/
│   │   ├── MemoryStudentRepository.cs   — demo duomenys
│   │   └── ApiStudentRepository.cs      — realūs duomenys iš HTTP
│   ├── Strategy/
│   │   └── SimpleAverageStrategy.cs     — STRATEGY šablonas
│   ├── Printer/
│   │   └── ConsoleStudentPrinter.cs
│   └── Menu/
│       └── ConsoleMenuService.cs
├── Services/
│   └── StudentService.cs   — verslo logika
└── Program.cs              — composition root (tik DI surišimas)
```

## Naudojami šablonai

| Šablonas | Kur naudojamas | Kodėl |
|---|---|---|
| **Repository** | `IStudentRepository` | Paslėpia duomenų šaltinį |
| **Strategy** | `IAverageStrategy` | Keičiamas vidurkio algoritmas |
| **Adapter** | `StudentValidatorAdapter` | Senas kodas prijungtas prie naujo interfeiso |

## Dizaino principai (SOLID)

- **SRP** — kiekviena klasė turi vieną atsakomybę (spausdintuvas tik spausdina, validatorius tik validuoja)
- **OCP** — `StudentService` nepasikeitė pereinant nuo atminties prie API
- **DIP** — visos priklausomybės perduodamos per konstruktorių kaip interfeisai

## Encapsulation pavyzdys

```
ApiStudentRepository:
  VIDUJE  → HttpClient, Dictionary<int,Student>, DTO konversija
  IŠORĖJE → IReadOnlyList<Student>, Group, Faculty
```
