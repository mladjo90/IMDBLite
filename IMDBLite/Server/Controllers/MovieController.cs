using IMDBLite.BLL.Interfaces;
using IMDBLite.DTO.DTORequests;
using IMDBLite.Shared.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using IMDBLite.DTO.DTOResponse;

namespace IMDBLite.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class MovieController : ControllerBase
    {
        private ILogger<MovieController> _logger;
        private readonly IMovieRepository _movieRepository;
        private readonly IRatingRepository _ratingRepository;
        private readonly ICastRepository _castRepository;
        private readonly ICastInMovieRepository _castInMovieRepository;

        public MovieController(IMovieRepository movieRepository, IRatingRepository ratingRepository, ICastRepository castRepository, ICastInMovieRepository castInMovieRepository, ILogger<MovieController> logger)
        {
            _movieRepository = movieRepository;
            _ratingRepository = ratingRepository;
            _castRepository = castRepository;
            _castInMovieRepository = castInMovieRepository;
            _logger = logger;
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> GetAll([FromBody] MovieSearchDTORequest request)
        {
            var rating = await _ratingRepository.FindAll().ToListAsync();
            var cast = await _castRepository.FindAll().ToListAsync();
            var castInMovie = await _castInMovieRepository.FindAll().ToListAsync();
            try
            {
                var movies = await _movieRepository.FindBy(x=> x.IsTvshow == request.IsTvShow).Select(el => new MovieDTO
                {
                    Id = el.Id,
                    Title = el.Title,
                    CoverImg = el.CoverImg,
                    IsTvshow = el.IsTvshow,
                    ReleaseDate = el.ReleaseDate
                }).ToListAsync();

                foreach(var movie in movies)
                {
                    var castForOneMovie = castInMovie.Where(el => el.MovieId == movie.Id).Select(el=>el.CastId).ToList();
                    movie.Rating = rating.Any(el=> el.MovieId == movie.Id) ? decimal.Round((rating.Where(x => x.MovieId == movie.Id).Sum(p => p.RatingStarts)) / (rating.Where(x => x.MovieId == movie.Id).Count()),1, MidpointRounding.AwayFromZero) : 0;
                    movie.Cast = string.Join(", ", cast.Where(el => castForOneMovie.Contains(el.Id)).Select(el => el.FirstName + " " + el.LastName).ToList());
                }

                if(!string.IsNullOrWhiteSpace(request.SearchedData) && !string.IsNullOrEmpty(request.SearchedData))
                    movies = GetMoviesByFilter(movies, request.SearchedData);

                movies = movies.OrderByDescending(el => el.Rating).ToList();


                var paginatedMovies = PaginatedList<MovieDTO>
                    .Create(movies, request.PageNumber ?? 1, request.PageSize ?? int.MaxValue);
                paginatedMovies.Successful = true;
                return Ok(paginatedMovies);
            }
            catch(Exception e)
            {
                _logger.LogError(e.Message);
                return BadRequest(new BaseResponse { Successful = false, Errors = new List<string> { "Error with getting data!" } });
            }

        }      
               
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> GetAllForRating([FromBody] MovieSearchDTORequest request)
        {
            var rating = await _ratingRepository.FindAll().ToListAsync();
            var cast = await _castRepository.FindAll().ToListAsync();
            var castInMovie = await _castInMovieRepository.FindAll().ToListAsync();
            try
            {
                var movies = _movieRepository.FindBy(x=> x.IsTvshow == request.IsTvShow).Select(el => new MovieDTO
                {
                    Id = el.Id,
                    Title = el.Title,
                    CoverImg = el.CoverImg,
                    IsTvshow = el.IsTvshow,
                    ReleaseDate = el.ReleaseDate
                });

                var paginatedMovies =await PaginatedList<MovieDTO>
                    .CreateAsync(movies, request.PageNumber ?? 1, request.PageSize ?? int.MaxValue);

                foreach (var movie in paginatedMovies.Items)
                {
                    var castForOneMovie = castInMovie.Where(el => el.MovieId == movie.Id).Select(el => el.CastId).ToList();
                    movie.Rating = rating.Any(el => el.MovieId == movie.Id) ? (rating.Where(x => x.MovieId == movie.Id).Sum(p => p.RatingStarts)) / (rating.Where(x => x.MovieId == movie.Id).Count()) : 0;
                    movie.Cast = string.Join(", ", cast.Where(el => castForOneMovie.Contains(el.Id)).Select(el => el.FirstName + " " + el.LastName).ToList());
                }
                paginatedMovies.Successful = true;
                return Ok(paginatedMovies);
            }
            catch(Exception e)
            {
                _logger.LogError(e.Message);
                return BadRequest(new BaseResponse { Successful = false, Errors = new List<string> { "Error with getting data!" } });
            }

        }

        private List<MovieDTO> GetMoviesByFilter(List<MovieDTO> moviesToFilter, string searchedData)
        {
            Func<MovieDTO, bool> searchCondition;

            Regex regexBiggerStar = new Regex("^(least|less than|more than|at least) [0-9]+ (star|stars)", RegexOptions.IgnoreCase);
            Regex regexSmallerStar = new Regex("^(less than|lower than) [0-9]+ (star|stars)", RegexOptions.IgnoreCase);
            Regex regexCorrectStar = new Regex("^[0-9]+ (star|stars)$", RegexOptions.IgnoreCase);
            Regex regexOlderYear = new Regex("^(older than|older) [0-9]+ (year|years)", RegexOptions.IgnoreCase);
            Regex regexYoungerYear = new Regex("^(younger than|younger) [0-9]+(| |year|years)", RegexOptions.IgnoreCase);
            Regex regexOlderYearCorrectYear = new Regex(@"^(after|in) \d\d\d\d(| |year|years)", RegexOptions.IgnoreCase);
            Regex regexYoungerYearCorrectYear = new Regex(@"^(before) \d\d\d\d(| |year|years)", RegexOptions.IgnoreCase);
           
            if (regexBiggerStar.IsMatch(searchedData.ToLower()))
            {
                int numberOfStarts = Int32.Parse(Regex.Match(searchedData, @"\d+").Value);
                searchCondition = el => el.Rating >= numberOfStarts;
            }

            else if (regexSmallerStar.IsMatch(searchedData.ToLower()))
            {
                int numberOfStarts = Int32.Parse(Regex.Match(searchedData, @"\d+").Value);
                searchCondition = el => el.Rating <= numberOfStarts;
            }

            else if (regexCorrectStar.IsMatch(searchedData.ToLower()))
            {
                int numberOfStarts = Int32.Parse(Regex.Match(searchedData, @"\d+").Value);
                searchCondition = el => el.Rating == numberOfStarts;
            }

            else if (regexOlderYear.IsMatch(searchedData.ToLower()))
            {
                int year = Int32.Parse(Regex.Match(searchedData, @"\d+").Value);
                DateTime searchedYear = new DateTime(DateTime.Now.AddYears(-year).Year, 1, 1);
                searchCondition = el => el.ReleaseDate <= searchedYear;
            }
            else if (regexYoungerYear.IsMatch(searchedData.ToLower()))
            {
                int year = Int32.Parse(Regex.Match(searchedData, @"\d+").Value);
                DateTime searchedYear = new DateTime(DateTime.Now.AddYears(-year).Year, 1, 1);
                searchCondition = el => el.ReleaseDate >= searchedYear;
            }
            else if (regexOlderYearCorrectYear.IsMatch(searchedData.ToLower()))
            {
                int year = Int32.Parse(Regex.Match(searchedData, @"\d+").Value);
                DateTime searchedYear = new DateTime(year, 1, 1);
                searchCondition = el => el.ReleaseDate >= searchedYear;
            }
            else if (regexYoungerYearCorrectYear.IsMatch(searchedData.ToLower()))
            {
                int year = Int32.Parse(Regex.Match(searchedData, @"\d+").Value);
                DateTime searchedYear = new DateTime(year, 1, 1);
                searchCondition = el => el.ReleaseDate <= searchedYear;
            }
            else
            {
                searchCondition = el => el.Title.ToLower().Contains(searchedData.ToLower());
            }

            return moviesToFilter.Where(searchCondition).OrderBy(el => el.Rating).ToList();
        }
    }
}
