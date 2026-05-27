namespace Lab6.Models;

public class Student
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public double Grade { get; set; }   // 1–10
    public int Credits { get; set; }    // for weighted average

    public override string ToString() =>
        $"[{Id}] {Name,-20} Grade: {Grade:F1}  Credits: {Credits}";
}
