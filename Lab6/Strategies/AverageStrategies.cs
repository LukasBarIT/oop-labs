using Lab6.Interfaces;
using Lab6.Models;

namespace Lab6.Strategies;

// --- Simple arithmetic mean ---
public class SimpleAverageStrategy : IAverageStrategy
{
    public string Name => "Simple Average";

    public double Calculate(List<Student> students)
    {
        if (students.Count == 0) return 0;
        return students.Average(s => s.Grade);
    }
}

// --- Credits-weighted mean ---
public class WeightedAverageStrategy : IAverageStrategy
{
    public string Name => "Weighted Average";

    public double Calculate(List<Student> students)
    {
        if (students.Count == 0) return 0;
        double totalWeight = students.Sum(s => s.Credits);
        if (totalWeight == 0) return 0;
        return students.Sum(s => s.Grade * s.Credits) / totalWeight;
    }
}

// --- Median ---
public class MedianAverageStrategy : IAverageStrategy
{
    public string Name => "Median";

    public double Calculate(List<Student> students)
    {
        if (students.Count == 0) return 0;
        var sorted = students.Select(s => s.Grade).OrderBy(g => g).ToList();
        int mid = sorted.Count / 2;
        return sorted.Count % 2 == 0
            ? (sorted[mid - 1] + sorted[mid]) / 2.0
            : sorted[mid];
    }
}
