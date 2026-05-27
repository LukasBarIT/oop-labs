using Lab6.Interfaces;
using Lab6.Models;

namespace Lab6.Validators;

public class StudentValidator : IStudentValidator
{
    private const double MinGrade = 1.0;
    private const double MaxGrade = 10.0;

    public bool Validate(Student student)
    {
        if (string.IsNullOrWhiteSpace(student.Name)) return false;
        if (student.Grade < MinGrade || student.Grade > MaxGrade) return false;
        if (student.Credits < 0) return false;
        return true;
    }

    public List<Student> ValidateAll(List<Student> students) =>
        students.Where(Validate).ToList();
}
