using AWClient.Models;
using AWClient.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace AWClient.DAL
{
    public class ArtTypeRepository : IArtTypeRepository
    {
        HttpClient client = new HttpClient();

        public ArtTypeRepository()
        {
            client.BaseAddress = Jeeves.DBUri;
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<List<ArtType>> GetArtType()
        {
            var response = await client.GetAsync("/api/arttypes");
            if (response.IsSuccessStatusCode)
            {
                List<ArtType> artTypes = await response.Content.ReadAsAsync<List<ArtType>>();
                return artTypes;
            }
            else
            {
                return new List<ArtType>();
            }
        }

        public async Task<ArtType> GetArtType(int ArtTypeID)
        {
            var response = await client.GetAsync($"/api/arttypes/{ArtTypeID}");
            if (response.IsSuccessStatusCode)
            {
                ArtType artType = await response.Content.ReadAsAsync<ArtType>();
                return artType;
            }
            else
            {
                return new ArtType();
            }
        }
    }
}
