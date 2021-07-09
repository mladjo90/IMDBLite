using IMDBLite.API.DataModels;
using IMDBLite.API.DataModels.Models;
using IMDBLite.API.Repository.Interfaces;

namespace IMDBLite.API.Repository
{
    public class CastInMovieRepository : RepositoryBase<CastInMovie>, ICastInMovieRepository
    {
        public CastInMovieRepository(dataContext repositoryContext)
            : base(repositoryContext){}
    }
}
