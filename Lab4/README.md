# LAB-4: Priklausomybių Izoliavimas – Variantas 4 (IStudentFinder)

## Kokia problema buvo sprendžiama?

LAB-3 projekte mokėjimo strategija buvo naudojama tiesiogiai. LAB-4 tikslas – parodyti, kad
**paieškos mechanizmas gali keistis** (pagal ID, Email, išorinė API ir pan.) neliečiant verslo logikos.

`StudentService` nežino *kaip* ieškoma – jis tik žino, kad gali paprašyti rasti studentą.

## Kas izoliuojama ir kodėl?

| Kas | Kodėl |
|-----|-------|
| `IStudentFinder` | Paieškos algoritmas gali keistis ateityje |
| `StudentService` | Verslo logika neturi priklausyti nuo konkrečios implementacijos |
| `Program.cs` | Dirba tik su interface, ne su konkrečiomis klasėmis |

## Struktūra

```
Lab4/
├── Models/
│   ├── Student.cs          – duomenų modelis
│   └── Group.cs            – grupės modelis
├── Interfaces/
│   └── IStudentFinder.cs   – paieškos kontraktas
├── Finders/
│   ├── StudentFinder.cs    – tikra implementacija (ID arba Email)
│   ├── FakeStudentFinder.cs – visada grąžina stabilų studentą
│   └── StubStudentFinder.cs – kontroliuojamas rezultatas (rastas / null)
├── Services/
│   └── StudentService.cs   – verslo logika, gauna finder per konstruktorių
└── Program.cs              – tik interface naudojimas, jokios verslo logikos
```

## Fake vs Stub

**Fake** – supaprastinta implementacija su stabilia elgsena. Naudinga kai norime patikrinti
verslo logiką nepriklausomai nuo realių duomenų. Visada grąžina tą patį studentą.

**Stub** – kontroliuojama implementacija. Galima nurodyti tikslų rezultatą – studentą arba `null`.
Naudojama norint patikrinti abi logikos šakas (rastas / nerastas).

## Pademonstruotos logikos šakos

1. **Studentas rastas** → atspausdinama studento informacija
2. **Studentas nerastas** → atspausdinama klaidos žinutė

Abi šakos pademonstruotos su: tikra paieška (ID ir Email), Fake ir Stub implementacijomis.

## Constructor Injection

```csharp
public StudentService(IStudentFinder finder)
{
    _finder = finder;  // jokio "new" viduje
}
```

Priklausomybė perduodama iš išorės – `StudentService` niekada nekuria `finder` pati.
