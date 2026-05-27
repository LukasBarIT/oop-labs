using Lab6.Interfaces;
using Lab6.Models;
using Lab6.Services;

namespace Lab6.Services;

public class MenuService : IMenuService
{
    private readonly StudentService _studentService;

    public MenuService(StudentService studentService)
    {
        _studentService = studentService;
    }

    public void Run()
    {
        Console.WriteLine("╔══════════════════════════════╗");
        Console.WriteLine("║     LAB-6  Student Manager   ║");
        Console.WriteLine("╚══════════════════════════════╝");

        bool running = true;
        while (running)
        {
            PrintMenu();
            var choice = Console.ReadLine()?.Trim();
            Console.WriteLine();
            switch (choice)
            {
                case "1": AddStudent(); break;
                case "2": _studentService.PrintAllStudents(); break;
                case "3": _studentService.PrintValidStudents(); break;
                case "4": ShowAverage(); break;
                case "5": _studentService.RunFullFlow(); break;
                case "6": RemoveStudent(); break;
                case "0": running = false; break;
                default: Console.WriteLine("  Unknown option."); break;
            }
        }
        Console.WriteLine("Goodbye!");
    }

    private static void PrintMenu()
    {
        Console.WriteLine("\n  [1] Add student");
        Console.WriteLine("  [2] Print all students");
        Console.WriteLine("  [3] Print valid students only");
        Console.WriteLine("  [4] Show group average");
        Console.WriteLine("  [5] Run full flow");
        Console.WriteLine("  [6] Remove student");
        Console.WriteLine("  [0] Exit");
        Console.Write("\n  Choice: ");
    }

    private void AddStudent()
    {
        Console.Write("  Name: ");
        var name = Console.ReadLine() ?? "";

        Console.Write("  Grade (1-10): ");
        if (!double.TryParse(Console.ReadLine(), out double grade))
        {
            Console.WriteLine("  Invalid grade."); return;
        }

        Console.Write("  Credits: ");
        if (!int.TryParse(Console.ReadLine(), out int credits))
        {
            Console.WriteLine("  Invalid credits."); return;
        }

        _studentService.AddStudent(new Student { Name = name, Grade = grade, Credits = credits });
        Console.WriteLine("  Student added.");
    }

    private void RemoveStudent()
    {
        Console.Write("  Student ID to remove: ");
        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("  Invalid ID."); return;
        }
        bool ok = _studentService.RemoveStudent(id);
        Console.WriteLine(ok ? "  Removed." : "  Student not found.");
    }

    private void ShowAverage()
    {
        double avg = _studentService.CalculateGroupAverage();
        Console.WriteLine($"  Group average: {avg:F2}");
    }
}
