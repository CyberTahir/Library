using System.Collections.Generic;

namespace Library
{
    internal class Book
    {
        public string Name { get; set; }
        public int Price { get; set; }
        public List<string> AuthorsList { get; set; }
        public string Authors => string.Join(", ", AuthorsList);

        public Book(string name, int price, List<string> authorsList = null)
        {
            Name = name;
            Price = price;
            AuthorsList = authorsList;
        }

        public Book()
        {
            Name = "";
            Price = 0;
            AuthorsList = null;
        }
    }
}
