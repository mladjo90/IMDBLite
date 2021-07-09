using IMDBLite.API.DataModels;
using IMDBLite.API.DataModels.Models;
using IMDBLite.API.Repository.Interfaces;

namespace IMDBLite.API.Repository
{
    public class RatingRepository : RepositoryBase<Rating>, IRatingRepository
    {
        public RatingRepository(dataContext repositoryContext)
            : base(repositoryContext){}
    }
}
