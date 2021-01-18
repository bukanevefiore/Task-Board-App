using Microsoft.AspNetCore.Mvc;
using TrelloClone.Services;
using TrelloClone.ViewModels;

namespace TrelloClone.Controllers
{
    // card için controller
    public class CardController : Controller
    {
        private readonly CardService _cardService;

        public CardController(CardService cardService)
        {
            _cardService = cardService;
        }
        // iş takibi için aksiyon
        [HttpGet]
        public IActionResult Details(int id)
        {
            var viewModel = _cardService.GetDetails(id);

            return View(viewModel);
        }
        // update için aksiyon
        [HttpPost]
        public IActionResult Update(CardDetails cardDetails)
        {
            _cardService.Update(cardDetails);

            TempData["Message"] = "Saved card Details";

            return RedirectToAction(nameof(Details), new { id = cardDetails.Id });
        }
    }
}