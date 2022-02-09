using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Hectre.BackEnd.Data;
using Hectre.BackEnd.Models;
using Newtonsoft.Json;

namespace Hectre.BackEnd.Migrations
{
    public class ChemicalInitiation
    {
        public static async Task Migrate(HectreDbContext dbContext)
        {
            if (dbContext == null) return;

            if (dbContext.Chemicals.Any())
                return;

            try
            {
                var jsonPath = $"{AppDomain.CurrentDomain.BaseDirectory}/Migrations/chemical.json";

                if (!File.Exists(jsonPath)) return;

                var jsonData = await File.ReadAllTextAsync(jsonPath);

                if (string.IsNullOrEmpty(jsonData)) return;

                var data = JsonConvert.DeserializeObject<List<Chemical>>(jsonData);

                if (data == null) return;

                await dbContext.Chemicals.AddRangeAsync(data);

                dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}
