using GameBI.API.BLL;
using IMDBLite.API.DataModels;
using IMDBLite.API.DataModels.Models;
using IMDBLite.BLL.Interfaces;

namespace IMDBLite.API.BLL
{
    public class CastRepository : RepositoryBase<Cast>, ICastRepository
    {
        public CastRepository(dataContext repositoryContext)
            :base(repositoryContext){}
    }
}
