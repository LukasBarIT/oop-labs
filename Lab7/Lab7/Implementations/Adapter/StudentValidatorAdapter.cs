using Lab7.Interfaces;
using Lab7.Legacy;
using Lab7.Models;

namespace Lab7.Implementations.Adapter;

/// <summary>
/// ADAPTER šablonas — prijungia seną LegacyStudentValidation prie naujo IStudentValidator.
/// 
/// Problema:   LegacyStudentValidation.CheckStudent(string, string)   — sena API
/// Tikslas:    IStudentValidator.Validate(Student)                     — nauja API
/// Sprendimas: Adapteris konvertuoja Student → string parametrus ir perduoda legacy kodo metodui.
/// 
/// Rezultatas: Niekam kitam nereikia žinoti, kad egzistuoja LegacyStudentValidation.
/// </summary>
public class StudentValidatorAdapter : IStudentValidator
{
    // Senas validatorius — slepiamas nuo visų kitų klasių
    private readonly LegacyStudentValidation _legacy = new();

    /// <summary>
    /// Konvertuoja Student objektą į string parametrus
    /// ir perduoda senajam CheckStudent metodui.
    /// </summary>
    public bool Validate(Student student)
        => _legacy.CheckStudent(student.FullName, student.Email);

    /// <summary>Grąžina tik validius studentus iš sąrašo.</summary>
    public IReadOnlyList<Student> ValidateAll(IReadOnlyList<Student> students)
        => students.Where(s => Validate(s)).ToList();
}
