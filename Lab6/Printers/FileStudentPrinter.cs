using Lab6.Interfaces;
using Lab6.Models;

namespace Lab6.Printers;

public class FileStudentPrinter : IStudentPrinter
{
    private readonly string _filePath;

    public FileStudentPrinter(string filePath = "students.txt")
    {
        _filePath = filePath;
    }

    public void PrintStudents(List<Student> students)
    {
        var lines = new List<string>
        {
            $"=== Student Report ({DateTime.Now:yyyy-MM-dd HH:mm}) ===",
            $"Total: {students.Count}"
        };

        foreach (var s in students)
            lines.Add(s.ToString());

        File.WriteAllLines(_filePath, lines);
        Console.WriteLine($"  Saved to {_filePath}");
    }
}
