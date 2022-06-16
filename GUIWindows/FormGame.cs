using GameEngine;
using System;
using System.Drawing;
using System.Windows.Forms;
using System.Text;


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
        private const int k_Suspension = 130;


        private readonly FormSettings r_FormSettings = new FormSettings();
        private readonly Label labelPlayer1 = new Label();
        private readonly Label labelPlayer2 = new Label();
        private PictureBoxPiece[,] m_PictureBoxBoard;
        private EventGameSettings m_EventGameSettings;
        private bool isSecondClick = false;
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

        private void initialzeNewGameForm()
        {
            //initializeGameDetails();
            setBoardFormSize();
            initializeBoardPictureBox();
        }

        private void initializeBoardPictureBox()
        {
            bool isNewLine = false, isFirstPictureBox = true;
            PictureBox lastlPiece = new PictureBox();
            PictureBoxPiece newPiece;
            m_PictureBoxBoard = new PictureBoxPiece[(int)m_EventGameSettings.BoardSize, (int)m_EventGameSettings.BoardSize];

            for (int i = 0; i < ((int)m_EventGameSettings.BoardSize); i++)
            {
                for (int j = 0; j < ((int)m_EventGameSettings.BoardSize); j++)
                {
                    newPiece = new PictureBoxPiece(i, j);
                    setPictureBoxLocation(newPiece, isNewLine, isFirstPictureBox, lastlPiece);
                    setPictureBox(newPiece);
                    m_PictureBoxBoard[i, j] = newPiece;
                    Controls.Add(newPiece);
                    isNewLine = false;
                    isFirstPictureBox = false;
                    lastlPiece = newPiece;
                }

                isNewLine = true;
            }
         
        }

        public void UpdatePictureBoxBoard(Board i_Board)
        {
            Piece.ePieceType PieceType = Piece.ePieceType.Empty;
            bool enablePiece = false;
            string cellImage = string.Empty;

            for (int i = 0; i < i_Board.BoardSize; i++)
            {
                for (int j = 0; j < i_Board.BoardSize; j++)
                {
                    PieceType = i_Board.GetPieceType(i, j);
                    if (Board.IsDiffrentType(i, j))
                    {
                        switch (PieceType)
                        {
                            case Piece.ePieceType.Empty:
                                enablePiece = true;
                                cellImage = Sources.Empty;
                                break;
                            case Piece.ePieceType.PieceO:
                                enablePiece = m_EventGameSettings.CurrentPlayerNumber == Player.ePlayerNumber.PlayerTwoO;
                                cellImage = PieceType == Piece.ePieceType.PieceO ? Sources.WhitePiece : Sources.WhiteKingPiece;
                                break;
                            case Piece.ePieceType.PieceX:
                                enablePiece = m_EventGameSettings.CurrentPlayerNumber == Player.ePlayerNumber.PlayerOneX;
                                cellImage = PieceType == Piece.ePieceType.PieceX ? Sources.BlackPiece : Sources.BlackKingPiece;
                                break;
                            default:
                                break;
                        }
                    }
                    else
                    {
                        enablePiece = false;
                        cellImage = Sources.NullCell;
                    }
                    
                    m_PictureBoxBoard[i, j].SetPictureBoxCell(cellImage, enablePiece, PieceType);
                }

                if (m_EventGameSettings.CurrentPlayer == Enum.GetName(typeof(Player.ePlayerType), Player.ePlayerType.Computer))
                {
                    System.Threading.Thread.Sleep(k_Suspension);
                }
            }
        }

        //TODO fix it
        private void setPictureBox(PictureBoxPiece io_CurrentPictureBox)
        {
            io_CurrentPictureBox.Size = new Size(k_PictureBoxSize, k_PictureBoxSize);
            io_CurrentPictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
            io_CurrentPictureBox.Enabled = false;
            io_CurrentPictureBox.Click += pictureBox_Click;
        }

        private void setPictureBoxLocation(PictureBoxPiece io_CurrentPictureBox, bool i_IsNewLine,
                                           bool i_IsFirstPictureBox, PictureBox i_LastlPiece)
        {
            Point newLocation;

            if (i_IsFirstPictureBox)
            {
                newLocation = new Point(k_StartingPictureBoxX, k_StartingPictureBoxY);
            }
            else
            {
                newLocation = i_LastlPiece.Location;
                if (!i_IsNewLine)
                {
                    newLocation.Offset(i_LastlPiece.Width, 0);
                }
                else
                {
                    newLocation.X = k_StartingPictureBoxX;
                    newLocation.Offset(0, i_LastlPiece.Height);
                }
            }

            io_CurrentPictureBox.Location = newLocation;
        }

        private void pictureBox_Click(object sender, EventArgs e)
        {
            PictureBoxPiece piecePressed = sender as PictureBoxPiece;

            if (!isSecondClick)
            {
                if (piecePressed != null)
                {
                    m_PictureBoxPressed = piecePressed;
                    //m_PictureBoxPressed.Enabled = false;
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
                        m_PictureBoxPressed.BorderStyle = BorderStyle.None;
                        //m_PictureBoxPressed.Enabled = true;
                        m_PictureBoxPressed = null;
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
            //setPlayerLables();
            initialzeNewGameForm();
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

        private void setBoardFormSize()
        {
            this.Size = new Size(((int)r_FormSettings.BoardSize * k_PictureBoxSize) + k_ExtraWidth,
                                 ((int)r_FormSettings.BoardSize * k_PictureBoxSize) + k_ExtraHeight);

        }

        protected virtual void OnGameSettingsFiled(EventGameSettings egs)
        {
            if (SettingsFilled != null)
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

        private void OnPopUpBox()
        {
            throw new NotImplementedException();
        }

        public void SwitchPlayers()
        {
            string playerNameSaver = m_EventGameSettings.CurrentPlayer;

            m_EventGameSettings.CurrentPlayer = m_EventGameSettings.NextPlayer;
            m_EventGameSettings.NextPlayer = playerNameSaver;
            m_EventGameSettings.CurrentPlayerNumber = m_EventGameSettings.CurrentPlayerNumber == Player.ePlayerNumber.PlayerOneX ? Player.ePlayerNumber.PlayerTwoO : Player.ePlayerNumber.PlayerOneX;
            //TODO
            //this.labelPlayer1Name.ForeColor = this.labelPlayer1Name.ForeColor == Color.Blue ? Color.Black : Color.Blue;
            //this.labelPlayer2Name.ForeColor = this.labelPlayer2Name.ForeColor == Color.Blue ? Color.Black : Color.Blue;
        }
    }
}
