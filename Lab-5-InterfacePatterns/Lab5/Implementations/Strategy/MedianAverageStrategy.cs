using Lab5.Interfaces;
using Lab5.Models;

namespace Lab5.Implementations.Strategy;

public class MedianAverageStrategy : IAverageStrategy
{
    public double Calculate(Student student)
    {
        if (student.Grades.Count == 0)
            return 0;

        var sorted = student.Grades.OrderBy(g => g).ToList();
        int mid = sorted.Count / 2;

        if (sorted.Count % 2 == 0)
            return (sorted[mid - 1] + sorted[mid]) / 2.0;

        return sorted[mid];
    }
}
