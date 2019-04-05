using System.Collections.Generic;
using System.Threading.Tasks;
using AWClient.Models;


namespace AWClient.DAL
{
    public interface IArtTypeRepository
    {
        Task<List<ArtType>> GetArtType();
        Task<ArtType> GetArtType(int ArtTypeID);        
    }
}
