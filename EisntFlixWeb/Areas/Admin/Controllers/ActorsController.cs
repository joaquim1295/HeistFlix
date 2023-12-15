using EisntFlix.Business.UnitOfWork;
using EisntFlix.Data.Access.Static;
using EisntFlix.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace EisntFlixWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = UserRoles.Admin)]
    public class ActorsController : Controller
    {
		private readonly IUnitOfWork _unitOfWork;

        public ActorsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        #region //Index and Details Actions

        //Index 
        public async Task<IActionResult> Index()
        {
            var allActors = await _unitOfWork.ActorsService.GetAllAsync();
            return View(allActors.OrderBy(n => n.FullName));
        }

        //Get: Actors/Details/1
        public async Task<IActionResult> Details(int id)
        {
            var actorDetails = await _unitOfWork.ActorsService.GetByIdAsync(id);

            if (actorDetails == null) return View("NotFound");
            return View(actorDetails);
        }
        #endregion

        #region Create Actions

        //GET: Actors/Create
        public IActionResult Create()
        {
            return View();
        }

        //POST: Actors/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FullName,ProfilePictureURL,Bio")] Actor actor)
        {


            if (!ModelState.IsValid) return View(actor);

            await _unitOfWork.ActorsService.AddAsync(actor);
            await _unitOfWork.SaveAsync();
            TempData["success"] = "Actor created successfully";

            return RedirectToAction("Index");
        }
        #endregion

        #region //Edit Actions
        
        //GET: Actors/Edit/1
        public async Task<IActionResult> Edit(int id)
        {
            var actorDetails = await _unitOfWork.ActorsService.GetByIdAsync(id);
            if (actorDetails == null) return View("Not Found");
            return View(actorDetails);
        }

        //POST: Actors/Edit/1
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FullName,ProfilePictureURL,Bio")] Actor actor)
        {
            if (!ModelState.IsValid) return View(actor);
            if (id == actor.Id)
            {
                await _unitOfWork.ActorsService.UpdateAsync(id, actor);
                await _unitOfWork.SaveAsync();
                TempData["success"] = "Actor updated successfully";

                return RedirectToAction("Index");
            }
            return View(actor);
        }
        #endregion

        #region//Delete Actions

        //GET: Actors/Delete/1
        public async Task<IActionResult> Delete(int id)
        {
            var actorDetails = await _unitOfWork.ActorsService.GetByIdAsync(id);
            if (actorDetails == null) return View("Not Found");
            return View(actorDetails);
        }

        //POST: Actors/Delete/1
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var actorDetails = await _unitOfWork.ActorsService.GetByIdAsync(id);
            if (actorDetails == null) return View("NotFound");

            await _unitOfWork.ActorsService.DeleteAsync(id);
            await _unitOfWork.SaveAsync();
            TempData["success"] = "Actor deleted successfully";

            return RedirectToAction("Index");
        }
        #endregion

        #region //Filter Actions

        //Searchbar Filter
        public async Task<IActionResult> Filter(string search)
        {

            var allActors = await _unitOfWork.ActorsService.GetAllAsync();
            var filteredResult = allActors;

            if (!string.IsNullOrEmpty(search))
            {
                string lowerSearch = search.ToLower();
                filteredResult = allActors.Where(n => n.FullName.ToLower().Contains(lowerSearch) || n.Bio.ToLower().Contains(lowerSearch)).ToList();
                if (filteredResult.Count() == 0) TempData["info"] = "No Match Found";
            }

            return View("Index", filteredResult);
        }

        //OrderbyName Filter

        [ActionName("Orderby")]
        public async Task<IActionResult> OrderbyName(string id)
        {
            var allActors = await _unitOfWork.ActorsService.GetAllAsync();
            var orderedResult = allActors;

            if (id.ToLower() == "nameasc")
            {orderedResult = allActors.OrderBy(n => n.FullName).ToList();}

            else if (id.ToLower() == "namedesc")
            {orderedResult = allActors.OrderByDescending(n => n.FullName).ToList();}

            return View("Index", orderedResult);

		}
        
            #endregion

        }
}
