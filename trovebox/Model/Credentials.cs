using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace trovebox.Model
{
    /// <summary>
    /// As the name applies, this class is used to store the user crediential.
    /// </summary>
    public class Credentials
    {
        public string oauth_consumer_key { get; set; }
        public string oauth_consumer_secret { get; set; }
        public string apiBaseUrl { get; set; }
        public string oauth_token { get; set; }
        public string oauth_token_secret { get; set; }

        /// <summary>
        /// The parameterless constructor is needed inorder to properly deserialize the class, when retriving stored creditails from local storage.
        /// </summary>
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
