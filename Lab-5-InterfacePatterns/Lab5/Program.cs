using Lab5.Interfaces;
using Lab5.Services;
using Lab5.Implementations.Repository;
using Lab5.Implementations.Printer;
using Lab5.Implementations.Strategy;
using Lab5.Implementations.Menu;
using Lab5.Implementations.Adapter;

namespace Lab5;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("=== LAB-5: Interface Patterns in Practice ===");
        Console.WriteLine();

        // Runtime switching demo - change these lines to swap implementations
        // without touching StudentService at all

        // --- Repository: swap between Memory / File / Api ---
        IStudentRepository repository = new MemoryStudentRepository();
        // IStudentRepository repository = new FileStudentRepository();
        // IStudentRepository repository = new ApiStudentRepository();

        // --- Printer: swap between Console / File / Json ---
        IStudentPrinter printer = new ConsoleStudentPrinter();
        // IStudentPrinter printer = new FileStudentPrinter("output.txt");
        // IStudentPrinter printer = new JsonStudentPrinter();

        // --- Strategy: swap between Simple / Weighted / Median ---
        IAverageStrategy strategy = new SimpleAverageStrategy();
        // IAverageStrategy strategy = new WeightedAverageStrategy();
        // IAverageStrategy strategy = new MedianAverageStrategy();

        // --- Validator: swap between Adapter (legacy) / Strict ---
        IStudentValidator validator = new StudentValidatorAdapter();
        // IStudentValidator validator = new StrictStudentValidator();

        StudentService service = new StudentService(repository, printer, strategy, validator);

        // --- Menu: swap between Console / Debug / Web ---
        IMenuService menu = new ConsoleMenuService(service);
        // IMenuService menu = new DebugMenuService(service);
        // IMenuService menu = new WebMenuSimulationService(service);

        menu.Run();
    }
}
