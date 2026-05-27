namespace Lab4;

public class StudentService
{
    private readonly IStudentFinder _finder;

    public StudentService(IStudentFinder finder)
    {
        _finder = finder;
    }

    public void RastiIrAtspausdinti(Group group, string query)
    {
        Student? studentas = _finder.Find(group, query);

        if (studentas != null)
        {
            Console.WriteLine($"Studentas rastas: {studentas}");
        }
        else
        {
            Console.WriteLine($"Studentas pagal '{query}' nerastas.");
        }
    }
}
