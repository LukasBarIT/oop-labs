using Lab6.Interfaces;
using Lab6.Models;

namespace Lab6.Services;

public class StudentService
{
    private readonly IStudentRepository _repository;
    private readonly IStudentPrinter _printer;
    private readonly IAverageStrategy _averageStrategy;
    private readonly IStudentValidator _validator;

    // Constructor injection
    public StudentService(
        IStudentRepository repository,
        IStudentPrinter printer,
        IAverageStrategy averageStrategy,
        IStudentValidator validator)
    {
        _repository = repository;
        _printer = printer;
        _averageStrategy = averageStrategy;
        _validator = validator;
    }

    public void AddStudent(Student student) =>
        _repository.Add(student);

    public bool RemoveStudent(int id) =>
        _repository.Remove(id);

    public Student? FindStudent(int id) =>
        _repository.GetById(id);

    // Task 5 — service methods work with collections
    public void PrintAllStudents()
    {
        var students = _repository.GetAll();
        _printer.PrintStudents(students);
    }

    public void PrintValidStudents()
    {
        var students = _repository.GetAll();
        var valid = _validator.ValidateAll(students);
        Console.WriteLine($"  Valid: {valid.Count} / {students.Count}");
        _printer.PrintStudents(valid);
    }

    public double CalculateGroupAverage()
    {
        var students = _repository.GetAll();
        var valid = _validator.ValidateAll(students);
        return _averageStrategy.Calculate(valid);
    }

    // Task 6 — full flow
    public void RunFullFlow()
    {
        Console.WriteLine("\n=== FULL FLOW ===");

        var students = _repository.GetAll();
        Console.WriteLine($"  Step 1 — Retrieved {students.Count} students");

        var valid = _validator.ValidateAll(students);
        Console.WriteLine($"  Step 2 — Valid students: {valid.Count}");

        var avg = _averageStrategy.Calculate(valid);
        Console.WriteLine($"  Step 3 — Group average ({_averageStrategy.Name}): {avg:F2}");

        Console.WriteLine("  Step 4 — Printing valid students:");
        _printer.PrintStudents(valid);
    }
}
