using Lab6.Interfaces;
using Lab6.Models;

namespace Lab6.Printers;

public class ConsoleStudentPrinter : IStudentPrinter
{
    public void PrintStudents(List<Student> students)
    {
        if (students.Count == 0)
        {
            Console.WriteLine("  (no students to display)");
            return;
        }

        Console.WriteLine(new string('-', 50));
        Console.WriteLine($"  {"ID",-5} {"Name",-20} {"Grade",6}  {"Credits",8}");
        Console.WriteLine(new string('-', 50));
        foreach (var s in students)
            Console.WriteLine($"  {s.Id,-5} {s.Name,-20} {s.Grade,6:F1}  {s.Credits,8}");
        Console.WriteLine(new string('-', 50));
        Console.WriteLine($"  Total students: {students.Count}");
    }
}
