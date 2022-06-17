using System;
using System.Text;

namespace GameEngine
{
    public class Board
    {
        public enum eBoardSizes
        {
            Small = 6,
            Medium = 8,
            Large = 10
        }

        private readonly Piece[,] r_Board;
        private readonly int r_Size;

        public Board(eBoardSizes boradSizes)
        {
            r_Size = (int)boradSizes;
            r_Board = new Piece[r_Size, r_Size];
        }

        public int BoardSize
        {
            get => r_Size;
        }

        public static bool CheckIfValidBoardSizeInput(string i_BoardSize, out eBoardSizes o_BoardSizeValidation)
        {
            bool isNumericEnum;

            isNumericEnum = eBoardSizes.TryParse(i_BoardSize, out o_BoardSizeValidation);
            return isNumericEnum && CheckIfValidSize(o_BoardSizeValidation);
        }

        public static bool CheckIfValidSize(eBoardSizes i_SizeToCheck)
        {
            return i_SizeToCheck == Board.eBoardSizes.Small || i_SizeToCheck == Board.eBoardSizes.Medium || i_SizeToCheck == Board.eBoardSizes.Large;
        }

        public static bool IsNotSameColor(int i_First, int i_Second)
        {
            return (i_First % 2) != (i_Second % 2);
        }

        public void ArrangesPiecesOnBoard(Player i_PlayerOne, Player i_PlayerTwo)
        {
            for (int i = 0; i < r_Size; i++)
            {
                for (int j = 0; j < r_Size; j++)
                {
                    if (i % 2 != j % 2)
                    {
                        if (i < (r_Size - 2) / 2)
                        {
                            r_Board[i, j] = new Piece(i, j, Piece.ePieceType.PieceO);
                            i_PlayerTwo.AddPiece(r_Board[i, j]);
                        }
                        else if (i > ((r_Size + 2) / 2) - 1)
                        {
                            r_Board[i, j] = new Piece(i, j, Piece.ePieceType.PieceX);
                            i_PlayerOne.AddPiece(r_Board[i, j]);
                        }
                        else 
                        {
                            // in between players empty zone
                            r_Board[i, j] = new Piece(i, j, Piece.ePieceType.Empty);
                        }
                    }
                    else  
                    {
                        // white cells 
                        r_Board[i, j] = new Piece(i, j, Piece.ePieceType.Empty);
                    }
                }
            }
        }

        public void ClearBoard(Player i_PlayerOne, Player i_PlayerTwo)
        {
            for (int i = 0; i < r_Size; i++)
            {
                for (int j = 0; j < r_Size; j++)
                {
                    if (i % 2 == j % 2)
                    {
                        r_Board[i, j] = new Piece(i, j);
                    }
                }
            }

            i_PlayerOne.ClearPiecesList();
            i_PlayerTwo.ClearPiecesList();
        }

        public void RemovePieceFromBoard(Player i_Player, Position i_PiecePosition)
        {
            i_Player.RemovePiece(r_Board[i_PiecePosition.Row, i_PiecePosition.Col]);
            r_Board[i_PiecePosition.Row, i_PiecePosition.Col] = new Piece(i_PiecePosition.Row, i_PiecePosition.Col);
        }

        public ref Piece GetPieceByRef(Position i_Position)
        {
            return ref r_Board[i_Position.Row, i_Position.Col];
        }

        public Piece.ePieceType GetPieceType(int i_Row, int i_Col)
        {
            return r_Board[i_Row, i_Col].PieceType;
        }

        public void SwapPiecesbyPosition(ref Piece io_PieceOne, ref Piece io_PieceTwo)
        {
            Piece pieceTemp;
            Position positionTemp;

            pieceTemp = io_PieceOne;
            io_PieceOne = io_PieceTwo;
            io_PieceTwo = pieceTemp;
            positionTemp = io_PieceOne.Position;
            io_PieceOne.Position = io_PieceTwo.Position;
            io_PieceTwo.Position = positionTemp;
        }

        public bool CheckIfOutOfBounds(Position i_PositionToBeCheck)
        {
            return i_PositionToBeCheck.Row < 0 || i_PositionToBeCheck.Col < 0 || i_PositionToBeCheck.Row >= r_Size || i_PositionToBeCheck.Col >= r_Size;
        }

        public override string ToString()
        {
            StringBuilder boardInString = new StringBuilder();
            string horizontalEqualsLine = createEqualsLine();
            
            boardInString.Append(" ");
            for (char c = 'A'; c < r_Size + 'A'; c++)
            {
                boardInString.Append(string.Format("  {0} ", c));
            }

            boardInString.Append(Environment.NewLine);
            for (int i = 0; i < r_Size; i++)
            {
                boardInString.Append(horizontalEqualsLine);
                boardInString.Append(string.Format("{0}", (char)(i + 'a')));
                for (int j = 0; j < r_Size; j++)
                {
                    boardInString.Append("| " + r_Board[i, j].ToString() + " ");
                }

                boardInString.Append("|");
                boardInString.Append(Environment.NewLine);
            }

            boardInString.Append(horizontalEqualsLine);

            return boardInString.ToString();
        }

        private string createEqualsLine()
        {
            StringBuilder equalLine = new StringBuilder();

            equalLine.Append(" ");
            for (int j = 0; j < r_Size; j++)
            {
                equalLine.Append("====");
            }

            equalLine.Append("=" + Environment.NewLine);

            return equalLine.ToString();
        }
    }
}