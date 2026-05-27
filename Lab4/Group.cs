namespace Lab4;

public class Group
{
    public string Pavadinimas { get; set; }
    public List<Student> Studentai { get; set; }

    public Group(string pavadinimas)
    {
        Pavadinimas = pavadinimas;
        Studentai = new List<Student>();
    }

    public void PridetiStudenta(Student s)
    {
        Studentai.Add(s);
    }
}
