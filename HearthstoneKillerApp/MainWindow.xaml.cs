using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HearthstoneKillerApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Database data = new Database();
        public MainWindow()
        {
            InitializeComponent();
            cbOrder.ItemsSource = Enum.GetNames(typeof(Order));
        }
        private enum Order
        {
            ManaCost = 0, Name = 1, Class = 2, Type = 3, IDRarity = 4
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            data.GetCards("", "");
            data.GetKeywords("");
            LoadCardList();
            LoadKeywordList();
            LoadImage();
            this.Background = new ImageBrush(new BitmapImage(new Uri(System.IO.Path.GetFullPath(@"Images\background.jpg"))));
        }
        private void LoadCardList()
        {
            lbCards.Items.Clear();
            foreach (Card c in data.cards)
            {
                lbCards.Items.Add(c);
            }
        }
        private void LoadKeywordList()
        {
            tbKeywords.Document.Blocks.Clear();
            foreach (Keyword k in data.keywords)
            {
                tbKeywords.Document.Blocks.Add(new Paragraph(new Run(k.ToString())));
            }
        }
        private void LoadRarity()
        {
            tbRarity.Document.Blocks.Clear();
            foreach (Rarity r in data.raritys)
            {
                tbRarity.Document.Blocks.Add(new Paragraph(new Run(r.ToString())));
            }
        }

        private void LoadImage()
        {
            try
            {
                string image = (((Card)lbCards.SelectedItem).Name);
                //Load Image for selected Card
                imgCard.Source = new BitmapImage(new Uri(System.IO.Path.GetFullPath(@"Images\" + image + " image.png")));
            }
            catch (Exception)
            {
                imgCard.Source = new BitmapImage(new Uri(System.IO.Path.GetFullPath(@"Images\NoImage.png")));
            }
        }

        private void lbCards_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lbCards.SelectedIndex >= 0)
            {
                data.GetKeywords(((Card)lbCards.SelectedItem).Name);
                data.GetRarity(((Card)lbCards.SelectedItem).Name);
                LoadKeywordList();
                LoadRarity();
                LoadImage();
            }
            else
            {
                data.GetKeywords("");
                LoadKeywordList();
                LoadImage();
            }
        }

        private void tbSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (tbSearch.Text.Contains("") && tbSearch.Text.IndexOf("") == 0 )
            {
                data.GetCards(tbSearch.Text, cbOrder.SelectedItem.ToString());

                LoadCardList();
                LoadKeywordList();
                LoadRarity();
                LoadImage();

                if (lbCards.Items.Count > 0)
                {
                    lbCards.SelectedIndex = 0;
                }
            }
        }

        private void cbOrder_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            data.GetCards(tbSearch.Text, cbOrder.SelectedItem.ToString());
            data.GetKeywords("");
            LoadCardList();
            LoadKeywordList();
            LoadRarity();
            LoadImage();
        }

        private void btnAddCard_Click(object sender, RoutedEventArgs e)
        {
            AddCardWindow w = new AddCardWindow();
            w.ShowDialog();
            data.GetCards("", "");
            data.GetRarity("");
            data.GetKeywords("");
            LoadCardList();
            LoadKeywordList();
            LoadRarity();
            LoadImage();
        }
    }
}
