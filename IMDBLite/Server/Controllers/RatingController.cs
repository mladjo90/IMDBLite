using IMDBLite.API.DataModels.Helper;
using IMDBLite.API.DataModels.Models;
using IMDBLite.BLL.Interfaces;
using IMDBLite.DTO.DTORequests;
using IMDBLite.DTO.DTOResponse;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IMDBLite.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class RatingController : ControllerBase
    {
        private readonly IRatingRepository _ratingRepository;
        private readonly IConfiguration _configuration;
        private ILogger<RatingController> _logger;

        public RatingController(IRatingRepository ratingRepository, IConfiguration configuration, ILogger<RatingController> logger)
        {
            _ratingRepository = ratingRepository;
            _configuration = configuration;
            _logger = logger;
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> SaveDataForRating([FromBody] List<RatingDTO> request)
        {
            string freeUserName = await StoredProcedureHandler.FindFreeUser(_configuration.GetConnectionString("DefaultConnection"));
            List<Rating> ratings = new List<Rating>(); 
            foreach(var oneRating in request)
            {
                ratings.Add(new Rating{
                    MovieId = oneRating.MovieId,
                    RatingStarts = oneRating.RatingStars,
                    UserId = freeUserName 
                });
            }
            try
            {
                await _ratingRepository.InsertRangeAsync(ratings, true);
                return Ok(new BaseResponse { Successful = true });
            }
            catch(Exception e)
            {
                _logger.LogError(e.Message);
                return BadRequest(new BaseResponse { Successful = false, Errors = new List<string> { "Error with getting data!" } });
            }
        }      
    }
}
