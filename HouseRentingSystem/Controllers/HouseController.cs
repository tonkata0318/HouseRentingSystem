using HouseRentingSystem.Contracts.Agent;
using HouseRentingSystem.Contracts.House;
using HouseRentingSystem.Infrastructure;
using HouseRentingSystem.Models.House;
using HouseRentingSystem.Services.Houses.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HouseRentingSystem.Controllers
{
    [Authorize]
    public class HouseController : Controller
    {
        private readonly IHouseService _houses;
        private readonly IAgentService _agents;
        public HouseController(IHouseService houses,IAgentService agents)
        {
            _houses = houses;
            _agents = agents;
        }

        [AllowAnonymous]
        public async Task<IActionResult> All([FromQuery] AllHousesQueryModel query)
        {
            var queryResult = _houses.All(
                query.Category,
                query.SearchTerm,
                query.Sorting,
                query.CurrentPage,
                AllHousesQueryModel.HousesPerPage);

            query.TotalHousesCount = queryResult.TotalHousesCount;
            query.Houses= queryResult.Houses;

            var houseCategories = _houses.AllCategoriesNames();
            query.Categories = (IEnumerable<string>)houseCategories;

            return View(query);
        }
        public async Task<IActionResult> Mine()
        {
            return View(new AllHousesQueryModel());
        }
        public async Task<IActionResult> Details(int id)
        {
            return View(new HouseDetailsViewModel());
        }
        public async Task<IActionResult> Add()
        {
            if (await _agents.ExistById(User.Id()) == false)
            {
                return RedirectToAction("Become", "Agents");
            }
            return View(new HouseFormModel
            {
                Categories = await _houses.AllCategories()
            });
        }

        [HttpPost]
        public async Task<IActionResult> Add(HouseFormModel model)
        {
            if (await _agents.ExistById(User.Id())==false)
            {
                return RedirectToAction(nameof(AgentsController.Become), "Agents");
            }

            if (await _houses.CategoryExists(model.CategoryId)==false)
            {
                ModelState.AddModelError(nameof(model.CategoryId), "Category does not exist");
            }

            if (!ModelState.IsValid)
            {
                model.Categories = await _houses.AllCategories();
                return View(model);
            }

            var agentId = await _agents.GetAgentId(User.Id());

            var newHouseId = await _houses.Create(model.Title, model.Address, model.Description, model.ImageUrl, model.PricePerMonth, model.CategoryId, agentId ?? 0);

            return RedirectToAction(nameof(Details), new
            {
                Id = newHouseId
            });
        }

        public async Task<IActionResult> Edit(int id)
        {
            return View(new HouseDetailsViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id,HouseDetailsViewModel model)
        {
            return RedirectToAction(nameof(Details), new {id="1"});
        }

        public async Task<IActionResult> Delete(int id)
        {
            return View(new HouseDetailsViewModel());
        }
        [HttpPost]
        public async Task<IActionResult> Delete(HouseDetailsViewModel model)
        {
            return RedirectToAction(nameof(All));
        }
        [HttpPost]
        public async Task<IActionResult> Rent(int id)
        {
            return RedirectToAction(nameof(Mine));
        }
        [HttpPost]
        public async Task<IActionResult> Leave(int id)
        {
            return RedirectToAction(nameof(Mine));
        }
    }
}
