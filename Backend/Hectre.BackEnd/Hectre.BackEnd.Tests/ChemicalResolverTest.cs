using System;
using FluentAssertions;
using Hectre.BackEnd.Common;
using Hectre.BackEnd.Data;
using Hectre.BackEnd.GraphQl.Chemical;
using Hectre.BackEnd.Models;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Hectre.BackEnd.Tests
{
    public class ChemicalResolverTest
    {
        private readonly DataContext _dataContext;

        public ChemicalResolverTest()
        {
            var options = new DbContextOptionsBuilder<HectreDbContext>()
                .UseInMemoryDatabase(databaseName: "MockUserDb")
                .Options;

            var dbContext = new HectreDbContext(options);

            IEfRepository<Chemical, HectreDbContext> mockRepository = new EFRepository<Chemical, HectreDbContext>(dbContext);
            _dataContext = new DataContext(mockRepository);
        }

        [Fact]
        public void AddChemical_Successfully()
        {
            //arrange
            var newItem = new Chemical()
            {
                Name = "New chemical 01",
                ActiveIngredient = "ActiveIngredient",
                ChemicalType = "ChemicalType",
                PreHarvestIntervalInDays = "5",
            };

            //action
            var addedItem = new ChemicalResolver().AddChemical(newItem, _dataContext);

            //assert
            addedItem.Should().NotBeNull();
            addedItem.Data.Should().NotBeNull();
            addedItem.Data.Id.Length.Should().Be(36);
            addedItem.Data.Name.Should().Be(newItem.Name);
            addedItem.Data.ActiveIngredient.Should().Be(newItem.ActiveIngredient);
            addedItem.Data.ChemicalType.Should().Be(newItem.ChemicalType);
            addedItem.Data.PreHarvestIntervalInDays.Should().Be(newItem.PreHarvestIntervalInDays);
            addedItem.Data.CreationDate.Date.Should().Be(DateTime.Now.Date);
            addedItem.Data.ModificationDate.Should().NotBeNull();
            addedItem.Data.ModificationDate.Value.Date.Should().Be(DateTime.Now.Date);
            addedItem.Data.DeletionDate.Should().BeNull();

            addedItem.Code.Should().BeNull();
            addedItem.Message.Should().BeNull();
            addedItem.Total.Should().Be(1);
        }

        [Fact]
        public void AddChemical_NullInput_ThrowError()
        {
            //arrange
            Chemical newItem = null;
            //action
            var addedItem = new ChemicalResolver().AddChemical(newItem, _dataContext);

            //assert
            addedItem.Should().NotBeNull();
            addedItem.Data.Should().BeNull();

            addedItem.Code.Should().Be(Constants.ErrorCode.ERR_100);
            addedItem.Message.Should().Be(Constants.ErrorMessage.InvalidInput);
            addedItem.Total.Should().Be(0);
        }

        [Theory]
        [InlineData(null, "ActiveIngredient", "ChemicalType", "5")]
        [InlineData("Name 01", "", "ChemicalType", "5")]
        [InlineData("Name 01", "ActiveIngredient", null, "5")]
        [InlineData("Name 01", "ActiveIngredient", "ChemicalType", "")]
        public void AddChemical_MissingMandateFields_ThrowError(string name, string activeIngredient, string chemicalType, string preHarvestIntervalInDays)
        {
            //arrange
            var newItem = new Chemical()
            {
                Name = name,
                ActiveIngredient = activeIngredient,
                ChemicalType = chemicalType,
                PreHarvestIntervalInDays = preHarvestIntervalInDays,
            };

            //action
            var addedItem = new ChemicalResolver().AddChemical(newItem, _dataContext);

            //assert
            addedItem.Should().NotBeNull();
            addedItem.Data.Should().BeNull();

            addedItem.Code.Should().Be(Constants.ErrorCode.ERR_200);
            addedItem.Message.Should().Be(Constants.ErrorMessage.MissingMandatoryFields);
            addedItem.Total.Should().Be(0);
        }
    }
}
