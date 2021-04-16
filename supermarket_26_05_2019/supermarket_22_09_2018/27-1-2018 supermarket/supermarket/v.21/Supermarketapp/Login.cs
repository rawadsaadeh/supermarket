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
    public partial class Login : Form
    {
        public static string username_loged;
        public static string userposition_loged;
        public static string userid_loged;
        int tryLog = 4;
        String cs2 = "server=127.0.0.1;uid=root;" + "pwd='';database=supermarket;";
        MySqlConnection conn2 = null;
        public  string employee;
        
        string cpuid ="";
        string hdid = "";
        public Login()
        {
           InitializeComponent();

           cpuid = checkcpu();
           Pass.PasswordChar = '*';
           hdid = checkhd();
            Pass.Text="";
           
        }




        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
        
      
        }


        private void button1_Click(object sender, EventArgs e)
        {
            if (Pass.Text == "" || UserNm.Text == "")
            {
                MessageBox.Show("One or more fields are empty");
            }
            else
            {
                string access = "";
                string employeePosition = "";
                string employeeName = "";
                string employeeId = "";
                conn2 = new MySqlConnection(cs2);
                conn2.Open();
                MySqlCommand cmd = new MySqlCommand();
                MySqlCommand cmd01 = new MySqlCommand();
                cmd.Connection = conn2;
                MySqlDataReader reader;
                MD5 md5Hash = MD5.Create();

                string passwordHash = GetMd5Hash(md5Hash, Pass.Text.ToString());

                cmd.CommandText = "select count(*),employee_position,username,id from employees where username='" + UserNm.Text.ToString() + "' and psd='" + passwordHash + "' ";
                reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        access = reader[0].ToString();
                        employeePosition = reader[1].ToString();
                        employeeName = reader[2].ToString();
                        employeeId = reader[3].ToString();
                    }
                    conn2.Close();
                }
                if (access.CompareTo("1") == 0)
                {
                    username_loged = employeeName;
                    userposition_loged = employeePosition;
                    userid_loged = employeeId;
                    employee = employeeName;
                    if (employeePosition.CompareTo("manager") == 0)
                    {
                        this.Hide();

                        HomeMan.ActiveForm();

                    }

                    else
                    {
                        this.Hide();

                        Home_Emp.ActiveForm();

                    }
                }

                else
                {
                    tryLog--;
                    MessageBox.Show("Wrong user name or password !! Remaining " + tryLog);
                    if (tryLog == 0)
                    {
                        Application.Exit();
                    }
                }
            }
            
        }

        private void Login_Load(object sender, EventArgs e)
        {
            //if (cpuid.CompareTo("BFEBFBFF000406C4") == 0 & hdid.CompareTo("286FD3D3") == 0)
            //{
                conn2 = new MySqlConnection(cs2);
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
                        UserNm.Items.Add(reader[0]);
                    }
                    conn2.Close();
                }
            //}


            //else
            //{
            //    MessageBox.Show("Could not start application");
            //    Application.Exit();
            //    System.Windows.Forms.Application.ExitThread();
            //    this.Close();
            //    System.Environment.Exit(0);  
            //} 
        }

        public string sendemp()
        {
            return employee;
        }

        internal static void ActiveForm()
        {
          //  throw new NotImplementedException();
            Login l1 = new Login();
            l1.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }



        private string checkcpu()
        {
            string cpuInfo = string.Empty;
            ManagementClass mc = new ManagementClass("win32_processor");
            ManagementObjectCollection moc = mc.GetInstances();

            foreach (ManagementObject mo in moc)
            {
                cpuInfo = mo.Properties["processorID"].Value.ToString();
                break;
            }
            return cpuInfo;
        }

        private string checkhd()
        {
            string drive = "C";
            ManagementObject dsk = new ManagementObject(
                @"win32_logicaldisk.deviceid=""" + drive + @":""");
            dsk.Get();
            string volumeSerial = dsk["VolumeSerialNumber"].ToString();
            return volumeSerial;
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

        private void Login_KeyDown(object sender, KeyEventArgs e)
        {
        }

        private void Pass_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (Pass.Text != "")
                {
                    button1.PerformClick();
                }

            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
