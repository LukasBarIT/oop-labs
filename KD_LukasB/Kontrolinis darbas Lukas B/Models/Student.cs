// Studentas — pagrindinis duomenų modelis
namespace CW1After.Models;

public class Student
{
    public int Id { get; set; }           // Unikalus studento numeris
    public string Name { get; set; } = "";      // Vardas ir pavardė
    public string Email { get; set; } = "";     // Elektroninis paštas
    public string GroupCode { get; set; } = ""; // Grupės kodas (pvz. PI23)
    public List<int> Grades { get; set; } = new(); // Pažymių sąrašas (1–10)
}
