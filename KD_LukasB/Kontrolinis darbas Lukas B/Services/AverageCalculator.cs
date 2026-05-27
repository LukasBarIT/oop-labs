// Vidurkio skaičiuoklė — vidurkio formulė laikoma VIENOJE vietoje (DRY principas).
// Naudojama ir StudentService, ir ReportService — niekur nekartojama.
namespace CW1After.Services;

public class AverageCalculator
{
    // Apskaičiuoja pažymių aritmetinį vidurkį be LINQ
    public double Calculate(List<int> grades)
    {
        if (grades.Count == 0) return 0.0; // Jei pažymių nėra — grąžiname 0

        int sum = 0;
        foreach (var g in grades)
            sum += g;

        return sum / (double)grades.Count;
    }
}
