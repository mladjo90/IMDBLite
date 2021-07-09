using IMDBLite.API.DataModels;
using IMDBLite.API.DataModels.Models;
using IMDBLite.API.Repository.Interfaces;

namespace IMDBLite.API.Repository
{
    public class CastRepository : RepositoryBase<Cast>, ICastRepository
    {
        public CastRepository(dataContext repositoryContext)
            :base(repositoryContext){}
    }
}
