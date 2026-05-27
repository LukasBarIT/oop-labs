using System.Text.Json;
using Lab6.Interfaces;
using Lab6.Models;

namespace Lab6.Printers;

public class JsonStudentPrinter : IStudentPrinter
{
    private readonly string _filePath;

    public JsonStudentPrinter(string filePath = "students.json")
    {
        _filePath = filePath;
    }

    public void PrintStudents(List<Student> students)
    {
        var json = JsonSerializer.Serialize(students, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(_filePath, json);
        Console.WriteLine($"  Saved JSON to {_filePath}");
    }
}
