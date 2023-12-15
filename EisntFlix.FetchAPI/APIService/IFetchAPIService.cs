using EisntFlix.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EisntFlix.FetchAPI.APIService
{
    public interface IFetchAPIService
    {
        public Task<List<Movie>> GetDayTrendMoviesAsync();

        public Task<List<Serie>> GetDayTrendSeriesAsync();

        public Task<List<Movie>> SearchMoviesAsync(string searchString, int page=1);

        public Task<List<Serie>> SearchSeriesAsync(string searchString, int page = 1);

    }
}
