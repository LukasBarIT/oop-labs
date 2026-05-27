namespace Lab3;

public class KortelesMokejimas : IPaymentService
{
    public void Moket(decimal suma)
    {
        Console.WriteLine($"Sumoketa kortele: {suma} EUR");
    }
}
