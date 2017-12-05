using System;
using System.Collections.Generic;
using Hammock.Authentication.OAuth;
using System.Compat.Web;

namespace SampleApp.Sources.democlient
{
    class OAuthWorkFlow
    {
        private String authUri;
        private String verifier;
        Dictionary<String, Link> links;
        String reqToken ;
        String reqSecret;

        public void retrieveOAuthProviderDetails()
        {
            Link requestLink = new Link();
            links = new Dictionary<String, Link>();
            requestLink.uri = "https://developer.deere.com/oauth/oauth10/initiate";
            links.Add("oauthRequestToken", requestLink);
            Link accessTokenLink = new Link();
            accessTokenLink.uri = "https://developer.deere.com/oauth/oauth10/token";
            links.Add("oauthAccessToken", accessTokenLink);
            Link authorizeTokenLink = new Link();
            authorizeTokenLink.uri = "https://developer.deere.com/oauth/auz/authorize";
            links.Add("oauthAuthorizeRequestToken", authorizeTokenLink);          
        }

         public void getRequestToken() 
         {
            Hammock.Authentication.OAuth.OAuthCredentials credentials = createOAuthCredentials(OAuthType.RequestToken, null, null, null, 
                "https://developer.deere.com/oauth/auz/grants/provider/authcomplete");
            
             Hammock.RestClient client = new Hammock.RestClient()
            {
                Authority = "",
                Credentials = credentials
            };

            Hammock.RestRequest request = new Hammock.RestRequest()
            {
                Path = links["oauthRequestToken"].uri
            };

            Hammock.RestResponse response = client.Request(request);
            reqToken = response.Content.Split('&')[0];
            authUri = links["oauthAuthorizeRequestToken"].uri + "?" + reqToken;
            reqToken =reqToken.Split('=')[1];
            reqSecret = response.Content.Split('&')[1].Split('=')[1];
        }
        
         public void authorizeRequestToken() {
            System.Diagnostics.Debug.WriteLine("authUri: " +authUri);
            Console.WriteLine("Hit this URL in browser and login. Copy the verifier from browser URL and paste below and hit Enter"  +authUri);
            verifier = Console.ReadLine();
        }

         public void exchangeRequestTokenForAccessToken() {
            OAuthCredentials credentials = createOAuthCredentials(OAuthType.AccessToken, reqToken, HttpUtility.UrlDecode(reqSecret), verifier, null);
            Hammock.RestClient client = new Hammock.RestClient()
            {
                Authority = "",
                Credentials = credentials
            };

            Hammock.RestRequest request = new Hammock.RestRequest()
            {
                Path = links["oauthAccessToken"].uri
            };

            Hammock.RestResponse response = client.Request(request);

            Console.WriteLine("Token:" + response.Content.Split('&')[0].Split('=')[1] + " \n Token Secret:" + response.Content.Split('&')[1].Split('=')[1]);
            String oauthToken = response.Content.Split('&')[0].Split('=')[1];
            String oauthTokenSecret = response.Content.Split('&')[1].Split('=')[1];

            System.Diagnostics.Debug.WriteLine("\n Token:" + oauthToken + "\n Token Secret:" + HttpUtility.UrlDecode(oauthTokenSecret));
        }

        public static OAuthCredentials createOAuthCredentials(OAuthType type, String strToken, String strSecret, String strVerifier, String strCallBack ){
            OAuthCredentials credentials = new OAuthCredentials()
            {
                Type =type,
                SignatureMethod = OAuthSignatureMethod.HmacSha1,
                ParameterHandling = OAuthParameterHandling.HttpAuthorizationHeader,
                ConsumerKey = ApiCredentials.CLIENT.key,
                ConsumerSecret = ApiCredentials.CLIENT.secret,
                Token = strToken,
                TokenSecret = strSecret,
                Verifier = strVerifier,
                CallbackUrl = strCallBack
            };
            return credentials;
        }
    }
}
