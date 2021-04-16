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
    class Itemsclass
    {
        int i = 0;
        int j = 0;
        string length;
        int lenghconv;
        public void additem(string itembarcode, string itemname, string qis, string itemcost, string itemprice,bool WithBarcode)
        {
            if (WithBarcode)
            {
                try
                {
                    cnx con = new cnx();
                    MySqlConnection conn2 = con.conx();
                    string query;
                    query = "insert into items (item_id,item_name,qis,item_cost,item_price) values (" + itembarcode + "," + '"' + itemname + '"' + "," + qis + "," + itemcost + "," + itemprice + ");";
                    MySqlCommand cmd = new MySqlCommand(query, conn2);
                    MySqlDataReader MyReader2;
                    conn2.Open();
                    MyReader2 = cmd.ExecuteReader();
                    conn2.Close();
                }

                catch
                {
                    MessageBox.Show("This item already exists");
                }
            }

                
            else
            {
               
                int timeStamp = 0;
                //MessageBox.Show(timeStamp+"");
                try
                {
                    long item_id=Convert.ToInt64(DateTime.Now.ToString("yyyyMMddHHmmss"));
                    cnx con = new cnx();
                    MySqlConnection conn2 = con.conx();
                    string query;
                    query = "insert into items (item_id,item_name,qis,item_cost,item_price,WithoutBarcode) values ("+item_id +","+ '"' + itemname + '"' + "," + qis + "," + itemcost + "," + itemprice + ",1);";
                    MySqlCommand cmd = new MySqlCommand(query, conn2);
                    MySqlDataReader MyReader2;
                    conn2.Open();
                    MyReader2 = cmd.ExecuteReader();
                    conn2.Close();
                }

                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }
            }
        }


        public string [] getInfoForNoBarcodeItem(string itemname)
        {
            cnx con = new cnx();
            MySqlConnection conn2 = con.conx();
            string query;
            query = "select * from items where item_name like '" + itemname + "';";
            MySqlCommand cmd = new MySqlCommand(query, conn2);
            MySqlDataReader reader;
            conn2.Open();
            reader = cmd.ExecuteReader();
            try
            {
                string[] itt = new string[5];

                if (reader.HasRows)
                {

                    while (reader.Read())
                    {
                        itt[0] = reader[0].ToString();
                        itt[1] = reader[1].ToString();
                        itt[2] = reader[2].ToString();
                        itt[3] = reader[3].ToString();
                        itt[4] = reader[4].ToString();
                    }
                    conn2.Close();
                }
                return itt;

            }

            catch
            {
                return null;
            }
            return null;
        }



        public void updateitem(string itembarcode, string itemname, string qis, string itemcost, string itemprice)
        {
            cnx con = new cnx();
            MySqlConnection conn2 = con.conx();
            string query;
            query = "update items set item_name="+'"'+ itemname+'"' + ",qis=" + qis + ",item_cost=" + itemcost + ",item_price=" + itemprice + " where item_id="+itembarcode+";";
            MySqlCommand cmd = new MySqlCommand(query, conn2);
            MySqlDataReader MyReader2;
            conn2.Open();
            MyReader2 = cmd.ExecuteReader();
            conn2.Close();
        }

        public string[,] Showitems()
        {
            cnx con = new cnx();
            MySqlConnection conn2 = con.conx();
            conn2.Open();
            MySqlCommand cmd = new MySqlCommand();
            MySqlCommand cmd01 = new MySqlCommand();
            cmd.Connection = conn2;
            MySqlDataReader reader;
            cmd.CommandText ="select count(*) from items;";
            reader=cmd.ExecuteReader();
            reader.Read();
            length=reader[0].ToString();
            lenghconv = Convert.ToInt32(length);
            string[,] Iq = new string[lenghconv, 2];
            cmd.CommandText = "select item_name,qis from items ORDER BY created DESC";
            conn2.Close();
            conn2.Open();
            reader = cmd.ExecuteReader();
            try
            {
               
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Iq[i,j]=reader[0].ToString();
                        j++;
                        Iq[i, j] = reader[1].ToString();
                        i++;
                        j = 0;
                    }
                    i = 0;
                    j = 0;
                   
                    conn2.Close();
                    return Iq;
                }

            }

            catch
            {
                return null;
            }
            return null;
            }

        public string[] edititems(string itemid)
        {
           
            cnx con = new cnx();
            MySqlConnection conn2 = con.conx();
            string query;
            query = "select * from items where item_id=" + itemid + ";";
            MySqlCommand cmd = new MySqlCommand(query, conn2);
            MySqlDataReader reader;
            conn2.Open();
            reader = cmd.ExecuteReader();
            try
            {
                string[] itt= new string[5];
               
                if (reader.HasRows)
                {

                    while (reader.Read())
                    {
                        itt[0] = reader[0].ToString();
                        itt[1] = reader[1].ToString();
                        itt[2] = reader[2].ToString();
                        itt[3] = reader[3].ToString();
                        itt[4] = reader[4].ToString();
                    }
                    conn2.Close();   
                }
                return itt;
              
            }

            catch
            {
                return null;
            }
            return null;
        }

        public string checkbarcode(string barcode)
        {

            cnx con = new cnx();
            MySqlConnection conn2 = con.conx();
            conn2.Open();
            MySqlCommand cmd = new MySqlCommand();
           // MySqlCommand cmd01 = new MySqlCommand();
            cmd.Connection = conn2;
            MySqlDataReader reader;
            cmd.CommandText = "select count(*) from items where item_id="+barcode+";";
            reader = cmd.ExecuteReader();
            reader.Read();
          //  MessageBox.Show(reader[0].ToString());
            return reader[0].ToString();
        }
        }
    }

