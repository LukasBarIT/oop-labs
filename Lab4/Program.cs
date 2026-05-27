using Lab4;

Group grupe = new Group("PI-2");
grupe.PridetiStudenta(new Student(1, "Jonas Jonaitis", "jonas@stud.lt", 7.4));
grupe.PridetiStudenta(new Student(2, "Laura Lauraitė", "laura@stud.lt", 9.1));
grupe.PridetiStudenta(new Student(3, "Tomas Tomaitis", "tomas@stud.lt", 5.8));

Console.WriteLine("=== Tikra paieška pagal ID ===");
StudentService service1 = new StudentService(new StudentFinder());
service1.RastiIrAtspausdinti(grupe, "2");
service1.RastiIrAtspausdinti(grupe, "99");

Console.WriteLine("\n=== Tikra paieška pagal Email ===");
StudentService service2 = new StudentService(new StudentFinder());
service2.RastiIrAtspausdinti(grupe, "tomas@stud.lt");
service2.RastiIrAtspausdinti(grupe, "nera@stud.lt");

Console.WriteLine("\n=== Fake finder ===");
StudentService service3 = new StudentService(new FakeStudentFinder());
service3.RastiIrAtspausdinti(grupe, "bet kas");

Console.WriteLine("\n=== Stub finder (rastas) ===");
var stubStudentas = new Student(5, "Stub Studentas", "stub@test.lt", 6.0);
StudentService service4 = new StudentService(new StubStudentFinder(stubStudentas));
service4.RastiIrAtspausdinti(grupe, "bet kas");

Console.WriteLine("\n=== Stub finder (nerastas) ===");
StudentService service5 = new StudentService(new StubStudentFinder(null));
service5.RastiIrAtspausdinti(grupe, "bet kas");
