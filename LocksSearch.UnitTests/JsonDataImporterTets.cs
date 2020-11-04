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

            Assert.Equal(3, jsonData.Buildings.Count);
            Assert.Equal(3, jsonData.Locks.Count);
            Assert.Equal(3, jsonData.Groups.Count);
            Assert.Equal(3, jsonData.Medias.Count);
        }
    }
}
