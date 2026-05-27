namespace Lab3;

public class KriptoMokejimas : IPaymentService
{
    public void Moket(decimal suma)
    {
        Console.WriteLine($"Sumoketa kriptovaliuta: {suma} EUR");
    }
}
