using EisntFlix.Data.Access.BaseRepository;
using EisntFlix.Models.ViewsModel;
using EisntFlix.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EisntFlix.Data.Access.DbContext;
using EisntFlix.Business.Services.IServices;

namespace EisntFlix.Business.Services
{
    public class SeriesService : EntityBaseRepository<Serie>, ISeriesService
    {
        private readonly AppDbContext _context;

        public SeriesService(AppDbContext context) : base(context)
        {
            _context = context;
        }


        #region // Add, Get and Update Service Methods
        public async Task AddNewSerieAsync(NewSerieVM data)
        {

            var newSerie = new Serie()
            {
                Name = data.Name,
                Description = data.Description,
                Price = data.Price,
                ImageURL = data.ImageURL,
                StreamingId = data.StreamingId,
                StartDate = data.StartDate,
                EndDate = data.EndDate,
                FilmCategory = data.MovieCategory,
                ProducerId = data.ProducerId
            };
            await _context.AddAsync(newSerie);

            //Add Serie Actors
            foreach (var actorId in data.ActorIds)
            {
                var newActorSerie = new Actor_Serie()
                {
                    SerieId = newSerie.Id,
                    ActorId = actorId
                };
                await _context.AddAsync(newActorSerie);
            }
        }
        public async Task<Serie> GetSerieByIdAsync(int id)
        {

            var serieDetails = await _context.Series
                .Include(c => c.Streaming)
                .Include(p => p.Producer)
                .Include(am => am.Actors_Series).ThenInclude(a => a.Actor)
                .FirstOrDefaultAsync(n => n.Id == id);

            return serieDetails;
        }

        public async Task UpdateSerieAsync(NewSerieVM data)
        {
            var dbSerie = await _context.Series.FirstOrDefaultAsync(n => n.Id == data.Id);

            if (dbSerie != null)
            {
                dbSerie.Name = data.Name;
                dbSerie.Description = data.Description;
                dbSerie.Price = data.Price;
                dbSerie.ImageURL = data.ImageURL;
                dbSerie.StreamingId = data.StreamingId;
                dbSerie.StartDate = data.StartDate;
                dbSerie.EndDate = data.EndDate;
                dbSerie.FilmCategory = data.MovieCategory;
                dbSerie.ProducerId = data.ProducerId;
            }

            //Remove existing actors
            var existingActorsDb = _context.Actors_Series.Where(n => n.SerieId == data.Id).ToList();
            _context.Actors_Series.RemoveRange(existingActorsDb);

            //Add Movie Actors
            foreach (var actorId in data.ActorIds)
            {
                var newActorSerie = new Actor_Serie()
                {
                    SerieId = data.Id,
                    ActorId = actorId
                };
                await _context.Actors_Series.AddAsync(newActorSerie);
            }
        }
        #endregion


        public async Task<NewFilmDropdownsVM> GetNewSerieDropdownsValues()
        {
            var response = new NewFilmDropdownsVM()
            {
                Actors = await _context.Actors.OrderBy(n => n.FullName).ToListAsync(),
                Streamings = await _context.Streamings.OrderBy(n => n.Name).ToListAsync(),
                Producers = await _context.Producers.OrderBy(n => n.FullName).ToListAsync()
            };

            return response;
        }
    }
}
