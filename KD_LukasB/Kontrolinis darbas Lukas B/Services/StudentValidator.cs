// Studentų validatorius — tikrina ar studento duomenys teisingi.
// SRP principas: ši klasė atsakinga TIK už validaciją.
using CW1After.Interfaces;
using CW1After.Models;

namespace CW1After.Services;

public class StudentValidator
{
    private readonly IStudentRepository _repo;

    // Konstruktoriaus injekcija — reikia repo patikrinti grupės kodą
    public StudentValidator(IStudentRepository repo)
    {
        _repo = repo;
    }

    // Grąžina klaidų sąrašą. Jei tuščias — studentas validus.
    public List<string> Validate(Student s)
    {
        var errors = new List<string>();

        if (string.IsNullOrWhiteSpace(s.Name))
            errors.Add("Vardas negali būti tuščias");

        if (!s.Email.Contains('@') || !s.Email.Contains('.'))
            errors.Add("Neteisingas el. pašto formatas");

        // Tikriname ar grupės kodas egzistuoja saugykloje
        if (_repo.GetAllGroups().All(g => g.Code != s.GroupCode))
            errors.Add("Nežinomas grupės kodas");

        // Tikriname ar visi pažymiai yra intervale 1–10
        foreach (var g in s.Grades)
            if (g < 1 || g > 10) { errors.Add($"Pažymys {g} už leistinų ribų (1–10)"); break; }

        return errors;
    }
}
