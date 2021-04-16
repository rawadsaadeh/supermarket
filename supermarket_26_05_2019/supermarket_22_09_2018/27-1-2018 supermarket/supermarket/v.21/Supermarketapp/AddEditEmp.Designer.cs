namespace Supermarketapp
{
    partial class AddEditEmp
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
            this.button1 = new System.Windows.Forms.Button();
            this.username = new System.Windows.Forms.TextBox();
            this.pass = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.positionsDD = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.employeesDD = new System.Windows.Forms.ComboBox();
            this.empName = new System.Windows.Forms.Label();
            this.employeename = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(204, 216);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(163, 54);
            this.button1.TabIndex = 0;
            this.button1.Text = "Submit";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // username
            // 
            this.username.Location = new System.Drawing.Point(204, 64);
            this.username.Name = "username";
            this.username.Size = new System.Drawing.Size(163, 22);
            this.username.TabIndex = 1;
            // 
            // pass
            // 
            this.pass.Location = new System.Drawing.Point(204, 160);
            this.pass.Name = "pass";
            this.pass.Size = new System.Drawing.Size(163, 22);
            this.pass.TabIndex = 2;
            this.pass.TextChanged += new System.EventHandler(this.pass_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(59, 69);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 17);
            this.label1.TabIndex = 3;
            this.label1.Text = "User Name";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(61, 165);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 17);
            this.label2.TabIndex = 4;
            this.label2.Text = "Password";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(59, 19);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(58, 17);
            this.label4.TabIndex = 7;
            this.label4.Text = "Position";
            this.label4.Visible = false;
            this.label4.Click += new System.EventHandler(this.label4_Click);
            // 
            // positionsDD
            // 
            this.positionsDD.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.positionsDD.FormattingEnabled = true;
            this.positionsDD.Items.AddRange(new object[] {
            "manager",
            "employee"});
            this.positionsDD.Location = new System.Drawing.Point(204, 16);
            this.positionsDD.Name = "positionsDD";
            this.positionsDD.Size = new System.Drawing.Size(163, 24);
            this.positionsDD.TabIndex = 8;
            this.positionsDD.Visible = false;
            this.positionsDD.SelectedIndexChanged += new System.EventHandler(this.positionsDD_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(59, 23);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(77, 17);
            this.label5.TabIndex = 9;
            this.label5.Text = "Employees";
            this.label5.Visible = false;
            // 
            // employeesDD
            // 
            this.employeesDD.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.employeesDD.FormattingEnabled = true;
            this.employeesDD.Location = new System.Drawing.Point(204, 16);
            this.employeesDD.Name = "employeesDD";
            this.employeesDD.Size = new System.Drawing.Size(163, 24);
            this.employeesDD.TabIndex = 10;
            this.employeesDD.Visible = false;
            this.employeesDD.SelectedIndexChanged += new System.EventHandler(this.employeesDD_SelectedIndexChanged);
            // 
            // empName
            // 
            this.empName.AutoSize = true;
            this.empName.Location = new System.Drawing.Point(59, 116);
            this.empName.Name = "empName";
            this.empName.Size = new System.Drawing.Size(111, 17);
            this.empName.TabIndex = 11;
            this.empName.Text = "Employee Name";
            // 
            // employeename
            // 
            this.employeename.Location = new System.Drawing.Point(204, 111);
            this.employeename.Name = "employeename";
            this.employeename.Size = new System.Drawing.Size(163, 22);
            this.employeename.TabIndex = 12;
            // 
            // AddEditEmp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(427, 317);
            this.Controls.Add(this.employeename);
            this.Controls.Add(this.empName);
            this.Controls.Add(this.employeesDD);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.positionsDD);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pass);
            this.Controls.Add(this.username);
            this.Controls.Add(this.button1);
            this.Name = "AddEditEmp";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AddEmp";
            this.Load += new System.EventHandler(this.AddEditEmp_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox username;
        private System.Windows.Forms.TextBox pass;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox positionsDD;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox employeesDD;
        private System.Windows.Forms.Label empName;
        private System.Windows.Forms.TextBox employeename;
    }
}