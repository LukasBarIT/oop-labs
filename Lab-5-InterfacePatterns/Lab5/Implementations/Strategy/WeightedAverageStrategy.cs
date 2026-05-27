using Lab5.Interfaces;
using Lab5.Models;

namespace Lab5.Implementations.Strategy;

public class WeightedAverageStrategy : IAverageStrategy
{
    public double Calculate(Student student)
    {
        if (student.Grades.Count == 0)
            return 0;

        double total = 0;
        int weightSum = 0;

        for (int i = 0; i < student.Grades.Count; i++)
        {
            int weight = i + 1;
            total += student.Grades[i] * weight;
            weightSum += weight;
        }

        return Math.Round(total / weightSum, 2);
    }
}
