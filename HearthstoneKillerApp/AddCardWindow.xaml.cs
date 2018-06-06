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
using System.Windows.Shapes;

namespace HearthstoneKillerApp
{
    /// <summary>
    /// Interaction logic for AddCardWindow.xaml
    /// </summary>
    public partial class AddCardWindow : Window
    {
        Database data = new Database();
        private enum Type
        {
            Minion = 0, Weapon = 1, Spell = 2
        }
        private enum Rarity
        {
            Common = 0, Uncommon = 1, Rare = 2, Epic = 3, Legendary = 4
        }
        private enum Class
        {
            Neutral = 0, Druid = 1,Hunter=2,Mage=3,Paladin=4,Priest=5,Rogue=6,Shaman=7,Warlock=8,Warrior=9
        }
        private enum Race
        {
            Beast = 0, Pirate = 1, Demon = 2, Murloc = 3
        }
        public AddCardWindow()
        {
            InitializeComponent();
            for(int i = 0; i < 12;i++)
            {
                cbManacost.Items.Add(i);
            }
            cbRarity.ItemsSource = Enum.GetNames(typeof(Rarity));
            cbType.ItemsSource = Enum.GetNames(typeof(Type));
            cbClass.ItemsSource = Enum.GetNames(typeof(Class));
            cbRace.ItemsSource = Enum.GetNames(typeof(Race));
        }

        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                data.AddCard(cbRarity.Text, cbType.Text, tbName.Text, (int)cbManacost.SelectedItem, cbClass.Text, cbRace.Text);
                this.Close();
            }
            catch
            {
                MessageBox.Show("Er is iets niet correct ingevuld");
            }
        }

        private void cbType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbType.SelectedItem.ToString() == "Minion")
            {
                cbRace.IsEnabled = true;
            }
            else
            {
                cbRace.IsEnabled = false;
            }
        }

        private void cbRace_Loaded(object sender, RoutedEventArgs e)
        {
            cbRace.IsEnabled = false;
        }

        private void AddCardWindow_Loaded(object sender, RoutedEventArgs e)
        {
            this.Background = new ImageBrush(new BitmapImage(new Uri(System.IO.Path.GetFullPath(@"Images\background.jpg"))));
        }
    }
}
