// Atminties saugykla — saugo studentus ir grupes List<T> kolekcijose.
// Naudojama pagal nutylėjimą (be --stub argumento).
using CW1After.Interfaces;
using CW1After.Models;

namespace CW1After.Services;

public class MemoryStudentRepository : IStudentRepository
{
    // Privačios kolekcijos — išorė negali jų tiesiogiai pasiekti (enkapsuliacijos principas)
    private readonly List<Student> _students = new();
    private readonly List<Group>   _groups   = new();

    public MemoryStudentRepository()
    {
        // Pradinės grupės
        _groups.Add(new Group { Code = "PI23", Name = "Programu inzinerija 2023" });
        _groups.Add(new Group { Code = "PI24", Name = "Programu inzinerija 2024" });

        // Pradiniai studentai su pažymiais
        _students.Add(new Student { Id = 1, Name = "Jonas Jonaitis",     Email = "jonas@kauko.lt",  GroupCode = "PI23", Grades = new() { 8, 9, 7, 10 } });
        _students.Add(new Student { Id = 2, Name = "Greta Petraityte",   Email = "greta@kauko.lt",  GroupCode = "PI23", Grades = new() { 6, 5, 7, 8  } });
        _students.Add(new Student { Id = 3, Name = "Mantas Kazlauskas",  Email = "mantas@kauko.lt", GroupCode = "PI24", Grades = new() { 9, 9, 10, 8 } });
        _students.Add(new Student { Id = 4, Name = "Ieva Andriukaityte", Email = "ieva@kauko.lt",   GroupCode = "PI23", Grades = new() { 10, 10, 9, 9 } });
        _students.Add(new Student { Id = 5, Name = "Tomas Bagdonas",     Email = "tomas@kauko.lt",  GroupCode = "PI24", Grades = new() { 5, 6, 6, 7  } });
    }

    public List<Student> GetAllStudents() => _students;
    public List<Group>   GetAllGroups()   => _groups;

    // FirstOrDefault — grąžina null jei studentas nerastas
    public Student? FindById(int id) => _students.FirstOrDefault(s => s.Id == id);

    public void AddStudent(Student student) => _students.Add(student);

    public void AddGrade(int studentId, int grade)
    {
        var s = FindById(studentId);
        s?.Grades.Add(grade); // ?. — nieko nedaro jei studentas nerastas
    }
}
