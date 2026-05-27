namespace Lab4;

public class Student
{
    public int Id { get; set; }
    public string Vardas { get; set; }
    public string Email { get; set; }
    public double Vidurkis { get; set; }

    public Student(int id, string vardas, string email, double vidurkis)
    {
        Id = id;
        Vardas = vardas;
        Email = email;
        Vidurkis = vidurkis;
    }

    public override string ToString()
    {
        return $"[{Id}] {Vardas} | {Email} | Vidurkis: {Vidurkis:F2}";
    }
}
