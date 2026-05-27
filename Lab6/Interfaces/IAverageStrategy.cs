using Lab6.Models;

namespace Lab6.Interfaces;

public interface IAverageStrategy
{
    string Name { get; }
    double Calculate(List<Student> students);
}
