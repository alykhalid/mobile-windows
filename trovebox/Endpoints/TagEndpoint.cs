using System;
using System.Collections.Generic;
using System.Linq;
using trovebox.Model;
using RestSharp;
using System.Threading.Tasks;

namespace trovebox.Endpoints
{
    public class TagEndpoint
    {
        public TagEndpoint(RestClient restClient)
        {
            this.restClient = restClient;
        }

        public async Task<ResponseEnvelope<List<Tag>>> GetList()
        {
            var t = new TaskCompletionSource<ResponseEnvelope<List<Tag>>>();
            var request = new RestRequest(TagEndpoint.EndpointUrlPlural + "/list.json", Method.GET);
            this.restClient.ExecuteAsync<ResponseEnvelope<List<Tag>>>(request, r => { t.TrySetResult(r.Data); });
            ResponseEnvelope<List<Tag>> temp = await t.Task;
            return temp;
        }

        public async Task<ResponseEnvelope<bool>> Create(string name, int count = 0, string email = "", double latitude = 0, double longitude = 0)
        {
            var t = new TaskCompletionSource<ResponseEnvelope<bool>>();
            var request = new RestRequest(TagEndpoint.EndpointUrlSingular + "/create.json", Method.POST);

            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("You have to supply a tag name");
            request.Parameters.Add(new Parameter() { Name = "tag", Value = name, Type = ParameterType.GetOrPost });

            if (!string.IsNullOrEmpty(email))
                request.Parameters.Add(new Parameter() { Name = "email", Value = email, Type = ParameterType.GetOrPost });
            if (count != 0)
                request.Parameters.Add(new Parameter() { Name = "count", Value = count, Type = ParameterType.GetOrPost });
            if (latitude != 0)
                request.Parameters.Add(new Parameter() { Name = "latitude", Value = latitude, Type = ParameterType.GetOrPost });
            if (longitude != 0)
                request.Parameters.Add(new Parameter() { Name = "longitude", Value = longitude, Type = ParameterType.GetOrPost });

            this.restClient.ExecuteAsync<ResponseEnvelope<bool>>(request, r => { t.TrySetResult(r.Data); });
            ResponseEnvelope<bool> temp = await t.Task;
            return temp;
        }

        public async Task<ResponseEnvelope<Tag>> Update(string tag, int count = 0, string email = "", double latitude = 0, double longitude = 0)
        {
            var t = new TaskCompletionSource<ResponseEnvelope<Tag>>();
            var request = new RestRequest(TagEndpoint.EndpointUrlSingular + "/" + tag + "/update.json", Method.POST);

            if (!string.IsNullOrEmpty(email))
                request.Parameters.Add(new Parameter() { Name = "email", Value = email, Type = ParameterType.GetOrPost });
            if (count != 0)
                request.Parameters.Add(new Parameter() { Name = "count", Value = count, Type = ParameterType.GetOrPost });
            if (latitude != 0)
                request.Parameters.Add(new Parameter() { Name = "latitude", Value = latitude, Type = ParameterType.GetOrPost });
            if (longitude != 0)
                request.Parameters.Add(new Parameter() { Name = "longitude", Value = longitude, Type = ParameterType.GetOrPost });

            this.restClient.ExecuteAsync<ResponseEnvelope<Tag>>(request, r => { t.TrySetResult(r.Data); });
            ResponseEnvelope<Tag> temp = await t.Task;
            return temp;
        }

        private RestClient restClient;
        public const string EndpointUrlSingular = "/tag";
        public const string EndpointUrlPlural = "/tags";
    }
}
