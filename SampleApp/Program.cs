using SampleApp.Sources.democlient;

namespace SampleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            OAuthWorkFlow of = new OAuthWorkFlow();

            /* please plugin your App Id and Secret from https://developer.deere.com app profile into ApiCredentials.cs CLIENT constructor
             *
             * Once access token and token secret are generated, please plug in the values from output console window into ApiCredentials.cs TOKEN constructor
             * 
             * Once the App ID, secret, Token and Token Secret are updated in ApiCredentials.cs run the program to get Fleet details example
             * 
             * The Access Token and Token Secret are good for an year. If the token has expired, please clear the TOKEN constructor and regenerate a token and secret with the above process outlined.
             */
            string ConsumerKey = ApiCredentials.CLIENT.key;
            string ConsumerSecret = ApiCredentials.CLIENT.secret;
            string OAuthToken = ApiCredentials.TOKEN.token;
            string OAuthTokenSecret = ApiCredentials.TOKEN.secret;
            if (ConsumerKey.Equals("Replace with App id from developer.deere.com") ||
                ConsumerSecret.Equals("Replace with secret for the App from developer.deere.com"))
            {
                System.Diagnostics.Debug.WriteLine("\n Please replace ApiCredentials.CLIENT with the App ID and secret of your App from developer.deere.com" 
                    + " and rerun the program to generate OAuth Token and Token Secret \n");
                return;
            }
            else if (OAuthToken.Equals("") || OAuthToken.Equals("token printed in console output after running the oauth worflow code") ||
              OAuthTokenSecret.Equals("") || OAuthTokenSecret.Equals("token secret printed in console output generated after running the oauth workflow code"))
            {
                of.retrieveOAuthProviderDetails();                        
                of.getRequestToken();                                     
                of.authorizeRequestToken();                              
                of.exchangeRequestTokenForAccessToken();                  
                System.Diagnostics.Debug.WriteLine("\n Please replace ApiCredentials.TOKEN with the Token and Token Secret printed in the output window"
                    + " and rerun the program to get the Fleet Details \n");
                return;
            }
            else
            {
                FleetInfo fi = new FleetInfo();                           
                fi.retrieveFleetDetails();                                
            }

        }
    }

}
