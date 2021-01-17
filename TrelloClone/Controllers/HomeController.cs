using Microsoft.AspNetCore.Mvc;
using TrelloClone.Services;
using TrelloClone.ViewModels;

namespace TrelloClone.Controllers
{
    [AutoValidateAntiforgeryToken]
    public class HomeController : Controller
    {
        private readonly BoardService _boardService;

        public HomeController(BoardService boardService)
        {
            _boardService = boardService;
        }
        // index için aksiyon oluşturulması
        public IActionResult Index()
        {
            var model = _boardService.ListBoard();

            return View(model);
        }

        // create için aksiyon get isteği
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        // board için http post ile aksiyon 
        [HttpPost]
        public IActionResult Create(NewBoard viewModel)
        {
            _boardService.AddBoard(viewModel);
            
            return RedirectToAction(nameof(Index));
        }
  
    }
}