using Lab5.Interfaces;
using Lab5.Models;

namespace Lab5.Implementations.Printer;

public class FileStudentPrinter : IStudentPrinter
{
    private readonly string _filePath;

    public FileStudentPrinter(string filePath = "students.txt")
    {
        _filePath = filePath;
    }

    public void Print(Group group)
    {
        var lines = new List<string>();
        foreach (var student in group.Students)
        {
            lines.Add($"{student.Id} {student.Name} {student.Email}");
        }
        File.WriteAllLines(_filePath, lines);
        Console.WriteLine($"Students written to {_filePath}");
    }
}
