using Lab5.Interfaces;
using Lab5.Models;

namespace Lab5.Implementations.Repository;

public class FileStudentRepository : IStudentRepository
{
    private readonly string _filePath;

    public FileStudentRepository(string filePath = "students.txt")
    {
        _filePath = filePath;
    }

    public Student? Find(string query)
    {
        if (!File.Exists(_filePath))
            return null;

        var lines = File.ReadAllLines(_filePath);
        foreach (var line in lines)
        {
            if (line.Contains(query, StringComparison.OrdinalIgnoreCase))
            {
                var parts = line.Split(' ');
                if (parts.Length >= 3 && int.TryParse(parts[0], out int id))
                    return new Student(id, parts[1], parts[2]);
            }
        }

        return null;
    }
}
