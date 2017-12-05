using SampleApp.Sources.democlient.rest;

namespace SampleApp.Sources.democlient
{
    abstract class ApiCredentials
    {
        public static OAuthClient CLIENT = new OAuthClient("Replace with App id from developer.deere.com", "Replace with secret for the App from developer.deere.com");
        public static OAuthToken TOKEN = new OAuthToken("token printed in console output after running the oauth worflow code", "token secret printed in console output generated after running the oauth workflow code");
    }
}
