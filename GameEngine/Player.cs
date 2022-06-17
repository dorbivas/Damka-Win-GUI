using System;
using System.Collections.Generic;

namespace GameEngine
{
    public class Player
    {
        public enum ePlayerNumber
        {
            PlayerOneX = 1, 
            PlayerTwoO = 2,  
        }

        public enum ePlayerType
        {
            Computer = 1,
            Human = 2,
        }

        private ePlayerType m_PlayerType;
        private string m_Name;
        private ePlayerNumber m_PlayerNumber;
        private ComputerAI m_AI;
        private List<Move> m_NormalPossibleMoves = new List<Move>();
        private List<Move> m_SkippingPossibleMoves = new List<Move>();
        private readonly List<Piece> r_PlayerPiecesList = new List<Piece>();

        public Player(string i_PlayerName, ePlayerNumber i_PlayerNumber, ePlayerType i_PlayerType)
        {
            m_Name = i_PlayerName;
            m_PlayerNumber = i_PlayerNumber;
            m_PlayerType = i_PlayerType;
            if (m_PlayerType == ePlayerType.Computer)
            {
                m_AI = new ComputerAI();
            }
        }

        public static bool CheckIfValidPlayerTypeInput(string i_PlayerType, out ePlayerType o_PlayerTypeValidation)
        {
            bool isNumericEnum;

            isNumericEnum = ePlayerType.TryParse(i_PlayerType, out o_PlayerTypeValidation);
            return isNumericEnum && checkIfValidType(o_PlayerTypeValidation);
        }

        public List<Move> GetValidMoves(Board i_Board)
        {
            UpdatePlayerMoves(i_Board);
            return CheckIfSkipingMoveAvailable() ? SkippingPossibleMoves : NormarlPossibleMoves;
        }

        public ePlayerType Type
        {
            get => m_PlayerType;
            set 
            { 
                m_PlayerType = value;
                if (m_PlayerType == ePlayerType.Computer)
                {
                    m_AI = new ComputerAI();
                }
            }
        }

        public ePlayerNumber PlayerNumber
        {
            get => m_PlayerNumber;
            set => m_PlayerNumber = value;
        }

        public ComputerAI AI
        {
            get => m_AI;
        }

        public string Name
        {
            get => m_Name;
            set => m_Name = value;
        }

        public List<Move> NormarlPossibleMoves
        {
            get => m_NormalPossibleMoves;
            set => m_NormalPossibleMoves = value;
        }

        public List<Move> SkippingPossibleMoves
        {
            get => m_SkippingPossibleMoves;
            set => m_SkippingPossibleMoves = value;
        }

        public List<Piece> PiecesList
        {
            get => r_PlayerPiecesList;
        }

        public int Score 
        { 
            get => calculatePlayerScore();
        }

       private static bool checkIfValidType(ePlayerType i_TypeToCheck) 
        {
            return i_TypeToCheck == ePlayerType.Computer || i_TypeToCheck == ePlayerType.Human;
        }

        public void AddPiece(Piece i_Piece)
        {
            r_PlayerPiecesList.Add(i_Piece);
        }

        public void RemovePiece(Piece i_Piece)
        {
            r_PlayerPiecesList.Remove(i_Piece);
        }

        public void ClearPiecesList()
        {
            r_PlayerPiecesList.Clear();
        }

        private bool IsPieceBelongsToOppnentOrEmpty(Piece i_Piece)
        {
            bool isThePieceInOpponentSide = true;

            switch (m_PlayerNumber)
            {
                case ePlayerNumber.PlayerTwoO:
                    if (i_Piece.PieceType == Piece.ePieceType.PieceO || i_Piece.PieceType == Piece.ePieceType.KingO)
                    {
                        isThePieceInOpponentSide = false;
                    }

                    break;
                case ePlayerNumber.PlayerOneX:
                    if (i_Piece.PieceType == Piece.ePieceType.PieceX || i_Piece.PieceType == Piece.ePieceType.KingX)
                    {
                        isThePieceInOpponentSide = false;
                    }

                    break;
                default:
                    break;
            }

            return isThePieceInOpponentSide;
        }

        public bool CheckIfSkipingMoveAvailable()
        {
            return m_SkippingPossibleMoves.Count > 0;
        }

        public bool CheckIfRegularMoveAvailable()
        {
            return CheckIfSkipingMoveAvailable() || m_NormalPossibleMoves.Count > 0;
        }

        public void UpdatePlayerMoves(Board i_Board)
        {
            m_NormalPossibleMoves.Clear();
            m_SkippingPossibleMoves.Clear();

            foreach (Piece playerPiece in r_PlayerPiecesList)
            {
                playerPiece.UpdateLegalNextPositions(i_Board);
                updatePiecePossibleMoves(playerPiece, i_Board);
            }
        }

        private void updatePiecePossibleMoves(Piece i_Piece, Board i_Board)
        {
            Move nextMove;
            Piece nextPiecePosition;
            Position skipNextPosition;

            foreach (Position nextPosition in i_Piece.LegalNextPositionsList)
            {
                nextPiecePosition = i_Board.GetPieceByRef(nextPosition);
                if (IsPieceBelongsToOppnentOrEmpty(nextPiecePosition) == true)
                {
                    if (nextPiecePosition.CheckIfThePieceEmpty() == true)
                    {
                        nextMove = new Move(i_Piece.Position, nextPosition);
                        m_NormalPossibleMoves.Add(nextMove);
                    }
                    else
                    {
                        skipNextPosition = new Position((nextPiecePosition.Position.Row * 2) - i_Piece.Position.Row, (nextPiecePosition.Position.Col * 2) - i_Piece.Position.Col);
                        if (i_Board.CheckIfOutOfBounds(skipNextPosition) == false && i_Board.GetPieceByRef(skipNextPosition).CheckIfThePieceEmpty())
                        {
                            nextMove = new Move(i_Piece.Position, skipNextPosition, nextPosition);
                            m_SkippingPossibleMoves.Add(nextMove);
                        }
                    }
                }
            }
        }

        public void ContiniusSkipingMove(Board i_Board, Move i_LastSkipingMove)
        {
            if (i_LastSkipingMove.IsSkipMove == true)
            {
                m_SkippingPossibleMoves.Clear();
                i_Board.GetPieceByRef(i_LastSkipingMove.To).UpdateLegalNextPositions(i_Board);
                updatePiecePossibleMoves(i_Board.GetPieceByRef(i_LastSkipingMove.To), i_Board);
                m_NormalPossibleMoves.Clear();
            }
        }

        private int calculatePlayerScore()
        {
            int score = 0;
            foreach (Piece piece in r_PlayerPiecesList)
            {
                score += (int)piece.Value;
            }

            return score;
        }
    }
}
