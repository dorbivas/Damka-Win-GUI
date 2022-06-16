using System.Collections.Generic;

namespace GameEngine
{ 
    public class Piece
    {
        public enum ePieceType
        {
            Empty = ' ',
            PieceO = 'O',
            PieceX = 'X',
            KingO = 'U',
            KingX = 'K' 
        }

        public enum eDirection
        {
            Down = 1,
            Up = -1,
            Left = -1,
            Right = 1
        }

        public enum ePieceValue
        {
            Regular = 1,
            King = 4
        }

        private Position m_PiecePos;
        private ePieceType m_Type;
        private List<Position> m_LegalNextPositions;
        private readonly eDirection r_Direction;
        private ePieceValue m_PieceValue;

        public Piece(int i_row, int i_Col, ePieceType i_Type)
        {
            m_PiecePos = new Position(i_row, i_Col);
            m_Type = i_Type;
            m_LegalNextPositions = new List<Position>();
            m_PieceValue = ePieceValue.Regular;

            if (i_Type == ePieceType.PieceO)
            {
                r_Direction = eDirection.Down;
            }
            else if (i_Type == ePieceType.PieceX)
            {
                r_Direction = eDirection.Up;
            }
        }

        public Piece(int i_Row, int i_Col)
        {
            m_PiecePos = new Position(i_Row, i_Col);
            m_Type = ePieceType.Empty;
            m_LegalNextPositions = new List<Position>();
            m_PieceValue = ePieceValue.Regular;
        }

        public Position Position
        {
            get => m_PiecePos;
            set => m_PiecePos = value;
        }

        public ePieceType PieceType
        {
            get => m_Type;

            set
            {
                m_Type = value;
                if (m_Type == ePieceType.Empty)
                {
                    m_LegalNextPositions.Clear();
                }
            }
        }

        public List<Position> LegalNextPositionsList
        {
            get => m_LegalNextPositions;
            set => m_LegalNextPositions = value;
        }

        public eDirection Direction
        {
            get => r_Direction;
        }

        public ePieceValue Value 
        {
            get => m_PieceValue;
        }

        public void SetPiecePosition(int i_Row, int i_Col)
        {
            Position = new Position(i_Row, i_Col);
        }

        public override string ToString()
        {
            return ((char)m_Type).ToString();
        }

        public void UpdatePieceType(Board i_Board)
        {
            if (checkIfPieceInTheFarestRow(i_Board))
            {
                m_PieceValue = ePieceValue.King;
                switch (m_Type)
                {
                    case ePieceType.PieceO:
                        m_Type = ePieceType.KingO;
                        break;
                    case ePieceType.PieceX:
                        m_Type = ePieceType.KingX;
                        break;
                    default:
                        break;
                }
            }
        }

        private bool checkIfPieceInTheFarestRow(Board i_Board)
        {
            bool shouldThePieceBeCrowned = false;

            switch (m_Type)
            {
                case ePieceType.PieceO:
                    shouldThePieceBeCrowned = m_PiecePos.Row == i_Board.BoardSize - 1;
                    break;
                case ePieceType.PieceX:
                    shouldThePieceBeCrowned = m_PiecePos.Row == 0;
                    break;
                default:
                    break;
            }

            return shouldThePieceBeCrowned;
        }

        public void UpdateLegalNextPositions(Board i_Board)
        {
            Position nextNosition = new Position(m_PiecePos.Row + (int)r_Direction, m_PiecePos.Col + (int)eDirection.Left);
            
            m_LegalNextPositions.Clear();
            AddLegalNextPosition(i_Board, nextNosition);
            nextNosition = new Position(m_PiecePos.Row + (int)r_Direction, m_PiecePos.Col + (int)eDirection.Right);
            AddLegalNextPosition(i_Board, nextNosition);
            if (cheackIfThePieceIsKing())
            { 
                nextNosition = new Position(m_PiecePos.Row + ((int)r_Direction * -1), m_PiecePos.Col + (int)eDirection.Left);
                AddLegalNextPosition(i_Board, nextNosition);
                nextNosition = new Position(m_PiecePos.Row + ((int)r_Direction * -1), m_PiecePos.Col + (int)eDirection.Right);
                AddLegalNextPosition(i_Board, nextNosition);
            }
        }

        private bool cheackIfThePieceIsKing()
        {
            return m_Type == ePieceType.KingO || m_Type == ePieceType.KingX;
        }

        public bool CheckIfThePieceEmpty()
        {
            return m_Type == ePieceType.Empty;
        }

        private void AddLegalNextPosition(Board i_Board, Position i_NextNosition)
        {
            if (i_Board.CheckIfOutOfBounds(i_NextNosition) == false)
            {
                m_LegalNextPositions.Add(i_NextNosition);
            }
        }
    }
}