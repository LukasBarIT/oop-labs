// Konsolės meniu — vartotojo sąsajos sluoksnis.
// SRP principas: visi Console.Write / Console.ReadLine yra TIK šiame faile.
// Verslo logika deleguojama StudentService ir ReportService.
using CW1After.Services;

namespace CW1After.UI;

public class ConsoleMenu
{
    private readonly StudentService _studentSrv;
    private readonly ReportService  _ataskaitosSrv;

    public ConsoleMenu(StudentService studentSrv, ReportService ataskaitosSrv)
    {
        _studentSrv    = studentSrv;
        _ataskaitosSrv = ataskaitosSrv;
    }

    public void Run()
    {
        while (true)
        {
            SpausdintMeniu();
            var pasirinkimas = Console.ReadLine()?.Trim();
            Console.WriteLine();

            switch (pasirinkimas)
            {
                case "0": Console.WriteLine("  Iki! 👋"); return;
                case "1": RodytiStudentus();   break;
                case "2": PridėtiStudentą();   break;
                case "3": PridėtiPažymį();     break;
                case "4": RodytiVidurkį();     break;
                case "5": IeškoтiPagalId();    break;
                case "6": ValiduotiStudentą(); break;
                case "7": RodytiTop3();        break;
                case "8": RodytiGrupę();       break;
                case "9": RodytiStatistiką();  break;
                default:
                    Klaida("Nežinomas pasirinkimas. Bandykite dar kartą.");
                    break;
            }
        }
    }

    // ── Meniu langas ────────────────────────────────────────────────────
    private static void SpausdintMeniu()
    {
        Console.WriteLine();
        Console.WriteLine("  ╔══════════════════════════════════════════════╗");
        Console.WriteLine("  ║       STUDENTŲ VALDYMO SISTEMA  v1.0        ║");
        Console.WriteLine("  ║              Lukas Baratinskas               ║");
        Console.WriteLine("  ╠══════════════════════════════════════════════╣");
        Console.WriteLine("  ║  1 │ Rodyti visus studentus                  ║");
        Console.WriteLine("  ║  2 │ Pridėti naują studentą                  ║");
        Console.WriteLine("  ║  3 │ Pridėti pažymį                          ║");
        Console.WriteLine("  ║  4 │ Rodyti studento vidurkį                 ║");
        Console.WriteLine("  ║  5 │ Ieškoti pagal ID                        ║");
        Console.WriteLine("  ║  6 │ Validuoti studentą                      ║");
        Console.WriteLine("  ╠══════════════════════════════════════════════╣");
        Console.WriteLine("  ║  7 │ TOP 3 pagal vidurkį   [LINQ / be LINQ]  ║");
        Console.WriteLine("  ║  8 │ Studentai grupėje     [LINQ / be LINQ]  ║");
        Console.WriteLine("  ║  9 │ Statistika            [LINQ / be LINQ]  ║");
        Console.WriteLine("  ╠══════════════════════════════════════════════╣");
        Console.WriteLine("  ║  0 │ Išeiti                                  ║");
        Console.WriteLine("  ╚══════════════════════════════════════════════╝");
        Console.Write("  Pasirinkimas: ");
    }

    // ── 1) Visi studentai ────────────────────────────────────────────────
    private void RodytiStudentus()
    {
        var studentai = _studentSrv.GetAll();
        Antraste($"Visi studentai ({studentai.Count})");
        Console.WriteLine($"  {"ID",-5} {"Vardas Pavardė",-22} {"Grupė",-6} {"El. paštas",-22} {"Vidurkis"}");
        Console.WriteLine("  " + new string('─', 65));
        foreach (var s in studentai)
            Console.WriteLine($"  {s.Id,-5} {s.Name,-22} {s.GroupCode,-6} {s.Email,-22} {_studentSrv.GetAverage(s):0.00}");
    }

