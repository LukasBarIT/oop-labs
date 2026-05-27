using Lab5.Interfaces;
using Lab5.Services;
using Lab5.Models;

namespace Lab5.Implementations.Menu;

public class WebMenuSimulationService : IMenuService
{
    private readonly StudentService _service;

    public WebMenuSimulationService(StudentService service)
    {
        _service = service;
    }

    public void Run()
    {
        Console.WriteLine("[WEB] Simulating web request handling...");
        Console.WriteLine("[WEB] GET /students");

        var group = new Group();
        var student = new Student(5, "WebUser", "web@test.com");
        student.AddGrade(10);
        student.AddGrade(9);
        group.AddStudent(student);

        Console.WriteLine("[WEB] Response:");
        _service.PrintGroup(group);

        Console.WriteLine($"[WEB] Average score: {_service.CalculateAverage(student)}");
        Console.WriteLine("[WEB] 200 OK");
    }
}
