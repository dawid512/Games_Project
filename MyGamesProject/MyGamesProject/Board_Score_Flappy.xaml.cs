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

namespace MyGamesProject
{
    /// <summary>
    /// Logika interakcji dla klasy Board_Score_Flappy.xaml
    /// </summary>
    public partial class Board_Score_Flappy : Window
    {
        public string username { get; set; }
        public Board_Score_Flappy(string username)
        {
            InitializeComponent();

            using (var ctx = new Context())
            {
                var List = ctx.Uzytkownik
                    .SqlQuery("Select * From Users")
                    .ToList<Users>();
                MyList.ItemsSource = List;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow(username);
            mainWindow.Show();
            this.Close();


        }
    }
}
