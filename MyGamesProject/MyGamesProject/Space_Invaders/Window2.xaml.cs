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

namespace MyGamesProject.Space_Invaders
{
    /// <summary>
    /// Logika interakcji dla klasy Window2.xaml
    /// </summary>
    public partial class Window2 : Window
    {
        
        bool goLeft, goRight ,goUp, goDown = false;
      
        List<Rectangle> itemstoremove = new List<Rectangle>();
        
        int enemyImages = 0;
        //  bullet timer wroga
        int bulletTimer;
        // czestotliwość pocisku wroga 
        int bulletTimerLimit = 200;
        
        int totalEnemeis;
       
        DispatcherTimer dispatcherTimer = new DispatcherTimer();
       
        ImageBrush playerSkin = new ImageBrush();
        // predkosc wroga początkowa
        int enemySpeed = 6;

        bool gameover = false;
        public string username { get; set; }

        public Window2(string user)
        {
            InitializeComponent();
          
            dispatcherTimer.Tick += gameEngine;
            //  timer działa co każde  20 milliseconds
            dispatcherTimer.Interval = TimeSpan.FromMilliseconds(20);

            startgame();

            this.username = user;
            DataContext = username;

        }

        private void startgame()
        {
            // start  timer
            dispatcherTimer.Start();

            playerSkin.ImageSource = new BitmapImage(new Uri(@"\\Mac\Home\Documents\Semestr 4\Lab\Programowanie 4\MyGamesProject\MyGamesProject\Space_Invaders\images\player.png"));

            player1.Fill = playerSkin;
            //ilość przeciwników
            makeEnemies(30);

            
            
        }

        private void Canvas_KeyisDown(object sender, KeyEventArgs e)
        {
            
            if (e.Key == Key.Left)
            {
                goLeft = true;
            }
            if (e.Key == Key.Right)
            {
                goRight = true;
            }
            if (e.Key == Key.Up)
            {
                goUp = true;
            }
            if (e.Key == Key.Down)
            {
                goDown = true;
            }
            if (e.Key == Key.Escape  )
            {
                // pod koniec gry escape powrót do menu 
                MainWindow mainWindow = new MainWindow(username);
                mainWindow.Show();
                this.Close();
            }


        }

        private void Canvas_KeyIsUp(object sender, KeyEventArgs e)
        {
            

            if (e.Key == Key.Left)
            {
                goLeft = false;
            }
            if (e.Key == Key.Right)
            {
                goRight = false;
            }
            if (e.Key == Key.Up)
            {
                goUp = false;
            }
            if (e.Key == Key.Down)
            {
                goDown = false;
            }

            if (e.Key == Key.R && gameover == true)
            {
                //jeśli jest koniec gry, klawisz r restartuje 
                Window2 window2 = new Window2(username);
                window2.Show();
                this.Close(); 
            }


            if (e.Key == Key.Space)
            {
                
                itemstoremove.Clear();

               
                Rectangle newBullet = new Rectangle
                {
                    Tag = "bullet",
                    Height = 20,
                    Width = 5,
                    Fill = Brushes.White,
                    Stroke = Brushes.Red
                };

                
                Canvas.SetTop(newBullet, Canvas.GetTop(player1) - newBullet.Height);
                Canvas.SetLeft(newBullet, Canvas.GetLeft(player1) + player1.Width / 2);
                // dodanie pocisku na ekran
                myCanvas.Children.Add(newBullet);

            }
        }

            private void enemyBulletMaker(double x, double y)
            {
               
                Rectangle newEnemyBullet = new Rectangle
                {
                    Tag = "enemyBullet",
                    Height = 40,
                    Width = 15,
                    Fill = Brushes.Yellow,
                    Stroke = Brushes.Black,
                    StrokeThickness = 5

                };

               
                Canvas.SetTop(newEnemyBullet, y);
               
                Canvas.SetLeft(newEnemyBullet, x);
                
                myCanvas.Children.Add(newEnemyBullet);
            }

            private void makeEnemies(int limit)
            {
            
                int left = 0;
          
                totalEnemeis = limit;

                for (int i = 0; i < limit; i++)
                {
                    
                    ImageBrush enemySkin = new ImageBrush();

                    
                    Rectangle newEnemy = new Rectangle
                    {
                        Tag = "enemy",
                        Height = 45,
                        Width = 45,
                        Fill = enemySkin,

                    };

                
                    Canvas.SetTop(newEnemy, 10); 
                    Canvas.SetLeft(newEnemy, left); 
                    myCanvas.Children.Add(newEnemy);
                  
                    left -= 60;

                    
                    enemyImages++;
                
                    if (enemyImages > 8)
                    {
                        enemyImages = 1;
                    }

                   
                    switch (enemyImages)
                    {
                        case 1:
                            enemySkin.ImageSource = new BitmapImage(new Uri(@"\\Mac\Home\Documents\Semestr 4\Lab\Programowanie 4\MyGamesProject\MyGamesProject\Space_Invaders\images\invader1.gif"));
                        break;
                        case 2:
                            enemySkin.ImageSource = new BitmapImage(new Uri(@"\\Mac\Home\Documents\Semestr 4\Lab\Programowanie 4\MyGamesProject\MyGamesProject\Space_Invaders\images\invader2.gif"));
                           
                            break;
                        case 3:
                            enemySkin.ImageSource = new BitmapImage(new Uri(@"\\\Mac\Home\Documents\Semestr 4\Lab\Programowanie 4\MyGamesProject\MyGamesProject\Space_Invaders\images\invader3.gif"));
                            
                            break;
                        case 4:
                            enemySkin.ImageSource = new BitmapImage(new Uri(@"\\Mac\Home\Documents\Semestr 4\Lab\Programowanie 4\MyGamesProject\MyGamesProject\Space_Invaders\images\invader4.gif"));
                           
                            break;
                        case 5:
                            enemySkin.ImageSource = new BitmapImage(new Uri(@"\\Mac\Home\Documents\Semestr 4\Lab\Programowanie 4\MyGamesProject\MyGamesProject\Space_Invaders\images\invader5.gif"));
                           
                            break;
                        case 6:
                            enemySkin.ImageSource = new BitmapImage(new Uri(@"\\Mac\Home\Documents\Semestr 4\Lab\Programowanie 4\MyGamesProject\MyGamesProject\Space_Invaders\images\invader6.gif"));
                            
                            break;
                        case 7:
                            enemySkin.ImageSource = new BitmapImage(new Uri(@"\\Mac\Home\Documents\Semestr 4\Lab\Programowanie 4\MyGamesProject\MyGamesProject\Space_Invaders\images\invader7.gif"));
                          
                            break;
                        case 8:
                            enemySkin.ImageSource = new BitmapImage(new Uri(@"\\Mac\Home\Documents\Semestr 4\Lab\Programowanie 4\MyGamesProject\MyGamesProject\Space_Invaders\images\invader8.gif"));
                          
                            break;
                    }
                }
            }

