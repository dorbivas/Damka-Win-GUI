namespace GUIWindows
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;
    using GameEngine;

    public partial class FormSettings : Form
    {
        public FormSettings()
        {
            InitializeComponent();
        }

        public Button ButtonDone
        {
            get => buttonDone;
        }

        public string Player1Name
        {
            get => textBoxPlayer1Name.Text;
            set => textBoxPlayer1Name.Text = value;
        }

        public string Player2Name
        {
            get => textBoxPlayer2Name.Text;
            set => textBoxPlayer2Name.Text = value;
        }

        public bool IsPlayer2PC
        {
            get => !checkBoxPlayer2.Checked;
        }

        public Board.eBoardSizes BoardSize
        {
            get
            {
                Board.eBoardSizes boardSize;

                if (radioButtonSmallSize.Checked)
                {
                    boardSize = GameEngine.Board.eBoardSizes.Small;
                }
                else if (radioButtonMediumSize.Checked)
                {
                    boardSize = GameEngine.Board.eBoardSizes.Medium;
                }
                else
                {
                    boardSize = GameEngine.Board.eBoardSizes.Large;
                }

                return boardSize;
            }
        }

        private void buttonDone_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxPlayer1Name.Text) || string.IsNullOrEmpty(textBoxPlayer2Name.Text))
            {
                MessageBox.Show("Fill the players names");
            }
            else
            {
                this.Close();
            }
        }

        private void checkBoxPlayer2_CheckedChanged(object sender, EventArgs e)
        {
            textBoxPlayer2Name.Enabled = textBoxPlayer2Name.Enabled != true;

            if (textBoxPlayer2Name.Enabled)
            {
                this.textBoxPlayer2Name.BackColor = Color.White;
                this.textBoxPlayer2Name.Text = string.Empty;
            }
            else
            {
                this.textBoxPlayer2Name.BackColor = SystemColors.MenuBar;
                this.textBoxPlayer2Name.Text = "[Deep-blue Computer]";
            }
        }
    }
}