using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Supermarketapp
{
    public partial class Home_Emp : Form
    {
        public Home_Emp()
        {
            InitializeComponent();
          //  label1.ForeColor = System.Drawing.Color.Black;
        }

        internal static void ActiveForm()
        {
            //throw new NotImplementedException();
            Home_Emp he = new Home_Emp();
            he.ShowDialog();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            
            Invoices.ActiveForm();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
           // this.Hide();
            items.ActiveForm();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Login.ActiveForm();
        }

        private void pictureBox1_MouseHover(object sender, EventArgs e)
        {
           // label1.ForeColor = System.Drawing.Color.Red;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Access Denied !!");
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Access Denied !!");
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Access Denied !!");
        }
    }
}
