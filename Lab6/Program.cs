using Lab6.Interfaces;
using Lab6.Models;
using Lab6.Printers;
using Lab6.Repositories;
using Lab6.Services;
using Lab6.Strategies;
using Lab6.Validators;

// ── Dependency Injection (manual wiring) ──────────────────────────────────────
IStudentRepository  repository = new MemoryStudentRepository();
IStudentPrinter     printer    = new ConsoleStudentPrinter();
IAverageStrategy    strategy   = new WeightedAverageStrategy();   // swap freely
IStudentValidator   validator  = new StudentValidator();

var studentService = new StudentService(repository, printer, strategy, validator);

// ── Seed demo data (Task 6 — add multiple students) ───────────────────────────
studentService.AddStudent(new Student { Name = "Alice Johnson",  Grade = 8.5, Credits = 6 });
studentService.AddStudent(new Student { Name = "Bob Smith",      Grade = 6.0, Credits = 4 });
studentService.AddStudent(new Student { Name = "Carol White",    Grade = 9.2, Credits = 6 });
studentService.AddStudent(new Student { Name = "Dave Brown",     Grade = 4.5, Credits = 3 });
studentService.AddStudent(new Student { Name = "Eve Invalid",    Grade = 0.0, Credits = 0 }); // invalid

// ── Start menu ────────────────────────────────────────────────────────────────
IMenuService menu = new MenuService(studentService);
menu.Run();
