using EisntFlix.Business.UnitOfWork;
using EisntFlix.Data.Access.Static;
using EisntFlix.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace EisntFlixWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = UserRoles.Admin)]
    public class StreamingsController : Controller
	{
		private readonly IUnitOfWork _unitOfWork;

		public StreamingsController(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		#region //Index and Details Actions

		//Index
		public async Task<IActionResult> Index()
		{
			var AllStreamings = await _unitOfWork.StreamingsService.GetAllAsync();
			return View(AllStreamings);
		}

		//GET: Streamings/Details/id
		public async Task<IActionResult> Details(int id)
		{
			var streamingDetails = await _unitOfWork.StreamingsService.GetByIdAsync(id);
			if (streamingDetails == null) return View("NotFound");
			return View(streamingDetails);
		}
		#endregion

		#region //Create Actions

		//GET: Streamings/Create
		public IActionResult Create()
		{
			return View();
		}

		//POST: Streamings/Create
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create([Bind("Logo,Name,Description")] Streaming streaming)
		{
			if (!ModelState.IsValid) return View(streaming);
			await _unitOfWork.StreamingsService.AddAsync(streaming);
            await _unitOfWork.SaveAsync();
            TempData["success"] = "Streaming created successfully";

			return RedirectToAction("Index");
		}
		#endregion

		#region //Edit Actions

		//GET: Streamings/Edit/1
		public async Task<IActionResult> Edit(int id)
		{
			var streamingDetails = await _unitOfWork.StreamingsService.GetByIdAsync(id);
			if (streamingDetails == null) return View("NotFound");
			return View(streamingDetails);
		}

		//POST: Streamings/Edit/1
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, [Bind("Id,Logo,Name,Description")] Streaming streaming)
		{
			if (!ModelState.IsValid) return View(streaming);
			await _unitOfWork.StreamingsService.UpdateAsync(id, streaming);
            await _unitOfWork.SaveAsync();
            TempData["success"] = "Streaming updated successfully";

			return RedirectToAction("Index");
		}
		#endregion

		#region //Delete Actions
		//GET: Streamings/Delete/1
		public async Task<IActionResult> Delete(int id)
		{
			var streamingDetails = await _unitOfWork.StreamingsService.GetByIdAsync(id);
			if (streamingDetails == null) return View("NotFound");
			return View(streamingDetails);
		}

		//POST: Streamings/Delete/1
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirm(int id)
		{
			var streamingDetails = await _unitOfWork.StreamingsService.GetByIdAsync(id);
			if (streamingDetails == null) return View("NotFound");

			await _unitOfWork.StreamingsService.DeleteAsync(id);
            await _unitOfWork.SaveAsync();
            TempData["success"] = "Streaming deleted successfully";

			return RedirectToAction(nameof(Index));
		}
		#endregion

		#region //Filter Actions

		//Searchbar Filter
		public async Task<IActionResult> Filter(string search)
		{

			var allStreamings = await _unitOfWork.StreamingsService.GetAllAsync();
			var filteredResult = allStreamings;


            if (!string.IsNullOrEmpty(search))
			{
				string lowerSearch = search.ToLower();
				filteredResult = allStreamings.Where(n => n.Name.ToLower().Contains(lowerSearch) || n.Description.ToLower().Contains(lowerSearch)).ToList();
				if (filteredResult.Count() == 0) TempData["info"] = "No Match Found";
				
			}

            return View("Index", filteredResult);
        }

		//OrderbyName Filter

		[ActionName("Orderby")]
		public async Task<IActionResult> OrderbyName(string id)
		{
			var allStreamings = await _unitOfWork.StreamingsService.GetAllAsync();
			var orderedResult = allStreamings;

            if (id.ToLower() == "nameasc")
			{orderedResult = allStreamings.OrderBy(n => n.Name).ToList();}

			else if (id.ToLower() == "namedesc")
			{orderedResult = allStreamings.OrderByDescending(n => n.Name).ToList();}

			return View("Index", orderedResult);

        }

		#endregion
	}
}

