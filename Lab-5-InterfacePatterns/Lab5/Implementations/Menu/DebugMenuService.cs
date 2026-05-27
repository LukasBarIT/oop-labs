using Lab5.Interfaces;
using Lab5.Services;
using Lab5.Models;

namespace Lab5.Implementations.Menu;

public class DebugMenuService : IMenuService
{
    private readonly StudentService _service;

    public DebugMenuService(StudentService service)
    {
        _service = service;
    }

    public void Run()
    {
        Console.WriteLine("[DEBUG] LAB-5 running in debug mode.");

        var group = new Group();

        var s1 = new Student(1, "Alice", "alice@test.com");
        s1.AddGrade(9);
        s1.AddGrade(7);
        s1.AddGrade(8);

        var s2 = new Student(2, "Bob", "bob@test.com");
        s2.AddGrade(6);
        s2.AddGrade(5);

        group.AddStudent(s1);
        group.AddStudent(s2);

        Console.WriteLine("[DEBUG] Printing group:");
        _service.PrintGroup(group);

        Console.WriteLine($"[DEBUG] Alice average: {_service.CalculateAverage(s1)}");
        Console.WriteLine($"[DEBUG] Alice valid: {_service.ValidateStudent(s1)}");
    }
}
