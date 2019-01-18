using SampleApp.Sources.democlient.rest;

namespace SampleApp.Sources.democlient
{
    public class ApiCredentials
    {
        public string ClientKey { get; set; }
        public string ClientSecret { get; set; }
        public string TokenKey { get; set; }
        public string TokenSecret { get; set; }

        public ApiCredentials()
        {
            // If you want to play around without needing to generate tokens every time, you could place your credentials
            // in this constructor, go back to the Program.main method, and comment out the calls to OAuthWorkFlow.
            ClientKey = "Your application's 'App ID' from developer.deere.com";
            ClientSecret = "Your application's 'Shared Secret' from developer.deere.com";

            TokenKey = "This is generated from the oAuth workflow; see OAuthWorkFlow.GetRequestToken()";
            TokenSecret = "This is generated from the oAuth workflow; see OAuthWorkFlow.GetRequestToken()";
        }
    }
}
