using Lab5.Interfaces;
using Lab5.Models;

namespace Lab5.Implementations.Repository;

public class ApiStudentRepository : IStudentRepository
{
    // Simulated API - in real app this would call an HTTP endpoint
    private readonly List<Student> _fakeApiData = new()
    {
        new Student(10, "ApiUser", "api@university.lt"),
        new Student(11, "RemoteStudent", "remote@university.lt")
    };

    public Student? Find(string query)
    {
        return _fakeApiData.FirstOrDefault(s =>
            s.Name.Contains(query, StringComparison.OrdinalIgnoreCase) ||
            s.Email.Contains(query, StringComparison.OrdinalIgnoreCase));
    }
}
