using Lab7.Interfaces;
using Lab7.Models;

namespace Lab7.Implementations.Strategy;

/// <summary>
/// Paprastas aritmetinis vidurkis — Strategy šablono implementacija.
/// 
/// Strategy šablonas leidžia keisti algoritmą nekeičiant klientų kodo.
/// Norint naudoti kitą vidurkį — pakeisti vieną eilutę Program.cs.
/// </summary>
public class SimpleAverageStrategy : IAverageStrategy
{
    /// <summary>
    /// Apskaičiuoja visų studentų pažymių aritmetinį vidurkį.
    /// Jei studentai neturi pažymių — grąžina 0.
    /// </summary>
    public double Calculate(IReadOnlyList<Student> students)
    {
        if (students.Count == 0) return 0;

        // Sujungiame visų studentų pažymius į vieną sąrašą
        var visiPazymiai = students.SelectMany(s => s.Grades).ToList();
        return visiPazymiai.Count == 0 ? 0 : visiPazymiai.Average();
    }
}
