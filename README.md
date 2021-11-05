 # LocationSearchApi
Location search example

Open .sln file is located in the LocationSearchApi folder.

The solution consists of 4 layers:

- WebAPI -> LocationSearchApi Web Project

  In the appsettings.json file there are 3 execution parameters:
        "CsvPath" - Relative path of the csv file with the locations
        "CacheLoadIntervalMinutes" - Duration in minutes of validity of cached locations.
        "CacheKeyName": Cached Key Name

- Services -> LocationSearchApi.Services Project

  Call the data access service and filter with Linq

- Model -> Project LocationSearchApi.Model

  Classes that define the application model
  
- Data Access -> LocationSearchApi.DataAccess Project

 - Asynchronously read the CSV file using the library:
   Faster CSV reader: https://www.joelverhagen.com/blog/2020/12/fastest-net-csv-parsers
   Site: https://github.com/MarkPflug/Sylvan
   
   The use of asynchronous methods is the most efficient way to access I/O resources
   
 - Load the locations in the cache using LazyCache, implementation used to avoid implementing lock logic.
   Necessary if we directly use IMemoryCache with heavy loading resources.
    Using LAZY CACHE: https://github.com/alastairtree/LazyCache/

- Test -> LocationSearchApi.Test project

  Application test using Xunit and Moq
