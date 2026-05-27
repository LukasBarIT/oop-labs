namespace Lab4;

public class StudentFinder : IStudentFinder
{
    public Student? Find(Group group, string query)
    {
        if (int.TryParse(query, out int id))
        {
            return group.Studentai.FirstOrDefault(s => s.Id == id);
        }

        return group.Studentai.FirstOrDefault(s =>
            s.Email.Equals(query, StringComparison.OrdinalIgnoreCase));
    }
}