    // ── 2) Pridėti studentą ──────────────────────────────────────────────
    private void PridėtiStudentą()
    {
        Antraste("Naujo studento pridėjimas");
        Console.Write("  ID        : ");
        if (!int.TryParse(Console.ReadLine(), out int id)) { Klaida("ID turi būti skaičius."); return; }
        Console.Write("  Vardas    : ");
        var vardas = Console.ReadLine() ?? "";
        Console.Write("  El. paštas: ");
        var pastas = Console.ReadLine() ?? "";
        Console.Write("  Grupė     : ");
        var grupe = Console.ReadLine() ?? "";

        try
        {
            var (ok, pranesimas) = _studentSrv.AddStudent(id, vardas, pastas, grupe);
            if (ok) Sekmė(pranesimas); else Klaida(pranesimas);
        }
        catch (NotSupportedException ex) { Klaida(ex.Message); }
    }

    // ── 3) Pridėti pažymį ────────────────────────────────────────────────
    private void PridėtiPažymį()
    {
        Antraste("Pažymio pridėjimas");
        Console.Write("  Studento ID: ");
        if (!int.TryParse(Console.ReadLine(), out int id)) { Klaida("ID turi būti skaičius."); return; }
        Console.Write("  Pažymys (1–10): ");
        if (!int.TryParse(Console.ReadLine(), out int pazymys)) { Klaida("Pažymys turi būti skaičius."); return; }

        try
        {
            var (ok, pranesimas) = _studentSrv.AddGrade(id, pazymys);
            if (ok) Sekmė(pranesimas); else Klaida(pranesimas);
        }
        catch (NotSupportedException ex) { Klaida(ex.Message); }
    }

    // ── 4) Rodyti vidurkį ────────────────────────────────────────────────
    private void RodytiVidurkį()
    {
        Antraste("Studento vidurkis");
        Console.Write("  Studento ID: ");
        if (!int.TryParse(Console.ReadLine(), out int id)) { Klaida("ID turi būti skaičius."); return; }
        var s = _studentSrv.FindById(id);
        if (s == null) { Klaida("Studentas nerastas."); return; }
        Console.WriteLine($"\n  {s.Name} → vidurkis: {_studentSrv.GetAverage(s):0.00}");
    }

    // ── 5) Ieškoti pagal ID ──────────────────────────────────────────────
    private void IeškoтiPagalId()
    {
        Antraste("Studento paieška pagal ID");
        Console.Write("  Studento ID: ");
        if (!int.TryParse(Console.ReadLine(), out int id)) { Klaida("ID turi būti skaičius."); return; }
        var s = _studentSrv.FindById(id);
        if (s == null) { Klaida("Studentas nerastas."); return; }

        Console.WriteLine($"\n  [{s.Id}] {s.Name}");
        Console.WriteLine($"  Grupė    : {s.GroupCode}");
        Console.WriteLine($"  El. paštas: {s.Email}");
        Console.WriteLine($"  Pažymiai : [{string.Join(", ", s.Grades)}]");
        Console.WriteLine($"  Vidurkis : {_studentSrv.GetAverage(s):0.00}");
    }

    // ── 6) Validuoti ─────────────────────────────────────────────────────
    private void ValiduotiStudentą()
    {
        Antraste("Studento validacija");
        Console.Write("  Studento ID: ");
        if (!int.TryParse(Console.ReadLine(), out int id)) { Klaida("ID turi būti skaičius."); return; }
        var s = _studentSrv.FindById(id);
        if (s == null) { Klaida("Studentas nerastas."); return; }

        var (ok, klaidos) = _studentSrv.ValidateStudent(id);
        if (ok)
            Sekmė($"{s.Name} — duomenys teisingi ✓");
        else
        {
            Klaida($"{s.Name} — rasta klaidų:");
            foreach (var k in klaidos)
                Console.WriteLine($"    • {k}");
        }
    }

