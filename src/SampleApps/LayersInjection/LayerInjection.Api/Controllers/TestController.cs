using LayerInjection.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace LayerInjection.Api.Controllers;

public class TestController(
    ITestService testService,
    IAnotherService anotherService) : ControllerBase
{
    [HttpGet]
    [Route("/test")]
    public IActionResult Get()
    {
        // We are not aware of the implementation details for any of interfaces used bellow
        // We are just using the interface
        // This is the magic of Dependency Injection
        // All we care is that we are using AddLSCoreDependencyInjection which scans assemblies, and we follow default conventions (IClass, Class : IClass)
        
        // Interface is inside Contracts which we actually catch
        // Implementation of ITestService is inside Domain layer
        // Domain layer is referenced in this application
        var testServiceResult = testService.Get();
        
        // Interface is inside Contracts which we actually catch
        // Implementation of IAnotherService is inside another domain layer (micro library or third party library)
        // Another domain layer is referenced in this application
        var testAnotherService = anotherService.Get();
        
        return Ok("Explore the code to see the magic!");
    }
}