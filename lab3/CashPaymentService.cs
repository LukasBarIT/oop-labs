namespace Lab3;

public class GryniesiaisMokejimas : IPaymentService
{
    public void Moket(decimal suma)
    {
        Console.WriteLine($"Sumoketa grynaisiais: {suma} EUR");
    }
}
