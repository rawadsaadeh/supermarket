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

namespace Supermarketapp
{
    class emp
    {


        public void updateEmployee( string [] EmployeeiNFO)
        {
            cnx con = new cnx();
            MySqlConnection conn2 = con.conx();
            string query;
            if (EmployeeiNFO[2] == "")
            {
                query = "UPDATE employees SET employee_name = '"+EmployeeiNFO[0]+"' , username = '"+EmployeeiNFO[1]+"' WHERE username = '"+EmployeeiNFO[1]+ "' ; ";
            }
            else 
            {
                query = "UPDATE employees SET employee_name = '" + EmployeeiNFO[0] + "' , username = '" + EmployeeiNFO[1] + "' ,psd ='" + EmployeeiNFO[2] + "' WHERE username = '" + EmployeeiNFO[1] + "' ; ";
            }
            
            MySqlCommand cmd = new MySqlCommand(query, conn2);
            conn2.Open();
            cmd.ExecuteReader();
            conn2.Close();
        }



        public void addEmployee(string[] EmployeeiNFO)
        {
            cnx con = new cnx();
            MySqlConnection conn2 = con.conx();
            string query;
            query = "insert into employees (employee_name,username,psd,employee_position)values ('" + EmployeeiNFO[0] + "','" + EmployeeiNFO[1] + "','" + EmployeeiNFO[2] + "','" + EmployeeiNFO[3] + "');";

            MySqlCommand cmd = new MySqlCommand(query, conn2);
            conn2.Open();
            cmd.ExecuteReader();
            conn2.Close();
        }


        public string[] getEmployeeDetails(string username)
        {
            cnx con = new cnx();
            MySqlConnection conn2 = con.conx();
            string query;
            query = "select id,employee_name,employee_position,username from employees where username like '" + username + "';";
            MySqlCommand cmd = new MySqlCommand(query, conn2);
            MySqlDataReader reader;
            conn2.Open();
            reader = cmd.ExecuteReader();
            try
            {
                string[] emp = new string[5];

                if (reader.HasRows)
                {

                    while (reader.Read())
                    {
                        emp[0] = reader[0].ToString();
                        emp[1] = reader[1].ToString();
                        emp[2] = reader[2].ToString();
                        emp[3] = reader[3].ToString();
                    }
                    conn2.Close();
                }
                return emp;

            }

            catch
            {
                return null;
            }
            return null;
        }
    }
}
