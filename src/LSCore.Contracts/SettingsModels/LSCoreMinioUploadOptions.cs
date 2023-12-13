namespace LSCore.Contracts.SettingsModels
{
    public class LSCoreMinioUploadOptions
    {
        private string _fileName { get; set; }
        private string _bucket { get; set; }

        public string Bucket { get => _bucket; set => _bucket = value.ToLower(); }
        public string FileName
        {
            get => _fileName;
            set
            {
                _fileName = value.Replace(Path.DirectorySeparatorChar, LSCoreContractsConstants.Minio.DictionarySeparatorChar);

                if (_fileName[0] != LSCoreContractsConstants.Minio.DictionarySeparatorChar)
                    _fileName = LSCoreContractsConstants.Minio.DictionarySeparatorChar + _fileName;
            }
        }
        public Stream FileStream { get; set; }
        public string ContentType { get; set; }
        public Dictionary<string, string> Tags { get; set; }
    }
}
