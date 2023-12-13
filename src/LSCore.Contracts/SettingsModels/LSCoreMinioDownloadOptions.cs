namespace LSCore.Contracts.SettingsModels
{
    public class LSCoreMinioDownloadOptions
    {
        private string _fileName { get; set; }
        private string _bucket { get; set; }

        public string Bucket { get => _bucket; set => _bucket = value.ToLower(); }
        public string FileName
        {
            get => _fileName;
            set => _fileName = value.Replace(Path.DirectorySeparatorChar, LSCoreContractsConstants.Minio.DictionarySeparatorChar);
        }
    }
}
