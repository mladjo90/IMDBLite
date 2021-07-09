using IMDBLite.DTO.DTORequests;
using IMDBLite.Shared.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using IMDBLite.DTO.DTOResponse;
using IMDBLite.API.Repository.Interfaces;
using IMDBLite.BLL.Interfaces;

namespace IMDBLite.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class MovieController : ControllerBase
    {
        private ILogger<MovieController> _logger;
        private IMovieBLL _movieBll;

        public MovieController(IMovieBLL movieBLL, ILogger<MovieController> logger)
        {
            _logger = logger;
            _movieBll = movieBLL;
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> GetAll([FromBody] MovieSearchDTORequest request)
        {
            try
            {
                var paginatedMovies = await _movieBll.GetAll(request);
                paginatedMovies.Successful = true;
                return Ok(paginatedMovies);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return BadRequest(new BaseResponse { Successful = false, Errors = new List<string> { "Error with getting data!" } });
            }
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> GetAllForRating([FromBody] MovieSearchDTORequest request)
        {
            try
            {
                var paginatedMovies = await _movieBll.GetAllForRating(request);
                return Ok(paginatedMovies);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return BadRequest(new BaseResponse { Successful = false, Errors = new List<string> { "Error with getting data!" } });
            }

        }

      
    }
}
