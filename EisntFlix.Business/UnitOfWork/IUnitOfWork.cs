using EisntFlix.Business.Services.IServices;
using EisntFlix.FetchAPI.APIService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EisntFlix.Business.UnitOfWork
{
    public interface IUnitOfWork
    {
       
        //Content
        IFilmsService FilmsService { get; }
        IMoviesService MoviesService { get; }
        ISeriesService SeriesService { get; }

        //Persons and Streamings
        IActorsService ActorsService { get; }
        IProducersService ProducersService { get; }
        IStreamingsService StreamingsService { get; }

        //API
        IFetchAPIService FetchAPIService { get; }

       public Task SaveAsync();
    }
}
