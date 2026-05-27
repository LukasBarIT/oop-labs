using Lab5.Interfaces;
using Lab5.Models;
using System.Text.Json;

namespace Lab5.Implementations.Printer;

public class JsonStudentPrinter : IStudentPrinter
{
    public void Print(Group group)
    {
        foreach (var student in group.Students)
        {
            var obj = new
            {
                id = student.Id,
                name = student.Name,
                email = student.Email,
                grades = student.Grades
            };
            Console.WriteLine(JsonSerializer.Serialize(obj));
        }
    }
}
