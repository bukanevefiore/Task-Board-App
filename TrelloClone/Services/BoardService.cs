using Microsoft.EntityFrameworkCore;
using System.Linq;
using TrelloClone.Data;
using TrelloClone.ViewModels;

namespace TrelloClone.Services
{
    public class BoardService
    {
        // salt okunur işlemi
        private readonly TrelloCloneDbContext _dbContext;

        public BoardService(TrelloCloneDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // boardlist sınıflı methodumuz
        public BoardList ListBoard()
        {
            var model = new BoardList();

            foreach (var board in _dbContext.Boards)
            {
                model.Boards.Add(new BoardList.Board
                {
                    Id = board.Id,
                    Title = board.Title
                });
            }

            return model;
        }

        public BoardView GetBoard(int id)
        {
            var model = new BoardView();

            var board = _dbContext.Boards
                .Include(b => b.Columns)
                .ThenInclude(c => c.Cards)
                .SingleOrDefault(x => x.Id == id);

            if (board == null) 
                return model;
            model.Id = board.Id;
            model.Title = board.Title;

            foreach (var column in board.Columns)
            {
                var modelColumn = new BoardView.Column
                {
                    Title = column.Title,
                    Id = column.Id
                };

                foreach (var card in column.Cards)
                {
                    var modelCard = new BoardView.Card
                    {
                        Id = card.Id,
                        Content = card.Contents
                    };

                    modelColumn.Cards.Add(modelCard);
                }

                model.Columns.Add(modelColumn);
            }

            return model;
        }

        public void AddCard(AddCard viewModel)
        {
            var board = _dbContext.Boards
                .Include(b => b.Columns)
                .SingleOrDefault(x => x.Id == viewModel.Id);

            if (board != null)
            {
                var firstColumn = board.Columns.FirstOrDefault();
                var secondColumn = board.Columns.FirstOrDefault();
                var thirdColumn = board.Columns.FirstOrDefault();
                var fourthColumn = board.Columns.FirstOrDefault();
                var fifthColumn = board.Columns.FirstOrDefault();
               
                // konların boş olma durumlarının kontrol edilmesi
                if (firstColumn == null || secondColumn == null || thirdColumn == null || fourthColumn == null || fifthColumn == null)
                {
                    // column başlıkları
                    firstColumn = new Models.Column { Title = "Todo"};
                    secondColumn = new Models.Column { Title = "In Progress" };
                    thirdColumn = new Models.Column { Title = "Revison" };
                    fourthColumn = new Models.Column { Title = "Check" };
                    fifthColumn = new Models.Column { Title = "Done" };
                    // başlıkları listeye ekleme
                    board.Columns.Add(firstColumn);
                    board.Columns.Add(secondColumn);
                    board.Columns.Add(thirdColumn);
                    board.Columns.Add(fourthColumn);
                    board.Columns.Add(fifthColumn);
                }
                // yeni kartımızı 1. columna ekleme
                firstColumn.Cards.Add(new Models.Card
                {
                    Contents = viewModel.Contents
                });
            }

            _dbContext.SaveChanges();
        }
        // dbcontext ile yeni tahta ekleme
        public void AddBoard(NewBoard viewModel)
        {
            _dbContext.Boards.Add(new Models.Board
            {
                Title = viewModel.Title
            });

            _dbContext.SaveChanges();
        }

        public void Move(MoveCardCommand command)
        {
            var card = _dbContext.Cards.SingleOrDefault(x => x.Id == command.CardId);
            card.ColumnId = command.ColumnId;
            _dbContext.SaveChanges();
        }
    }
}
