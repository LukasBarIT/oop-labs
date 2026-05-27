namespace Lab7.Models;

/// <summary>
/// Studentas — pagrindinis duomenų modelis.
/// Duomenys nekeičiami po sukūrimo (tik init).
/// Pažymiai saugomi privačiai, pasiekiami per IReadOnlyList.
/// </summary>
public class Student
{
    // ── Pagrindiniai laukai ────────────────────────────────────────────
    public int    Id             { get; }
    public string FirstName      { get; }
    public string LastName       { get; }
    public string FullName       => $"{FirstName} {LastName}";  // Pilnas vardas
    public string Email          { get; }
    public string StudyProgram   { get; }
    public int    EnrollmentYear { get; }

    // ── Pažymiai — privati kolekcija, išorė mato tik per IReadOnlyList ─
    private readonly List<int> _grades = new();
    public IReadOnlyList<int> Grades => _grades;

    public Student(int id, string firstName, string lastName,
                   string email, string studyProgram, int enrollmentYear)
    {
        Id             = id;
        FirstName      = firstName;
        LastName       = lastName;
        Email          = email;
        StudyProgram   = studyProgram;
        EnrollmentYear = enrollmentYear;
    }

    /// <summary>Prideda pažymį studentui.</summary>
    public void AddGrade(int grade) => _grades.Add(grade);
}
