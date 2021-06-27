using GameBI.API.BLL;
using IMDBLite.API.DataModels;
using IMDBLite.API.DataModels.Models;
using IMDBLite.BLL.Interfaces;

namespace IMDBLite.API.BLL
{
    public class CastInMovieRepository : RepositoryBase<CastInMovie>, ICastInMovieRepository
    {
        public CastInMovieRepository(dataContext repositoryContext)
            : base(repositoryContext){}
    }
}
