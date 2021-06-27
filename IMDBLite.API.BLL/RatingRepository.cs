using GameBI.API.BLL;
using IMDBLite.API.DataModels;
using IMDBLite.API.DataModels.Models;
using IMDBLite.BLL.Interfaces;

namespace IMDBLite.API.BLL
{
    public class RatingRepository : RepositoryBase<Rating>, IRatingRepository
    {
        public RatingRepository(dataContext repositoryContext)
            : base(repositoryContext){}
    }
}
