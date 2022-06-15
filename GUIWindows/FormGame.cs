using GameEngine;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUIWindows
{
    public partial class FormGame : Form
    {

        const int k_bla = 50;
        public event EventHandler SettingsFilled, MessageBoxInteractions;
        public event Action<Move> Moved;

        private readonly FormSettings r_FormSettings = new FormSettings();

        private readonly Label labelPlayer1 = new Label();
        private readonly Label labelPlayer2 = new Label();
        private PictureBoxComponents[,] PictureBoxPieces;



        public FormGame()
        {
            InitializeComponent();
        }

        

        public void MessageBoxError(string i_Message)
        {
            MessageBox.Show(i_Message);
        }

        public void ResetBoard()
        {
            foreach (PictureBoxComponents piece in PictureBoxPieces)
            {
                piece.BackColor = Color.Black;
                //piece.BackgroundImage = GUIWindows.Resources; image TODO
                piece.Image = null;
               
            }
        }

        public void StartNewSession()
        {

        }

        public void m_FormSettings_Closed(object sender, FormClosedEventArgs e)
        {
            if (string.IsNullOrEmpty(r_FormSettings.Player1Name))
            {
                r_FormSettings.Player1Name = "Noob Player 1";
            }

            if (r_FormSettings.IsPlayer2PC)
            {
                r_FormSettings.Player2Name = "Deep-Blue";
            }

            if (string.IsNullOrEmpty(r_FormSettings.Player2Name))
            {
                r_FormSettings.Player2Name = "Noob Player 2";
            }

            //todo handle event setting filled
            setBoardSize();
            creatPiecesPictureMatrix();
            setPlayerLables();
        }

        private void setPlayerLables()
        {
            int StartingScore = 0;

            //setPlayersLabelPosition();
            labelPlayer1.Location = new Point(50, 50);
            labelPlayer2.Location = new Point(50, 50);
            /**/
            labelPlayer1.Font = new Font(labelPlayer1.Font, FontStyle.Bold & FontStyle.Underline);
            labelPlayer2.Font = new Font(labelPlayer2.Font, FontStyle.Bold & FontStyle.Underline);
            labelPlayer1.Text = string.Format("{0}: {1}", r_FormSettings.Player1Name, StartingScore);
            labelPlayer2.Text = string.Format("{0}: {1}", r_FormSettings.Player2Name, StartingScore);

            this.Controls.Add(labelPlayer1);
            this.Controls.Add(labelPlayer2);
        }

        private void setPlayersLabelPosition()
        {
            throw new NotImplementedException();
        }

        private void FormGame_OnLoad(object sender, EventArgs e)
        {
            r_FormSettings.FormClosed += m_FormSettings_Closed;
            r_FormSettings.ShowDialog();
        }

        private void creatPiecesPictureMatrix()
        {
            throw new NotImplementedException();
        }

        private void setBoardSize()
        {
            // 8x8
            this.Size = new Size(k_bla * r_FormSettings.BoardSize , k_bla * r_FormSettings.BoardSize);
            throw new NotImplementedException();
        }

        protected virtual void OnGameSettingsFiled()
        {
            throw new NotImplementedException();

        }

        protected virtual void OnMoved()
        {
            throw new NotImplementedException();

 

        }


        private void OnPopUpBox( )
        {
            throw new NotImplementedException();
        }

    }



}
