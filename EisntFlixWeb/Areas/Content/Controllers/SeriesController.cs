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
    public class SeriesController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public SeriesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        #region //Series Controller Methods

        public async Task CreateDropdownsViewBag()
        {
            var serieDropdownsData = await _unitOfWork.SeriesService.GetNewSerieDropdownsValues();

            ViewBag.Streamings = new SelectList(serieDropdownsData.Streamings, "Id", "Name");
            ViewBag.Producers = new SelectList(serieDropdownsData.Producers, "Id", "FullName");
            ViewBag.Actors = new SelectList(serieDropdownsData.Actors, "Id", "FullName");
        }
        #endregion

        #region //Index and Details Actions

        //Index
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var allSeries = await _unitOfWork.SeriesService.GetAllAsync(n => n.Streaming);
            return View(allSeries);
        }

        //GET: Movies/Details/1
        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
        {
            var serieDetails = await _unitOfWork.SeriesService.GetSerieByIdAsync(id);
            return View(serieDetails);
        }
        #endregion

        #region //Create Actions

        //GET: Series/Create
        public async Task<IActionResult> Create()
        {
            await CreateDropdownsViewBag();

            return View();
        }
        //POST: Series/Create 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(NewSerieVM serie)
        {
            if (!ModelState.IsValid)
            {
                await CreateDropdownsViewBag();

                return View(serie);
            }


            await _unitOfWork.SeriesService.AddNewSerieAsync(serie);
            await _unitOfWork.SaveAsync();
            TempData["success"] = "Serie created successfully";
            return RedirectToAction("Index");
        }
        #endregion

        #region //Edit Actions

        //GET: Series/Edit/1
        public async Task<IActionResult> Edit(int id)
        {
            var serieDetails = await _unitOfWork.SeriesService.GetSerieByIdAsync(id);
            if (serieDetails == null) return View("NotFound");

            var response = new NewSerieVM(serieDetails);

            await CreateDropdownsViewBag();
            return View(response);
        }

        //POST: Series/Edit/1
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, NewSerieVM serie)
        {
            if (id != serie.Id) return View("NotFound");

            if (!ModelState.IsValid)
            {
                await CreateDropdownsViewBag();
                return View(serie);
            }

            await _unitOfWork.SeriesService.UpdateSerieAsync(serie);
            await _unitOfWork.SaveAsync();
            TempData["success"] = "Serie updated successfully";

            return RedirectToAction("Index");
        }
        #endregion

        #region //Delete Actions

        //GET: Series/Delete/1
        public async Task<IActionResult> Delete(int id)
        {
            var serieDetails = await _unitOfWork.SeriesService.GetSerieByIdAsync(id);
            if (serieDetails == null) return View("NotFound");

            var response = new NewSerieVM(serieDetails);

            await CreateDropdownsViewBag();
            return View(response);
        }
        //POST: Series/Delete/1

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirm(int id, NewSerieVM serie)
        {
            if (id != serie.Id) return View("NotFound");

            await _unitOfWork.SeriesService.DeleteAsync(id);
            await _unitOfWork.SaveAsync();
            TempData["success"] = "Serie deleted successfully";

            return RedirectToAction("Index");

        }
        #endregion

        #region //Filter Actions

        //Searchbar Filter
        [AllowAnonymous]
        public async Task<IActionResult> Filter(string search)
        {
            var allSeries = await _unitOfWork.SeriesService.GetAllAsync(n => n.Streaming);
            var filteredResult = allSeries;

            if (!string.IsNullOrEmpty(search))
            {
                string lowerSearch = search.Trim().ToLower();
                filteredResult = allSeries.Where(n => n.Name.ToLower().Contains(lowerSearch) || n.Description.ToLower().Contains(lowerSearch)).ToList();
                if (filteredResult.Count() == 0) TempData["info"] = "No Match Found";
            }
            return View("Index", filteredResult);
        }
        //Filter Category
        [AllowAnonymous]
        [ActionName("Category")]
        public async Task<IActionResult> FilterCategory(string id)
        {
            var allSeries = await _unitOfWork.SeriesService.GetAllAsync(n => n.Streaming);

            if (!string.IsNullOrEmpty(id))
            {
                var filteredResult = allSeries.Where(n => n.FilmCategory.ToString().Contains(id)).ToList();
                if (filteredResult.Count() == 0) return View("NotFound");
                return View("Index", filteredResult);
            }

            return RedirectToAction("Index");
        }
        #endregion
    }
}

