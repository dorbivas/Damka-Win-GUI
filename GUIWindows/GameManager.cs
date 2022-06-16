using GameEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUIWindows
{
    class GameManager
    {
        private readonly FormGame r_FormGame = new FormGame();
        private readonly GameLogic r_Game = new GameLogic();

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

            r_Game.InitializeGameSpecifications(egs.BoardSize, egs.Player1Name, egs.Player2Name, (egs.Player2Type == Player.ePlayerType.Computer) ? true : false);
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

        //TODO
        private void r_FormGame_MessageBoxInteractions(object sender, EventArgs e)
        {
            MessageBoxYesNoEvent mbyne = e as MessageBoxYesNoEvent;

            if (mbyne.IsMessageBoxClickedYes)
            {
                r_GameEngine_GameStarted(sender);
                //r_FormGame.ResetBoard();
                r_FormGame.InitialzeNewGameForm();
                r_FormGame.UpdatePictureBoxBoard(r_Game.Board);

            }
            else
            {
                r_FormGame.Close();
            }

        }

        private void r_GameEngine_BoardUpdated(Board i_Board)
        {
            r_FormGame.UpdatePictureBoxBoard(i_Board);
        }

        //TODO
        private void r_GameEngine_GameFinished(object sender)
        {
            GameLogic game = sender as GameLogic;

            r_FormGame.CreateYesNoMessageBox(EndSessionResult(game.GameStatus), "Checkers");
        }

        private string EndSessionResult(GameEngine.GameLogic.eGameStatus i_Result)
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

            endGameMessage.Append("Do You Want To Play Again?");

            return endGameMessage.ToString();
        }

        private void r_GameEngine_SwitchedPlayers()
        {
            r_FormGame.SwitchPlayers();
        }

        private void r_GameEngine_GameStarted(object sender)
        {
            r_Game.ResetGameEngine();
        }

        private void makeBoard() // todo naming!
        {
            //ArrangesPiecesOnBoard();
            //syncPictereBoxes();
        }

    }
}
