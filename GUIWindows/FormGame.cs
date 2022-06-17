namespace GUIWindows
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;
    using GameEngine;

    public partial class FormGame : Form
    {
        public event EventHandler SettingsFilled, MessageBoxInteractions;

        public event Action<Move> Moved;

        private const int k_PictureBoxSize = 50;
        private const int k_ExtraWidth = 20;
        private const int k_ExtraHeight = 100;
        private const int k_StartingPictureBoxX = 0;
        private const int k_StartingPictureBoxY = 40;
        private readonly FormSettings r_FormSettings = new FormSettings();
        private Label labelPlayer1Name;
        private Label labelPlayer2Name;
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

        public void ResetEventFormGameSettings()
        {
            OnGameSettingsFiled(m_EventGameSettings);
        }

        public void InitialzeNewGameForm()
        {
            setBoardFormSize();
            initializeBoardPictureBox();
        }
        
        public void r_FormSettings_Closed(object sender, FormClosedEventArgs e)
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
                r_FormSettings.IsPlayer2PC ? Player.ePlayerType.Computer : Player.ePlayerType.Human);
            InitialzeNewGameForm();
            OnGameSettingsFiled(m_EventGameSettings);
        }

        public void KeepSessionInformation(int i_Player1Score, int i_Player2Score, string i_CurrentPlayerName)
        {
            m_EventGameSettings.SetPlayers(i_CurrentPlayerName);
            setLabels(i_Player1Score, i_Player2Score);
        }
        
        public void UpdateBoardUI(Board i_Board)
        {
            Piece.ePieceType PieceType;
            bool isPieceEnabled = false;
            string cellImage = string.Empty;

            for (int i = 0; i < i_Board.BoardSize; i++)
            {
                for (int j = 0; j < i_Board.BoardSize; j++)
                {
                    PieceType = i_Board.GetPieceType(i, j);
                    if (Board.IsNotSameColor(i, j))
                    {
                        switch (PieceType)
                        {
                            case Piece.ePieceType.Empty:
                                isPieceEnabled = true;
                                cellImage = Sources.Empty;
                                break;
                            case Piece.ePieceType.PieceO:
                                isPieceEnabled = m_EventGameSettings.CurrentPlayerNumber == Player.ePlayerNumber.PlayerTwoO;
                                cellImage = PieceType == Piece.ePieceType.PieceO ? Sources.WhitePiece : Sources.WhiteKingPiece;
                                break;
                            case Piece.ePieceType.PieceX:
                                isPieceEnabled = m_EventGameSettings.CurrentPlayerNumber == Player.ePlayerNumber.PlayerOneX;
                                cellImage = PieceType == Piece.ePieceType.PieceX ? Sources.BlackPiece : Sources.BlackKingPiece;
                                break;
                            case Piece.ePieceType.KingO:
                                isPieceEnabled = true;
                                cellImage = Sources.WhiteKingPiece;
                                break;
                            case Piece.ePieceType.KingX:
                                isPieceEnabled = true;
                                cellImage = Sources.BlackKingPiece;
                                break;
                            default:
                                break;
                        }
                    }
                    else
                    {
                        isPieceEnabled = false;
                        cellImage = Sources.NullCell;
                    }

                    m_PictureBoxBoard[i, j].SetPictureBoxCell(cellImage, isPieceEnabled, PieceType);
                }
            }
        }

        public void SwitchPlayers()
        {
            string playerName = m_EventGameSettings.CurrentPlayer;

            m_EventGameSettings.CurrentPlayer = m_EventGameSettings.NextPlayer;
            m_EventGameSettings.NextPlayer = playerName;
            m_EventGameSettings.CurrentPlayerNumber = m_EventGameSettings.CurrentPlayerNumber == Player.ePlayerNumber.PlayerOneX ? Player.ePlayerNumber.PlayerTwoO : Player.ePlayerNumber.PlayerOneX;
        }

        public void CreateYesNoMessageBox(string i_MessageBoxString, string i_Caption)
        {
            DialogResult dialogResult = MessageBox.Show(i_MessageBoxString, i_Caption, MessageBoxButtons.YesNo);
            MessageBoxYesNoEvent mbyne;
            bool isMessageBoxAnswerIsYes = false;

            if (dialogResult == DialogResult.Yes)
            {
                isMessageBoxAnswerIsYes = true;
            }
            else if (dialogResult == DialogResult.No)
            {
                isMessageBoxAnswerIsYes = false;
            }

            mbyne = new MessageBoxYesNoEvent(isMessageBoxAnswerIsYes);
            OnMessageBoxYesNoAnswered(mbyne);
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
                    handleSinglePictureBoxPiece(ref isNewLine, ref isFirstPictureBox, ref lastlPiece, newPiece, i, j);
                }

                isNewLine = true;
            }
        }

        private void handleSinglePictureBoxPiece(ref bool i_IsNewLine, ref bool i_IsFirstPictureBox, ref PictureBox io_LastlPiece, PictureBoxPiece io_NewPiece, int i, int j)
        {
            setPictureBoxLocation(io_NewPiece, i_IsNewLine, i_IsFirstPictureBox, io_LastlPiece);
            setPictureBox(io_NewPiece);
            m_PictureBoxBoard[i, j] = io_NewPiece;
            Controls.Add(io_NewPiece);
            i_IsNewLine = false;
            i_IsFirstPictureBox = false;
            io_LastlPiece = io_NewPiece;
        }

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

                    m_PictureBoxPressed.BorderStyle = BorderStyle.None;
                    m_PictureBoxPressed = null;
                    isSecondClick = false;
                }
            }
        }

        private void FormGame_OnLoad(object sender, EventArgs e)
        {
            r_FormSettings.FormClosed += r_FormSettings_Closed;
            r_FormSettings.ShowDialog();
        }

        private void setBoardFormSize()
        {
            this.Size = new Size(((int)r_FormSettings.BoardSize * k_PictureBoxSize) + k_ExtraWidth,
                                 ((int)r_FormSettings.BoardSize * k_PictureBoxSize) + k_ExtraHeight);
        }

        private void setLabels(int i_Player1Score, int i_Player2Score)
        {
            if (r_FormSettings.IsPlayer2PC)
            {
                this.labelPlayer2Name.Text = string.Format("Deep-blue AI: {0}", i_Player2Score);
            }
            else
            {
                this.labelPlayer2Name.Text = string.Format("{0}: {1}", r_FormSettings.Player2Name, i_Player2Score);
            }

            labelPlayer1Name.Text = string.Format("{0}: {1}", r_FormSettings.Player1Name, i_Player1Score);
            labelPlayer1Name.ForeColor = Color.Black;
            labelPlayer2Name.ForeColor = Color.Black;
        }

        private void OnMessageBoxYesNoAnswered(MessageBoxYesNoEvent mbyne)
        {
            if (MessageBoxInteractions != null)
            {
                MessageBoxInteractions.Invoke(this, mbyne);
            }
        }

        private void OnGameSettingsFiled(EventGameSettings egs)
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
    }
}
