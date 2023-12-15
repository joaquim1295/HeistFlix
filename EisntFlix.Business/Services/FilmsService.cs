using EisntFlix.Business.Services.IServices;
using EisntFlix.Data.Access.DbContext;
using EisntFlix.Models;
using EisntFlix.Models.ViewsModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EisntFlix.Business.Services
{
    public class FilmsService : IFilmsService
    {
        private readonly IMoviesService  _moviesService;
        private readonly ISeriesService _seriesService;

        public FilmsService(IMoviesService moviesService, ISeriesService seriesService)
        {
            _moviesService = moviesService;
            _seriesService = seriesService;
        }

        public async Task<IEnumerable<Film>> GetAllContentAsync()
        {
            var allMovies = await _moviesService.GetAllAsync(n => n.Streaming);
            var allSeries = await _seriesService.GetAllAsync(n => n.Streaming);

            var allContent = new List<Film>();
            allContent.AddRange(allMovies);
            allContent.AddRange(allSeries);


            return allContent;
        }
    }
}
