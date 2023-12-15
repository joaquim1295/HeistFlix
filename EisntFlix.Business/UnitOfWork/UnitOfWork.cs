using EisntFlix.Business.Services;
using EisntFlix.Business.Services.IServices;
using EisntFlix.Data.Access.DbContext;
using EisntFlix.Business.UnitOfWork;
using EisntFlix.FetchAPI.APIService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EisntFlix.Business.UnitOfWork
{
    public class UnitOfWork: IUnitOfWork
    {
        private readonly AppDbContext _context;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;

            FilmsService = new FilmsService(new MoviesService(_context),
                           new SeriesService(_context));
            MoviesService= new MoviesService(_context);
            SeriesService= new SeriesService(_context);

            ActorsService = new ActorsService(_context);
            ProducersService = new ProducersService(_context);
            StreamingsService = new StreamingsService(_context);

            FetchAPIService = new FetchAPIService(_context);

        }

        //Content
        public IFilmsService FilmsService { get; private set; }
        public IMoviesService MoviesService { get; private set; }
        public ISeriesService SeriesService { get; private set; }

        //Persons and Streamings
        public IActorsService ActorsService { get; private set; }
        public IProducersService ProducersService { get; private set; }
        public IStreamingsService StreamingsService { get; private set; }

        //API
        public IFetchAPIService FetchAPIService { get; private set; }

        public async Task SaveAsync() => await _context.SaveChangesAsync();

    }

}
