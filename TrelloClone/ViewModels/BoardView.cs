using System.Collections.Generic;

namespace TrelloClone.ViewModels
{
    public class BoardView
    {
        // propertyler
        public int Id { get; set; }
        public string Title { get; set; }
        // column için list
        public List<Column> Columns { get; set; } = new List<Column>();

        public class Column
        {
            public int Id { get; set; }
            public string Title { get; set; }
            // card için list
            public List<Card> Cards { get; set; } = new List<Card>();
        }
        // karddaki content kısmının alınması
        public class Card
        {
            public int Id { get; set; }
            public string Content { get; set; }
        }
    }
}
