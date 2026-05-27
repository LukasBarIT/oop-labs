using Lab7.Interfaces;
using Lab7.Models;

namespace Lab7.Implementations.Printer;

/// <summary>
/// Konsolės spausdintuvas — atvaizduoja studentus, grupes ir fakultetą.
/// 
/// SRP principas: ši klasė atsakinga TIK už atvaizdavimą.
/// Jokios verslo logikos, jokių skaičiavimų čia nėra.
/// </summary>
public class ConsoleStudentPrinter : IStudentPrinter
{
    /// <summary>Atspausdina visų studentų sąrašą lentelės formatu.</summary>
    public void PrintStudents(IReadOnlyList<Student> students)
    {
        if (students.Count == 0)
        {
            Console.WriteLine("  (studentų nėra)");
            return;
        }

        // Antraštė
        Console.WriteLine($"\n  {"ID",-5} {"Vardas Pavardė",-25} {"El. paštas",-30} {"Programa",-20} {"Metai"}");
        Console.WriteLine("  " + new string('─', 87));

        foreach (var s in students)
            Console.WriteLine($"  {s.Id,-5} {s.FullName,-25} {s.Email,-30} {s.StudyProgram,-20} {s.EnrollmentYear}");
    }

    /// <summary>Atspausdina grupės informaciją ir jos studentus.</summary>
    public void PrintGroup(Group grupe)
    {
        Console.WriteLine($"\n  ┌─ [{grupe.Code}]  {grupe.StudyProgram}  |  Metai: {grupe.EnrollmentYear}  |  Studentų: {grupe.Students.Count}");
        Console.WriteLine($"  │");

        foreach (var s in grupe.Students)
            Console.WriteLine($"  │  {s.Id,-5} {s.FullName,-25} {s.Email}");

        Console.WriteLine($"  └{"─",50}");
    }

    /// <summary>Atspausdina fakulteto struktūrą su visomis grupėmis.</summary>
    public void PrintFaculty(Faculty fakultetas)
    {
        Console.WriteLine($"\n  ╔══ {fakultetas.Name}");
        Console.WriteLine($"  ║   Grupių: {fakultetas.Groups.Count}   |   Iš viso studentų: {fakultetas.TotalStudents}");
        Console.WriteLine($"  ╠{"═",60}");

        foreach (var g in fakultetas.Groups)
            Console.WriteLine($"  ║   [{g.Code,-8}]  {g.StudyProgram,-25}  {g.EnrollmentYear}   — {g.Students.Count} studentų");

        Console.WriteLine($"  ╚{"═",60}");
    }
}
