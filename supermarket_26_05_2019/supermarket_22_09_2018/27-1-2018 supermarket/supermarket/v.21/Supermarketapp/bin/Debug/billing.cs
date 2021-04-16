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
using System.Collections;


namespace Supermarketapp
{
    class billing
    {
        string[] pn = new string[3];
        string item_p;
        string item_n;
        string item_id;
        string[] failed = {"-1"};
        
        public string[] check_item_barcode(string item_name,int withbarcode)
        {
            string query = "";
            if (withbarcode == 0)
            {
                 query = "select item_name,item_price,item_id from items where item_name like '" + item_name + "';";
            }

            else if (withbarcode == 1)
            {
                 query = "select item_name,item_price,item_id from items where item_id=" + item_name;
            }

            String cs2 = "server=127.0.0.1;uid=root;" + "pwd='';database=supermarket;";
            MySqlConnection conn2 = null;
            conn2 = new MySqlConnection(cs2);
            conn2.Open();
            MySqlCommand cmd = new MySqlCommand();
            MySqlCommand cmd01 = new MySqlCommand();
            cmd.Connection = conn2;
            MySqlDataReader reader;
            
            try
            {
                cmd.CommandText = query;
                item_p = null;
                item_n = null;
                item_id = null;
                reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {            
                        item_n = reader[0].ToString();
                        item_p = reader[1].ToString();
                        item_id = reader[2].ToString();
                        pn[0] = item_n;
                        pn[1] = item_p;
                        pn[2] = item_id;
                    }
                    conn2.Close();
                    return pn;
                }            
                return failed;
            }
            
            catch
            {
                MessageBox.Show("Error,Please enter a number");
                return null;
            }
           
        }

        public int select_invoice()
        {
            int maxinvoice;
            cnx con = new cnx();
            MySqlConnection conn2 = con.conx();
            string query;
            query = "select max(invoice_id) from account;";
            MySqlCommand cmd = new MySqlCommand(query, conn2);
            MySqlDataReader MyReader2;
            conn2.Open();
            MyReader2 = cmd.ExecuteReader();
            MyReader2.Read();
            maxinvoice = Convert.ToInt32(MyReader2[0]);
            conn2.Close();
            return maxinvoice;
        }

        public string[] submit(string[] items,string[] barcode,string[] quantity,string[] invoicenumber)
        {
            int lenght = barcode.Length;
            cnx con = new cnx();
            MySqlConnection conn2 = con.conx();
            string query;
            string query2;
        
            MySqlDataReader MyReader2;
            conn2.Open();
            string date = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
            query2 = "insert into account (invoice_id,customer_id,datetime1) values (" + invoicenumber[0] + "," + "null" + ", " + '"' + date + '"' + ");";
            MySqlCommand cmd2 = new MySqlCommand(query2, conn2);
            MyReader2 = cmd2.ExecuteReader();
            conn2.Close();
            for (int i = 0; i < lenght; i++)
            {
                conn2.Open();
                query = "insert into invoice (invoice_id,item_id,quantity_purchased) values (" + invoicenumber[0] + "," + barcode[i] + "," + quantity[i] + ");";
                MySqlCommand cmd = new MySqlCommand(query, conn2);          
                MyReader2 = cmd.ExecuteReader();
                conn2.Close();
            }


            
            return null;
        }

        public string[] dicreaseitems(string[] barcode,string[]qis)
        {
            cnx con = new cnx();
            MySqlConnection conn2 = con.conx();
            MySqlDataReader MyReader2;
            int lenght = barcode.Length;
            string query;
            string[] quantityavailable = new string[lenght];
            int[] updateqis = new int[lenght];
            for (int i = 0; i < lenght; i++)
            {
                conn2.Open();
                query = "select qis from items where item_id =" +barcode[i]+";";
                MySqlCommand cmd = new MySqlCommand(query, conn2);
                MyReader2 = cmd.ExecuteReader();
                MyReader2.Read();
                quantityavailable[i] = MyReader2[0].ToString();
                conn2.Close();
            }
            for (int i = 0; i < lenght; i++)
            {
                updateqis[i] = Convert.ToInt32(quantityavailable[i])-Convert.ToInt32(qis[i]) ;
            }

            for (int i = 0; i < lenght; i++)
            {
                conn2.Open();
                query = "update items set qis ="+updateqis[i]+" where item_id=" +barcode[i] +";";
                MySqlCommand cmd = new MySqlCommand(query, conn2);
                MyReader2 = cmd.ExecuteReader();
                //MyReader2.Read();
                //quantityavailable[i] = MyReader2[0].ToString();
                conn2.Close();
            }

            return null;
        }
    }
}
