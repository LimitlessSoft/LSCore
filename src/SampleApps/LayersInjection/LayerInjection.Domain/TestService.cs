using LayerInjection.Contracts;

namespace LayerInjection.Domain;

public class TestService : ITestService
{
    public string Get()
    {
        return "Hello from domain layer";
    }
}