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
    public class ArtWorkRepository : IArtWorkRepository
    {
        HttpClient client = new HttpClient();

        public ArtWorkRepository()
        {
            client.BaseAddress = Jeeves.DBUri;
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<List<ArtType>> GetArtWorks()
        {
            var response = await client.GetAsync("/api/artworks");
            if (response.IsSuccessStatusCode)
            {
                List<ArtType> artWorks = await response.Content.ReadAsAsync<List<ArtWork>>();
                return artWorks;
            }
            else
            {
                return new List<ArtType>();
            }
        }

        public async Task<List<ArtWork>> GetArtWorksByArtType(int ArtTypeID)
        {
            var response = await client.GetAsync($"/api/artworks/byArtType/{ArtTypeID}");
            if (response.IsSuccessStatusCode)
            {
                List<ArtWork> ArtWorks = await response.Content.ReadAsAsync<List<ArtWork>>();
                return ArtWorks;
            }
            else
            {
                return new List<ArtWork>();
            }
        }

        public async Task<ArtType> GetArtWorks(int ID)
        {
            var response = await client.GetAsync($"/api/artworks/{ID}");
            if (response.IsSuccessStatusCode)
            {
                ArtWork ArtWork = await response.Content.ReadAsAsync<ArtWork>();
                return ArtWork;
            }
            else
            {
                return new ArtWork();
            }
        }

        public async Task AddArtWork(ArtWork awToAdd)
        {
            var response = await client.PostAsJsonAsync("/api/artworks", awToAdd);
            if (!response.IsSuccessStatusCode)
            {
                var ex = Jeeves.CreateApiException(response);
                throw ex;
            }
        }

        public async Task UpdateArtWork(ArtWork awToUpdate)
        {
            var response = await client.PutAsJsonAsync($"/api/artworks/{awToUpdate.ID}", awToUpdate);
            if (!response.IsSuccessStatusCode)
            {
                var ex = Jeeves.CreateApiException(response);
                throw ex;
            }
        }

        public async Task DeleteArtWork(ArtWork awToDelete)
        {
            var response = await client.DeleteAsync($"/api/artworks/{awToDelete.ID}");
            if (!response.IsSuccessStatusCode)
            {
                var ex = Jeeves.CreateApiException(response);
                throw ex;
            }
        }
    }
}
