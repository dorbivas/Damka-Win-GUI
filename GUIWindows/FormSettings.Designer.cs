namespace GUIWindows
{
    public partial class FormSettings
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="i_DisposeFlag">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool i_DisposeFlag)
        {
            if (i_DisposeFlag && (components != null))
            {
                components.Dispose();
            }

            base.Dispose(i_DisposeFlag);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.buttonDone = new System.Windows.Forms.Button();
            this.radioButtonSmallSize = new System.Windows.Forms.RadioButton();
            this.radioButtonMediumSize = new System.Windows.Forms.RadioButton();
            this.radioButtonLargeSize = new System.Windows.Forms.RadioButton();
            this.labelBoardSize = new System.Windows.Forms.Label();
            this.textBoxPlayer2Name = new System.Windows.Forms.TextBox();
            this.labelPlayers = new System.Windows.Forms.Label();
            this.labelPlayer1 = new System.Windows.Forms.Label();
            this.checkBoxPlayer2 = new System.Windows.Forms.CheckBox();
            this.textBoxPlayer1Name = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // buttonDone
            // 
            this.buttonDone.BackColor = System.Drawing.Color.Lime;
            this.buttonDone.Location = new System.Drawing.Point(212, 257);
            this.buttonDone.Name = "buttonDone";
            this.buttonDone.Size = new System.Drawing.Size(169, 27);
            this.buttonDone.TabIndex = 0;
            this.buttonDone.Text = "Done";
            this.buttonDone.UseVisualStyleBackColor = false;
            this.buttonDone.Click += new System.EventHandler(this.buttonDone_Click);
            // 
            // radioButtonSmallBoardSize
            // 
            this.radioButtonSmallSize.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.radioButtonSmallSize.Checked = true;
            this.radioButtonSmallSize.FlatAppearance.BorderColor = System.Drawing.Color.Blue;
            this.radioButtonSmallSize.FlatAppearance.BorderSize = 10;
            this.radioButtonSmallSize.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.radioButtonSmallSize.Location = new System.Drawing.Point(210, 164);
            this.radioButtonSmallSize.Name = "radioButtonSmallBoardSize";
            this.radioButtonSmallSize.Size = new System.Drawing.Size(59, 32);
            this.radioButtonSmallSize.TabIndex = 1;
            this.radioButtonSmallSize.TabStop = true;
            this.radioButtonSmallSize.Text = "6 x 6";
            this.radioButtonSmallSize.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.radioButtonSmallSize.UseVisualStyleBackColor = false;
            // 
            // radioButtonMediumBoardSize
            // 
            this.radioButtonMediumSize.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.radioButtonMediumSize.FlatAppearance.BorderColor = System.Drawing.Color.Blue;
            this.radioButtonMediumSize.FlatAppearance.BorderSize = 10;
            this.radioButtonMediumSize.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.radioButtonMediumSize.Location = new System.Drawing.Point(272, 164);
            this.radioButtonMediumSize.Name = "radioButtonMediumBoardSize";
            this.radioButtonMediumSize.Size = new System.Drawing.Size(57, 32);
            this.radioButtonMediumSize.TabIndex = 1;
            this.radioButtonMediumSize.Text = "8 x 8";
            this.radioButtonMediumSize.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.radioButtonMediumSize.UseVisualStyleBackColor = false;
            // 
            // radioButtonLargeBoardSize
            // 
            this.radioButtonLargeSize.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.radioButtonLargeSize.FlatAppearance.BorderColor = System.Drawing.Color.Blue;
            this.radioButtonLargeSize.FlatAppearance.BorderSize = 10;
            this.radioButtonLargeSize.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.radioButtonLargeSize.Location = new System.Drawing.Point(331, 164);
            this.radioButtonLargeSize.Name = "radioButtonLargeBoardSize";
            this.radioButtonLargeSize.Size = new System.Drawing.Size(59, 32);
            this.radioButtonLargeSize.TabIndex = 1;
            this.radioButtonLargeSize.Text = "10 x 10";
            this.radioButtonLargeSize.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.radioButtonLargeSize.UseVisualStyleBackColor = false;
            // 
            // labelBoardSize
            // 
            this.labelBoardSize.BackColor = System.Drawing.Color.Bisque;
            this.labelBoardSize.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelBoardSize.Location = new System.Drawing.Point(210, 135);
            this.labelBoardSize.Name = "labelBoardSize";
            this.labelBoardSize.Size = new System.Drawing.Size(180, 26);
            this.labelBoardSize.TabIndex = 2;
            this.labelBoardSize.Text = "Board Size";
            this.labelBoardSize.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textBoxPlayer2Name
            // 
            this.textBoxPlayer2Name.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.textBoxPlayer2Name.Enabled = false;
            this.textBoxPlayer2Name.Location = new System.Drawing.Point(283, 106);
            this.textBoxPlayer2Name.Name = "textBoxPlayer2Name";
            this.textBoxPlayer2Name.Size = new System.Drawing.Size(107, 20);
            this.textBoxPlayer2Name.TabIndex = 3;
            this.textBoxPlayer2Name.Text = "[Deep-blue Computer]";
            // 
            // labelPlayers
            // 
            this.labelPlayers.BackColor = System.Drawing.Color.Bisque;
            this.labelPlayers.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelPlayers.Location = new System.Drawing.Point(212, 48);
            this.labelPlayers.Name = "labelPlayers";
            this.labelPlayers.Size = new System.Drawing.Size(178, 26);
            this.labelPlayers.TabIndex = 2;
            this.labelPlayers.Text = "Players";
            this.labelPlayers.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelPlayer1
            // 
            this.labelPlayer1.AutoSize = true;
            this.labelPlayer1.BackColor = System.Drawing.Color.Transparent;
            this.labelPlayer1.Location = new System.Drawing.Point(229, 83);
            this.labelPlayer1.Name = "labelPlayer1";
            this.labelPlayer1.Size = new System.Drawing.Size(48, 13);
            this.labelPlayer1.TabIndex = 4;
            this.labelPlayer1.Text = "Player 1:";
            // 
            // checkBoxPlayer2
            // 
            this.checkBoxPlayer2.AutoSize = true;
            this.checkBoxPlayer2.BackColor = System.Drawing.Color.Transparent;
            this.checkBoxPlayer2.Location = new System.Drawing.Point(210, 109);
            this.checkBoxPlayer2.Name = "checkBoxPlayer2";
            this.checkBoxPlayer2.Size = new System.Drawing.Size(67, 17);
            this.checkBoxPlayer2.TabIndex = 5;
            this.checkBoxPlayer2.Text = "Player 2:";
            this.checkBoxPlayer2.UseVisualStyleBackColor = false;
            this.checkBoxPlayer2.CheckedChanged += new System.EventHandler(this.checkBoxPlayer2_CheckedChanged);
            // 
            // textBoxPlayer1Name
            // 
            this.textBoxPlayer1Name.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.textBoxPlayer1Name.Location = new System.Drawing.Point(283, 80);
            this.textBoxPlayer1Name.Name = "textBoxPlayer1Name";
            this.textBoxPlayer1Name.Size = new System.Drawing.Size(107, 20);
            this.textBoxPlayer1Name.TabIndex = 3;
            // 
            // FormSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::GUIWindows.ResourceGK.gk;
            this.ClientSize = new System.Drawing.Size(393, 296);
            this.Controls.Add(this.checkBoxPlayer2);
            this.Controls.Add(this.labelPlayer1);
            this.Controls.Add(this.textBoxPlayer2Name);
            this.Controls.Add(this.textBoxPlayer1Name);
            this.Controls.Add(this.labelPlayers);
            this.Controls.Add(this.labelBoardSize);
            this.Controls.Add(this.radioButtonLargeSize);
            this.Controls.Add(this.radioButtonMediumSize);
            this.Controls.Add(this.radioButtonSmallSize);
            this.Controls.Add(this.buttonDone);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FormSettings";
            this.RightToLeftLayout = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Game Settings";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonDone;
        private System.Windows.Forms.RadioButton radioButtonSmallSize;
        private System.Windows.Forms.RadioButton radioButtonMediumSize;
        private System.Windows.Forms.RadioButton radioButtonLargeSize;
        private System.Windows.Forms.Label labelBoardSize;
        private System.Windows.Forms.TextBox textBoxPlayer2Name;
        private System.Windows.Forms.Label labelPlayers;
        private System.Windows.Forms.Label labelPlayer1;
        private System.Windows.Forms.CheckBox checkBoxPlayer2;
        private System.Windows.Forms.TextBox textBoxPlayer1Name;
    }
}