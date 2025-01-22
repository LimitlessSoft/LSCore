namespace LSCore.Contracts.Interfaces;

public interface ILSCorePageable
{
    int PageSize { get; set; }
    int CurrentPage { get; set; }
}
