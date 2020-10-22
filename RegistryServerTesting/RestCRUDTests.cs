using System.Collections.Generic;
using System.Text.Json;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PeerToPeerLib;
using RegistryServerREST.Manager;

namespace RegistryServerTesting
{
    [TestClass]
    public class RestCRUDTests
    {
        private FileEndPointsManager _manager = new FileEndPointsManager();

        [TestInitialize]
        public void Init()
        {
            _manager = new FileEndPointsManager();
        }

        [TestMethod]
        public void GetAllTest()
        {
            //Arrange
            int expectedCount = 2;
            List<FileEndPoint> endPoints =
            JsonSerializer.Deserialize<List<FileEndPoint>>(_manager.GetEndPointsThatHasFile("testFile"));

            //Act  
            int actualCount = endPoints.Count;

            //Assert
            Assert.AreEqual(expectedCount, actualCount);
        }
    }
}
