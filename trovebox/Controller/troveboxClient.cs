using RestSharp;
using RestSharp.Authenticators;
using RestSharp.Authenticators.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using trovebox.Endpoints;

namespace trovebox
{
    public class troveboxClient
    {
        public troveboxClient(trovebox.Model.Credentials credential)
        {
            cred = credential;
        }

        public PhotoEndpoint Photos 
        {
            get
            {
                if (this.photoEndpoint == null)
                    this.photoEndpoint = new PhotoEndpoint(this.RestClient);
                return this.photoEndpoint;
            }
        }

        public TagEndpoint Tags
        {
            get
            {
                if (this.tagEndpoint == null)
                    this.tagEndpoint = new TagEndpoint(this.RestClient);
                return this.tagEndpoint;
            }
        }

        public async Task<string> HelloApiTest()
        {
            var t = new TaskCompletionSource<string>();
            var request = new RestRequest("/hello.json", Method.GET);
            this.RestClient.ExecuteAsync(request, r => { t.TrySetResult(r.Content); });
            string temp = await t.Task;
            return temp;
        }

        private RestClient RestClient
        {
            get
            {
                if (this.restClient == null)
                {
                    this.restClient = new RestClient(cred.apiBaseUrl);
                    // setup OAuth
                    var oauthAuthenticator = OAuth1Authenticator
                        .ForProtectedResource(cred.oauth_consumer_key, cred.oauth_consumer_secret, cred.oauth_token, cred.oauth_token_secret);
                    oauthAuthenticator.ParameterHandling = OAuthParameterHandling.UrlOrPostParameters;
                    this.restClient.Authenticator = oauthAuthenticator;
                }
                return this.restClient;
            }
        }

        private RestClient restClient;
        private PhotoEndpoint photoEndpoint;
        private TagEndpoint tagEndpoint;
        private trovebox.Model.Credentials cred;

        
    }
}
