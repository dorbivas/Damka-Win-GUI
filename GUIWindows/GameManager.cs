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

        private const int k_MaxCharsInName = 20;
        private const char k_ExitGameSignal = 'Q';
        private bool m_ExitGameFlag = false;
        private int m_KeepPlayFlag = 1;
        private string m_Player1Name;
        private string m_Player2Name;
        private Move m_LastMove;
        private GameLogic.eGameStatus m_GameStatus = GameLogic.eGameStatus.Ongoing;
        private Player.ePlayerType m_OpponentType;
        private Board.eBoardSizes m_BoardSize;
        EventGameSettings egsf;

        private readonly FormGame r_FormGame = new FormGame();
        private readonly GameLogic r_Game;

        public void Run()
        {
            attachGameEvents();
            attachUIEvents();
            r_FormGame.ShowDialog();
        }

        private void attachGameEvents()
        {
            r_Game.GameStarted += r_GameEngine_GameStared;
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
            egsf = new EventGameSettings(FormSettings.);

            //r_Game.InitializeGameSpecifications();
        }

        private void r_FormGame_Moved(Move i_NextMove)
        {
            if (Move.CheckIfUserMoveValid(ref i_NextMove, r_Game.CurrentPlayer.SkippingPossibleMoves, r_Game.CurrentPlayer.NormarlPossibleMoves))
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
               // r_Game.InitializeGameSpecifications(r_Game.Board.BoardSize);
                
            }
            else
            {
                r_FormGame.Close();
            }

        }

        private void r_GameEngine_BoardUpdated(Board i_Board)
        {
            //todo
        }

        private void r_GameEngine_BoardUpdated(object sender, EventArgs e)
        {
            //handle game events

            //markCurrentPlayer();
            //update FormGame funcs
        }

        private void r_GameEngine_GameFinished(object sender)
        {
            GameLogic game = sender as GameLogic;

            // r_FormGame.ContinuePlayingMessageBox(game); todo
        }

        private void r_GameEngine_SwitchedPlayers()
        {
            //r_FormGame.SwitchPlayers();
        }

        private void r_GameEngine_GameStared(object sender)
        {
            m_ExitGameFlag = false;
            m_GameStatus = GameLogic.eGameStatus.Ongoing;
            r_Game.InitializeGameSpecifications(m_BoardSize, m_Player1Name, m_Player2Name, (m_OpponentType == Player.ePlayerType.Computer) ? true : false);
            r_Game.ResetGameEngine();

            r_FormGame.StartNewSession();

        }

        private void makeBoard() // todo naming!
        {
            //ArrangesPiecesOnBoard();
            //syncPictereBoxes();
        }

    }
}
