using LocksSearch.Services;
using System;
using System.IO;
using Xunit;

namespace LocksSearch.UnitTests
{
    public class JsonDataImporterTets
    {
        [Fact]
        public void DataImportTest()
        {
            var filePath = Path.Combine("Assets", "sv_lsm_data.json");
            var jsonData = new JsonDataImporter(filePath);

            Assert.Single(jsonData.Buildings);
            Assert.Equal(2, jsonData.Locks.Count);
            Assert.Single(jsonData.Groups);
            Assert.Single(jsonData.Medias);
        }
    }
}
