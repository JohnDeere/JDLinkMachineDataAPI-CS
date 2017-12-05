using System;

namespace SampleApp.Sources.democlient.rest
{
    class OAuthClient
    {
        public String key;
        public String secret;

    public OAuthClient(String key, String secret) {
        this.key = key;
        this.secret = secret;
    }

    public String getKey() {
        return key;
    }

    public String getSecret() {
        return secret;
    }

    }
}
