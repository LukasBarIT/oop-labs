using Lab5.Interfaces;
using Lab5.Models;

namespace Lab5.Implementations.Adapter;

public class StrictStudentValidator : IStudentValidator
{
    public bool Validate(Student student)
    {
        if (string.IsNullOrWhiteSpace(student.Name))
            return false;

        if (!student.Email.Contains("@") || !student.Email.Contains("."))
            return false;

        return true;
    }
}
