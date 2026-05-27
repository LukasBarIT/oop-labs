// Studentų servisas — verslo logikos sluoksnis (meniu punktai 1–6).
// Priklauso nuo IStudentRepository interfeiso, ne nuo konkrečios klasės (OCP principas).
// Visi duomenys gaunami per konstruktoriaus injekciją (Dependency Injection).
using CW1After.Interfaces;
using CW1After.Models;

namespace CW1After.Services;

public class StudentService
{
    private readonly IStudentRepository _repo;
    private readonly AverageCalculator  _calc;
    private readonly StudentValidator   _validator;

    // Konstruktoriaus injekcija — visos priklausomybės perduodamos iš išorės
    public StudentService(IStudentRepository repo, AverageCalculator calc, StudentValidator validator)
    {
        _repo      = repo;
        _calc      = calc;
        _validator = validator;
    }

    // Grąžina visų studentų sąrašą
    public List<Student> GetAll() => _repo.GetAllStudents();

    // Ieško studento pagal ID
    public Student? FindById(int id) => _repo.FindById(id);

    // Apskaičiuoja studento vidurkį per AverageCalculator (DRY principas)
    public double GetAverage(Student s) => _calc.Calculate(s.Grades);

    // Prideda naują studentą su visų laukų validacija
    public (bool ok, string message) AddStudent(int id, string name, string email, string groupCode)
    {
        if (_repo.FindById(id) != null)
            return (false, "Studentas su šiuo ID jau egzistuoja.");

        if (string.IsNullOrWhiteSpace(name))
            return (false, "Vardas negali būti tuščias.");

        if (!email.Contains('@') || !email.Contains('.'))
            return (false, "Neteisingas el. pašto formatas.");

        if (_repo.GetAllGroups().All(g => g.Code != groupCode))
            return (false, "Tokios grupės nėra sistemoje.");

        _repo.AddStudent(new Student { Id = id, Name = name, Email = email, GroupCode = groupCode });
        return (true, "Studentas sėkmingai pridėtas.");
    }

    // Prideda pažymį studentui su ribų patikrinimu
    public (bool ok, string message) AddGrade(int studentId, int grade)
    {
        if (_repo.FindById(studentId) == null)
            return (false, "Studentas nerastas.");

        if (grade < 1 || grade > 10)
            return (false, "Pažymys turi būti tarp 1 ir 10.");

        _repo.AddGrade(studentId, grade);
        return (true, $"Pažymys {grade} pridėtas studentui {_repo.FindById(studentId)!.Name}.");
    }

    // Grąžina validacijos rezultatą: ar OK ir klaidų sąrašas
    public (bool ok, List<string> errors) ValidateStudent(int id)
    {
        var s = _repo.FindById(id);
        if (s == null) return (false, new List<string> { "Studentas nerastas." });
        var errors = _validator.Validate(s);
        return (errors.Count == 0, errors);
    }
}
