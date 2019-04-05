using System.Collections.Generic;
using System.Threading.Tasks;
using AWClient.Models;

namespace AWClient.DAL
{
    public interface IArtWorkRepository
    {
        Task<List<ArtWork>> GetArtWorks();
        Task<ArtWork> GetArtWork(int ID);
        Task<List<ArtWork>> GetArtWorksByArtType(int ArtTypeID);
        Task AddArtWork(ArtWork awToAdd);
        Task UpdateArtWork(ArtWork awToUpdate);
        Task DeleteArtWork(ArtWork awToDelete);
    }
}
