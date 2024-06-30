namespace ProjectBase.Domain.Configuration
{
    public class AppSettingConfiguration
    {
        public JWTSection JWTSection { get; set; }
        public ConnectionStrings ConnectionStrings { get; set; }
        public AWSSection AWSSection { get; set; }
    }

    public class JWTSection
    {
        public string SecretKey { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public int ExpiresInMinutes { get; set; }
    }
    public class ConnectionStrings
    {
        public string DefaultConnection { get; set; }
    }
    public class AWSSection
    {
        public string AccessKey { get; set; }
        public string Secret { get; set; }
        public string S3Url { get; set; }
        public string SQSUrl1 { get; set; }
        public string UserFileBucket { get; set; }
        public string ProductFileBucket { get; set; }
    }
}
