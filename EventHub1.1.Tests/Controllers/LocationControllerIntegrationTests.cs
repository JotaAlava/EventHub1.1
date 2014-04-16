using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EventHub.Tests.Controllers
{
    [TestClass]
    public class LocationControllerTests
    {
        [TestMethod]
        public void GetActiveLocations_WithEmptyDb()
        {
            // I have to get a self-host set up to run integration tests!
            // http://www.asp.net/web-api/overview/hosting-aspnet-web-api/self-host-a-web-api
        }
        //[TestMethod]
        //public void GetActiveLocations_AllLocationsReturnedAreActive()
        //{
        //    // Arrange
        //    var resultFromGet = new List<Location>()
        //    {
        //        new Location() {Active = true, Activities = null, Address = "Some Address", Name = "Test Name 1"},
        //        new Location() {Active = true, Activities = null, Address = "Some Address", Name = "Test Name 1"},
        //        new Location() {Active = true, Activities = null, Address = "Some Address", Name = "Test Name 1"},
        //        new Location() {Active = true, Activities = null, Address = "Some Address", Name = "Test Name 1"},
        //        new Location() {Active = true, Activities = null, Address = "Some Address", Name = "Test Name 1"}
        //    };

        //    var mockService = new Mock<ILocationService>();
        //    mockService.Setup(x => x.GetActiveLocations()).Returns(resultFromGet);

        //    var controllerUnderTest = new LocationController(mockService.Object);

        //    // Act
        //    IEnumerable<Location> actualResult = controllerUnderTest.GetActiveLocations();

        //    // Assert
        //    foreach (var location in actualResult)
        //    {
        //        Assert.AreEqual(true, location.Active);
        //    }
        //}
    }
}
