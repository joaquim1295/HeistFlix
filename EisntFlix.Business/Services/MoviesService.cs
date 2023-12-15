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
    public class MoviesService : EntityBaseRepository<Movie>, IMoviesService
	{
		private readonly AppDbContext _context;
		public MoviesService(AppDbContext context) : base(context)
		{
			_context = context;
		}

        #region //Add, Get and Update Service Methods
        public async Task AddNewMovieAsync(NewMovieVM data)
		{
			var newMovie = new Movie()
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
			await _context.Movies.AddAsync(newMovie);

			//Add Movie Actors
			foreach (var actorId in data.ActorIds)
			{
				var newActorMovie = new Actor_Movie()
				{
					MovieId = newMovie.Id,
					ActorId = actorId
				};
				await _context.Actors_Movies.AddAsync(newActorMovie);
			}
		}


		public async Task<Movie> GetMovieByIdAsync(int id)
		{

			var movieDetails = await _context.Movies
				.Include(c => c.Streaming)
				.Include(p => p.Producer)
				.Include(am => am.Actors_Movies).ThenInclude(a => a.Actor)
				.FirstOrDefaultAsync(n => n.Id == id);

			return movieDetails;
		}

        public async Task UpdateMovieAsync(NewMovieVM data)
        {
            var dbMovie = await _context.Movies.FirstOrDefaultAsync(n => n.Id == data.Id);

            if (dbMovie != null)
            {
                dbMovie.Name = data.Name;
                dbMovie.Description = data.Description;
				dbMovie.Rating = data.Rating;
                dbMovie.ImageURL = data.ImageURL;
                dbMovie.StreamingId = data.StreamingId;
                dbMovie.StartDate = data.StartDate;
                dbMovie.EndDate = data.EndDate;
                dbMovie.FilmCategory = data.MovieCategory;
                dbMovie.ProducerId = data.ProducerId;
            }

            //Remove existing actors
            var existingActorsDb = _context.Actors_Movies.Where(n => n.MovieId == data.Id).ToList();
            _context.Actors_Movies.RemoveRange(existingActorsDb);

            //Add Movie Actors
            foreach (var actorId in data.ActorIds)
            {
                var newActorMovie = new Actor_Movie()
                {
                    MovieId = data.Id,
                    ActorId = actorId
                };
                await _context.Actors_Movies.AddAsync(newActorMovie);
            }

        }
        #endregion
        

		public async Task<NewFilmDropdownsVM> GetNewMovieDropdownsValues()
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

