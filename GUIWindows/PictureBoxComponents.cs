namespace GUIWindows
{
    using GameEngine;
    using System;
    using System.IO;
    using System.Windows.Forms;
    using System.Drawing;

    public class PictureBoxComponents : PictureBox
    {
        private Position m_BoardPosition;
        private bool m_ShowEnabled;

        public PictureBoxComponents(int i_Row, int i_Col)
        {
            m_BoardPosition = new Position(i_Row, i_Col);
        }

        public void SetPictureBoxCell(string i_PieceImage, bool i_Enable, Piece.ePieceType i_Type)
        {
            string filePath = Path.Combine(Sources.SourcesPath, i_PieceImage);
            this.Image = Image.FromFile(filePath);
            this.Name = Enum.GetName(typeof(Piece.ePieceType), i_Type);
            this.Enabled = i_Enable;
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