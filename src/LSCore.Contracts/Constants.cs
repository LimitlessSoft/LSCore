﻿namespace LSCore.Contracts;

public static class LSCoreContractsConstants
{
    public const string ApiKeyCustomHeader = "X-Api-Auth";
    public const string UndefinedContextUsername = "UNDEFINED";

    public static class ClaimNames
    {
        public const string CustomIdentifier = "custom:identifier";
    }

    public static class ImageTypesMIME
    {
        public const string Jpeg = "image/jpeg";
        public const string Png = "image/png";
    }

    public static class Minio
    {
        public const char DictionarySeparatorChar = '/';
    }

    public static class ResponseLogMessages
    {
        public const string LogErrorBaseMessageFormat = "Bad LSCoreResponse occured with status {0} at {1} line {2}!";
        public const string LogErrorObjectFormat = "LSCoreResponse object: {0}";
    }
}
