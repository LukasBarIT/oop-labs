namespace Lab7.Legacy;

/// <summary>
/// Senas validavimo kodas, kurio NEGALIME keisti.
/// Tai imituoja realią situaciją, kai reikia naudoti seną biblioteką
/// su nauja sistema — sprendžiama Adapter šablonu.
/// </summary>
public class LegacyStudentValidation
{
    /// <summary>
    /// Tikrina ar vardas ir el. paštas yra teisingi.
    /// Sena API — priima string, ne Student objektą.
    /// </summary>
    public bool CheckStudent(string name, string email)
        => !string.IsNullOrWhiteSpace(name) && email.Contains('@');
}
