using EisntFlix.Business.UnitOfWork;
using EisntFlix.FetchAPI.APIService;
using EisntFlix.Models.ViewsModel;
using EisntFlix.Root.Enums;
using Microsoft.AspNetCore.Mvc;

namespace EisntFlixWeb.Areas.Content.Controllers
{
    [Area("Content")]
    public class HomeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public HomeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IActionResult> Index()
        {
            var dayTrendAll = new FilmListVM()
            {
                Allmovies = await _unitOfWork.FetchAPIService.GetDayTrendMoviesAsync(),
                Allseries = await _unitOfWork.FetchAPIService.GetDayTrendSeriesAsync()
            };
            await _unitOfWork.SaveAsync();

            return View(dayTrendAll);
        }
          

        //Searchbar Filter
        public async Task<IActionResult> Filter(string search)
        {
            var filteredResult = await _unitOfWork.FetchAPIService.SearchSeriesAsync(search);
            if (filteredResult.Count() == 0) TempData["info"] = "No Match Found";
            await _unitOfWork.SaveAsync();
            return View("Index", filteredResult);
        }
    }
}
