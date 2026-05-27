using Lab7.Models;

namespace Lab7.Interfaces;

/// <summary>
/// Vidurkio skaičiavimo strategijos interfeisas.
/// Strategy šablonas — algoritmas keičiamas nekeičiant klientų (OCP).
/// </summary>
public interface IAverageStrategy
{
    double Calculate(IReadOnlyList<Student> students);
}
