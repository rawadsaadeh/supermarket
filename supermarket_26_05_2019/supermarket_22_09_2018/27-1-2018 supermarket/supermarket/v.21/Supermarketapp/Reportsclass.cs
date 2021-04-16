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
using System.Drawing.Printing;

namespace Supermarketapp
{
    class Reportsclass
    {
        ArrayList Number_of_invoices_id = new ArrayList();
        ArrayList detailing = new ArrayList();
        long item_idtodelete;
        public ArrayList reports(string datetime)
        {
            Number_of_invoices_id.Clear();
            cnx con = new cnx();
            MySqlConnection conn2 = con.conx();      
            string query;
            conn2.Open();
            MySqlDataReader MyReader;

            query = "select t2.invoice_id,(t2.totalp-(t2.totalp*if(discount=1.00,0,discount) )) as totalp,account.datetime1,added_by_username,CASE WHEN discount = 1.00 THEN 0 ELSE discount*100 END AS discount,account.cash_in,account.cash_out from (select t1.invoice_id,sum(price) as totalp from (select invoice.invoice_id,invoice.item_id,(invoice.quantity_purchased*invoice.real_time_price) as price from invoice,items  where invoice.item_id=items.item_id) as t1 group by t1.invoice_id) as t2,account where t2.invoice_id=account.invoice_id  and datetime1 between" + '"' + datetime + " 00:00:00" + '"' + "and" + '"' + datetime + " 23:59:59" + '"' + ";";
            MySqlCommand cmd = new MySqlCommand(query, conn2);
            MyReader = cmd.ExecuteReader();
            while (MyReader.Read())
            {
                Number_of_invoices_id.Add(MyReader[0]);
                Number_of_invoices_id.Add(MyReader[1]);
                Number_of_invoices_id.Add(MyReader[2]);
                Number_of_invoices_id.Add(MyReader[3]);
                Number_of_invoices_id.Add(MyReader[4]);
                Number_of_invoices_id.Add(MyReader[5]);
                Number_of_invoices_id.Add(MyReader[6]);
            }
            conn2.Close();
            return Number_of_invoices_id;
            
        }

        public ArrayList show_invoice_details(string invnumber)
        {
            try
            {
                detailing.Clear();
                cnx con = new cnx();
                MySqlConnection conn2 = con.conx();
                string query;
                conn2.Open();
                MySqlDataReader MyReader;
                query = "select items.item_name,invoice.real_time_price,invoice.quantity_purchased,CASE WHEN discount = 1.00 THEN 0 ELSE discount END AS discount  FROM items LEFT JOIN invoice ON items.item_id=invoice.item_id LEFT JOIN account ON invoice.invoice_id = account.invoice_id where invoice.invoice_id=" + invnumber + ";";
                MySqlCommand cmd = new MySqlCommand(query, conn2);
                MyReader = cmd.ExecuteReader();
                while (MyReader.Read())
                {
                    detailing.Add(MyReader[0]);
                    detailing.Add(MyReader[1]);
                    detailing.Add(MyReader[2]);
                    detailing.Add(MyReader[3]);
                }
                conn2.Close();
                return detailing;
            }

            catch
            {
                return null;
            }

        }

        public string getcost(string invoicenum)
        {
            cnx con = new cnx();
            MySqlConnection conn2 = con.conx();
            string query;
            conn2.Open();
            MySqlDataReader MyReader;
            query = "select sum(t1.item_cost*t1.quantity_purchased) as totalbill from (select items.item_name,invoice.real_time_cost as item_cost,invoice.quantity_purchased  from items,invoice where items.item_id=invoice.item_id and invoice.invoice_id="+invoicenum+") as t1;";
            MySqlCommand cmd = new MySqlCommand(query, conn2);
            MyReader = cmd.ExecuteReader();
            MyReader.Read();
            return MyReader[0].ToString();
        }

        public string getdailyprofit(string date)
        {
            cnx con = new cnx();
            MySqlConnection conn2 = con.conx();
            string query;
            conn2.Open();
            MySqlDataReader MyReader;
            query = "select sum(  (t2.totalp-(t2.totalp*if(discount=1.00,0,discount)))-t2.totalc)as profit from (select t1.invoice_id,sum(price) as totalp,sum(cost)as totalc from (select invoice.invoice_id,invoice.item_id,(invoice.quantity_purchased*invoice.real_time_price) as price,(invoice.quantity_purchased*invoice.real_time_cost) as cost from invoice,items where invoice.item_id=items.item_id) as t1 group by t1.invoice_id) as t2,account where t2.invoice_id=account.invoice_id and datetime1 between '" + date + " 00:00:00' and '" + date + " 23:59:59';";
            MySqlCommand cmd = new MySqlCommand(query, conn2);
            MyReader = cmd.ExecuteReader();
            MyReader.Read();
            return MyReader[0].ToString();
          
        }

        public void getitemsbyinvoiceid(string invoice_id)
        {
            long invoiceidd = Convert.ToInt32(invoice_id);
            cnx con = new cnx();
            MySqlConnection conn2 = con.conx();
            string query;
            conn2.Open();
            MySqlDataReader MyReader;
            query = "select item_id,quantity_purchased from invoice where invoice_id=" + invoiceidd;
            MySqlCommand cmd = new MySqlCommand(query, conn2);
            MyReader = cmd.ExecuteReader();
            while (MyReader.Read())
            {
                updateqis(Convert.ToInt64(MyReader[0].ToString()),Convert.ToInt64(MyReader[1].ToString()));

            }
            conn2.Close();
            deleteinvoice(invoiceidd);
            //return MyReader[0].ToString();
        }

