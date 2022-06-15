using GameEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


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

        private readonly FormGame r_FormGame = new FormGame();
        private readonly GameLogic r_Game;

        public void Run()
        {

        }

        private void attachFormEvents()
        {
            //Todo
            //m_FormGame.SettingsFilled += m_FormGame_SettingsFilled;
            //m_FormGame.MoveEnterd += m_FormGame_Moved;
            //m_FormGame.MessageBoxAnswered += m_FormGame_MessageBoxAnswered;
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
            //EventGameSettings sf = new EventGameSettings

            //r_Game.InitializeGameSpecifications();
        }

        private void r_FormGame_Moved(object sender, EventArgs e)
        {

        }

        private void r_FormGame_SettingsFilled(object sender, EventArgs e)
        {

        }


            private void r_GameEngine_BoardUpdated(Board i_Board)
        {
            //todo
        }

        private void r_FormGame_MoveEnterd(object sender, EventArgs e)
        {
            //TODO action

            //MoveEnterdEventArgs me = e as MoveEnterdEventArgs;
            //Move newMove = new Move(me.From, me.To);
            //string errorMessage;
            //bool isLegalMove;

            //isLegalMove = m_AmericanCheckers.ImplementMove(newMove, out errorMessage);
            //if (!isLegalMove)
            //{
            //    m_FormGame.ErrorMessageBox(errorMessage);
            //}

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
