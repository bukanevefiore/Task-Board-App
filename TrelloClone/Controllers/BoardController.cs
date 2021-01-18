using Microsoft.AspNetCore.Mvc;
using TrelloClone.Services;
using TrelloClone.ViewModels;

namespace TrelloClone.Controllers
{
    public class BoardController : Controller
    {
        private readonly BoardService _boardService;
        // board için controller
        public BoardController(BoardService boardService)
        {
            _boardService = boardService;
        }
        // indexi için aksiyon
        public IActionResult Index(int id)
        {
            var model = _boardService.GetBoard(id);

            return View(model);
        }
        // details için aksiyon(id)
        public IActionResult Details(int id)
        {
            return View(_boardService.GetBoard(id));
        }
        // tahtaya addcard aksiyonu
        public IActionResult AddCard(int id)
        {
            return View();
        }
        // httppost kısmı
        [HttpPost]
        public IActionResult AddCard(AddCard viewModel)
        {
            if (!ModelState.IsValid) return View(viewModel);

            _boardService.AddCard(viewModel);
            
            return RedirectToAction(nameof(Index), new { id = viewModel.Id });
        }
    }
}