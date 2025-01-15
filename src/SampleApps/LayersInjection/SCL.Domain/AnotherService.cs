using LayerInjection.Contracts;

namespace SCL.Domain;

public class AnotherService : IAnotherService
{
    public string Get()
    {
        return "Hello from another domain layer";
    }
}