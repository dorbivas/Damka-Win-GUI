namespace GUIWindows
{
    using System;
    using System.Drawing;
    using System.IO;
    using System.Windows.Forms;
    using GameEngine;

    public class PictureBoxPiece : PictureBox
    {
        private Position m_Position;

        public PictureBoxPiece()
        {
        }

        public PictureBoxPiece(int i_Row, int i_Col)
        {
            m_Position = new Position(i_Row, i_Col);
        }

        public void SetPictureBoxCell(string i_PieceImage, bool i_Enable, Piece.ePieceType i_PieceType)
        {
            string filePath = Path.Combine(Sources.SourcesPath, i_PieceImage);
            this.Image = Image.FromFile(filePath);
            this.Name = Enum.GetName(typeof(Piece.ePieceType), i_PieceType);
            this.Enabled = i_Enable;
            this.BackgroundImage = Image.FromFile(Path.Combine(Sources.SourcesPath, Sources.BackgroundImage));
        }

        public Position GetPosition
        {
            get => m_Position;
        }
    }
}
