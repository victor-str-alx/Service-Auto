using Microsoft.EntityFrameworkCore;
using ServiceAutoApp.Models;

namespace ServiceAutoApp.Data;

public static class ServiceTypesSeeder
{
    public static async Task SeedAsync(ApplicationDbContext context)
    {
        var defaults = new List<ServiceType>
        {
            new()
            {
                Name = "Revizie periodica",
                Description = "Schimb ulei si filtre (ulei, aer, habitaclu), verificare nivel lichide si inspectie generala.",
                Price = 200
            },
            new()
            {
                Name = "Sistem franare",
                Description = "Verificare etrieri, conducte, placute si discuri pentru siguranta maxima.",
                Price = 150
            },
            new()
            {
                Name = "Placute frana (ax)",
                Description = "Inlocuire placute frana pe o axa (fata sau spate).",
                Price = 110
            },
            new()
            {
                Name = "Discuri frana (ax)",
                Description = "Inlocuire discuri frana pe o axa (fata sau spate).",
                Price = 160
            },
            new()
            {
                Name = "Lichid frana",
                Description = "Schimb complet lichid frana cu purjare instalatie.",
                Price = 100
            },
            new()
            {
                Name = "Amortizoare si suspensie",
                Description = "Verificare si inlocuire amortizoare, arcuri, bielete si silent block-uri.",
                Price = 180
            },
            new()
            {
                Name = "Geometrie roti",
                Description = "Reglaj directie si geometrie 2-4 roti.",
                Price = 130
            },
            new()
            {
                Name = "Echilibrare roti",
                Description = "Echilibrare set complet de 4 roti.",
                Price = 60
            },
            new()
            {
                Name = "Diagnostic electronic",
                Description = "Citire erori din ECU/ABS/airbag si raport de diagnoza.",
                Price = 80
            },
            new()
            {
                Name = "Motor si transmisie",
                Description = "Reparatii motor, ambreiaj si cutie de viteze.",
                Price = 350
            },
            new()
            {
                Name = "Climatizare si incalzire",
                Description = "Umplere freon, verificare compresor si reparatii sistem HVAC.",
                Price = 200
            }
        };

        foreach (var item in defaults)
        {
            var existing = await context.ServiceTypes.FirstOrDefaultAsync(x => x.Name == item.Name);
            if (existing is null)
            {
                context.ServiceTypes.Add(item);
                continue;
            }

            existing.Description = item.Description;
            existing.Price = item.Price;
        }

        await context.SaveChangesAsync();
    }
}
