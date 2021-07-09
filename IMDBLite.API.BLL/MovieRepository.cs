using IMDBLite.API.DataModels;
using IMDBLite.API.DataModels.Models;
using IMDBLite.API.Repository.Interfaces;

namespace IMDBLite.API.Repository
{
    public class MovieRepository : RepositoryBase<Movie>, IMovieRepository
    {
        public MovieRepository(dataContext repositoryContext)
            : base(repositoryContext){}
    }
}
