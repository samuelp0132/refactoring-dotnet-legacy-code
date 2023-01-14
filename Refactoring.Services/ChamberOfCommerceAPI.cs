using Newtonsoft.Json;
using Refactoring.Web.Services.Helpers;
using System.Net.Http;

namespace Rectoring.Services {
    public class ChamberOfCommerceApi : IChamberOfCommerceApi
    {
        private readonly IHttpClientFactory _factory;

        public ChamberOfCommerceApi(IHttpClientFactory factory)
        {
            _factory = factory;
        }
        
        /// <summary>
        /// For the provided district returns the "DataResult" object containing
        /// the id, thumbnail url, and title for the district's image
        /// </summary>
        /// <param name="district"></param>
        /// <returns></returns>
        public async Task<DataResult> GetImageAndThumbnailDataFor(string district)
        {
            using var client = _factory.CreateClient("districtAPI");
            var absoluteUrl = BuildUrlForDistrict(client.BaseAddress, district);
            var request = new HttpRequestMessage(HttpMethod.Get, absoluteUrl);
            var response = client.SendAsync(request);
            var data = await response.Result.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<DataResult>(data);
            return result;
        }

        private Uri BuildUrlForDistrict(Uri baseUrl, string district)
        {
            var districtId = District.GetDistrictNumberByName(district);
            var basePhotoUrl = new Uri(baseUrl, districtId);
            return new Uri(basePhotoUrl, districtId);
        }
    }

    public interface IChamberOfCommerceApi
    {
        Task<DataResult> GetImageAndThumbnailDataFor(string district);
    }

    public struct DataResult {
        public int Id { get; set; }
        public string ThumbnailUrl { get; set; }
        public string Title { get; set; }
    }
}