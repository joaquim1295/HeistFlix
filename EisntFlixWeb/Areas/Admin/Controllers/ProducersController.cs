using EisntFlix.Business.UnitOfWork;
using EisntFlix.Data.Access.Static;
using EisntFlix.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EisntFlixWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = UserRoles.Admin)]
    public class ProducersController : Controller
	{
		private readonly IUnitOfWork _unitOfWork;

		public ProducersController(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		#region //Index and Details Actions

		//Index
		[AllowAnonymous]
		public async Task<IActionResult> Index()
		{
			var allProducers = await _unitOfWork.ProducersService.GetAllAsync();
			return View(allProducers);
		}

		//GET: Producers/Details/1
		[AllowAnonymous]
		public async Task<IActionResult> Details(int id)
		{
			var producerDetails = await _unitOfWork.ProducersService.GetByIdAsync(id);
			if (producerDetails == null) return View("NotFound");
			return View(producerDetails);
		}
		#endregion

		#region //Create Actions

		//GET: Producers/Create
		public IActionResult Create()
		{
			return View();
		}

		//POST: Producers/Create
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create([Bind("ProfilePictureURL,FullName,Bio")] Producer producer)
		{
			if (!ModelState.IsValid) return View(producer);

			await _unitOfWork.ProducersService.AddAsync(producer);
            await _unitOfWork.SaveAsync();
            TempData["success"] = "Producer created successfully";

			return RedirectToAction("Index");
		}
		#endregion

		#region //Edit Actions

		//GET: Producers/Edit/1
		public async Task<IActionResult> Edit(int id)
		{
			var producerDetails = await _unitOfWork.ProducersService.GetByIdAsync(id);
			if (producerDetails == null) return View("NotFound");
			return View(producerDetails);
		}

		//POST: Producers/Edit/1
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, [Bind("Id,ProfilePictureURL,FullName,Bio")] Producer producer)
		{
			if (!ModelState.IsValid) return View(producer);
			if (id == producer.Id)
			{
				await _unitOfWork.ProducersService.UpdateAsync(id, producer);
                await _unitOfWork.SaveAsync();
                TempData["success"] = "Producer updated successfully";

				return RedirectToAction(nameof(Index));
			}

			return View(producer);
		}
		#endregion

		#region //Delete Actions

		//GET: Producers/Delete/1
		public async Task<IActionResult> Delete(int id)
		{
			var producerDetails = await _unitOfWork.ProducersService.GetByIdAsync(id);
			if (producerDetails == null) return View("NotFound");
			return View(producerDetails);
		}

		//POST: Producers/Delete/1
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			var producerDetails = await _unitOfWork.ProducersService.GetByIdAsync(id);
			if (producerDetails == null) return View("NotFound");

			await _unitOfWork.ProducersService.DeleteAsync(id);
            await _unitOfWork.SaveAsync();
            TempData["success"] = "Producer deleted successfully";
			return RedirectToAction(nameof(Index));
		}
		#endregion

		#region //Filter Actions

		//Searchbar Filter
		public async Task<IActionResult> Filter(string search)
		{

			var allProducers = await _unitOfWork.ProducersService.GetAllAsync();
			var filteredResult = allProducers;

            if (!string.IsNullOrEmpty(search))
			{
				string lowerSearch = search.ToLower();
				filteredResult = allProducers.Where(n => n.FullName.ToLower().Contains(lowerSearch) || n.Bio.ToLower().Contains(lowerSearch)).ToList();
				if (filteredResult.Count() == 0) TempData["info"] = "No Match Found";	
			}

            return View("Index", filteredResult);
        }

		//OrderbyName Filter

		[ActionName("Orderby")]
		public async Task<IActionResult> OrderbyName(string id)
		{
			var allProducers = await _unitOfWork.ProducersService.GetAllAsync();
			var orderedResult = allProducers;

            if (id.ToLower() == "nameasc")
			{orderedResult = allProducers.OrderBy(n => n.FullName).ToList();}

			else if (id.ToLower() == "namedesc")
			{orderedResult = allProducers.OrderByDescending(n => n.FullName).ToList();}

            return View("Index", orderedResult);

        }
		#endregion
	}
}

