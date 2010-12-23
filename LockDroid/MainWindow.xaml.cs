using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using LockDroid.Ships;

namespace LockDroid
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<string> alpha = new List<string> { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J" };
        private bool gameStarted = false;
        private List<Position> occupiedPos = new List<Position>();
        
        public MainWindow()
        {
            InitializeComponent();
            BuildBoard();
            var ac = new AircraftCarrier();
            occupiedPos.AddRange(ac.updateLocation(new Position { xCord = "A", yCord = 1 }, false));
        }

        public void BuildBoard()
        {
            var leftSide = BuildGrid("leftside");
            var rightSide = BuildGrid("rightside");

            Grid board = new Grid();
            ColumnDefinition colDef = new ColumnDefinition();
            colDef.Width = new GridLength(450);
            ColumnDefinition colDef2 = new ColumnDefinition();
            colDef2.Width = new GridLength(450);
            board.ColumnDefinitions.Add(colDef);
            board.ColumnDefinitions.Add(colDef2);

            Grid.SetColumn(leftSide, 0);
            Grid.SetColumn(rightSide, 1);
            board.Children.Add(leftSide);
            board.Children.Add(rightSide);

            RootWindow.Children.Add(board);
        }

        public Grid BuildGrid(string whichBoard)
        {
            Grid myGrid = new Grid();
            //Row
            for (int x = 0; x < 11; x++)
            {
                RowDefinition rowDef = new RowDefinition();
                rowDef.Height = new GridLength(35);
                myGrid.RowDefinitions.Add(rowDef);
                //Column
                for (int y = 0; y < 11; y++)
                {
                    ColumnDefinition colDef = new ColumnDefinition();
                    colDef.Width = new GridLength(35);
                    myGrid.ColumnDefinitions.Add(colDef);
                }
            }

            for (int x = 1; x < 11; x++)
            {
                Label newLabel = new Label();
                newLabel.Content = x.ToString();
                Grid.SetColumn(newLabel, 0);
                Grid.SetRow(newLabel, x);
                newLabel.VerticalContentAlignment = System.Windows.VerticalAlignment.Center;
                newLabel.HorizontalContentAlignment = System.Windows.HorizontalAlignment.Center;
                newLabel.BorderThickness = new Thickness(5);
                myGrid.Children.Add(newLabel);
            }

            //Column
            for (int y = 1; y < 11; y++)
            {
                Label newLabel = new Label();
                newLabel.Content = alpha[y-1];
                Grid.SetColumn(newLabel, y);
                Grid.SetRow(newLabel, 0);
                newLabel.VerticalContentAlignment = System.Windows.VerticalAlignment.Center;
                newLabel.HorizontalContentAlignment = System.Windows.HorizontalAlignment.Center;
                newLabel.BorderThickness = new Thickness(5);
                myGrid.Children.Add(newLabel);
            }

            for (int x = 1; x < 11; x++)
            {
                for (int y = 1; y < 11; y++)
                {
                    Button square = new Button();
                    square.Background = Brushes.LightGray;
                    square.Name = "pos_" + x + "_" + y;
                    square.BorderThickness = new Thickness(1);
                    square.BorderBrush = Brushes.Black;
                    if(whichBoard.Equals("rightside"))
                        square.Click +=new RoutedEventHandler(square_Click);
                    else
                        square.IsEnabled = false;
                    
                    Grid.SetColumn(square, x);
                    Grid.SetRow(square, y);
                    myGrid.Children.Add(square);
                }
            }
            return myGrid;
        }

        public void square_Click(object sender, RoutedEventArgs args)
        {
            if (!gameStarted)
                return;

            var btn = (Button)sender;
            var pos = ((string)btn.Name).Split('_');
            
            bool isTrue = Boards.checkForHit(new Position
            {
                xCord = alpha[int.Parse(pos[1]) - 1],
                yCord = int.Parse(pos[2])
            }, occupiedPos);
            
            if (isTrue)
            {
               btn.Background = Brushes.Red;
            }
            else
            {
               btn.Background = Brushes.WhiteSmoke;
            }
            btn.Click -= square_Click;
        }

        private void StartGame_Click(object sender, RoutedEventArgs e)
        {
            //Will need to do some checks to see if all ships are placed
            StartGame.IsEnabled = false;
            StartGame.Content = "Game Started!";
            gameStarted = true;
        }

        private void EndGame_Click(object sender, RoutedEventArgs e)
        {
            gameStarted = false;
            StartGame.IsEnabled = true;
            StartGame.Content = "Start Game";
            occupiedPos = new List<Position>();
        }

    }
}
