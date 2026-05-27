// Saugyklos interfeisas — apibrėžia visas operacijas su studentų duomenimis.
// StudentService priklauso tik nuo šio interfeiso, ne nuo konkrečios klasės (DIP principas).
using CW1After.Models;

namespace CW1After.Interfaces;

public interface IStudentRepository
{
    List<Student> GetAllStudents();              // Grąžina visų studentų sąrašą
    List<Group>   GetAllGroups();                // Grąžina visų grupių sąrašą
    Student?      FindById(int id);              // Ieško studento pagal ID, grąžina null jei nerasta
    void          AddStudent(Student student);   // Prideda naują studentą
    void          AddGrade(int studentId, int grade); // Prideda pažymį studentui
}
