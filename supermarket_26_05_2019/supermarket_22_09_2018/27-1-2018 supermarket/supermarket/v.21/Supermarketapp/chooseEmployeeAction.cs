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
    public partial class chooseEmployeeAction : Form
    {
        public static string userEmpAction;
        public chooseEmployeeAction()
        {
            InitializeComponent();
        }

        public void Add_Click(object sender, EventArgs e)
        {
            userEmpAction = "add";
            this.Hide();
            AddEditEmp.ActiveForm();  
        }

        private void Edit_Click(object sender, EventArgs e)
        {
            userEmpAction = "edit";
            this.Hide();
            AddEditEmp.ActiveForm();
        }

        internal static void ActiveForm()
        {
            //throw new NotImplementedException();
            chooseEmployeeAction cea = new chooseEmployeeAction();
            cea.ShowDialog();
        }

        private void chooseEmployeeAction_Load(object sender, EventArgs e)
        {

        }
    }
}
