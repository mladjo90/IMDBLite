using GameBI.API.BLL;
using IMDBLite.API.DataModels;
using IMDBLite.API.DataModels.Models;
using IMDBLite.BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMDBLite.API.BLL
{
    public class MovieRepository : RepositoryBase<Movie>, IMovieRepository
    {
        public MovieRepository(dataContext repositoryContext)
            : base(repositoryContext){}
    }
}
