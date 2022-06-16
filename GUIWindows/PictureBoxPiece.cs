using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameEngine;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace GUIWindows
{
    class PictureBoxPiece : PictureBox
    {
        private Position m_Position;

        public PictureBoxPiece(int i_Row, int i_Col)
        {
            m_Position = new Position(i_Row, i_Col);
        }

        public void SetPictureBoxCell(string i_PieceImage, bool i_Enable, Piece.ePieceType i_PieceType)
        {
            this.Image = Image.FromFile(Path.Combine(Sources.SourcesPath, i_PieceImage));
            this.Name = Enum.GetName(typeof(Piece.ePieceType), i_PieceType);
            this.Enabled = i_Enable;
        }

        public Position GetPosition
        {
            get => m_Position;
        }
    }
}
