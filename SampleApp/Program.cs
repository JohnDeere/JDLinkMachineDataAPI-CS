using SampleApp.Sources.democlient;
using SampleApp.Sources.democlient.rest;

namespace SampleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            // Please paste your App Id and Shared Secret from your application profile on https://developer.deere.com
            string clientKey = "Your application's 'App ID' from developer.deere.com";
            string clientSecret = "Your application's 'Shared Secret' from developer.deere.com";
            
            var oauthWorkflowExample = new OAuthWorkFlow();
            // We currently support oAuth 1.0. You can dig in to the authentication code for an example using the
            // open source DevDefined.OAuth libraries.
            var apiCredentials = oauthWorkflowExample.Authenticate(clientKey, clientSecret);

            var oauthRestClient = new OAuthSignedRestClient(apiCredentials);

            new FleetInfo(oauthRestClient).RetrieveFleetDetails();
        }
    }

}
