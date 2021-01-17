using System.ComponentModel.DataAnnotations;

namespace TrelloClone.Models
{
  public class Card
    {
        // teknik kart da istenen bilgi kolonları tanımlanması
        public int Id { get; set; }
        [Required]
        public string Contents { get; set; }
        public string Notes { get; set; }
        public int ColumnId { get; set; }
       
        public string Date { get; set; }
        public string TahminiSure { get; set; }
        public string GerceklesenSure { get; set; }

        public Column Column { get; set; }  

    }
}
