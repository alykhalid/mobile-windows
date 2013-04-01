using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace trovebox.Model
{
    public class Credentials
    {
        public string oauth_consumer_key { get; set; }
        public string oauth_consumer_secret { get; set; }
        public string apiBaseUrl { get; set; }
        public string oauth_token { get; set; }
        public string oauth_token_secret { get; set; }

        public Credentials()
        {

        }

        public Credentials(string apiBaseUrl, string oauthConsumerKey, string oauthConsumerSecret, string oauthToken, string oauthTokenSecret)
        {
            this.apiBaseUrl = apiBaseUrl;
            this.oauth_consumer_key = oauthConsumerKey;
            this.oauth_consumer_secret = oauthConsumerSecret;
            this.oauth_token = oauthToken;
            this.oauth_token_secret = oauthTokenSecret;
        }
    }
}
