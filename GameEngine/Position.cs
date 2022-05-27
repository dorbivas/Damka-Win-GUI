namespace GameEngine
{
    public struct Position
    {
        private int m_Col;
        private int m_Row;

        public int Col
        {
            get => m_Col;
            set => m_Col = value;
        }

        public int Row
        {
            get => m_Row;
            set => m_Row = value;
        }

        public Position(int i_Row, int i_Col)
        {
            m_Col = i_Col;
            m_Row = i_Row;
        }

        public static bool operator ==(Position i_Left, Position i_Right)
        {
            return i_Left.Col == i_Right.Col && i_Left.Row == i_Right.Row;
        }

        public static bool operator !=(Position i_Left, Position i_Right)
        {
            return !(i_Left == i_Right);
        }

        /*Ignore.*/
        /*Override for future use, comes in packege with opr == and != */
        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        /*************************************************************/
    }
}