            private void gameEngine(object sender, EventArgs e)
            {
                

               
                Rect player = new Rect(Canvas.GetLeft(player1), Canvas.GetTop(player1), player1.Width, player1.Height);
              
                enemiesLeft.Content = "Invaders Left: " + totalEnemeis;

              
                if (goLeft && Canvas.GetLeft(player1) > 0)
                {
                    Canvas.SetLeft(player1, Canvas.GetLeft(player1) - 10);
                }
              
                else if (goRight && Canvas.GetLeft(player1) + 80 < Application.Current.MainWindow.Width)
                {
                    Canvas.SetLeft(player1, Canvas.GetLeft(player1) + 10);
                }

            else if (goUp && Canvas.GetTop(player1) + 80 < Application.Current.MainWindow.Height)
            {
                Canvas.SetTop(player1, Canvas.GetTop(player1) - 10);
            }
            else if (goDown && Canvas.GetTop(player1) + 80 < Application.Current.MainWindow.Height)
            {
                Canvas.SetTop(player1, Canvas.GetTop(player1) + 10);
            }



            bulletTimer -= 3;

                
                if (bulletTimer < 0)
                {
                    
                    enemyBulletMaker((Canvas.GetLeft(player1) + 20), 10);
                    
                    bulletTimer = bulletTimerLimit;
                }

              
                if (totalEnemeis < 10)
                {
                    enemySpeed = 20;
                }

               
                foreach (var x in myCanvas.Children.OfType<Rectangle>())
                {
                  
                    if (x is Rectangle && (string)x.Tag == "bullet")
                    {
                       
                        Canvas.SetTop(x, Canvas.GetTop(x) - 20);

                     
                        Rect bullet = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x), x.Width, x.Height);

                        //sprawdzenie czy pocisk opóścił ekran gry
                        if (Canvas.GetTop(x) < 10)
                        {
                          
                            itemstoremove.Add(x);
                        }

                     
                        foreach (var y in myCanvas.Children.OfType<Rectangle>())
                        {
                           
                            if (y is Rectangle && (string)y.Tag == "enemy")
                            {
                                
                                Rect enemy = new Rect(Canvas.GetLeft(y), Canvas.GetTop(y), y.Width, y.Height);


                                //sprawdzenie czy pocisk styka sie z wrogiem 
                                if (bullet.IntersectsWith(enemy))
                                {
                                    //usuwanie i zmniejszenie liczby wroga
                                    itemstoremove.Add(x);
                                    itemstoremove.Add(y);
                                    totalEnemeis -= 1;
                                }
                            }


                        }

                    }

                 
                    if (x is Rectangle && (string)x.Tag == "enemy")
                    {
                        
                        Canvas.SetLeft(x, Canvas.GetLeft(x) + enemySpeed);

                       
                        if (Canvas.GetLeft(x) > 820)
                        {
                      
                            Canvas.SetLeft(x, -80);
                            
                            Canvas.SetTop(x, Canvas.GetTop(x) + (x.Height + 10));
                        }

                        
                        Rect enemy = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x), x.Width, x.Height);

                        // sprawdzenie czy gracz styka sie z wrogiem
                        if (player.IntersectsWith(enemy))
                        {
                            //jeśli dotkniesz wroga
                            dispatcherTimer.Stop();
                        gameover = true;
                        MessageBox.Show("Przegrałeś");
                        }
                    }


                    
                    if (x is Rectangle && (string)x.Tag == "enemyBullet")
                    {
                        
                        Canvas.SetTop(x, Canvas.GetTop(x) + 10);

                       
                        if (Canvas.GetTop(x) > 480)
                        {
                            itemstoremove.Add(x);

                        }

                      
                        Rect enemyBullets = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x), x.Width, x.Height);

                        //sprawdzenie czy pocisk dotknie gracza

                        if (enemyBullets.IntersectsWith(player))

                        {

                            //jeśli zginiesz 
                            dispatcherTimer.Stop();
                        gameover = true;
                        MessageBox.Show("Przegrałeś");

                        }

                    }
                }

                
                foreach (Rectangle y in itemstoremove)
                {
                    
                    myCanvas.Children.Remove(y);
                }

                // gdy liczba wrogów 0
                if (totalEnemeis == 0)
                {
                    // stop  timer i pokaż wiadomość
                    dispatcherTimer.Stop();
                gameover = true;
                MessageBox.Show("Wygrałeś");
                }
            }

        }
    }

