using IMDBLite.API.DataModels.Helper;
using IMDBLite.API.DataModels.Models;
using IMDBLite.API.Repository.Interfaces;
using IMDBLite.BLL.Interfaces;
using IMDBLite.DTO.DTORequests;
using IMDBLite.Shared.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace IMDBLite.BLL.Implementation
{
   
    public class RatingBLL : IRatingBLL
    {
        private readonly IRatingRepository _ratingRepository;
        private readonly IConfiguration _configuration;
        public RatingBLL(IRatingRepository ratingRepository, IConfiguration configuration)
        {
            _ratingRepository = ratingRepository;
            _configuration = configuration;
        }

        public async Task<bool> SaveDataForRating(List<RatingDTO> request)
        {
            string freeUserName = await StoredProcedureHandler.FindFreeUser(_configuration.GetConnectionString("DefaultConnection"));
            List<Rating> ratings = new List<Rating>();
            foreach (var oneRating in request)
            {
                ratings.Add(new Rating
                {
                    MovieId = oneRating.MovieId,
                    RatingStarts = oneRating.RatingStars,
                    UserId = freeUserName
                });
            }
            try
            {
                await _ratingRepository.InsertRangeAsync(ratings, true);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}
