using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Text.Json;
using System.IO;

namespace Library
{
    public partial class MainWindow : Window
    {
        private List<Book> books = new List<Book>();

        public MainWindow()
        {
            InitializeComponent();

            string json = File.ReadAllText("Library.json");
            books = JsonSerializer.Deserialize<List<Book>>(json);
            BooksList.ItemsSource = books;
            BooksList.Items.Refresh();

            Closing += (s, e) =>
            {
                object data =
                    from b in books
                    select new
                    {
                        b.Name,
                        b.Price,
                        b.AuthorsList
                    };

                string output = JsonSerializer.Serialize(data);

                File.WriteAllText("Library.json", output);
            };
        }

        private List<Book> GetBooksByAuthor(string authorName) {
            return books.Where(book => book.Authors.Contains(authorName)).ToList();
        }

        private int GetSummaryCostByAuthor(string authorName) {
            return GetBooksByAuthor(authorName).Aggregate(0, (sum, book) => sum + book.Price);
        }

        private void Insert(string n, int p, string[] Names, int[] Prices, int i)
        {
            string name = n;
            int price = p;

            string tempName;
            int tempPrice;

            while (i < 5)
            {
                tempName = Names[i];
                tempPrice = Prices[i];

                Names[i] = name;
                Prices[i] = price;

                name = tempName;
                price = tempPrice;
                ++i;
            }
        }

        private void Refresh(List<Book> bookList)
        {
            BooksList.ItemsSource = bookList;
            BooksList.Items.Refresh();
        }

        private void FindAuthor(object sender, RoutedEventArgs e)
        {
            string name = SearchAuthor.Text;
            Refresh(GetBooksByAuthor(name));
        }

        private void ShowList(object sender, RoutedEventArgs e)
        {
            Refresh(books);
        }

        private void GetTopFive(object sender, RoutedEventArgs e)
        { }
    }
}
