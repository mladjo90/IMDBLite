using IMDBLite.API.DataModels.Helper;
using IMDBLite.API.DataModels.Models;
using IMDBLite.API.Repository.Interfaces;
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

        private ILogger<RatingController> _logger;
        private IRatingBLL _ratingBLL;

        public RatingController(IRatingBLL ratingBLL, ILogger<RatingController> logger)
        {
            _ratingBLL = ratingBLL;
            _logger = logger;
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> SaveDataForRating([FromBody] List<RatingDTO> request)
        {
            try
            {
                return Ok(new BaseResponse { Successful = await _ratingBLL.SaveDataForRating(request) });
            }
            catch(Exception e)
            {
                _logger.LogError(e.Message);
                return BadRequest(new BaseResponse { Successful = false, Errors = new List<string> { "Error with getting data!" } });
            }
        }      
    }
}
