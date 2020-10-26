using System.Collections.Generic;
using System.Net;
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

        [TestMethod]
        public void RegisterTest()
        {
            // Arrange
            string fileName = "aNewFileForTesting";
            FileEndPoint peer = new FileEndPoint(IPAddress.Loopback.ToString(), 9998);

            // Act
            int expectedResult = 1;
            int actualResult = _manager.Register(fileName, peer);

            // Assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        public void DeregisterTest()
        {
            Assert.Fail("Test not made.");
        }
    }
}
