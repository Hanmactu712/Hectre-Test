using System;
using Hectre.BackEnd.Common;
using Microsoft.EntityFrameworkCore;

namespace Hectre.BackEnd.Models
{
    //{
    //"_id": "6181eb674e68df4b5845299c",
    //"chemicalType": "Plant Growth Regulator",
    //"preHarvestIntervalInDays": "Up to 90% petal fall",
    //"activeIngredient": "SPINETORAM",
    //"name": "SERENADE OPTIMUM",
    //"creationDate": "2014-06-28T06:27:56-12:00",
    //"modificationDate": null,
    //"deletionDate": null
    //},
    public class Chemical : BaseEntity
    {
        public string ChemicalType { get; set; }
        public string PreHarvestIntervalInDays { get; set; }
        public string ActiveIngredient { get; set; }
        public string Name { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? ModificationDate { get; set; }
        public DateTime? DeletionDate { get; set; }

        public static void ModelBuilder(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Chemical>(entity =>
            {
                entity.ToTable(nameof(Chemical), Constants.DbSchema);
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnType("char(36)").ValueGeneratedNever();

                entity.Property(e => e.Name).HasColumnType("nvarchar(200)");
                entity.Property(e => e.ChemicalType).HasColumnType("nvarchar(200)");
                entity.Property(e => e.ActiveIngredient).HasColumnType("nvarchar(500)");
                entity.Property(e => e.PreHarvestIntervalInDays).HasColumnType("nvarchar(500)");
                entity.Property(e => e.CreationDate).HasColumnType("datetime");
                entity.Property(e => e.DeletionDate).HasColumnType("datetime");
                entity.Property(e => e.ModificationDate).HasColumnType("datetime");
            });
        }
    }
}
