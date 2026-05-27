
Sale sale = new Sale();
SalesService servisas = new SalesService();

servisas.PridetiNari(sale, new Narys("Jonas", "Jonaitis", "Mėnesinis"));
servisas.PridetiNari(sale, new Narys("Petras", "Petraitis", "Metinis"));
servisas.PridetiNari(sale, new Narys("Laura", "Lauraitė", "Savaitinis"));

Console.WriteLine("=== Sporto salės nariai ===");
foreach (var narys in servisas.GautiVisus(sale))
{
    Console.WriteLine(narys.Aprasas());
}

class Narys
{
    public string Vardas { get; }
    public string Pavarde { get; }
    public string Abonementas { get; }

    public Narys(string vardas, string pavarde, string abonementas)
    {
        Vardas = vardas;
        Pavarde = pavarde;
        Abonementas = abonementas;
    }

    public string Aprasas()
    {
        return $"{Vardas} {Pavarde} | Abonementas: {Abonementas}";
    }
}

class Sale
{
    public List<Narys> Nariai { get; } = new();
}

class SalesService
{
    public void PridetiNari(Sale sale, Narys narys)
    {
        sale.Nariai.Add(narys);
    }

    public List<Narys> GautiVisus(Sale sale)
    {
        return sale.Nariai;
    }
}