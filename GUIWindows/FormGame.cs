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

        public event EventHandler SettingsFilled, MessageBoxInteractions;
        public event Action<Move> Moved;
        private const int k_PictureBoxSize = 50;
        private const int k_ExtraWidth = 20;
        private const int k_ExtraHeight = 100;
        private const int k_StartingPictureBoxX = 0;
        private const int k_StartingPictureBoxY = 40;
        private const int k_WidthExtention = 20;
        private const int k_HeightExtention = 80;

        private readonly FormSettings r_FormSettings = new FormSettings();
        private readonly Label labelPlayer1 = new Label();
        private readonly Label labelPlayer2 = new Label();
        private PictureBoxPiece[,] m_PictureBoxBoard;
        private EventGameSettings m_EventGameSettings;
        private bool isSecondClick = false;
        //private Position m_from;
        private PictureBoxPiece m_PictureBoxPressed;
        private Move m_EnteredMove;
        public FormGame()
        {
            InitializeComponent();
        }

        public void MessageBoxError(string i_Message)
        {
            MessageBox.Show(i_Message);
        }

        public void ResetBoard() // TODO
        {
            foreach (PictureBoxPiece piece in m_PictureBoxBoard)
            {
                piece.BackColor = Color.Black;
                //piece.BackgroundImage = GUIWindows.Resources; image TODO
                piece.Image = null;
               
            }
        }

        public void StartNewSession()
        {

        }

        //TODO
        private void initializeBoardPictureBox()
        {
            bool isNewLine = false, isFirstPictureBox = true;
            PictureBox lastPictureBoxInMatrix = new PictureBox();
            PictureBoxPiece newPictureBox;

            for (int i = 0; i < ((int)m_EventGameSettings.BoardSize); i++)
            {
                for (int j = 0; j < ((int)m_EventGameSettings.BoardSize); j++)
                {
                    newPictureBox = new PictureBoxPiece(i, j);
                    intializePictureBox(newPictureBox);
                    m_PictureBoxBoard[i, j] = newPictureBox;
                    this.Controls.Add(newPictureBox);
                    isNewLine = false;
                    isFirstPictureBox = false;
                    lastPictureBoxInMatrix = newPictureBox;
                }

                isNewLine = true;
            }
        }

        //TODO fix it
        private void intializePictureBox(PictureBoxPiece i_CurrentPictureBox)
        {
            i_CurrentPictureBox.Size = new Size(k_PictureBoxSize, k_PictureBoxSize);
            setPictureBoxLocation(newPictureBox, isNewLine, isFirstPictureBox, lastPictureBoxInMatrix);
            i_CurrentPictureBox.SizeMode = PictureBoxSizeMode.StretchImage;

            setPictureBoxFigure(i_CurrentPictureBox);
            i_CurrentPictureBox.Enabled = false;
            i_CurrentPictureBox.Click += pictureBox_Click;
        }

        private void setPictureBoxFigure(PictureBoxPiece i_CurrentPictureBox)
        {
            i_CurrentPictureBox.Height = k_PictureBoxSize;
            i_CurrentPictureBox.Width = k_PictureBoxSize;
        }

        private void setPictureBoxLocation(
            PictureBoxPiece i_CurrentPictureBox,
            bool i_NewLine,
            bool i_IsFirstPictureBox,
            PictureBox i_LastPictureBoxInMatrix)
        {
            Point newLocation;
            int pictureBoxMatrixMaxLine = ((int)m_EventGameSettings.BoardSize) - 1;
            int pictureBoxMatrixMaxCol = ((int)m_EventGameSettings.BoardSize) - 1;

            if (i_IsFirstPictureBox)
            {
                newLocation = new Point(k_StartingPictureBoxX, k_StartingPictureBoxY);
            }
            else
            {
                newLocation = i_LastPictureBoxInMatrix.Location;
                if (!i_NewLine)
                {
                    newLocation.Offset(i_LastPictureBoxInMatrix.Width, 0);
                }
                else
                {
                    newLocation.X = k_StartingPictureBoxX;
                    newLocation.Offset(0, i_LastPictureBoxInMatrix.Height);
                }
            }

            i_CurrentPictureBox.Location = newLocation;
        }

        private void pictureBox_Click(object sender, EventArgs e)
        {
            PictureBoxPiece piecePressed = sender as PictureBoxPiece;

            if (!isSecondClick)
            {
                if (piecePressed != null)
                {
                    m_PictureBoxPressed = piecePressed;
                    m_PictureBoxPressed.Enabled = false;
                    m_PictureBoxPressed.BorderStyle = BorderStyle.Fixed3D;
                    isSecondClick = true;
                }
            }
            else
            {
                if (piecePressed != null)
                {
                    if (piecePressed.GetPosition != m_PictureBoxPressed.GetPosition)
                    {
                        m_EnteredMove = new Move(m_PictureBoxPressed.GetPosition, piecePressed.GetPosition);
                        OnMoved(m_EnteredMove);
                    }
                    else
                    {
                        m_PictureBoxPressed = null;
                        m_PictureBoxPressed.Enabled = true;
                        m_PictureBoxPressed.BorderStyle = BorderStyle.None;
                        isSecondClick = false;
                    }
                }
            }
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

            m_EventGameSettings = new EventGameSettings(
                r_FormSettings.Player1Name,
                r_FormSettings.Player2Name,
                r_FormSettings.BoardSize,
                r_FormSettings.IsPlayer2PC ? Player.ePlayerType.Computer : Player.ePlayerType.Human); //TODO maybe playerType is redundent
            //todo handle event setting filled
            //setBoardSize();
            //creatPiecesPictureMatrix();
            //setPlayerLables();
            OnGameSettingsFiled(m_EventGameSettings);
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
            this.Size = new Size(((int)r_FormSettings.BoardSize * k_PictureBoxSize) + k_ExtraWidth,
                                 ((int)r_FormSettings.BoardSize * k_PictureBoxSize) + k_ExtraHeight);

        }

        protected virtual void OnGameSettingsFiled(EventGameSettings egs)
        {
            if(SettingsFilled != null)
            {
                SettingsFilled(this, egs);
            }

        }

        private void OnMoved(Move i_move)
        {
            if (Moved != null)
            {
                Moved.Invoke(i_move);
            }
        }

        

        private void OnPopUpBox( )
        {
            throw new NotImplementedException();
        }

    }



}
