using Lab6.Models;

namespace Lab6.Interfaces;

public interface IStudentValidator
{
    bool Validate(Student student);
    List<Student> ValidateAll(List<Student> students);
}
