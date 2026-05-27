namespace Lab7.Models;

/// <summary>
/// Fakultetas — grupių konteineris.
/// Agreguoja visas grupes ir skaičiuoja bendrą studentų kiekį.
/// </summary>
public class Faculty
{
    public string Name { get; }   // Fakulteto pavadinimas

    // ── Privačios grupės — kapsuliavimasas ────────────────────────────
    private readonly List<Group> _groups = new();
    public IReadOnlyList<Group> Groups => _groups;

    /// <summary>Bendras studentų skaičius visuose grupėse.</summary>
    public int TotalStudents => _groups.Sum(g => g.Students.Count);

    public Faculty(string name) => Name = name;

    /// <summary>
    /// Prideda grupę į fakultetą.
    /// Kviečia tik Repository inicializacijos metu.
    /// </summary>
    public void AddGroup(Group group) => _groups.Add(group);
}