    // ── 7) TOP 3 ─────────────────────────────────────────────────────────
    private void RodytiTop3()
    {
        Antraste("TOP 3 studentai pagal vidurkį");

        Console.WriteLine("  ┌─ Su LINQ ──────────────────────────────────┐");
        foreach (var (s, avg) in _ataskaitosSrv.GetTopByAverage(3))
            Console.WriteLine($"  │  {s.Name,-26} vidurkis: {avg:0.00}  │");
        Console.WriteLine("  └─────────────────────────────────────────────┘");

        Console.WriteLine();
        Console.WriteLine("  ┌─ Be LINQ ───────────────────────────────────┐");
        foreach (var (s, avg) in _ataskaitosSrv.GetTopByAverageWithoutLinq(3))
            Console.WriteLine($"  │  {s.Name,-26} vidurkis: {avg:0.00}  │");
        Console.WriteLine("  └─────────────────────────────────────────────┘");
    }

    // ── 8) Grupės studentai ──────────────────────────────────────────────
    private void RodytiGrupę()
    {
        Antraste("Studentai grupėje (surūšiuoti pagal vardą)");
        Console.Write("  Grupės kodas (pvz. PI23): ");
        var kodas = Console.ReadLine()?.Trim() ?? "";

        Console.WriteLine($"\n  ┌─ Su LINQ [{kodas}] ─────────────────────────┐");
        var linqRez = _ataskaitosSrv.GetStudentsInGroupSortedByName(kodas);
        if (linqRez.Count == 0) Console.WriteLine("  │  (grupė tuščia arba neegzistuoja)           │");
        foreach (var (s, avg) in linqRez)
            Console.WriteLine($"  │  [{s.Id}] {s.Name,-24} avg: {avg:0.00}  │");
        Console.WriteLine("  └─────────────────────────────────────────────┘");

        Console.WriteLine();
        Console.WriteLine($"  ┌─ Be LINQ [{kodas}] ────────────────────────┐");
        var bezLinqRez = _ataskaitosSrv.GetStudentsInGroupSortedByNameWithoutLinq(kodas);
        if (bezLinqRez.Count == 0) Console.WriteLine("  │  (grupė tuščia arba neegzistuoja)           │");
        foreach (var (s, avg) in bezLinqRez)
            Console.WriteLine($"  │  [{s.Id}] {s.Name,-24} avg: {avg:0.00}  │");
        Console.WriteLine("  └─────────────────────────────────────────────┘");
    }

    // ── 9) Statistika ────────────────────────────────────────────────────
    private void RodytiStatistiką()
    {
        Antraste("Sistemos statistika");
        Console.WriteLine("  ┌─ Su LINQ ───────────────┬─ Be LINQ ───────────────┐");
        SpausdintStatistika(_ataskaitosSrv.GetStatistics(), _ataskaitosSrv.GetStatisticsWithoutLinq());
        Console.WriteLine("  └─────────────────────────┴─────────────────────────┘");
    }

    private static void SpausdintStatistika(ReportService.Statistika a, ReportService.Statistika b)
    {
        StatEilute("Studentų skaičius",  a.VisoStudentu.ToString(),        b.VisoStudentu.ToString());
        StatEilute("Pažymių skaičius",   a.VisoPazymiu.ToString(),         b.VisoPazymiu.ToString());
        StatEilute("Vidurkių vidurkis",  a.VidurkiuVidurkis.ToString("0.00"), b.VidurkiuVidurkis.ToString("0.00"));
        StatEilute("Aukščiausias pažym.",a.AuksciausiasP.ToString(),       b.AuksciausiasP.ToString());
        StatEilute("Yra neišlaikusiųjų", a.YraNeisgavusiu.ToString(),      b.YraNeisgavusiu.ToString());
        StatEilute("Visi turi el. paštą",a.VisiTuriPasta.ToString(),       b.VisiTuriPasta.ToString());
    }

    private static void StatEilute(string pav, string linqVal, string bezLinqVal)
        => Console.WriteLine($"  │  {pav,-22}  {linqVal,-8} │  {bezLinqVal,-8}               │");

    // ── Pagalbiniai metodai ──────────────────────────────────────────────
    private static void Antraste(string tekstas)
        => Console.WriteLine($"\n  ═══ {tekstas} ═══");

    private static void Sekmė(string tekstas)
        => Console.WriteLine($"  ✓ {tekstas}");

    private static void Klaida(string tekstas)
        => Console.WriteLine($"  ✗ {tekstas}");
}
