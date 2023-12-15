using EisntFlix.Business.UnitOfWork;
using EisntFlix.Data.Access.Static;
using EisntFlix.Root.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace EisntFlixWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = UserRoles.Admin)]
    public class ContentController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public ContentController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IActionResult> Index()
        {
           var allContent = await _unitOfWork.FilmsService.GetAllContentAsync();
            return View(allContent);
        }

		#region //Filter Actions

		//Searchbar Filter
		public async Task<IActionResult> Filter(string search)
		{

			var allContent = await _unitOfWork.FilmsService.GetAllContentAsync();
			var filteredResult = allContent;

			if (!string.IsNullOrEmpty(search))
			{
				string lowerSearch = search.ToLower();
				filteredResult = allContent.Where(n => n.Name.ToLower().Contains(lowerSearch) || n.Description.ToLower().Contains(lowerSearch)).ToList();
				if (filteredResult.Count() == 0) TempData["info"] = "No Match Found";
			}

			return View("Index", filteredResult);
		}

		//OrderbyName Filter

		public async Task<IActionResult> OrderbyName(string id)
		{
			var allContent = await _unitOfWork.FilmsService.GetAllContentAsync();
			var orderedResult = allContent;

			if (id.ToLower() == "asc")
			{ orderedResult = allContent.OrderBy(n => n.Name).ToList(); }

			else if (id.ToLower() == "desc")
			{ orderedResult = allContent.OrderByDescending(n => n.Name).ToList(); }

			return View("Index", orderedResult);

		}

        //OrderbyCategory Filter
        public async Task<IActionResult> OrderbyCategory(string id)
		{
			var allContent = await _unitOfWork.FilmsService.GetAllContentAsync();
			var orderedResult = allContent;

			if (id.ToLower() == "asc")
			{ orderedResult = allContent.OrderBy(n => n.Name).ToList().
					OrderBy(n => n.FilmCategory).ToList(); }

			else if (id.ToLower() == "desc")
			{
				orderedResult = allContent.OrderBy(n => n.Name).ToList().
					OrderByDescending(n => n.FilmCategory).ToList(); }

			return View("Index", orderedResult);

		}

		//OrderbyContentType Filter

		[ActionName("OrderbyContent")]
        public async Task<IActionResult> OrderbyContentType(string id)
		{
			var allContent = await _unitOfWork.FilmsService.GetAllContentAsync();
			var orderedResult = allContent;

			if (id.ToLower() == "asc")
			{
				orderedResult = allContent.OrderBy(n => n.Name).ToList().
					OrderBy(n => n.Type).ToList();
			}

			else if (id.ToLower() == "desc")
			{
				orderedResult = allContent.OrderBy(n => n.Name).ToList().
					OrderByDescending(n => n.Type).ToList();
			}
			//new FilmCategory { };
			//Enum.GetName(GetType();

			return View("Index", orderedResult);

		}

		#endregion



	}
}
