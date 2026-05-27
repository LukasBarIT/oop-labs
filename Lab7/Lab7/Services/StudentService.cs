using Lab7.Interfaces;
using Lab7.Models;

namespace Lab7.Services;

/// <summary>
/// Verslo logikos sluoksnis — koordinuoja visas operacijas.
/// 
/// Ši klasė NEŽINO:
///   - Kaip saugomi duomenys (List? Dictionary? Duomenų bazė?)
///   - Iš kur ateina duomenys (atmintis? API? failas?)
///   - Kaip atrodo vidinė saugyklos struktūra
/// 
/// OCP principas: duomenų šaltinis pasikeitė (Lab6 → Lab7 su API),
/// bet ŠI KLASĖ NEPASIKEITĖ — ji naudoja tik IStudentRepository interfeisą.
/// </summary>
public class StudentService
{
    // ── Priklausomybės — tik interfeisai, jokių konkrečių klasių ──────
    private readonly IStudentRepository _repository;
    private readonly IStudentPrinter    _printer;
    private readonly IAverageStrategy   _strategy;
    private readonly IStudentValidator  _validator;

    /// <summary>
    /// Konstruktoriaus injekcija — visos priklausomybės perduodamos iš išorės.
    /// Tai leidžia lengvai keisti implementacijas nekeičiant šios klasės.
    /// </summary>
    public StudentService(
        IStudentRepository repository,
        IStudentPrinter    printer,
        IAverageStrategy   strategy,
        IStudentValidator  validator)
    {
        _repository = repository;
        _printer    = printer;
        _strategy   = strategy;
        _validator  = validator;
    }

    // ── Studentų operacijos ────────────────────────────────────────────

    /// <summary>Prideda studentą tik jei praeina validaciją.</summary>
    public void AddStudent(Student studentas)
    {
        if (_validator.Validate(studentas))
            _repository.Add(studentas);
        else
            Console.WriteLine($"  Validacija nepraėjo: {studentas.FullName}");
    }

    public IReadOnlyList<Student> GetAllStudents() => _repository.GetAll();
    public Student? FindStudentById(int id)        => _repository.GetById(id);
    public bool RemoveStudent(int id)              => _repository.Remove(id);

    /// <summary>Atspausdina visus studentus per printer.</summary>
    public void PrintAllStudents()
        => _printer.PrintStudents(_repository.GetAll());

    /// <summary>
    /// Apskaičiuoja grupės vidurkį tik iš validių studentų.
    /// Validacija → Strategija → Rezultatas.
    /// </summary>
    public double CalculateGroupAverage()
    {
        var validus = _validator.ValidateAll(_repository.GetAll());
        return _strategy.Calculate(validus);
    }

    // ── Grupių operacijos ──────────────────────────────────────────────

    public IReadOnlyList<Group> GetAllGroups() => _repository.GetAllGroups();

    /// <summary>Ieško grupės pagal kodą ir ją atspausdina.</summary>
    public void PrintGroup(string gruposKodas)
    {
        var grupe = _repository.GetGroupByCode(gruposKodas);
        if (grupe is null)
            Console.WriteLine($"  Grupė '{gruposKodas}' nerasta.");
        else
            _printer.PrintGroup(grupe);
    }

    // ── Fakulteto operacijos ───────────────────────────────────────────

    public Faculty GetFaculty() => _repository.GetFaculty();
    public void PrintFaculty()  => _printer.PrintFaculty(_repository.GetFaculty());
}
