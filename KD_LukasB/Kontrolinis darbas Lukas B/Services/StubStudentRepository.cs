// Stub saugykla — naudojama testavimui su "--stub" komandinės eilutės argumentu.
// Grąžina tik vieną fiksuotą studentą ir grupę — naudinga demonstracijai ir testavimui.
// Task 3: perjungiama per "dotnet run -- --stub"
using CW1After.Interfaces;
using CW1After.Models;

namespace CW1After.Services;

public class StubStudentRepository : IStudentRepository
{
    // Viena fiksuota grupė testavimui
    private readonly List<Group> _groups = new()
    {
        new Group { Code = "TEST", Name = "Test grupe" }
    };

    // Vienas fiksuotas studentas su maksimaliais pažymiais
    private readonly List<Student> _students = new()
    {
        new Student { Id = 999, Name = "Test Student", Email = "test@test.lt", GroupCode = "TEST", Grades = new() { 10, 10, 10 } }
    };

    public List<Student> GetAllStudents() => _students;
    public List<Group>   GetAllGroups()   => _groups;
    public Student?      FindById(int id) => _students.FirstOrDefault(s => s.Id == id);

    // Stub saugykla nepalaiko rašymo operacijų — tai yra tik skaitymui skirtas stub
    public void AddStudent(Student student)        => throw new NotSupportedException("Stub saugykla nepalaiko studentų pridėjimo.");
    public void AddGrade(int studentId, int grade) => throw new NotSupportedException("Stub saugykla nepalaiko pažymių pridėjimo.");
}
