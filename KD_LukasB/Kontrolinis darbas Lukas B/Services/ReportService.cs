// Ataskaitų servisas — meniu punktai 7, 8, 9.
// Kiekviena funkcija pateikiama DVIEM versijomis: su LINQ ir be LINQ (Task 2 reikalavimas).
// Be LINQ versijose draudžiama: Where, Select, OrderBy, Sum, Average, Count(lambda),
// Any, All, Max, Min, Take, SelectMany, FirstOrDefault(lambda), GroupBy.
using CW1After.Interfaces;
using CW1After.Models;

namespace CW1After.Services;

public class ReportService
{
    private readonly IStudentRepository _repo;
    private readonly AverageCalculator  _calc;

    public ReportService(IStudentRepository repo, AverageCalculator calc)
    {
        _repo = repo;
        _calc = calc;
    }

    // =========================================================
    // 7) TOP N studentų pagal vidurkį
    // =========================================================

    // LINQ versija — grandinė: Select → OrderByDescending → Take
    public List<(Student s, double avg)> GetTopByAverage(int n)
    {
        return _repo.GetAllStudents()
            .Select(s => (s, avg: _calc.Calculate(s.Grades)))
            .OrderByDescending(x => x.avg)
            .Take(n)
            .ToList();
    }

    // Be LINQ versija — tik foreach, List.Sort ir for
    public List<(Student s, double avg)> GetTopByAverageWithoutLinq(int n)
    {
        // 1 žingsnis: suskaičiuojame kiekvieno studento vidurkį
        var poros = new List<(Student s, double avg)>();
        foreach (var s in _repo.GetAllStudents())
            poros.Add((s, _calc.Calculate(s.Grades)));

        // 2 žingsnis: rūšiuojame mažėjančia tvarka pagal vidurkį
        poros.Sort((a, b) => b.avg.CompareTo(a.avg));

        // 3 žingsnis: paimame pirmus N įrašų
        var rezultatas = new List<(Student s, double avg)>();
        for (int i = 0; i < poros.Count && i < n; i++)
            rezultatas.Add(poros[i]);

        return rezultatas;
    }

    // =========================================================
    // 8) Studentai grupėje, surūšiuoti pagal vardą
    // =========================================================

    // LINQ versija — Where + OrderBy + Select
    public List<(Student s, double avg)> GetStudentsInGroupSortedByName(string groupCode)
    {
        return _repo.GetAllStudents()
            .Where(s => s.GroupCode == groupCode)
            .OrderBy(s => s.Name)
            .Select(s => (s, avg: _calc.Calculate(s.Grades)))
            .ToList();
    }

    // Be LINQ versija — tik foreach ir List.Sort
    public List<(Student s, double avg)> GetStudentsInGroupSortedByNameWithoutLinq(string groupCode)
    {
        // 1 žingsnis: filtruojame studentus pagal grupės kodą
        var filtruoti = new List<Student>();
        foreach (var s in _repo.GetAllStudents())
            if (s.GroupCode == groupCode)
                filtruoti.Add(s);

        // 2 žingsnis: rūšiuojame abėcėlės tvarka pagal vardą
        filtruoti.Sort((a, b) => string.Compare(a.Name, b.Name, StringComparison.Ordinal));

        // 3 žingsnis: pridedam apskaičiuotą vidurkį prie kiekvieno studento
        var rezultatas = new List<(Student s, double avg)>();
        foreach (var s in filtruoti)
            rezultatas.Add((s, _calc.Calculate(s.Grades)));

        return rezultatas;
    }

    // =========================================================
    // 9) Statistika
    // =========================================================

    // Rezultato įrašas (record) — grąžinamas abiejų versijų
    public record Statistika(int VisoStudentu, int VisoPazymiu, double VidurkiuVidurkis, int AuksciausiasP, bool YraNeisgavusiu, bool VisiTuriPasta);

    // LINQ versija — Count, Sum, Average, SelectMany, Max, Any, All
    public Statistika GetStatistics()
    {
        var studentai = _repo.GetAllStudents();
        return new Statistika(
            VisoStudentu     : studentai.Count,
            VisoPazymiu      : studentai.Sum(s => s.Grades.Count),
            VidurkiuVidurkis : studentai.Average(s => _calc.Calculate(s.Grades)),
            AuksciausiasP    : studentai.SelectMany(s => s.Grades).DefaultIfEmpty(0).Max(),
            YraNeisgavusiu   : studentai.Any(s => s.Grades.Any(g => g < 5)),
            VisiTuriPasta    : studentai.All(s => !string.IsNullOrWhiteSpace(s.Email))
        );
    }

    // Be LINQ versija — tik foreach ir if
    public Statistika GetStatisticsWithoutLinq()
    {
        var studentai = _repo.GetAllStudents();

        int    visoStudentu     = studentai.Count; // Count — savybė, ne LINQ metodas
        int    visoPazymiu      = 0;
        double vidurkiuSuma     = 0.0;
        int    auksciausiasP    = 0;
        bool   yraNeisgavusiu   = false;
        bool   visiTuriPasta    = true;

        foreach (var s in studentai)
        {
            // Kiek iš viso pažymių visoje sistemoje
            visoPazymiu += s.Grades.Count;

            // Vidurkių suma — vėliau dalinsime iš studentų skaičiaus
            vidurkiuSuma += _calc.Calculate(s.Grades);

            // Ieškome aukščiausio pažymio
            foreach (var p in s.Grades)
                if (p > auksciausiasP) auksciausiasP = p;

            // Tikriname ar studentas turi neišlaikytų (pažymys < 5)
            foreach (var p in s.Grades)
                if (p < 5) { yraNeisgavusiu = true; break; }

            // Tikriname ar visi turi el. paštą
            if (string.IsNullOrWhiteSpace(s.Email))
                visiTuriPasta = false;
        }

        double vidurkiuVidurkis = visoStudentu == 0 ? 0.0 : vidurkiuSuma / visoStudentu;

        return new Statistika(visoStudentu, visoPazymiu, vidurkiuVidurkis, auksciausiasP, yraNeisgavusiu, visiTuriPasta);
    }
}
