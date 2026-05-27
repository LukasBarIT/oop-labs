using Lab7.Interfaces;
using Lab7.Models;

namespace Lab7.Implementations.Repository;

/// <summary>
/// Atminties saugykla — naudojama kai Docker API nepaleista.
/// Naudoja tą pačią vidinę struktūrą (Dictionary) kaip ApiStudentRepository,
/// kad studentai matytų šabloną be Docker.
/// 
/// ENCAPSULATION pavyzdys:
///   VIDUJE  — sudėtinga: Dictionary, grupių kūrimas, kodo generavimas
///   IŠORĖJE — paprasta:  IReadOnlyList<Student>, Group, Faculty
/// </summary>
public class MemoryStudentRepository : IStudentRepository
{
    // ── Vidinės duomenų struktūros (paslėptos nuo visų klientų) ───────
    private readonly Dictionary<int, Student>  _studentsById  = new();  // O(1) paieška pagal ID
    private readonly Dictionary<string, Group> _groupsByCode  = new();  // O(1) paieška pagal kodą
    private readonly Faculty                   _faculty;

    public MemoryStudentRepository()
    {
        _faculty = new Faculty("Technologijų fakultetas (Demo)");

        // Pradiniai duomenys — imituoja tai, ką grąžintų tikra API
        var pradiniai = new[]
        {
            new Student(1, "Aleksas",   "Jonaitis",  "aleksas@uni.lt",  "Informatika",         2023),
            new Student(2, "Birutė",    "Kazlauskė", "birute@uni.lt",   "Informatika",         2023),
            new Student(3, "Česlovas",  "Petraitis", "ceslovas@uni.lt", "Informatika",         2024),
            new Student(4, "Daiva",     "Liaukė",    "daiva@uni.lt",    "Programų sistemos",   2023),
            new Student(5, "Edvinas",   "Viliūnas",  "edvinas@uni.lt",  "Programų sistemos",   2023),
            new Student(6, "Fausta",    "Milierius",  "fausta@uni.lt",  "Programų sistemos",   2024),
        };

        foreach (var s in pradiniai)
            UzregistruotiStudenta(s);
    }

    // ── Privati pagalbinė logika ───────────────────────────────────────

    /// <summary>
    /// Registruoja studentą: prideda į _studentsById ir automatiškai
    /// priskiria atitinkamai grupei (sukuria grupę, jei jos dar nėra).
    /// </summary>
    private void UzregistruotiStudenta(Student student)
    {
        _studentsById[student.Id] = student;

        var kodas = GeneruotiGrupesKoda(student.StudyProgram, student.EnrollmentYear);
        if (!_groupsByCode.TryGetValue(kodas, out var grupe))
        {
            grupe = new Group(kodas, student.StudyProgram, student.EnrollmentYear);
            _groupsByCode[kodas] = grupe;
            _faculty.AddGroup(grupe);
        }
        grupe.AddStudent(student);
    }

    /// <summary>
    /// Generuoja grupės kodą iš studijų programos ir metų.
    /// Pvz.: "Informatika" + 2023 → "I-23"
    /// </summary>
    private static string GeneruotiGrupesKoda(string programa, int metai)
    {
        var zodziai = programa.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        var prefiksas = string.Concat(zodziai.Select(z => char.ToUpper(z[0])));
        return $"{prefiksas}-{metai % 100:D2}";
    }

    // ── Viešas interfeisas — paprastas, švarus, O(1) paieška ──────────
    public IReadOnlyList<Student> GetAll()          => _studentsById.Values.ToList();
    public Student? GetById(int id)                 => _studentsById.TryGetValue(id, out var s) ? s : null;
    public IReadOnlyList<Group> GetAllGroups()      => _groupsByCode.Values.ToList();
    public Group? GetGroupByCode(string kodas)      => _groupsByCode.TryGetValue(kodas, out var g) ? g : null;
    public Faculty GetFaculty()                     => _faculty;

    public void Add(Student student)                => UzregistruotiStudenta(student);

    public bool Remove(int id)
    {
        if (!_studentsById.Remove(id)) return false;
        // Pastaba: pašalinimas iš grupės reikalautų papildomos logikos
        return true;
    }
}
