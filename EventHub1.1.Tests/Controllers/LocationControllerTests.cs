using System.Collections.Generic;
using EventHub1._1.Controllers;
using EventHub1._1.DAL.Services;
using EventHub1._1.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace EventHub.Tests.Controllers
{
    [TestClass]
    public class LocationControllerTests
    {
        [TestMethod]
        public void GetActiveLocations_AllLocationsReturnedAreActive()
        {
            // Arrange
            var resultFromGet = new List<Location>()
            {
                new Location() {Active = true, Activities = null, Address = "Some Address", Name = "Test Name 1"},
                new Location() {Active = true, Activities = null, Address = "Some Address", Name = "Test Name 1"},
                new Location() {Active = true, Activities = null, Address = "Some Address", Name = "Test Name 1"},
                new Location() {Active = true, Activities = null, Address = "Some Address", Name = "Test Name 1"},
                new Location() {Active = true, Activities = null, Address = "Some Address", Name = "Test Name 1"}
            };

            var mockService = new Mock<ILocationService>();
            mockService.Setup(x => x.GetAllActiveLocations()).Returns(resultFromGet);

            var controllerUnderTest = new LocationController(mockService.Object);

            // Act
            IEnumerable<Location> actualResult = controllerUnderTest.GetAllActiveLocations();

            // Assert
            foreach (var location in actualResult)
            {
                Assert.AreEqual(true, location.Active);
            }
        }
    }
}
