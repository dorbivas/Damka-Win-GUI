namespace GUIWindows
{
    using GameEngine;
    using System;
    using System.Text;
    using System.Windows.Forms;

    public class GameManager
    {
        private readonly FormGame r_FormGame = new FormGame();
        private readonly GameLogic r_Game = new GameLogic();
        private readonly Move r_DummyMove = new Move(new Position(0, 0), new Position(0, 0));

        public void Run()
        {
            attachGameEvents();
            attachUIEvents();
            r_FormGame.ShowDialog();
        }

        private void attachGameEvents()
        {
            r_Game.GameStarted += r_GameEngine_GameStarted;
            r_Game.GameFinished += r_GameEngine_GameFinished;
            r_Game.BoardUpdated += r_GameEngine_BoardUpdated;
            r_Game.PlayerSwitched += r_GameEngine_SwitchedPlayers;
        }

        private void attachUIEvents()
        {
            r_FormGame.SettingsFilled += r_FormGame_SettingsFilled;
            r_FormGame.MessageBoxInteractions += r_FormGame_MessageBoxInteractions;
            r_FormGame.Moved += r_FormGame_Moved;
        }

        private void r_FormGame_SettingsFilled(object sender, EventArgs e)
        {
            EventGameSettings egs = e as EventGameSettings;

            r_Game.InitializeGameSpecifications(egs.BoardSize, egs.Player1Name, egs.Player2Name, egs.Player2Type == Player.ePlayerType.Computer);
        }

        private void r_FormGame_Moved(Move i_NextMove)
        {
            if (!Move.CheckIfUserMoveValid(ref i_NextMove, r_Game.CurrentPlayer.SkippingPossibleMoves, r_Game.CurrentPlayer.NormarlPossibleMoves))
            {
                MessageBox.Show("Invalid move!, please try again", "Error");
            }
            else
            {
                r_Game.ExecuteSingleTurn(i_NextMove);
            }
        }

        private void r_FormGame_MessageBoxInteractions(object sender, EventArgs e)
        {
            MessageBoxYesNoEvent mbyne = e as MessageBoxYesNoEvent;

            if (mbyne.IsMessageBoxClickedYes)
            {
                r_GameEngine_GameStarted(sender);
                if (r_Game.CurrentPlayer.PlayerNumber != Player.ePlayerNumber.PlayerOneX )
                {
                    r_GameEngine_SwitchedPlayers();
                }

                r_Game.GameStatus = GameLogic.eGameStatus.Ongoing;
                r_FormGame.ResetEventFormGameSettings();
                r_FormGame.UpdateBoardUI(r_Game.Board);               
            }
            else
            {
                r_FormGame.Close();
            }
        }

        private void r_GameEngine_BoardUpdated(Board i_Board)
        {
            r_FormGame.UpdateBoardUI(i_Board);
        }

        private void r_GameEngine_GameFinished(object sender)
        {
            GameLogic game = sender as GameLogic;

            r_FormGame.CreateYesNoMessageBox(endSessionResult(game.GameStatus), "Damka");
        }

        private void r_GameEngine_SwitchedPlayers()
        {
            r_FormGame.SwitchPlayers();
            if (r_Game.GameStatus == GameLogic.eGameStatus.Ongoing && r_Game.CurrentPlayer.Type == Player.ePlayerType.Computer)
            {
                do
                {
                    r_Game.ExecuteSingleTurn(r_DummyMove);
                } 
                while (r_Game.CurrentPlayer.SkippingPossibleMoves.Count > 0 && r_Game.CurrentPlayer.Type == Player.ePlayerType.Computer);
            }
        }

        private void r_GameEngine_GameStarted(Object sender)
        {
            r_FormGame.KeepSessionInformation(r_Game.Player1Score, r_Game.Player2Score, r_Game.CurrentPlayer.Name);
            r_Game.UpdateGameScores();
            r_Game.ResetGameEngine();
        }

        private string endSessionResult(GameEngine.GameLogic.eGameStatus i_Result)
        {
            StringBuilder endGameMessage = new StringBuilder();

            if (i_Result == GameEngine.GameLogic.eGameStatus.Draw)
            {
                endGameMessage.Append("Tie!");
            }
            else
            {
                endGameMessage.Append(string.Format("{0} Won!{1}", r_Game.NextPlayer.Name, Environment.NewLine));
            }

            endGameMessage.Append("Do you want to do another game?");

            return endGameMessage.ToString();
        }
    }
}
