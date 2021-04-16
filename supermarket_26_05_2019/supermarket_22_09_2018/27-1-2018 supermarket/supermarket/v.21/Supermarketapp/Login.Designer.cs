namespace Supermarketapp
{
    partial class Login
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.UserNm = new System.Windows.Forms.ComboBox();
            this.Pass = new System.Windows.Forms.TextBox();
            this.PSD = new System.Windows.Forms.Label();
            this.Username = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // UserNm
            // 
            this.UserNm.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.UserNm.FormattingEnabled = true;
            this.UserNm.Location = new System.Drawing.Point(323, 28);
            this.UserNm.Margin = new System.Windows.Forms.Padding(4);
            this.UserNm.Name = "UserNm";
            this.UserNm.Size = new System.Drawing.Size(160, 24);
            this.UserNm.TabIndex = 0;
            this.UserNm.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // Pass
            // 
            this.Pass.Location = new System.Drawing.Point(323, 96);
            this.Pass.Margin = new System.Windows.Forms.Padding(4);
            this.Pass.Name = "Pass";
            this.Pass.Size = new System.Drawing.Size(160, 22);
            this.Pass.TabIndex = 1;
            this.Pass.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Pass_KeyDown_1);
            // 
            // PSD
            // 
            this.PSD.AutoSize = true;
            this.PSD.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PSD.ForeColor = System.Drawing.Color.Black;
            this.PSD.Location = new System.Drawing.Point(185, 96);
            this.PSD.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.PSD.Name = "PSD";
            this.PSD.Size = new System.Drawing.Size(83, 20);
            this.PSD.TabIndex = 3;
            this.PSD.Text = "Password";
            // 
            // Username
            // 
            this.Username.AutoSize = true;
            this.Username.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Username.Location = new System.Drawing.Point(187, 28);
            this.Username.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Username.Name = "Username";
            this.Username.Size = new System.Drawing.Size(86, 20);
            this.Username.TabIndex = 4;
            this.Username.Text = "Username";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(323, 135);
            this.button1.Margin = new System.Windows.Forms.Padding(4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(161, 59);
            this.button1.TabIndex = 5;
            this.button1.Text = "Login";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(323, 208);
            this.button2.Margin = new System.Windows.Forms.Padding(4);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(161, 28);
            this.button2.TabIndex = 6;
            this.button2.Text = "Exit";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(41, 207);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(121, 25);
            this.label1.TabIndex = 7;
            this.label1.Text = "Diet Center";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // Login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(552, 251);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.Username);
            this.Controls.Add(this.PSD);
            this.Controls.Add(this.Pass);
            this.Controls.Add(this.UserNm);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Login";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Diet Center";
            this.Load += new System.EventHandler(this.Login_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Login_KeyDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox UserNm;
        private System.Windows.Forms.TextBox Pass;
        private System.Windows.Forms.Label PSD;
        private System.Windows.Forms.Label Username;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label1;
    }
}

