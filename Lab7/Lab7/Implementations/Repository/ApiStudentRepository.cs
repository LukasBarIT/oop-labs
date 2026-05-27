using System.Net.Http.Json;
using Lab7.DTOs;
using Lab7.Interfaces;
using Lab7.Models;

namespace Lab7.Implementations.Repository;

/// <summary>
/// API saugykla — gauna duomenis iš REST API per HTTP.
/// 
/// PAGRINDINIS OOP PAMOKOS PAVYZDYS:
/// 
///   VIDUJE  — sudėtinga: HttpClient, Dictionary, JSON apdorojimas, DTO → Model konversija
///   IŠORĖJE — paprasta:  IReadOnlyList, Group, Faculty (jokio HTTP, jokio JSON)
/// 
/// StudentService NEŽINO:
///   - Kad duomenys ateina iš API
///   - Kad egzistuoja Dictionary
///   - Kad egzistuoja DTO klasės
/// 
/// Tai OCP principas: galima pakeisti duomenų šaltinį nekeičiant StudentService.
/// </summary>
public class ApiStudentRepository : IStudentRepository
{
    // ══════════════════════════════════════════════════════════════
    // VIDINĖ SUDĖTINGUMAS — privatu, paslėpta nuo visų kitų klasių
    // ══════════════════════════════════════════════════════════════
    private readonly Dictionary<int, Student>  _studentsById  = new();  // Greita paieška pagal ID
    private readonly Dictionary<string, Group> _groupsByCode  = new();  // Greita paieška pagal kodą
    private Faculty _faculty = new("Technologijų fakultetas");

    private readonly HttpClient _http;

    public ApiStudentRepository(string bazinisUrl)
    {
        _http = new HttpClient { BaseAddress = new Uri(bazinisUrl) };
        // Sinchroniškai palaukiame async metodo — tik konstruktoriuje
        UzkrautiIsApi().GetAwaiter().GetResult();
    }

    /// <summary>
    /// Gauna duomenis iš API ir sudeda į vidines struktūras.
    /// Konvertuoja DTO → domeno modelius (Student, Group, Faculty).
    /// </summary>
    private async Task UzkrautiIsApi()
    {
        Console.WriteLine("  [Saugykla] Jungiamasi prie API...");

        var dto = await _http.GetFromJsonAsync<ApiFacultyDto>("/api/faculty");
        if (dto is null)
        {
            Console.WriteLine("  [Saugykla] ĮSPĖJIMAS: API grąžino null.");
            return;
        }

        // Konvertuojame DTO → domeno modelius
        _faculty = new Faculty(dto.Name);

        foreach (var grupeDto in dto.Groups)
        {
            var grupe = new Group(grupeDto.Code, grupeDto.StudyProgram, grupeDto.EnrollmentYear);

            foreach (var sDto in grupeDto.Students)
            {
                // DTO → Student modelis (jokios DTO logikos neišeina iš šios klasės)
                var studentas = new Student(
                    sDto.Id, sDto.FirstName, sDto.LastName,
                    sDto.Email, sDto.StudyProgram, sDto.EnrollmentYear);

                _studentsById[studentas.Id] = studentas;  // O(1) indeksas pagal ID
                grupe.AddStudent(studentas);
            }

            _groupsByCode[grupeDto.Code] = grupe;          // O(1) indeksas pagal kodą
            _faculty.AddGroup(grupe);
        }

        Console.WriteLine($"  [Saugykla] Užkrauta {_studentsById.Count} studentų " +
                          $"iš {_groupsByCode.Count} grupių.\n");
    }

    // ══════════════════════════════════════════════════════════════
    // VIEŠAS INTERFEISAS — paprastas, švarus, jokio API/Dictionary
    // ══════════════════════════════════════════════════════════════
    public IReadOnlyList<Student> GetAll()         => _studentsById.Values.ToList();
    public Student? GetById(int id)                => _studentsById.TryGetValue(id, out var s) ? s : null;
    public IReadOnlyList<Group> GetAllGroups()     => _groupsByCode.Values.ToList();
    public Group? GetGroupByCode(string kodas)     => _groupsByCode.TryGetValue(kodas, out var g) ? g : null;
    public Faculty GetFaculty()                    => _faculty;

    public void Add(Student studentas)             => _studentsById[studentas.Id] = studentas;
    public bool Remove(int id)                     => _studentsById.Remove(id);
}
