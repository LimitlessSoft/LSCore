namespace LSCore.DependencyInjection.Models;

public class LSCoreDependencyInjectionOptions
{
    public LSCoreDependencyInjectionScanningOptions Scan { get; } = new ();
}