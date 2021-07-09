using IMDBLite.API.Repository.Interfaces;
using IMDBLite.BLL.Interfaces;
using IMDBLite.DTO.DTORequests;
using IMDBLite.Shared.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace IMDBLite.BLL.Implementation
{
   
    public class MovieBLL : IMovieBLL
    {
        private readonly IMovieRepository _movieRepository;

        public MovieBLL(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }

        public async Task<PaginatedList<MovieDTO>> GetAll(MovieSearchDTORequest request)
        {
            try
            {
                var movies = await _movieRepository.FindBy(x => x.IsTvshow == request.IsTvShow)
                    .Include(el => el.CastInMovies)
                    .ThenInclude(el => el.Cast)
                    .Include(el => el.Ratings)
                    .Select(el => new MovieDTO
                    {
                        Id = el.Id,
                        Title = el.Title,
                        CoverImg = el.CoverImg,
                        IsTvshow = el.IsTvshow,
                        ReleaseDate = el.ReleaseDate,
                        Rating = el.Ratings.Any() ? decimal.Round((el.Ratings.Sum(p => p.RatingStarts)) / (el.Ratings.Count()), 1, MidpointRounding.AwayFromZero) : 0,
                        Cast = string.Join(", ", el.CastInMovies.Select(el => el.Cast.FirstName + " " + el.Cast.LastName).ToList())
                    }).ToListAsync();

                if (!string.IsNullOrWhiteSpace(request.SearchedData) && !string.IsNullOrEmpty(request.SearchedData))
                    movies = GetMoviesByFilter(movies, request.SearchedData);

                movies = movies.OrderByDescending(el => el.Rating).ToList();

                var paginatedMovies = PaginatedList<MovieDTO>
                    .Create(movies, request.PageNumber ?? 1, request.PageSize ?? int.MaxValue);
                paginatedMovies.Successful = true;
                return paginatedMovies;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public async Task<PaginatedList<MovieDTO>> GetAllForRating(MovieSearchDTORequest request)
        {
            try
            {
                var movies = _movieRepository.FindBy(x => x.IsTvshow == request.IsTvShow)
                    .Include(el => el.CastInMovies)
                    .ThenInclude(el => el.Cast)
                    .Include(el => el.Ratings)
                    .Select(el => new MovieDTO
                    {
                        Id = el.Id,
                        Title = el.Title,
                        CoverImg = el.CoverImg,
                        IsTvshow = el.IsTvshow,
                        ReleaseDate = el.ReleaseDate,
                        Rating = el.Ratings.Any() ? decimal.Round((el.Ratings.Sum(p => p.RatingStarts)) / (el.Ratings.Count()), 1, MidpointRounding.AwayFromZero) : 0,
                        Cast = string.Join(", ", el.CastInMovies.Select(el => el.Cast.FirstName + " " + el.Cast.LastName).ToList())
                    });

                var paginatedMovies = await PaginatedList<MovieDTO>
                    .CreateAsync(movies, request.PageNumber ?? 1, request.PageSize ?? int.MaxValue);

                paginatedMovies.Successful = true;
                return paginatedMovies;
            }
            catch (Exception e)
            {
                return null;
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
