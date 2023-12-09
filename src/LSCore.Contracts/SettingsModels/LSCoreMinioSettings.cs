namespace LSCore.Contracts.SettingsModels
{
    public class LSCoreMinioSettings
    {
        public string BucketBase { get; set; }
        public string Host { get; set; }
        public string AccessKey { get; set; }
        public string SecretKey { get; set; }
        public string Port { get; set; }
    }
}
