using System.Collections.Generic;

namespace TrelloClone.Models
{
  public class Column
    {

        // coloumn tablo kolonları tanımlanması
        public int Id { get; set; }
        public string Title { get; set; }
        // card daki bilgileri listeye atma 
        public List<Card> Cards { get; set; } = new List<Card>();
        public int BoardId { get; set; }
    }
}
