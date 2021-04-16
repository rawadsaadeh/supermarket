using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Management;
using System.Management.Instrumentation; 
using System.Configuration;
using System.Security.Cryptography;

namespace Supermarketapp
{
    public partial class AddEditEmp : Form
    {
        String cs2 = "server=127.0.0.1;uid=root;" + "pwd='';database=supermarket;";

        public AddEditEmp()
        {
            InitializeComponent();
        }

        internal static void ActiveForm()
        {
            //throw new NotImplementedException();
            AddEditEmp aee = new AddEditEmp();
            aee.ShowDialog();
        }

       


        private void AddEditEmp_Load(object sender, EventArgs e)
        {
            string userEmpAction;
            userEmpAction =  chooseEmployeeAction.userEmpAction;
            if (userEmpAction.CompareTo("edit") == 0)
            {
                label5.Visible = true;
                employeesDD.Visible = true;
                MySqlConnection conn2 = new MySqlConnection(cs2);
                conn2.Open();
                MySqlCommand cmd = new MySqlCommand();
                MySqlCommand cmd01 = new MySqlCommand();
                cmd.Connection = conn2;
                MySqlDataReader reader;
                cmd.CommandText = "select username from employees;";
                reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        employeesDD.Items.Add(reader[0]);
                    }
                    conn2.Close();
                }

            }
            else 
            {
                label4.Visible = true;
                positionsDD.Visible = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
          
            string userEmpAction;
            userEmpAction = chooseEmployeeAction.userEmpAction;
            if (userEmpAction.CompareTo("edit") == 0)
            {
                if (employeename.Text == "" || username.Text == "")
                {
                    MessageBox.Show("Missing fields");
                }
                else
                {

                    string[] EmpInfoUpdated = new string[3];
                    EmpInfoUpdated[0] = employeename.Text;
                    EmpInfoUpdated[1] = username.Text;

                    MD5 md5Hash = MD5.Create();

                    string passwordHash = GetMd5Hash(md5Hash, pass.Text.ToString());

                    EmpInfoUpdated[2] = passwordHash;
                    emp employee = new emp();
                    employee.updateEmployee(EmpInfoUpdated);
                    MessageBox.Show("Success");
                    this.Close();
                }
            }
            else
            {
                if (employeename.Text == "" || username.Text == "" || pass.Text == "" || positionsDD.Text == "")
                {
                    MessageBox.Show("Missing fields");
                }
                else
                {
                    string[] EmpInfoUpdated = new string[4];
                    EmpInfoUpdated[0] = employeename.Text;
                    EmpInfoUpdated[1] = username.Text;

                    MD5 md5Hash = MD5.Create();

                    string passwordHash = GetMd5Hash(md5Hash, pass.Text.ToString());

                    EmpInfoUpdated[2] = passwordHash;
                    EmpInfoUpdated[3] = positionsDD.Text;
                    emp employee = new emp();
                    employee.addEmployee(EmpInfoUpdated);
                    MessageBox.Show("Success");
                    this.Close();
                }
            }
        }

        private void employeesDD_SelectedIndexChanged(object sender, EventArgs e)
        {
            emp employee = new emp();
            string[] EmployeeInfo = new string[4];
            EmployeeInfo=employee.getEmployeeDetails(employeesDD.Text.ToString());
            username.Text = EmployeeInfo[3];
            employeename.Text = EmployeeInfo[1];
            positionsDD.Text = EmployeeInfo[2];
        }

        private void positionsDD_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void pass_TextChanged(object sender, EventArgs e)
        {
        
        }

        private void cpass_TextChanged(object sender, EventArgs e)
        {
        
        }
        static string GetMd5Hash(MD5 md5Hash, string input)
        {

            // Convert the input string to a byte array and compute the hash.
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data 
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string.
            return sBuilder.ToString();
        }



    }
}
