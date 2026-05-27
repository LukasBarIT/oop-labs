using Lab7.Models;

namespace Lab7.Interfaces;

/// <summary>
/// Saugyklos interfeisas — apibrėžia visas operacijas su duomenimis.
/// 
/// OCP principas: StudentService nenaudoja konkrečių klasių,
/// tik šį interfeisą — galima keisti implementaciją nekeičiant logikos.
/// </summary>
public interface IStudentRepository
{
    // ── Studentų operacijos ────────────────────────────────────────────
    IReadOnlyList<Student> GetAll();
    Student?               GetById(int id);   // O(1) kai Dictionary viduje
    void                   Add(Student student);
    bool                   Remove(int id);

    // ── Grupių ir fakulteto prieiga ────────────────────────────────────
    IReadOnlyList<Group>   GetAllGroups();
    Group?                 GetGroupByCode(string code);  // O(1) kai Dictionary viduje
    Faculty                GetFaculty();
}
