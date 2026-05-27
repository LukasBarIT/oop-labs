using Lab7.Models;

namespace Lab7.Interfaces;

/// <summary>
/// Validatoriaus interfeisas.
/// Adapter šablonas: LegacyStudentValidation prijungiama per StudentValidatorAdapter.
/// </summary>
public interface IStudentValidator
{
    bool                   Validate(Student student);
    IReadOnlyList<Student> ValidateAll(IReadOnlyList<Student> students);
}
