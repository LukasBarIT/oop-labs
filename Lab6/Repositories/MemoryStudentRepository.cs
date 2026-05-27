using Lab6.Interfaces;
using Lab6.Models;

namespace Lab6.Repositories;

public class MemoryStudentRepository : IStudentRepository
{
    // Container is PRIVATE — encapsulation rule
    private readonly List<Student> _students = new();
    private int _nextId = 1;

    public List<Student> GetAll() => new List<Student>(_students); // return copy

    public Student? GetById(int id) =>
        _students.FirstOrDefault(s => s.Id == id);

    public void Add(Student student)
    {
        student.Id = _nextId++;
        _students.Add(student);
    }

    public bool Remove(int id)
    {
        var student = _students.FirstOrDefault(s => s.Id == id);
        if (student is null) return false;
        _students.Remove(student);
        return true;
    }
}
