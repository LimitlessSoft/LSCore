namespace LSCore.Contracts.Dtos
{
    public class LSCoreFileDto
    {
        public string? ContentType { get; set; }
        public int ContentLength { get => Data?.Length ?? 0; }
        public byte[]? Data { get; set; }
        public Dictionary<string, string> Tags { get; set; } = new Dictionary<string, string>();
    }
}
