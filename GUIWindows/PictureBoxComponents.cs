namespace GUIWindows
{
    using GameEngine;
    using System.Windows.Forms;

    public class PictureBoxComponents : PictureBox
    {
        private Position m_BoardPosition;
        private bool m_ShowEnabled;

        public PictureBoxComponents(int i_Row, int i_Col)
        {
            m_BoardPosition = new Position(i_Row, i_Col);
        }

        public Position BoardPosition
        {
            get => m_BoardPosition;
        }

        public bool ShowEnabled
        {
            get => m_ShowEnabled;
            set => m_ShowEnabled = value;
        }
    }
}