using System.Net.Http;
using Newtonsoft.Json;

namespace CowLocation.InterService
{
    public class MasterData : IMasterData
    {
        private readonly HttpClient _client;

        public MasterData(HttpClient client)
        {
            _client = client;
        }

        public string GetCowName(string earTag)
        {
            var response = _client.GetAsync("/Cow/Read/" + earTag + "/").Result;

            var dataFromResponse = response.Content.ReadAsStringAsync().Result;

            var returnDto = JsonConvert.DeserializeObject<Dto.CowRead>(dataFromResponse);
            return returnDto.Name;
        }
    }
}
