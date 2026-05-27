namespace Lab7.Models;

/// <summary>
/// Grupė — studentų rinkinys pagal studijų programą ir įstojimo metus.
/// Studentai saugomi privačiai — išorė mato tik IReadOnlyList.
/// </summary>
public class Group
{
    public string Code           { get; }   // Grupės kodas, pvz. "CS-23"
    public string StudyProgram   { get; }   // Studijų programa
    public int    EnrollmentYear { get; }   // Įstojimo metai

    // ── Privati studentų kolekcija — kapsuliavimasas ───────────────────
    private readonly List<Student> _students = new();
    public IReadOnlyList<Student> Students => _students;

    public Group(string code, string studyProgram, int enrollmentYear)
    {
        Code           = code;
        StudyProgram   = studyProgram;
        EnrollmentYear = enrollmentYear;
    }

    /// <summary>
    /// Prideda studentą į grupę.
    /// Kviečia tik Repository — išoriniai objektai tiesiogiai neprideda.
    /// </summary>
    public void AddStudent(Student student) => _students.Add(student);
}
