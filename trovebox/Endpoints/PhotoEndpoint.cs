using System;
using System.Collections.Generic;
using System.Linq;
using trovebox.Model;
using RestSharp;
using System.Threading.Tasks;

namespace trovebox.Endpoints
{
    /// <summary>
    /// This class represents the methods which are available in the trovebox API which can be used to update, create, delete etc photos.
    /// </summary>
    /// For more information about the methods and the required parameters, please visit http://theopenphotoproject.org/documentation/
    /// The initial version of this code was taken from https://github.com/slavo/openphoto-c-sharp but was modified for use in Windows Phone.
    /// </remarks>
    public class PhotoEndpoint
    {
        public const string EndpointUrlPlural = "/photos";
        public const string EndpointUrlSingular = "/photo";

        private RestClient restClient;

        public PhotoEndpoint(RestClient restClient)
        {
            this.restClient = restClient;
        }

        public async Task<ResponseEnvelope<Photo>> Get(string id)
        {
            var t = new TaskCompletionSource<ResponseEnvelope<Photo>>();
            var request = new RestRequest(PhotoEndpoint.EndpointUrlSingular + "/" + id + "/view.json", Method.GET);
            this.restClient.ExecuteAsync<ResponseEnvelope<Photo>>(request, r => { t.TrySetResult(r.Data); });
            ResponseEnvelope<Photo> temp = await t.Task;
            return temp;
        }

        public async Task<ResponseEnvelope<Photo>> GetThumbnail(string id)
        {
            var t = new TaskCompletionSource<ResponseEnvelope<Photo>>();
            var request = new RestRequest(PhotoEndpoint.EndpointUrlSingular + "/" + id + "/view.json", Method.GET);
            request.Parameters.Add(new Parameter() { Name = "returnSizes", Value = "100x100xCR", Type = ParameterType.GetOrPost });
            this.restClient.ExecuteAsync<ResponseEnvelope<Photo>>(request, r => { t.TrySetResult(r.Data); });
            ResponseEnvelope<Photo> temp = await t.Task;
            return temp;
        }

        public async Task<ResponseEnvelope<List<Photo>>> GetList(int page)
        {
            var t = new TaskCompletionSource<ResponseEnvelope<List<Photo>>>();
            var request = new RestRequest(PhotoEndpoint.EndpointUrlPlural + "/list.json", Method.GET);
            request.Parameters.Add(new Parameter() { Name = "returnSizes", Value = "100x100xCR", Type = ParameterType.GetOrPost });
            request.Parameters.Add(new Parameter() { Name = "pageSize", Value = "15", Type = ParameterType.GetOrPost });
            request.Parameters.Add(new Parameter() { Name = "page", Value = page.ToString(), Type = ParameterType.GetOrPost });
            this.restClient.ExecuteAsync<ResponseEnvelope<List<Photo>>>(request, r => { t.TrySetResult(r.Data); });
            ResponseEnvelope<List<Photo>> temp = await t.Task;
            return temp;
        }

        public async Task<ResponseEnvelope<PhotoNextPreviousCollection>> NextPrevious(string id)
        {
            var t = new TaskCompletionSource<ResponseEnvelope<PhotoNextPreviousCollection>>();
            var request = new RestRequest(PhotoEndpoint.EndpointUrlSingular + "/" + id + "/nextprevious.json", Method.GET);
            this.restClient.ExecuteAsync<ResponseEnvelope<PhotoNextPreviousCollection>>(request, r => { t.TrySetResult(r.Data); });
            ResponseEnvelope<PhotoNextPreviousCollection> temp = await t.Task;
            return temp;
        }

        public async Task<ResponseEnvelope<bool>> delete(string id)
        {
            var t = new TaskCompletionSource<ResponseEnvelope<bool>>();
            var request = new RestRequest(PhotoEndpoint.EndpointUrlSingular + "/" + id + "/delete.json", Method.POST);
            this.restClient.ExecuteAsync<ResponseEnvelope<bool>>(request, r => { t.TrySetResult(r.Data); });
            ResponseEnvelope<bool> temp = await t.Task;
            return temp;
        }

        public async Task<ResponseEnvelope<Photo>> Update(string id, Photo newPhoto)
        {
            var t = new TaskCompletionSource<ResponseEnvelope<Photo>>();
            var request = new RestRequest(PhotoEndpoint.EndpointUrlSingular + "/" + id + "/update.json", Method.POST);

            request.Parameters.Add(new Parameter() { Name = "permission", Value = newPhoto.Permission, Type = ParameterType.GetOrPost });
            request.Parameters.Add(new Parameter() { Name = "title", Value = newPhoto.Title, Type = ParameterType.GetOrPost });
            request.Parameters.Add(new Parameter() { Name = "description", Value = newPhoto.Description, Type = ParameterType.GetOrPost });
            request.Parameters.Add(new Parameter() { Name = "dateUploaded", Value = newPhoto.DateUploaded, Type = ParameterType.GetOrPost });
            request.Parameters.Add(new Parameter() { Name = "dateTaken", Value = newPhoto.DateTaken, Type = ParameterType.GetOrPost });
            request.Parameters.Add(new Parameter() { Name = "license", Value = newPhoto.License, Type = ParameterType.GetOrPost });
            request.Parameters.Add(new Parameter() { Name = "latitude", Value = newPhoto.Latitude, Type = ParameterType.GetOrPost });
            request.Parameters.Add(new Parameter() { Name = "longitude", Value = newPhoto.Longitude, Type = ParameterType.GetOrPost });

            this.restClient.ExecuteAsync<ResponseEnvelope<Photo>>(request, r => { t.TrySetResult(r.Data); });
            ResponseEnvelope<Photo> temp = await t.Task;
            return temp;
        }

        public async Task<ResponseEnvelope<Photo>> Upload(byte[] photoData, string fileName)
        {
            var t = new TaskCompletionSource<ResponseEnvelope<Photo>>();
            var request = new RestRequest(PhotoEndpoint.EndpointUrlSingular + "/upload.json", Method.POST);

            var fileToUpload = FileParameter.Create("photo", photoData, fileName);
            request.Files.Add(fileToUpload);

            this.restClient.ExecuteAsync<ResponseEnvelope<Photo>>(request, r => { t.TrySetResult(r.Data); });
            ResponseEnvelope<Photo> temp = await t.Task;
            return temp;
        }
    }
}
