// Composition Root — vienintelė vieta kur pasirenkamos konkrečios klasės.
// Visur kitur naudojami TIK interfeisai (IStudentRepository).
// Task 3: repository pasirenkamas pagal komandinės eilutės argumentą --stub.
using CW1After.Interfaces;
using CW1After.Services;
using CW1After.UI;

namespace CW1After;

public static class Program
{
    public static void Main(string[] args)
    {
        // Tikriname ar perduotas --stub argumentas
        bool naudotiStub = args.Contains("--stub");

        // Pasirenkame saugyklą — MemoryStudentRepository arba StubStudentRepository.
        // StudentService nežino kuris iš jų naudojamas — jis dirba tik su IStudentRepository.
        IStudentRepository saugykla = naudotiStub
            ? new StubStudentRepository()
            : new MemoryStudentRepository();

        Console.WriteLine(naudotiStub
            ? "[INFO] Using StubStudentRepository (--stub)."
            : "[INFO] Using MemoryStudentRepository (default).");

        // Konstruktoriaus injekcija — visos priklausomybės surenkamos čia
        var skaiciuokle  = new AverageCalculator();
        var validatorius = new StudentValidator(saugykla);
        var studentuSrv  = new StudentService(saugykla, skaiciuokle, validatorius);
        var ataskaitosSrv = new ReportService(saugykla, skaiciuokle);

        // Paleidžiame meniu
        var meniu = new ConsoleMenu(studentuSrv, ataskaitosSrv);
        meniu.Run();
    }
}
