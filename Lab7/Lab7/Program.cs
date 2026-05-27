using Lab7.Implementations.Adapter;
using Lab7.Implementations.Menu;
using Lab7.Implementations.Printer;
using Lab7.Implementations.Repository;
using Lab7.Implementations.Strategy;
using Lab7.Interfaces;
using Lab7.Services;

namespace Lab7;

/// <summary>
/// COMPOSITION ROOT — vienintelė vieta, kur pasirenkamos konkrečios klasės.
/// 
/// Visur kitur naudojami TIK interfeisai.
/// Norint pakeisti implementaciją (pvz. atminties → API saugykla) —
/// pakanka pakeisti VIENĄ eilutę čia, nieko kito nekeičiant.
/// </summary>
class Program
{
    static void Main(string[] args)
    {
        // ── Konfigūracija ──────────────────────────────────────────────
        const string apiUrl = "http://localhost:6001";
        const bool   naudotiApi = false;  // ← pakeisti į true kai Docker paleistas

        // ── Priklausomybių surinkimas (Dependency Injection rankiniu būdu) ──
        IStudentRepository saugykla = naudotiApi
            ? new ApiStudentRepository(apiUrl)   // Realūs duomenys iš Docker
            : new MemoryStudentRepository();      // Demo duomenys atmintyje

        IStudentPrinter   spausdintuvas = new ConsoleStudentPrinter();
        IAverageStrategy  strategija    = new SimpleAverageStrategy();
        IStudentValidator validatorius  = new StudentValidatorAdapter();  // Adapter šablonas

        // ── Servisas gauna viską per konstruktorių ─────────────────────
        var servisas = new StudentService(saugykla, spausdintuvas, strategija, validatorius);

        // ── Paleidžiame meniu ──────────────────────────────────────────
        IMenuService meniu = new ConsoleMenuService(servisas);
        meniu.Run();
    }
}
