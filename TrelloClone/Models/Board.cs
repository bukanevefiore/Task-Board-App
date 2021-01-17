using System.Collections.Generic;

namespace TrelloClone.Models
{
  public class Board
    {
        // board kolonlarının tanımlanması
        public int Id { get; set; }
        public string Title { get; set; }
        // kolonları liste aktarma 
        public List<Column> Columns { get; set; } = new List<Column>();
    }
}
