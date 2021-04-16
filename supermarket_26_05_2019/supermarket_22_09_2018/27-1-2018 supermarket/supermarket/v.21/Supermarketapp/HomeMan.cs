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
    public partial class HomeMan : Form
    {
        public HomeMan()
        {
            InitializeComponent();
             Login l1 = new Login();
             //label6.Text=l1.sendemp();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        internal static void ActiveForm()
        {
            HomeMan h1 = new HomeMan();
             h1.ShowDialog();
        }

        private void Home_Load(object sender, EventArgs e)
        {
            
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            //this.Hide();
            Reports.ActiveForm();
        }

        private void pictureBox2_MouseHover(object sender, EventArgs e)
        {
           
        }

        private void acc_Click(object sender, EventArgs e)
        {
            Invoices.ActiveForm();
           // this.Hide();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            Login.ActiveForm();
        }

        private void label6_Click(object sender, EventArgs e)
        {
           
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            items.ActiveForm();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            chooseEmployeeAction.ActiveForm();
        }

      
    }
}
