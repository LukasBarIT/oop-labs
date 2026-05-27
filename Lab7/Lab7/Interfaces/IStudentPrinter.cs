using Lab7.Models;

namespace Lab7.Interfaces;

/// <summary>
/// Spausdintuvo interfeisas — atsakingas tik už duomenų atvaizdavimą.
/// Jokios verslo logikos čia nebus (SRP principas).
/// </summary>
public interface IStudentPrinter
{
    void PrintStudents(IReadOnlyList<Student> students);  // Studentų sąrašas
    void PrintGroup(Group group);                         // Viena grupė
    void PrintFaculty(Faculty faculty);                   // Visas fakultetas
}
