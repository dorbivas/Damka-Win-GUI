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

        private readonly FormGame m_FormGame = new FormGame();
        private readonly GameLogic m_Game;

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
            //Todo
        }

        private void m_FormGame_MoveEnterd(object sender, EventArgs e)
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

        private void m_AmericanCheckers_BoardUpdated(object sender, EventArgs e)
        {
            //handle game events

            //markCurrentPlayer();
            //update FormGame funcs
        }

        private void makeBoard() // todo naming!
        {
            //ArrangesPiecesOnBoard();
            //syncPictereBoxes();
        }

    }
}
