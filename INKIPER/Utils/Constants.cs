namespace INKIPER.Utils;

public class Constants
{
    public static string BASE_URL
    {

        get
        {
            string backendUrl = Environment.GetEnvironmentVariable("BACKEND_URL")!;
            return string.IsNullOrEmpty(backendUrl) ? "http://localhost:3331" : backendUrl;
        }
    }

    public static string LOGIN_URL = "/auth/login";

    public static string AUTH_TYPE = "BASIC_AUTH";

    public static string GRAPHQL_URL = $"{BASE_URL}/graphql";
}