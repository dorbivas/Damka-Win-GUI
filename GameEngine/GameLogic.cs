using System;

namespace GameEngine
{
    public class GameLogic
    {
        public enum eGameStatus
        {
            Ongoing,
            Draw,
            Player2OWin,
            Player1XWin
        }

        private Board m_GameBoard;
        private Player m_CurrentPlayer;
        private Player m_NextPlayer;
        private Move m_LastMove;
        private eGameStatus m_GameStatus;
        private int m_Player1Score;
        private int m_Player2Score;

        public Board Board
        {
            get => m_GameBoard;
        }

        public Player CurrentPlayer
        {
            get => m_CurrentPlayer;
        }

        public Player NextPlayer
        {
            get => m_NextPlayer;
        }

        public Move LastMove 
        {
            get => m_LastMove;
        }

        public int Player1Score 
        { 
            get => m_Player1Score; 
        }

        public int Player2Score
        {
            get => m_Player2Score;
        }

        public GameLogic Copy()
        {
            GameLogic copy = new GameLogic();
            copy.m_CurrentPlayer = this.m_CurrentPlayer.Copy();
            copy.m_NextPlayer = this.m_NextPlayer.Copy();
            copy.m_GameBoard = this.m_GameBoard.Copy();
            copy.m_GameStatus = this.m_GameStatus;
            copy.m_LastMove = this.m_LastMove;
            copy.m_Player1Score = this.m_Player1Score;
            copy.m_Player2Score = this.m_Player2Score;


            return copy;
        }

        public eGameStatus GameStatus
        {
            get => m_GameStatus;
        }

        public void ResetGameEngine()
        {
            m_GameBoard.ClearBoard(m_CurrentPlayer, m_NextPlayer);
            m_GameBoard.ArrangesPiecesOnBoard(m_CurrentPlayer, m_NextPlayer);
            m_GameStatus = eGameStatus.Ongoing;
            m_CurrentPlayer.UpdatePlayerMoves(m_GameBoard);
            m_NextPlayer.UpdatePlayerMoves(m_GameBoard);
            m_LastMove = null;
        }

        public void InitializeGameSpecifications(Board.eBoardSizes i_BoardSizes, string i_Player1Name, string i_Player2Name, bool i_IsPc)
        {
            updateBoardSize(i_BoardSizes);
            updatePlayersDetails(i_Player1Name, i_Player2Name, i_IsPc);
        }

        public void ExecuteSingleTurn(Move io_ValidMoveFromUI)
        {
            //if (m_CurrentPlayer.Type == Player.ePlayerType.Computer)
            //{
            //    MinimaxAI.Minimax(this, 3, 1, out io_ValidMoveFromUI);
            //}
            m_LastMove = io_ValidMoveFromUI;
            io_ValidMoveFromUI.Execute(m_NextPlayer, m_GameBoard);
            if (m_GameStatus == eGameStatus.Ongoing)
            {
                if (io_ValidMoveFromUI.IsSkipMove == true)
                {
                    m_CurrentPlayer.ContiniusSkipingMove(m_GameBoard, io_ValidMoveFromUI);
                    if (m_CurrentPlayer.SkippingPossibleMoves.Count == 0)
                    {
                        switchPlayers(ref m_CurrentPlayer, ref m_NextPlayer);
                    }
                }
                else
                {
                    switchPlayers(ref m_CurrentPlayer, ref m_NextPlayer);
                }

                m_LastMove = io_ValidMoveFromUI;
            }
        }

        private void updatePlayersDetails(string i_Player1Name, string i_Player2Name, bool i_IsComputer)
        {
            Player.ePlayerType nextPlayerType = (i_IsComputer == true) ? Player.ePlayerType.Computer : Player.ePlayerType.Human;

            m_CurrentPlayer = new Player(i_Player1Name, Player.ePlayerNumber.PlayerOneX, Player.ePlayerType.Human);
            m_NextPlayer = new Player(i_Player2Name, Player.ePlayerNumber.PlayerTwoO, nextPlayerType);           
        }

        private void updateBoardSize(Board.eBoardSizes i_BoardSizes)
        {
            m_GameBoard = new Board(i_BoardSizes);
        }

        private void switchPlayers(ref Player io_CurrentPlayer, ref Player io_NextPlayer)
        {
            Player temp = io_CurrentPlayer;

            io_CurrentPlayer = io_NextPlayer;
            io_NextPlayer = temp;
            io_CurrentPlayer.UpdatePlayerMoves(m_GameBoard);
            io_NextPlayer.UpdatePlayerMoves(m_GameBoard);
        }

        public void UpdateGameStatus(bool i_ExitGameFlag)
        {
            if (i_ExitGameFlag == true)
            {
                m_GameStatus = decideWinner(m_CurrentPlayer);
            }
            else if (m_CurrentPlayer.PiecesList.Count == 0)
            {
                m_GameStatus = decideWinner(m_CurrentPlayer);
            }
            else if (checkNoAvailableMoveInGame() == true)
            {
                if (m_CurrentPlayer.Score - m_NextPlayer.Score > 0)
                {
                    m_GameStatus = decideWinner(m_CurrentPlayer);
                }
                else if (m_NextPlayer.Score - m_CurrentPlayer.Score > 0)
                {
                    m_GameStatus = decideWinner(m_NextPlayer);
                }
                else
                {
                    m_GameStatus = eGameStatus.Draw;
                }
            }
        }

        private eGameStatus decideWinner(Player i_Player)
        {
            return i_Player.PlayerNumber == Player.ePlayerNumber.PlayerTwoO ?
                            eGameStatus.Player1XWin : eGameStatus.Player2OWin;
        }

        private bool checkNoAvailableMoveInGame()
        {
            return m_CurrentPlayer.CheckIfRegularMoveAvailable() == false && m_NextPlayer.CheckIfRegularMoveAvailable() == false;
        }

        public void UpdateGameScores()
        {
            int player1Score = m_CurrentPlayer.PlayerNumber == Player.ePlayerNumber.PlayerOneX ? m_CurrentPlayer.Score : m_NextPlayer.Score;
            int player2Score = m_CurrentPlayer.PlayerNumber == Player.ePlayerNumber.PlayerTwoO ? m_CurrentPlayer.Score : m_NextPlayer.Score;

            switch (m_GameStatus)
            {
                case eGameStatus.Player2OWin:
                    m_Player2Score += Math.Abs(player1Score - player2Score);
                    break;
                case eGameStatus.Player1XWin:
                    m_Player1Score += Math.Abs(player1Score - player2Score);
                    break;
                default:
                    break;
            }
        }
    }
}