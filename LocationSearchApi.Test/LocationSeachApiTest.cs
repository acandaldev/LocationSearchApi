using LocationSearchApi.Controllers;
using LocationSearchApi.DataAccess;
using LocationSearchApi.Model;
using LocationSearchApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace LocationSearchApi.Test
{
    public class LocationSearchApiTests
    {
    
        [Fact]
        public async Task TestEmptySearchFilter()
        {
            // mock Empty CSV
            Mock<IReadLocationDataAccess> readLocationDataAccessMock = new Mock<IReadLocationDataAccess>();
            readLocationDataAccessMock.Setup(tri => tri.LoadLocationsAsync()).Returns(Task.FromResult(new System.Collections.Generic.List<Location>() { }));
            var objrest = await getMockController(getMockService(readLocationDataAccessMock.Object)).GetLocationsAsync(new SearchFilter());
            Assert.IsType<OkObjectResult>(objrest.Result);
            Assert.IsType<SearchResult>(((OkObjectResult)objrest.Result).Value);
            Assert.Equal(0, ((SearchResult)((OkObjectResult)objrest.Result).Value).LocationsCount);
        }

        [Fact]
        public async Task TestSearch10Locations3Near()
        {
            // mock CSV with 10 locations
            Mock<IReadLocationDataAccess> readLocationDataAccessMock = new Mock<IReadLocationDataAccess>();
            List<Location> locations = new List<Location>();
            for (int i = 0; i < 10; i++)
                locations.Add(new Location(i, i));
            readLocationDataAccessMock.Setup(tri => tri.LoadLocationsAsync()).Returns(Task.FromResult(locations));
            var objrest = await getMockController(getMockService(readLocationDataAccessMock.Object)).GetLocationsAsync(
                                                                 new SearchFilter() { Latitude = 5, Longitude = 5, MaxDistance = 200000, MaxResults = 10});
            Assert.IsType<OkObjectResult>(objrest.Result);
            Assert.IsType<SearchResult>(((OkObjectResult)objrest.Result).Value);
            Assert.Equal(3, ((SearchResult)((OkObjectResult)objrest.Result).Value).LocationsCount);
        }


        private LocationSearchController getMockController(ILocationSearchService locationSearchService)
        {
            // constr Controller
            ILogger<LocationSearchController> mockControllerLogger = new Mock<ILogger<LocationSearchController>>().Object;
            LocationSearchController mockController = new LocationSearchController(mockControllerLogger, locationSearchService);
            return mockController;
        }

        private ILocationSearchService getMockService(IReadLocationDataAccess dataAccess)
        {
            // Mock Service
            ILogger<LocationSearchService> mockServiceLogger = new Mock<ILogger<LocationSearchService>>().Object;
            return new LocationSearchService(mockServiceLogger, dataAccess);
        }
    }
}
