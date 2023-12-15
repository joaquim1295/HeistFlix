using EisntFlix.Business.UnitOfWork;
using EisntFlix.Data.Access.Static;
using EisntFlix.Models.ViewsModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data;

namespace EisntFlixWeb.Areas.Content.Controllers
{
    [Area("Content")]
    [Authorize(Roles = UserRoles.Admin)]
    public class MoviesController : Controller
	{
		private readonly IUnitOfWork _unitOfWork;
		public MoviesController(IUnitOfWork  unitOfWork)
		{
			_unitOfWork = unitOfWork;

		}

		#region //Movie Controller Methods

		public async Task CreateDropdownsViewBag()
		{
			var movieDropdownsData = await _unitOfWork.MoviesService.GetNewMovieDropdownsValues();
			ViewBag.Streamings = new SelectList(movieDropdownsData.Streamings, "Id", "Name");
			ViewBag.Producers = new SelectList(movieDropdownsData.Producers, "Id", "FullName");
			ViewBag.Actors = new SelectList(movieDropdownsData.Actors, "Id", "FullName");
		}
		#endregion

		#region //Index and Details Actions

		//Index
		[AllowAnonymous]
        public async Task<IActionResult> Index()
		{
            var allMovies = await _unitOfWork.MoviesService.GetAllAsync(n => n.Streaming);
			return View(allMovies);
		}

		//GET: Movies/Details/1
		[AllowAnonymous]
		public async Task<IActionResult> Details(int id)
		{
            var movieDetails = await _unitOfWork.MoviesService.GetMovieByIdAsync(id);
			return View(movieDetails);
		}
		#endregion

		#region //Create Actions

		//GET: Movies/Create
		public async Task<IActionResult> Create()
		{
			await CreateDropdownsViewBag();

			return View();
		}

		//POST: Movie/Create
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(NewMovieVM movie)
		{
			if (!ModelState.IsValid)
			{
				await CreateDropdownsViewBag();

				return View(movie);
			}


			await _unitOfWork.MoviesService.AddNewMovieAsync(movie);
            await _unitOfWork.SaveAsync();
            TempData["success"] = "Movie created successfully";

			return RedirectToAction("Index");
		}
		#endregion

		#region //Edit Actions

		//GET: Movies/Edit/1
		public async Task<IActionResult> Edit(int id)
		{
			var movieDetails = await _unitOfWork.MoviesService.GetMovieByIdAsync(id);
			if (movieDetails == null) return View("NotFound");

			var response = new NewMovieVM(movieDetails);

			await CreateDropdownsViewBag();
			return View(response);
		}

		//POST: Movie/Edit/1
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, NewMovieVM movie)
		{
			if (id != movie.Id) return View("NotFound");

			if (!ModelState.IsValid)
			{
				await CreateDropdownsViewBag();
				return View(movie);
			}

			await _unitOfWork.MoviesService.UpdateMovieAsync(movie);
			await _unitOfWork.SaveAsync();
			TempData["success"] = "Movie updated successfully";

			return RedirectToAction("Index");
		}
		#endregion

		#region //Delete Actions

		//GET: Movie/Delete/1
		public async Task<IActionResult> Delete(int id)
		{
			var movieDetails = await _unitOfWork.MoviesService.GetMovieByIdAsync(id);
			if (movieDetails == null) return View("NotFound");

			var response = new NewMovieVM(movieDetails);

			await CreateDropdownsViewBag();
			return View(response);
		}
		//POST: Movie/Delete/1

		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirm(int id, NewMovieVM movie)
		{
			if (id != movie.Id) return View("NotFound");

			await _unitOfWork.MoviesService.DeleteAsync(id);
			await _unitOfWork.SaveAsync();	
			TempData["success"] = "Movie deleted successfully";

			return RedirectToAction("Index");

		}
		#endregion

		#region //Filter Actions

		//Searchbar Filter
		[AllowAnonymous]
		public async Task<IActionResult> Filter(string search)
		{
			var allMovies = await _unitOfWork.MoviesService.GetAllAsync(n => n.Streaming);
			var filteredResult = allMovies;

            if (!string.IsNullOrEmpty(search))
			{
				string lowerSearch = search.Trim().ToLower();
				filteredResult = allMovies.Where(n => n.Name.ToLower().Contains(lowerSearch) || n.Description.ToLower().Contains(lowerSearch)).ToList();
				if (filteredResult.Count() == 0) TempData["info"] = "No Match Found";   
            }
            return View("Index", filteredResult);
        }

		//Filter Category
		[AllowAnonymous]
		[ActionName("Category")]
		public async Task<IActionResult> FilterCategory(string id)
		{
			var allMovies = await _unitOfWork.MoviesService.GetAllAsync(n => n.Streaming);

			if (!string.IsNullOrEmpty(id))
			{
				var filteredResult = allMovies.Where(n => n.FilmCategory.ToString().Contains(id)).ToList();
				if (filteredResult.Count() == 0) return View("NotFound");
                return View("Index", filteredResult);
            }
            return RedirectToAction("Index");
		}
		#endregion
	}
}
