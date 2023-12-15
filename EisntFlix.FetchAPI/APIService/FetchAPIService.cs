using EisntFlix.Data.Access.DbContext;
using EisntFlix.Models;
using EisntFlix.Root.Enums;
using EisntFlix.FetchAPI.APIModels;
using Microsoft.EntityFrameworkCore.Metadata;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.EntityFrameworkCore;
using EisntFlix.Data.Access.BaseRepository;
using EisntFlix.Root.Base;
using EisntFlix.FetchAPI.APIMethods;

namespace EisntFlix.FetchAPI.APIService
{
    public class FetchAPIService: IFetchAPIService
    {
        #region //Initialization
        private readonly AppDbContext _context;

        public FetchAPIService(AppDbContext context)
        {
            _context = context;
        }

        private const string apiKey = "2680464f8f9eaa53fc4722cc264e1f66";
        private const string imgUrlBase = "https://image.tmdb.org/t/p/original";
        #endregion

        #region //Add to Database Methods
        private async Task<List<Movie>> AddMovieIfNotInDb(HttpResponseMessage response)
        {
            List<Movie> movies = new();
            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                var apiResponse = JsonConvert.DeserializeObject<APIResponseMovie>(jsonString);

                foreach (var result in apiResponse.Results)
                {
                    int indexGenre = result.Genre_Ids.Length > 0 ? result.Genre_Ids[0] : 1;
                    Movie movie = new()
                    {
                        ApiId = result.Id,
                        Name = result.Title,
                        Description = result.Overview,
                        ImageURL = imgUrlBase + result.Poster_Path,
                        StartDate = result.Release_date,
                        Rating = (float)Math.Round(result.Vote_Average, 1),
                        FilmCategory =
                            (FilmCategory)Enum.ToObject(typeof(FilmCategory),
                            APIMethodsRepo.IndexGenreIdConverter(indexGenre)),
                    };
                    movies.Add(movie);

                    var existingMovie = await _context.Movies.FirstOrDefaultAsync(m => m.ApiId == movie.ApiId);
                    if (existingMovie == null) await _context.Movies.AddAsync(movie);
                }

            }
            return movies;
        }

        private async Task<List<Serie>> AddSerieIfNotInDb(HttpResponseMessage response)
        {
            List<Serie> series = new();
            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                var apiResponse = JsonConvert.DeserializeObject<ApiResponseSerie>(jsonString);

                foreach (var result in apiResponse.Results)
                {
                    int indexGenre = result.Genre_Ids.Length > 0 ? result.Genre_Ids[0] : 1;
                    Serie serie = new()
                    {
                        ApiId = result.Id,
                        Name = result.Name,
                        Description = result.Overview,
                        ImageURL = imgUrlBase + result.Poster_Path,
                        StartDate = result.First_air_date,
                        Rating = (float)Math.Round(result.Vote_Average, 1),
                        FilmCategory =
                            (FilmCategory)Enum.ToObject(typeof(FilmCategory),
                            APIMethodsRepo.IndexGenreIdConverter(indexGenre)),
                    };
                    series.Add(serie);

                    var existingSerie = await _context.Series.FirstOrDefaultAsync(m => m.ApiId == serie.ApiId);
                    if (existingSerie == null) await _context.Series.AddAsync(serie);
                }
               
            }
            return series;
        }
        #endregion

        #region //GetDayTrend Methods
        public async Task<List<Movie>> GetDayTrendMoviesAsync()
        {
            using (var client = new HttpClient())
            {
                string url = "https://api.themoviedb.org/3/trending/movie/day?api_key=" + apiKey;
                var response = await client.GetAsync(url);
                return await AddMovieIfNotInDb(response);
      
            }
        }

        public async Task<List<Serie>> GetDayTrendSeriesAsync()
        {

            using (var client = new HttpClient())
            {
                string url = "https://api.themoviedb.org/3/trending/tv/day?api_key=" + apiKey;
                var response = await client.GetAsync(url);
                return await AddSerieIfNotInDb(response);
            }
            
        }
        #endregion

        #region //Search Methods
        public async Task<List<Movie>> SearchMoviesAsync(string searchString, int page = 1)
        {
            List<Movie> movies = new List<Movie>();
            using (var client = new HttpClient())
            {
                searchString = Uri.EscapeDataString(searchString.Trim().ToLower());
                string url = "https://api.themoviedb.org/3/search/movie?api_key=" + apiKey
                    + "&language=en-US&query=" + searchString 
                    + "&page=" + page + "  &include_adult=false";

                var response = await client.GetAsync(url);
                return await AddMovieIfNotInDb(response);
            }
        }

        public async Task<List<Serie>> SearchSeriesAsync(string searchString, int page = 1)
        {
            List<Serie> series = new List<Serie>();
            using (var client = new HttpClient())
            {
                searchString = Uri.EscapeDataString(searchString.Trim().ToLower());
                string url = "https://api.themoviedb.org/3/search/tv?api_key=" + apiKey 
                    + "&language=en-US&query=" + searchString 
                    + "&page=" + page + "&include_adult=false";

                var response = await client.GetAsync(url);
                return await AddSerieIfNotInDb(response);    
            }
        }
        #endregion
    }
}