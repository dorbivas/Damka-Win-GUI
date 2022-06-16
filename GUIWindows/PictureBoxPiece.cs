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

        //TODO KOMBINA
        public PictureBoxPiece(int i_Row =-1, int i_Col=-1)
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
