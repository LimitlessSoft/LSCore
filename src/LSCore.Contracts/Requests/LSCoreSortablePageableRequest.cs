namespace LSCore.Contracts.Requests
{
    public class LSCoreSortablePageableRequest
    {
        public int PageSize { get; set; } = 10;
        public int CurrentPage { get; set; } = 1;
    }
}
