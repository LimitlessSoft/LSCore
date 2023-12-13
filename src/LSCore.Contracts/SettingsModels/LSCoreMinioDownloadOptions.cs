namespace LSCore.Contracts.SettingsModels
{
    public class LSCoreMinioDownloadOptions
    {
        private string _fileName { get; set; }

        public string Bucket { get; set; }
        public string FileName
        {
            get => _fileName;
            set => _fileName = value.Replace(Path.DirectorySeparatorChar, LSCoreContractsConstants.Minio.DictionarySeparatorChar);
        }
    }
}
