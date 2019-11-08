using KringeEnKruise.Properties;
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
//using Microsoft.VisualBasic.Interaction;

namespace KringeEnKruise
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<List<Button>> Buttons;
        List<Player> Players;

        int score1;
        int score2;

        bool GameOver;

        public MainWindow()
        {
            InitializeComponent();
            GameOver = false;
            score1 = 0;
            score2 = 0;
            AssignButtons();           
            NewGame();
        }
    
        protected void NewGame()
        {
            //Clear Matrix
            foreach (List<Button> lstBTN in Buttons)
            {
                foreach (Button btn in lstBTN)
                {
                    btn.Content = "";
                }

            }
            //Format
            btnReset.Visibility = Visibility.Hidden;
            Players = new List<Player>();
            Players.Add(new Player("Player 1", "X", score1));
            Players.Add(new Player("Player 2", "O", score2));
            SelectedStarter();
            UpdateScoreBoard();
            UpdateNotifier();
            GameOver = false;
            //fun
            //#FFFFFFFF
            var bc = new BrushConverter();
            grid.Background = (Brush)bc.ConvertFrom("#FFFFFFFF");
        }

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            NewGame();
        }

        private void AssignButtons()
        {
            /*Playing Field*/
            //Matrix
            Buttons = new List<List<Button>>();
            //Rows
            Buttons.Add(new List<Button>());
            Buttons.Add(new List<Button>());
            Buttons.Add(new List<Button>());
            //Items
            //A - Row
            Buttons[0].Add(btnPosA1);
            Buttons[0].Add(btnPosA2);
            Buttons[0].Add(btnPosA3);
            //B - Row
            Buttons[1].Add(btnPosB1);
            Buttons[1].Add(btnPosB2);
            Buttons[1].Add(btnPosB3);
            //C - Row
            Buttons[2].Add(btnPosC1);
            Buttons[2].Add(btnPosC2);
            Buttons[2].Add(btnPosC3);
        }

        protected void UpdateScoreBoard()
        {
            lblScoreP1.Content = Players[0].ToString();
            lblScoreP2.Content = Players[1].ToString();
        }

        protected void SelectedStarter()
        {
            Random rand = new Random();
            int n = rand.Next(1, 3);

            if (n == 2)
            {
                Players[1].Started = true;
                Players[0].Started = false;
            }
            else
            {
                Players[1].Started = false;
                Players[0].Started = true;
            }

            foreach (Player playr in Players)
            {
                playr.isTurn = playr.Started;
            }
        }

        protected void NextTurn()
        {
            foreach (Player playr in Players)
            {
                playr.isTurn = !playr.isTurn;
            }

            UpdateNotifier();
        }

        private void btnPosXY_Click(object sender, RoutedEventArgs e)
        {
            UpdateScoreBoard();
            Button btn = (Button)sender;

            if (ButtonUsed(btn) || GameOver)
            {
                return;
            }
            foreach (Player p in Players)
            {
                if (p.isTurn)
                {
                    btn.Content = p.Symbol;
                }
            }
            CheckGameForWinner();
            if (!GameOver)
            {
                NextTurn();
            }
        }

        protected bool ButtonUsed(Button btn)
        {
            if ((string)btn.Content == "")
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        protected void UpdateNotifier()
        {
            foreach (Player p in Players)
            {
                if (p.isTurn)
                {
                    lblTurnNotify.Content = p.Name + "'s turn...";
                }

            }

        }

        protected void CheckGameForWinner()
        {
            string sym = "";
            //Check Winner
            //______ Top Row
            if (Buttons[0][0].Content.Equals(Buttons[0][1].Content) && Buttons[0][1].Content.Equals(Buttons[0][2].Content))
            {
                sym = Buttons[0][1].Content.ToString();
                if (sym != "")
                {
                    WinGame(StringToPlayer(sym));
                    return;
                }
            }
            //______ Top Row
            if (Buttons[1][0].Content.Equals(Buttons[1][1].Content) && Buttons[1][1].Content.Equals(Buttons[1][2].Content))
            {
                sym = Buttons[1][1].Content.ToString();
                if (sym != "")
                {
                    WinGame(StringToPlayer(sym));
                    return;
                }
            }
            //______ Top Row
            if (Buttons[2][0].Content.Equals(Buttons[2][1].Content) && Buttons[2][1].Content.Equals(Buttons[2][2].Content))
            {
                sym = Buttons[2][1].Content.ToString();
                if (sym != "")
                {
                    WinGame(StringToPlayer(sym));
                    return;
                }

            }
            //______ Left Column
            if (Buttons[0][0].Content.Equals(Buttons[1][0].Content) && Buttons[1][0].Content.Equals(Buttons[2][0].Content))
            {
                sym = Buttons[1][0].Content.ToString();
                if (sym != "")
                {
                    WinGame(StringToPlayer(sym));
                    return;
                }
            }
            //______ Middle Column
            if (Buttons[0][1].Content.Equals(Buttons[1][1].Content) && Buttons[1][1].Content.Equals(Buttons[2][1].Content))
            {
                sym = Buttons[1][1].Content.ToString();
                if (sym != "")
                {
                    WinGame(StringToPlayer(sym));
                    return;
                }
            }
            //______ Right Column
            if (Buttons[0][2].Content.Equals(Buttons[1][2].Content) && Buttons[1][2].Content.Equals(Buttons[2][2].Content))
            {
                sym = Buttons[1][2].Content.ToString();
                if (sym != "")
                {
                    WinGame(StringToPlayer(sym));
                    return;
                }
            }
            //______ Positive Diagonal
            if (Buttons[0][0].Content.Equals(Buttons[1][1].Content) && Buttons[1][1].Content.Equals(Buttons[2][2].Content))
            {
                sym = Buttons[1][1].Content.ToString();
                if (sym != "")
                {
                    WinGame(StringToPlayer(sym));
                    return;
                }
            }
            //______ Negative Diagonal
            if (Buttons[2][0].Content.Equals(Buttons[1][1].Content) && Buttons[1][1].Content.Equals(Buttons[0][2].Content))
            {
                sym = Buttons[1][1].Content.ToString();
                if (sym != "")
                {
                    WinGame(StringToPlayer(sym));
                    return;
                }
            }
            //=====================================================
            //Check Draw
            int x = 0;
            foreach (List<Button> lstBTN in Buttons)
            {
                foreach (Button b in lstBTN)
                {
                    if (ButtonUsed(b))
                    {
                        x++;
                    }
                }
            }
            if (x >= 9)
            {
                DrawGame();
                return;
            }
        }

        private void DrawGame()
        {
            lblTurnNotify.Content = "Game ended in a Draw...";
            btnReset.Visibility = Visibility.Visible;
            GameOver = true;
            score1 = Players[0].Score;
            score2 = Players[1].Score;
            UpdateScoreBoard();
            //fun
            //#FFEAFD95
            var bc = new BrushConverter();
            grid.Background = (Brush)bc.ConvertFrom("#FFEAFD95");
        }

        private void WinGame(Player player)
        {
            lblTurnNotify.Content = player.Name + " has won the round!";
            player.Score++;
            btnReset.Visibility = Visibility.Visible;
            GameOver = true;
            score1 = Players[0].Score;
            score2 = Players[1].Score;
            UpdateScoreBoard();
            //fun
            //#FF45E068
            var bc = new BrushConverter();
            grid.Background = (Brush)bc.ConvertFrom("#FF45E068");
        }

        private Player StringToPlayer(String Sym)
        {
            Player p = new Player();
            foreach (Player pl in Players)
            {
                if (pl.Symbol == Sym)
                {
                    p = pl;
                }
            }
            return p;
        }
    }
}
