using HouseRentingSystem.Contracts.Agent;
using HouseRentingSystem.Infrastructure;
using HouseRentingSystem.Models.Agent;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HouseRentingSystem.Controllers
{
    [Authorize]
    public class AgentsController : Controller
    {
        private readonly IAgentService _agents;

        public AgentsController(IAgentService agents)
        {
            _agents = agents;
        }
        public async Task<IActionResult> Become()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Become(BecomeAgentFormModel model)
        {
            var userId = User.Id();

            if (await _agents.ExistById(userId))
            {
                return BadRequest();
            }

            if (await _agents.UserWithPhoneNumberExists(model.PhoneNumber))
            {
                ModelState.AddModelError(nameof(model.PhoneNumber),
                    "Phone number already exist enter another one");
            }

            if (await _agents.UserHasRents(userId))
            {
                ModelState.AddModelError("Error", "You shoud have not rents to become agent!");
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await _agents.Create(userId,model.PhoneNumber);

            return RedirectToAction(nameof(HouseController.All), "House");
        }
    }
}
