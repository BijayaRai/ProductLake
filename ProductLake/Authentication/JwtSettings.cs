using System.Net.NetworkInformation;

namespace ProductLake.Authentication
{
    public static class JwtSettings
    {
        public static string Issuer = "Author";
        public static string Audience = "User";
        public static string Key = "TestKey";
    }
}