        public void deleteItem(long invoice_id,string itemname,string qistoupdate)
        {
            getitemid(itemname);
            updateqis(item_idtodelete, Convert.ToInt64(qistoupdate));
            long realItemPrice = getInvoiceItemPrice(invoice_id, item_idtodelete);
            updateCashOut(realItemPrice,invoice_id);
            cnx con = new cnx();
            MySqlConnection conn2 = con.conx();
            string query;
            conn2.Open();
            query = "delete from invoice where item_id ="+item_idtodelete +" and invoice_id = " +invoice_id;
            MySqlCommand cmd = new MySqlCommand(query, conn2);
            cmd.ExecuteReader();
            conn2.Close();
        }

        public void updateCashOut(long realItemPrice,long invoice_id) 
        {
            cnx con = new cnx();
            MySqlConnection conn2 = con.conx();
            string query;
            conn2.Open();
            query = "Update account SET cash_out = cash_out +" + realItemPrice + " WHERE invoice_id=" + invoice_id;
            MySqlCommand cmd = new MySqlCommand(query, conn2);
            cmd.ExecuteReader();
            conn2.Close();
        }

        public long getInvoiceItemPrice(long invoice_id, long item_id) 
        {
            cnx con = new cnx();
            MySqlConnection conn2 = con.conx();
            string query;
            conn2.Open();
            MySqlDataReader MyReader;
            query = "select real_time_price,quantity_purchased from invoice where invoice_id =" + invoice_id + " AND item_id=" + item_id;
            MySqlCommand cmd = new MySqlCommand(query, conn2);
            MyReader = cmd.ExecuteReader();
            MyReader.Read();
            long itemPrice = Convert.ToInt64(MyReader[0].ToString());
            int quantity = Convert.ToInt32(MyReader[1].ToString());
            conn2.Close();
            return itemPrice*quantity;
        }


        private void getitemid(string itemname)
        {
            cnx con = new cnx();
            MySqlConnection conn2 = con.conx();
            string query;
            conn2.Open();
            MySqlDataReader MyReader;
            query = "select item_id from items where item_name like '"+itemname+"'";
            MySqlCommand cmd = new MySqlCommand(query, conn2);
            MyReader=cmd.ExecuteReader();
            MyReader.Read();
            item_idtodelete = Convert.ToInt64(MyReader[0].ToString());
            conn2.Close();
        }

        public Tuple<long, string, int> fetchitemid(string itemname)
        {
            cnx con = new cnx();
            MySqlConnection conn2 = con.conx();
            string query;
            conn2.Open();
            MySqlDataReader MyReader;
            query = "select item_id, CASE WHEN WithoutBarcode IS NULL THEN '0' ELSE '1' END AS withoutbarcode  from items where item_name like '" + itemname + "'";
            MySqlCommand cmd = new MySqlCommand(query, conn2);
            MyReader = cmd.ExecuteReader();
            MyReader.Read();
            long itemId = Convert.ToInt64(MyReader[0].ToString());
            int withoutbarcode = Convert.ToInt32(MyReader[1].ToString());
            return Tuple.Create(itemId, itemname,withoutbarcode);
        }


        public void updateqis(long itemid, long qisadd)
        {
            cnx con = new cnx();
            MySqlConnection conn2 = con.conx();
            string query;
            conn2.Open();
            query = "UPDATE items SET qis = qis + " + qisadd + " WHERE item_id =" + itemid;
            MySqlCommand cmd = new MySqlCommand(query, conn2);
            cmd.ExecuteReader();
            conn2.Close();
        }

        public int checkInvoiceCountbydate(string datetime)
        {
            int count;
            cnx con = new cnx();
            MySqlConnection conn2 = con.conx();
            string query;
            query = "select count(*) from (select t1.invoice_id,sum(price) as totalp from (select invoice.invoice_id,invoice.item_id,(invoice.quantity_purchased*invoice.real_time_price) as price from invoice,items  where invoice.item_id=items.item_id) as t1 group by t1.invoice_id) as t2,account where t2.invoice_id=account.invoice_id  and datetime1 between" + '"' + datetime + " 00:00:00" + '"' + "and" + '"' + datetime + " 23:59:59" + '"' + ";";
            MySqlCommand cmd = new MySqlCommand(query, conn2);
            MySqlDataReader MyReader2;
            conn2.Open();
            MyReader2 = cmd.ExecuteReader();
            MyReader2.Read();
            count = Convert.ToInt32(MyReader2[0]);
            conn2.Close();
            return count;
        }

        public void deleteinvoice(long invoiceid)
        {
            cnx con = new cnx();
            MySqlConnection conn2 = con.conx();
            string query;
            conn2.Open();
            query = "DELETE FROM `account` WHERE `account`.`invoice_id` =" + invoiceid;
            MySqlCommand cmd = new MySqlCommand(query, conn2);
            cmd.ExecuteReader();
            conn2.Close();
        }

        public int count_items_in_invoice(int invoice_id)
        {
            int count;
            cnx con = new cnx();
            MySqlConnection conn2 = con.conx();
            string query;
            query = "select count(*) from invoice where invoice_id =" + invoice_id;
            MySqlCommand cmd = new MySqlCommand(query, conn2);
            MySqlDataReader MyReader2;
            conn2.Open();
            MyReader2 = cmd.ExecuteReader();
            MyReader2.Read();
            count = Convert.ToInt32(MyReader2[0]);
            conn2.Close();
            return count;
        }

    }
}
