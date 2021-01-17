using System.ComponentModel.DataAnnotations;

namespace TrelloClone.ViewModels
{
    public class AddCard
    {
        public int Id { get; set; }

        [Required]
        public string Contents { get; set; }
        public string Notes { get; set; }
        public string Date { get; set; }
        public string TahminiSure { get; set; }
        public string GerceklesenSure { get; set; }

    }
}
