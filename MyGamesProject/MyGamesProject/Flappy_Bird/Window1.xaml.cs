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

using System.Windows.Threading; //  timer

namespace MyGamesProject.Flappy_Bird
{
    /// <summary>
    /// Logika interakcji dla klasy Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        DispatcherTimer gameTimer = new DispatcherTimer();

        int gravity = 8;

        double score;

        Rect FlappyRect;

        public string username { get; set; }

        bool gameover = false;
        public Window1(string user)
        {
            InitializeComponent();

            this.username = user;
            DataContext = username;
            //domyślne ustawienia timera
            gameTimer.Tick += gameEngine;

            gameTimer.Interval = TimeSpan.FromMilliseconds(20);
            startGame();
        }

        

        private void Canvas_KeyisDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                // przesuniecie ptaka o -20 
                flappyBird.RenderTransform = new RotateTransform(-20, flappyBird.Width / 2, flappyBird.Height / 2);
                
                gravity = -8;
            }
            if (e.Key == Key.R && gameover == true)
            {
                //jeśli jest koniec gry, klawisz r restartuje 
                startGame();

            }
            if(e.Key == Key.Escape && gameover == true)
            {
                // pod koniec gry escape powrót do menu 
                MainWindow mainWindow = new MainWindow(username);
                mainWindow.Show();
                this.Close();
            }

        }

        private void Canvas_KeyisUp(object sender, KeyEventArgs e)
        {
            flappyBird.RenderTransform = new RotateTransform(5, flappyBird.Width / 2, flappyBird.Height / 2);
            
            gravity = 6;
        }

        private void startGame()
        {
            //ta startowa funkcja ładuje ustawienia domyślne gry 

            int temp = 200;

            score = 0;

            Canvas.SetTop(flappyBird, 100);

            gameover = false;
            foreach (var x in MyCanvas.Children.OfType<Image>())
            {
                // ustawienie ob1 na pozycje domyślną 
                if (x is Image && (string)x.Tag == "obs1")
                {
                    Canvas.SetLeft(x, 500);
                }
                // ustawienie obs2 na pozycje domyślną
                if (x is Image && (string)x.Tag == "obs2")
                {
                    Canvas.SetLeft(x, 800);
                }
                // ustawienie obs3 na pozycje domyślną
                if (x is Image && (string)x.Tag == "obs3")
                {
                    Canvas.SetLeft(x, 1000);
                }
                // ustawienie chmur na pozycje domyślną
                if (x is Image && (string)x.Tag == "clouds")
                {
                    Canvas.SetLeft(x, (300 + temp));
                    temp = 800;
                }

            }
            // start  timera gry 
            gameTimer.Start();
        }

        private void gameEngine(object sender, EventArgs e)
        {
           

           
            scoreText.Content = "Score: " + score;

            // przypisanie obrazka do flappy rect class
            FlappyRect = new Rect(Canvas.GetLeft(flappyBird), Canvas.GetTop(flappyBird), flappyBird.Width , flappyBird.Height );

           
            //ustawienie gravity do obrazka bird w canvas
            Canvas.SetTop(flappyBird, Canvas.GetTop(flappyBird) + gravity);

            // sprawdzenie aby bird nie wyszedł poza grę 
            if (Canvas.GetTop(flappyBird) + flappyBird.Height > 490 || Canvas.GetTop(flappyBird) < 10 )
            {
               
                gameTimer.Stop();
                gameover = true;
                MessageBox.Show("Twój wynik to " + score + Environment.NewLine+ " Wciśnij R aby zagrać ponownie");

                using (var ctx = new Context())
                {
                    var userInfo = new Users();
                    userInfo.Username = username;
                    userInfo.Score = score;

                    ctx.Uzytkownik.Add(userInfo);
                    ctx.SaveChanges();
                }



                // scoreText.Content += "  Wciśnij R aby zagrać ponownie";

            }
            


            foreach (var x in MyCanvas.Children.OfType<Image>())
            {
                if ((string)x.Tag == "obs1" || (string)x.Tag == "obs2" || (string)x.Tag == "obs3")
                {

                   
                    // poruszenie obrazu w lewo 

                    Canvas.SetLeft(x, Canvas.GetLeft(x) - 5);

                    
                    Rect pillars = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x), x.Width, x.Height);

                    // gdy bird styka sie z słupkami 
                    if (FlappyRect.IntersectsWith(pillars))
                    {
                        
                        gameTimer.Stop();
                        gameover = true;
                        MessageBox.Show("Twój wynik to " + score+ Environment.NewLine+ " Wciśnij R aby zagrać ponownie");
                        // scoreText.Content += "   Wciśnij R aby zagrać ponownie";

                        using (var ctx = new Context())
                        {
                            var userInfo = new Users();
                            userInfo.Username = username;
                            userInfo.Score = score;

                            ctx.Uzytkownik.Add(userInfo);
                            ctx.SaveChanges();
                        }

                    }

                }

            
                // po upływie planszy(100 pixeli) resetuje słupki aby gra trwała dalej 
                if ((string)x.Tag == "obs1" && Canvas.GetLeft(x) < -100)
                {
                   
                    //reset słupka do 800 pixeli
                    Canvas.SetLeft(x, 800);
                    // Dodaj 1 do score
                    score = score +0.5;

                }
                // po upływie planszy(200 pixeli) resetuje słupki aby gra trwała dalej 
                
                if ((string)x.Tag == "obs2" && Canvas.GetLeft(x) < -200)
                {
                    //reset słupka do 800 pixeli
                    Canvas.SetLeft(x, 800);
                    // Dodaj 1 do score
                    score = score +0.5 ;
                }
                // po upływie planszy(250 pixeli) resetuje słupki aby gra trwała dalej 
                
                if ((string)x.Tag == "obs3" && Canvas.GetLeft(x) < -250)
                {
                    //reset słupka do 800 pixeli
                    Canvas.SetLeft(x, 800);
                    // Dodaj 1 do score
                    score = score +0.5;

                }

                // ustawienie chmur
                if ((string)x.Tag == "clouds")
                {
                    // ruch chmur 
                    Canvas.SetLeft(x, Canvas.GetLeft(x) - 0.6);


                    //po upływie 220 pixeli resetuje 
                    if (Canvas.GetLeft(x) < -220)
                    {
                        //reset do 550px 
                        Canvas.SetLeft(x, 550);
                    }
                }

                
            }

            
        }

    }
}
