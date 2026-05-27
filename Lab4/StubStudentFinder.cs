namespace Lab4;

public class StubStudentFinder : IStudentFinder
{
    private readonly Student? _rezultatas;

    public StubStudentFinder(Student? rezultatas)
    {
        _rezultatas = rezultatas;
    }

    public Student? Find(Group group, string query)
    {
        return _rezultatas;
    }
}
