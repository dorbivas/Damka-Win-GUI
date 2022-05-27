using System.Collections.Generic;
using System.Text;

namespace GameEngine
{
    public class Move
    {
        private Position m_From;
        private Position m_To;
        private Position m_Eaten;
        private bool m_SkipMoveFlag = false;

        public Move(Position i_From, Position i_To)
        {
            m_From = i_From;
            m_To = i_To;
        }

        public Move(Position i_From, Position i_To, Position i_Eaten)
        {
            m_From = i_From;
            m_To = i_To;
            m_Eaten = i_Eaten;
            m_SkipMoveFlag = true;
        }

        public Position From
        {
            get => m_From;
            set => m_From = value;
        }

        public Position To
        {
            get => m_To;
            set => m_To = value;
        }

        public Position Eaten
        {
            get => m_Eaten;
            set => m_Eaten = value;
        }

        public bool IsSkipMove
        {
            get => m_SkipMoveFlag;
            set => m_SkipMoveFlag = value;
        }

        public static bool CheckIfUserMoveValid(ref Move io_inputMove, List<Move> io_SkippingMovesList, List<Move> io_NormalMovesList)
        {
            bool validMoveFlag;

            if (io_SkippingMovesList.Count > 0)
            {
                validMoveFlag = checkIfValidMoveInList(ref io_inputMove, io_SkippingMovesList);
            }
            else
            {
                validMoveFlag = checkIfValidMoveInList(ref io_inputMove, io_NormalMovesList);
            }

            return validMoveFlag;
        }

        private static bool checkIfValidMoveInList(ref Move io_inputMove, List<Move> io_List)
        {
            bool validMoveFlag = false;

            foreach (Move currentMove in io_List)
            {
                if (checkIfEqualLocation(currentMove, io_inputMove))
                {
                    validMoveFlag = true;
                    io_inputMove = currentMove;
                    break;
                }
            }

            return validMoveFlag;
        }

        private static bool checkIfEqualLocation(Move i_Move, Move i_inputMove)
        {
            return i_Move.m_From == i_inputMove.m_From && i_Move.To == i_inputMove.To;
        }

        public void Execute(Player i_OpponentPlayer, Board i_Board)
        {
            i_Board.SwapPiecesbyPosition(ref i_Board.GetPieceByRef(m_From), ref i_Board.GetPieceByRef(m_To));
            if (m_SkipMoveFlag == true)
            {
                i_Board.RemovePieceFromBoard(i_OpponentPlayer, m_Eaten);
            }

            i_Board.GetPieceByRef(m_To).UpdatePieceType(i_Board);
        }

        public static string ConvertLastMoveToString(Move i_LastMove)
        {
            StringBuilder lastMove = new StringBuilder();

            if (i_LastMove != null)
            {
                lastMove.Append((char)('A' + i_LastMove.From.Col));
                lastMove.Append((char)('a' + i_LastMove.From.Row));
                lastMove.Append('>');
                lastMove.Append((char)('A' + i_LastMove.To.Col));
                lastMove.Append((char)('a' + i_LastMove.To.Row));
            }

            return lastMove.ToString();
        }
    }
}
