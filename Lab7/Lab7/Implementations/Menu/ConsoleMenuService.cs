using Lab7.Interfaces;
using Lab7.Services;

namespace Lab7.Implementations.Menu;

/// <summary>
/// Konsolės meniu — vartotojo sąsaja.
/// 
/// SRP principas: ši klasė atsakinga TIK už vartotojo įvesties apdorojimą.
/// Verslo logika deleguojama StudentService.
/// </summary>
public class ConsoleMenuService : IMenuService
{
    // Meniu naudoja tik StudentService — niekada tiesiogiai repository/printer
    private readonly StudentService _service;

    public ConsoleMenuService(StudentService service) => _service = service;

    public void Run()
    {
        Console.WriteLine("\n╔══════════════════════════════════════════╗");
        Console.WriteLine("║   LAB-7  —  Saugykla + API Demo         ║");
        Console.WriteLine("╚══════════════════════════════════════════╝");

        while (true)
        {
            SpausdintMeniu();

            switch (Console.ReadLine()?.Trim())
            {
                case "1":
                    // Visi studentai
                    _service.PrintAllStudents();
                    break;

                case "2":
                    // Paieška pagal ID
                    Console.Write("  Studento ID: ");
                    if (int.TryParse(Console.ReadLine(), out var id))
                    {
                        var s = _service.FindStudentById(id);
                        Console.WriteLine(s is null
                            ? "  Nerastas."
                            : $"\n  {s.Id,-5} {s.FullName,-25} {s.Email,-30} {s.StudyProgram} {s.EnrollmentYear}");
                    }
                    break;

                case "3":
                    // Grupės vidurkis
                    Console.WriteLine($"\n  Grupės vidurkis: {_service.CalculateGroupAverage():0.00}");
                    break;

                case "4":
                    // Visų grupių sąrašas
                    var grupes = _service.GetAllGroups();
                    Console.WriteLine($"\n  Grupės ({grupes.Count} viso):");
                    foreach (var g in grupes)
                        Console.WriteLine($"  [{g.Code,-8}]  {g.StudyProgram,-25} {g.EnrollmentYear}  — {g.Students.Count} studentų");
                    break;

                case "5":
                    // Vienos grupės detalės
                    Console.Write("  Grupės kodas (pvz. I-23): ");
                    _service.PrintGroup(Console.ReadLine()?.Trim() ?? "");
                    break;

                case "6":
                    // Visas fakultetas
                    _service.PrintFaculty();
                    break;

                case "0":
                    Console.WriteLine("\n  Viso gero!\n");
                    return;

                default:
                    Console.WriteLine("  Nežinoma komanda.");
                    break;
            }
        }
    }

    /// <summary>Atspausdina meniu pasirinkimus.</summary>
    private static void SpausdintMeniu()
    {
        Console.WriteLine("\n  ── Studentai ────────────────────────────");
        Console.WriteLine("  1. Rodyti visus studentus");
        Console.WriteLine("  2. Ieškoti studento pagal ID");
        Console.WriteLine("  3. Skaičiuoti grupės vidurkį");
        Console.WriteLine("  ── Grupės ir fakultetas ─────────────────");
        Console.WriteLine("  4. Rodyti visas grupes");
        Console.WriteLine("  5. Rodyti grupės detales");
        Console.WriteLine("  6. Rodyti fakulteto struktūrą");
        Console.WriteLine("  ─────────────────────────────────────────");
        Console.WriteLine("  0. Išeiti");
        Console.Write("\n  Jūsų pasirinkimas: ");
    }
}
