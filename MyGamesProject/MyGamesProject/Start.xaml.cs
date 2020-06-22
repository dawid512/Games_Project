using System;
using System.Collections.Generic;
using System.Data.Entity;
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
    /// Logika interakcji dla klasy Start.xaml
    /// </summary>
    public partial class Start : Window
    {
        public Start()
        {
            InitializeComponent();

            using (var database = new Context())
            {
                if (!database.Database.Exists())
                {
                    Database.SetInitializer(new CreateDatabaseIfNotExists<Context>());
                    var context1 = new Context();
                    context1.Database.Create();
                }
            }


        }

        

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var username = Username.Text;


            MainWindow mainWindow = new MainWindow(username);
            Flappy_Bird.Window1 window1 = new Flappy_Bird.Window1(username);
            mainWindow.Show();
            this.Close();

           
        }
    }
}
