// LINQ Drills — papildoma užduotis (Drills, 1 balas)
// Kiekvienas drill turi dvi versijas:
//   (A) _Linq   — originali LINQ versija
//   (B) _Plain  — ekvivalentiška versija be LINQ (tik for/foreach/if/List.Sort)
// Draudžiama _Plain versijose: Where, Select, OrderBy, Sum, Average,
// Count(lambda), Any, All, Take, Min, Max, GroupBy, SelectMany ir pan.

#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;

namespace CW1After.LinqDrills;

public static class LinqDrills
{
    // ─────────────────────────────────────────────────────────────────────
    // Drill 1 — Where (filtravimas)
    // Surasti visus skaičius >= 5
    // ─────────────────────────────────────────────────────────────────────
    public static List<int> AtLeast5_Linq(List<int> input) =>
        input.Where(n => n >= 5).ToList();

    public static List<int> AtLeast5_Plain(List<int> input)
    {
        // Einame per visą sąrašą ir įtraukiame tik tuos, kurie atitinka sąlygą
        var rezultatas = new List<int>();
        foreach (var n in input)
            if (n >= 5)
                rezultatas.Add(n);
        return rezultatas;
    }

    // ─────────────────────────────────────────────────────────────────────
    // Drill 2 — OrderByDescending + Take (rūšiavimas + N pirmųjų paėmimas)
    // Grąžinti 3 didžiausius skaičius mažėjančia tvarka
    // ─────────────────────────────────────────────────────────────────────
    public static List<int> Top3Desc_Linq(List<int> input) =>
        input.OrderByDescending(n => n).Take(3).ToList();

    public static List<int> Top3Desc_Plain(List<int> input)
    {
        // Kopijuojame sąrašą — nenorime keisti originalo
        var kopija    = new List<int>(input);
        var rezultatas = new List<int>();

        // Tris kartus randame maksimumą, pridedame į rezultatą ir pašaliname
        for (int i = 0; i < 3 && kopija.Count > 0; i++)
        {
            int maxReiksme = kopija[0];
            int maxIndeksas = 0;
            for (int j = 1; j < kopija.Count; j++)
                if (kopija[j] > maxReiksme) { maxReiksme = kopija[j]; maxIndeksas = j; }

            rezultatas.Add(maxReiksme);
            kopija.RemoveAt(maxIndeksas);
        }

        return rezultatas;
    }

    // ─────────────────────────────────────────────────────────────────────
    // Drill 3 — Sum + Average (agregavimas)
    // Apskaičiuoti sumą ir vidurkį
    // ─────────────────────────────────────────────────────────────────────
    public static (int Sum, double Avg) SumAndAvg_Linq(List<int> input) =>
        (input.Sum(), input.Count == 0 ? 0.0 : input.Average());

    public static (int Sum, double Avg) SumAndAvg_Plain(List<int> input)
    {
        int suma = 0;
        foreach (var n in input)
            suma += n;

        // Saugome nuo dalybos iš nulio
        double vidurkis = input.Count == 0 ? 0.0 : suma / (double)input.Count;
        return (suma, vidurkis);
    }

    // ─────────────────────────────────────────────────────────────────────
    // Drill 4 — Count + Any + All (booleaniniai agregatai)
    // Suskaičiuoti > 7; ar yra neigiamų; ar visi >= 0
    // ─────────────────────────────────────────────────────────────────────
    public static (int Above7, bool AnyNegative, bool AllNonNegative) Bools_Linq(List<int> input) =>
        (input.Count(n => n > 7), input.Any(n => n < 0), input.All(n => n >= 0));

    public static (int Above7, bool AnyNegative, bool AllNonNegative) Bools_Plain(List<int> input)
    {
        int  virš7           = 0;
        bool yraNeigiamų     = false;
        bool visiNeneigiami  = true;

        foreach (var n in input)
        {
            if (n > 7) virš7++;
            if (n < 0) yraNeigiamų    = true;
            if (n < 0) visiNeneigiami  = false;
        }

        return (virš7, yraNeigiamų, visiNeneigiami);
    }

    // ─────────────────────────────────────────────────────────────────────
    // Drill 5 — Where + OrderByDescending + Select (kombinacija)
    // Studentų vardai (lowercase) kurių vidurkis > 7, rūšiuoti desc pagal vidurkį
    // ─────────────────────────────────────────────────────────────────────
    public sealed record MiniStudent(string Name, double Avg);

    public static List<string> TopNames_Linq(List<MiniStudent> input) =>
        input
            .Where(s => s.Avg > 7)
            .OrderByDescending(s => s.Avg)
            .Select(s => s.Name.ToLowerInvariant())
            .ToList();

    public static List<string> TopNames_Plain(List<MiniStudent> input)
    {
        // 1 žingsnis: filtruojame — tik vidurkis > 7
        var filtruoti = new List<MiniStudent>();
        foreach (var s in input)
            if (s.Avg > 7)
                filtruoti.Add(s);

        // 2 žingsnis: rūšiuojame mažėjančia tvarka pagal vidurkį
        filtruoti.Sort((a, b) => b.Avg.CompareTo(a.Avg));

        // 3 žingsnis: grąžiname tik vardus lowercase formatu
        var rezultatas = new List<string>();
        foreach (var s in filtruoti)
            rezultatas.Add(s.Name.ToLowerInvariant());

        return rezultatas;
    }
}
