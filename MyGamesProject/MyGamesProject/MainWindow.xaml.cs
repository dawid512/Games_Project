using MyGamesProject.Flappy_Bird;
using MyGamesProject.Space_Invaders;
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

namespace MyGamesProject
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window 
    {
       public string username { get; set; }

        public MainWindow(string user)
        {
            InitializeComponent();
            this.username = user;
            DataContext = username;
        }

        
        
        

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            

            Window1 window1 = new Window1(username);
            window1.Show();
            this.Close();
        }

       

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Window2 window2 = new Window2();
            window2.Show();
            this.Close();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Board_Score_Flappy board_Score_ = new Board_Score_Flappy();
            board_Score_.Show();
            this.Close();
        }
    }
}
