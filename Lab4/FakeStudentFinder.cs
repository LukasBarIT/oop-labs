namespace Lab4;

public class FakeStudentFinder : IStudentFinder
{
    public Student? Find(Group group, string query)
    {
        return new Student(99, "Fake Studentas", "fake@test.lt", 8.5);
    }
}
