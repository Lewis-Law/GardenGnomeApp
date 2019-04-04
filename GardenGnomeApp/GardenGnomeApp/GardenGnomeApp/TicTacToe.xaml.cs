using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GardenGnomeApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TicTacToe : ContentPage
    {
        string playerOneImageSource = "";
        string playerTwoImageSource = "";
        public TicTacToe(string p1, string p2, bool aiMode)
        {
            InitializeComponent();
            playerOneImageSource = p1;
            playerTwoImageSource = p2;
            playerOneLabelImage.Source = playerOneImageSource;
            playerTwoLabelImage.Source = playerTwoImageSource;
            //playAgainstButton.Text = "Play Against Computer";
            //easyButton.IsVisible = false;
            //moderateButton.IsVisible = false;
            //hardButton.IsVisible = false;
            playerOneScore.Text = string.Format("P1 Score: {0}",score1);
            playerTwoScore.Text = string.Format("P2 Score: {0}",score2);
            //moderateButton.IsEnabled = false;
            if (aiMode == true)
            {
                playerTwoLabel.Text = "Computer :";
                difficulty = SettingsPage.savedDifficulty;
                ResetClicked();
                ai = true;
            }
        }

        // Settings page button
        async void Clicked6(object sender, EventArgs e)
        {
            SettingsPage.inGame = true;
            await Navigation.PushAsync(new SettingsPage());
        }

        // Responsive Layout
        private double width = 0;
        private double height = 0;
     
        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);
            if (this.width != width || this.height != height)
            {
                this.width = width;
                this.height = height;
                //Reconfigure layout
                if (width > height)
                {
                    outerStack.Orientation = StackOrientation.Horizontal;
                    mainGrid.HeightRequest = height;
                    mainGrid.WidthRequest = height;
                }
                else
                {
                    outerStack.Orientation = StackOrientation.Vertical;
                    mainGrid.HeightRequest = width;
                    mainGrid.WidthRequest = width;
                }
            }
        }

        //The Game
        bool turn = true;
        bool winner = false;




        private async void OnTapGestureRecognizerTapped(object sender, EventArgs args)
        {
            Image I = (Image)sender;
            if (turn)
            {
                I.Source = playerOneImageSource;
                I.ClassId = "X";
            }
            else
            {
                I.Source = playerTwoImageSource;
                I.ClassId = "O";
            }
            turn = !turn;
            I.IsEnabled = false;
            CheckForWinner();
            if (winner == false && ai == true)
            {
                mainGrid.IsEnabled = false;
                await Task.Delay(1500);
                mainGrid.IsEnabled = true;
                ComputerMove();
            }
        }


        // Check for Win
        private void CheckForWinner()
        {
            //Horizontal checks
            if ((I00.ClassId == I10.ClassId) && (I10.ClassId == I20.ClassId) && (I00.IsEnabled == false))
            { winner = true;
            }
            if ((I01.ClassId == I11.ClassId) && (I11.ClassId == I21.ClassId) && (I01.IsEnabled == false))
            { winner = true;
            }
            if ((I02.ClassId == I12.ClassId) && (I12.ClassId == I22.ClassId) && (I02.IsEnabled == false))
            { winner = true;
            }

            //Veritical Checks                                                                                 
            if ((I00.ClassId == I01.ClassId) && (I01.ClassId == I02.ClassId) && (I00.IsEnabled == false))
            { winner = true;
            }
            if ((I10.ClassId == I11.ClassId) && (I11.ClassId == I12.ClassId) && (I10.IsEnabled == false))
            { winner = true;
            }
            if ((I20.ClassId == I21.ClassId) && (I21.ClassId == I22.ClassId) && (I20.IsEnabled == false))
            { winner = true;
            }

            //Diagonal Checks                                                                                   
            if ((I00.ClassId == I11.ClassId) && (I11.ClassId == I22.ClassId) && (I00.IsEnabled == false))
            { winner = true;
            }
            if ((I02.ClassId == I11.ClassId) && (I11.ClassId == I20.ClassId) && (I02.IsEnabled == false))
            { winner = true;
            }
            
            //Declaring winner
            if (winner == true)
            {
                string winnerName = "";
                if (turn)
                {
                    if (ai == true)
                    { winnerName = "Computer"; }
                    else
                    { winnerName = "Player 2"; }
                    score2 += 1;
                    playerTwoScore.Text = string.Format("P2 Score: {0}", score2);
                }
                else
                {
                    winnerName = "Player 1";
                    score1 += 1;
                    playerOneScore.Text = string.Format("P1 Score: {0}", score1);
                }

                async void AlertWin()
                {
                    var answer = await DisplayAlert("GameOver", winnerName + " wins!", "Play Again", "Quit");
                    System.Diagnostics.Debug.WriteLine("Answer: " + answer);
                    if (answer == false)
                    {
                        if (ai == true)
                        {
                            Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count - 2]);
                            Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count - 2]);
                            await Navigation.PopAsync();
                        }
                        else
                        {
                            Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count - 2]);
                            Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count - 2]);
                            await Navigation.PopAsync();
                        }

                    }
                    else
                    {
                        ResetClicked();
                    }
                }
                I00.IsEnabled = false;
                I10.IsEnabled = false;
                I20.IsEnabled = false;
                I01.IsEnabled = false;
                I11.IsEnabled = false;
                I21.IsEnabled = false;
                I02.IsEnabled = false;
                I12.IsEnabled = false;
                I22.IsEnabled = false;
                AlertWin();
                
            }

            //Check for Draw
            if (
            (I00.IsEnabled == false)&&
            (I10.IsEnabled == false)&&
            (I20.IsEnabled == false)&&
            (I01.IsEnabled == false)&&
            (I11.IsEnabled == false)&&
            (I21.IsEnabled == false)&&
            (I02.IsEnabled == false)&&
            (I12.IsEnabled == false)&&
            (I22.IsEnabled == false)&&
            (winner == false))
            {
                async void AlertDraw()
                {
                    var answer = await DisplayAlert("GameOver", "Draw!", "Play Again", "Quit");
                    System.Diagnostics.Debug.WriteLine("Answer: " + answer);
                    if (answer == false)
                    {
                        if (ai == true)
                        {
                            Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count - 2]);
                            Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count - 2]);
                            await Navigation.PopAsync();
                        }
                        else
                        {
                            Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count - 2]);
                            await Navigation.PopAsync();
                        }
                    }
                    else
                    {
                        ResetClicked();
                    }
                }
                I00.IsEnabled = false;
                I10.IsEnabled = false;
                I20.IsEnabled = false;
                I01.IsEnabled = false;
                I11.IsEnabled = false;
                I21.IsEnabled = false;
                I02.IsEnabled = false;
                I12.IsEnabled = false;
                I22.IsEnabled = false;
                AlertDraw();
            }
            System.Diagnostics.Debug.WriteLine("Checking for Winner: " + winner);
        }




        // Used for restting the game
        private void ResetClicked()
        {
            I00.ClassId = "";
            I10.ClassId = "";
            I20.ClassId = "";
            I01.ClassId = "";
            I11.ClassId = "";
            I21.ClassId = "";
            I02.ClassId = "";
            I12.ClassId = "";
            I22.ClassId = "";
            I00.Source = null;
            I10.Source = null;
            I20.Source = null;
            I01.Source = null;
            I11.Source = null;
            I21.Source = null;
            I02.Source = null;
            I12.Source = null;
            I22.Source = null;
            I00.IsEnabled = true;
            I10.IsEnabled = true;
            I20.IsEnabled = true;
            I01.IsEnabled = true;
            I11.IsEnabled = true;
            I21.IsEnabled = true;
            I02.IsEnabled = true;
            I12.IsEnabled = true;
            I22.IsEnabled = true;

            turn = true;
            winner = false;
            System.Diagnostics.Debug.WriteLine("Game Reset!");
        }

        // Play Against Computer
        bool ai;
        string difficulty = "Moderate";

        // easy AI
        private void EasyComputerMove()
        {
            Image move = null;
            
            if (
                (I00.IsEnabled == false) &&
                (I10.IsEnabled == false) &&
                (I20.IsEnabled == false) &&
                (I01.IsEnabled == false) &&
                (I11.IsEnabled == false) &&
                (I21.IsEnabled == false) &&
                (I02.IsEnabled == false) &&
                (I12.IsEnabled == false) &&
                (I22.IsEnabled == false))
            {

            }
            else
            {
                while (move == null)
                {
                    move = RandomMove();
                }
                move.ClassId = "O";
                move.Source = playerTwoImageSource;
                move.IsEnabled = false;
                CheckForWinner();
            }
        }

        // Easy computer uses random move (does not block or try to win)
        private Image RandomMove()
        {
            
            Random rnd = new Random();
            int anyMove = rnd.Next(1, 10);
            System.Diagnostics.Debug.WriteLine("computer looking random move"+anyMove);
            if ((I11.ClassId == "") && (anyMove == 1))
                return I11;
            if ((I10.ClassId == "") && (anyMove == 2))
                return I10;
            if ((I01.ClassId == "") && (anyMove == 3))
                return I01;
            if ((I21.ClassId == "") && (anyMove == 4))
                return I21;
            if ((I12.ClassId == "") && (anyMove == 5))
                return I12;
            if ((I00.ClassId == "") && (anyMove == 6))
                return I00;
            if ((I20.ClassId == "") && (anyMove == 7))
                return I20;
            if ((I02.ClassId == "") && (anyMove == 8))
                return I02;
            if ((I22.ClassId == "") && (anyMove == 9))
                return I22;

            return null;
        }

        // Morderate AI
        private void ModerateComputerMove()
        {
            //Priority 1:  get tick tac toe
            //Priority 2:  block x tic tac toe
            //Priority 3:  go for corner space
            //Priority 4:  pick open space

            Image move = null;

            //Look for tic tac toe opportunities
            move = LookForWinOrBlock("O"); //look for win
            if (move == null)
            {
                move = LookForWinOrBlock("X"); //look for block
                if (move == null)
                {
                    move = LookForCorner();
                    if (move == null)
                    {
                        move = LookForOpenSpace();
                    }
                }
            }
            if (
            (I00.IsEnabled == false) &&
            (I10.IsEnabled == false) &&
            (I20.IsEnabled == false) &&
            (I01.IsEnabled == false) &&
            (I11.IsEnabled == false) &&
            (I21.IsEnabled == false) &&
            (I02.IsEnabled == false) &&
            (I12.IsEnabled == false) &&
            (I22.IsEnabled == false))
            {

            }
            else
            {
                move.ClassId = "O";
                move.Source = playerTwoImageSource;
                move.IsEnabled = false;
                CheckForWinner();
            }
        }

        private Image LookForWinOrBlock(string mark)
        {
            System.Diagnostics.Debug.WriteLine("computer checking win or block");
            //HORIZONTAL TESTS
            if ((I00.ClassId == mark) && (I10.ClassId == mark) && (I20.ClassId == ""))
                return I20;
            if ((I10.ClassId == mark) && (I20.ClassId == mark) && (I00.ClassId == ""))
                return I00;
            if ((I00.ClassId == mark) && (I20.ClassId == mark) && (I10.ClassId == ""))
                return I10;

            if ((I01.ClassId == mark) && (I11.ClassId == mark) && (I21.ClassId == ""))
                return I21;
            if ((I11.ClassId == mark) && (I21.ClassId == mark) && (I01.ClassId == ""))
                return I01;
            if ((I01.ClassId == mark) && (I21.ClassId == mark) && (I11.ClassId == ""))
                return I11;

            if ((I02.ClassId == mark) && (I12.ClassId == mark) && (I22.ClassId == ""))
                return I22;
            if ((I12.ClassId == mark) && (I22.ClassId == mark) && (I02.ClassId == ""))
                return I02;
            if ((I02.ClassId == mark) && (I22.ClassId == mark) && (I12.ClassId == ""))
                return I12;

            //VERTICAL TESTS
            if ((I00.ClassId == mark) && (I01.ClassId == mark) && (I02.ClassId == ""))
                return I02;
            if ((I01.ClassId == mark) && (I02.ClassId == mark) && (I00.ClassId == ""))
                return I00;
            if ((I00.ClassId == mark) && (I02.ClassId == mark) && (I01.ClassId == ""))
                return I01;

            if ((I10.ClassId == mark) && (I11.ClassId == mark) && (I12.ClassId == ""))
                return I12;
            if ((I11.ClassId == mark) && (I12.ClassId == mark) && (I10.ClassId == ""))
                return I10;
            if ((I10.ClassId == mark) && (I12.ClassId == mark) && (I11.ClassId == ""))
                return I11;

            if ((I20.ClassId == mark) && (I21.ClassId == mark) && (I22.ClassId == ""))
                return I22;
            if ((I21.ClassId == mark) && (I22.ClassId == mark) && (I20.ClassId == ""))
                return I20;
            if ((I20.ClassId == mark) && (I22.ClassId == mark) && (I21.ClassId == ""))
                return I21;

            //DIAGONAL TESTS
            if ((I00.ClassId == mark) && (I11.ClassId == mark) && (I22.ClassId == ""))
                return I22;
            if ((I11.ClassId == mark) && (I22.ClassId == mark) && (I00.ClassId == ""))
                return I00;
            if ((I00.ClassId == mark) && (I22.ClassId == mark) && (I11.ClassId == ""))
                return I11;

            if ((I20.ClassId == mark) && (I11.ClassId == mark) && (I02.ClassId == ""))
                return I02;
            if ((I11.ClassId == mark) && (I02.ClassId == mark) && (I20.ClassId == ""))
                return I20;
            if ((I20.ClassId == mark) && (I02.ClassId == mark) && (I11.ClassId == ""))
                return I11;

            return null;
        }

        // Looks for corner if can't win or block
        private Image LookForCorner()
        {
            System.Diagnostics.Debug.WriteLine("computer looking for corner");
            if (I00.ClassId == "")
                return I00;
            if (I20.ClassId == "")
                return I20;
            if (I02.ClassId == "")
                return I02;
            if (I22.ClassId == "")
                return I22;

            return null;
        }

        // Looks for open space if all corners are full and can't win or block
        private Image LookForOpenSpace()
        {
            System.Diagnostics.Debug.WriteLine("computer looking for empty space");
            if (I11.ClassId == "")
                return I11;
            if (I10.ClassId == "")
                return I10;
            if (I01.ClassId == "")
                return I01;
            if (I21.ClassId == "")
                return I21;
            if (I12.ClassId == "")
                return I12;

            return null;
        }

        // Hard AI
        // The only difference between Hard AI and Moderate AI is that Hard AI will look for random corners
        private void HardComputerMove()
        {
            //Priority 1:  get tick tac toe
            //Priority 2:  block x tic tac toe
            //Priority 3:  go for corner space
            //Priority 4:  pick open space

           Image move = null;

            //Look for tic tac toe opportunities
            move = LookForWinOrBlock("O"); //look for win
            if (move == null)
            {
                move = LookForWinOrBlock("X"); //look for block
                if (move == null)
                {
                    move = LookForCornerHard(); 
                    if (move == null)
                    {
                        if ( 
                            (I00.IsEnabled == false) &&
                            (I10.IsEnabled == false) &&
                            (I20.IsEnabled == false) &&
                            (I01.IsEnabled == false) &&
                            (I11.IsEnabled == false) &&
                            (I21.IsEnabled == false) &&
                            (I02.IsEnabled == false) &&
                            (I12.IsEnabled == false) &&
                            (I22.IsEnabled == false))
                        {

                        }
                        else
                        {
                            while (move == null)
                            {
                                move = RandomMove();
                            }
                        }
                    }
                }
            }
            if (
            (I00.IsEnabled == false) &&
            (I10.IsEnabled == false) &&
            (I20.IsEnabled == false) &&
            (I01.IsEnabled == false) &&
            (I11.IsEnabled == false) &&
            (I21.IsEnabled == false) &&
            (I02.IsEnabled == false) &&
            (I12.IsEnabled == false) &&
            (I22.IsEnabled == false))
            {

            }
            else
            {
                move.ClassId = "O";
                move.Source = playerTwoImageSource;
                move.IsEnabled = false;
                CheckForWinner();
            }
        }

        private Image LookForCornerHard()
        {
            System.Diagnostics.Debug.WriteLine("computer looking for corner");
            if (I00.ClassId == "O")
            {
                if (I22.ClassId == "")
                    return I22;
            }

            if (I20.ClassId == "O")
            {
                if (I02.ClassId == "")
                    return I02;
            }

            if (I22.ClassId == "O")
            {
                if (I00.ClassId == "")
                    return I00;
            }

            if (I02.ClassId == "O")
            {
                if (I20.ClassId == "")
                    return I20;
            }


            if (I00.ClassId == "")
                return I00;
            if (I20.ClassId == "")
                return I20;
            if (I02.ClassId == "")
                return I02;
            if (I22.ClassId == "")
                return I22;

            return null;
        }

        // Computer Input Move
        private void ComputerMove()
        {
            if ((!turn) && (ai == true))
            {
                turn = !turn;
                System.Diagnostics.Debug.WriteLine("computer is moving, difficulty:" + difficulty);
                if (difficulty == "Easy")
                    EasyComputerMove();
                if (difficulty == "Moderate")
                    ModerateComputerMove();
                if (difficulty == "Hard")
                    HardComputerMove();
            }
        }

        //Scoring system
        double score1 = 0;
        double score2 = 0;

        // Reset Score button
        private void ResetScore()
        {
            System.Diagnostics.Debug.WriteLine("Score Reset");
            score1 = 0;
            score2 = 0;
            playerOneScore.Text = string.Format("P1 Score: {0}", score1);
            playerTwoScore.Text = string.Format("P2 Score: {0}", score2);
        }

    }
}

