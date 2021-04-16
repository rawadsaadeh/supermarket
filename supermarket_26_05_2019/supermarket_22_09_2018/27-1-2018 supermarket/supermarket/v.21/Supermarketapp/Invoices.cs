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
using System.IO;
using System.Net.NetworkInformation;
using System.Drawing.Printing;
using System.Configuration;
using Microsoft.VisualBasic;


namespace Supermarketapp
{
    public partial class Invoices : Form
    {
        string[] data = new string[2];
        string[] item_np = new string[3];
        int condition = 1;
        long totalinv=0;
        long total;
        int i = 0;
        decimal usd;
        double discount = 1;
        int enablePrinting = 1;
        int maxinvoice;
        int b=0;
        string index;
        int condition2=0;
        double cashin; double cashout;
        int o;
      //  int number_of_items=0;
        string onholdInvoiceName = "";
        ArrayList barcodes = new ArrayList();
        billing B1 = new billing();
        long quantityadded;
        int labelcolor = 0;
        int withbarcode = 1;

        public Invoices()
        {
         
            InitializeComponent();
            checkForOnholdTables();
            string imgUrl = ConfigurationManager.AppSettings["BackImg"].ToString();
            Bitmap myimage = new Bitmap(imgUrl);
            Bitmap resized= new Bitmap(myimage, new Size(1366, 768));
            this.BackgroundImage = resized;
            //button11.Enabled = false;
            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;
            button12.Enabled = false;
            maxinvoice = B1.select_invoice()+1;
            label8.Text = maxinvoice.ToString();
        //    label9.Hide();
            
            System.Windows.Forms.Timer t = new System.Windows.Forms.Timer();
            addNoBarcodeItems();  
            t.Interval = 500; // specify interval time as you want
            //t.Tick += new EventHandler(changecolor);
            t.Start();
           // button17.BackColor = Color.Red;
           // dataGridView1.Columns[4].ReadOnly = false;
            UpdateFont();
        }


        internal static void ActiveForm()
        {
            //throw new NotImplementedException();
            Invoices I1 = new Invoices();
            I1.ShowDialog();

        }

        private void UpdateFont()
        {
            //Change cell font
            foreach (DataGridViewColumn c in dataGridView1.Columns)
            {
                c.DefaultCellStyle.Font = new Font("Arial",17F, GraphicsUnit.Pixel);
            }
        }

        private void checkForOnholdTables()
        {
            cnx con = new cnx();
            MySqlConnection conn2 = con.conx();
            string query;
            query = "SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE = 'BASE TABLE' AND TABLE_SCHEMA='supermarket' and TABLE_NAME like 't_%'";
            MySqlCommand cmd = new MySqlCommand(query, conn2);
            MySqlDataReader MyReader2;
            conn2.Open();
            MyReader2 = cmd.ExecuteReader();

            if (MyReader2.HasRows)
            {
                while (MyReader2.Read())
                {
                    comboBox1.Items.Add(MyReader2[0].ToString().Substring(2));
                }
                conn2.Close();
            }
            conn2.Close();
        }

        private void addNoBarcodeItems()
        {
            cnx con = new cnx();
            MySqlConnection conn2 = con.conx();
            string query;
            query = "select item_name from items where WithoutBarcode=1";
            MySqlCommand cmd = new MySqlCommand(query, conn2);
            MySqlDataReader MyReader2;
            conn2.Open();
            MyReader2 = cmd.ExecuteReader();

            if (MyReader2.HasRows)
            {
                while (MyReader2.Read())
                {
                    comboBox2.Items.Add(MyReader2[0]);
                }
                conn2.Close();
            }
            conn2.Close();
        }

        private void changecolor(object sender, EventArgs e)
        {
            if (labelcolor == 0)
            {
                label10.ForeColor = System.Drawing.Color.DeepPink;
                labelcolor++;
            }

            else
            {
                label10.ForeColor = System.Drawing.Color.Blue;
                labelcolor = 0;
            }

        }


        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
     
        }

  

    void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }


    private void button1_Click_1(object sender, EventArgs e)
    {
     
    }

    private void textBox1_TextChanged_1(object sender, EventArgs e)
    {
    
    }

    private void Invoices_Load(object sender, EventArgs e)
    {
        string userloged;
        userloged = Login.userid_loged;
        int pstatus;
        itemsread();
        pstatus=Convert.ToInt32(B1.getprintstatus(userloged));
        if (pstatus == 0)
        {
            enablePrinting = 0;
            button17.Text = "Enable Printing";
         //   button17.BackColor = Color.Green;
        }
        else { 
            enablePrinting = 1;
            button17.Text = "Disable Printing";
 //           button17.BackColor = Color.Red;
        }
    }

 


    private void textBox1_TextChanged_2(object sender, EventArgs e)
    {

    }

    public void itemsread() 
   {
        //number_of_items = dataGridView1.Rows.Count;
       try
       {
           if (textBox1.Text != "" && withbarcode == 1 || withbarcode == 0)
           {
               string itemname = "";
               if (withbarcode == 1)
               {
                   itemname = textBox1.Text;
               }

               else if (withbarcode == 0)
               {
                   itemname = comboBox2.Text;
               }

               item_np = B1.check_item_barcode(itemname, withbarcode);
               if (item_np[0].CompareTo("-1") == 0)
               {
                   //label9.Show();
                   textBox1.Text = null;
                   textBox2.Text = "1";
               }
               else
               {
                   try
                   {

                       for (int f = 0; f < dataGridView1.Rows.Count - 1; f++)
                       {
                           if (dataGridView1.Rows[f].Cells[0].Value.ToString().CompareTo(item_np[0]) == 0)
                           {
                               index = dataGridView1.Rows[f].Cells[1].RowIndex.ToString();
                               textBox1.Text = null;
                             //  label9.Hide();
                               condition = 0;
                               o = i;
                             
                               quantityadded = Convert.ToInt64(dataGridView1.Rows[Convert.ToInt32(index)].Cells[1].Value);
                               quantityadded += Convert.ToInt64(textBox2.Text);
                               dataGridView1.Rows[Convert.ToInt32(index)].Cells[1].Value = quantityadded + "";
                               quantityadded = 0;
                               total = (Convert.ToInt64(dataGridView1.Rows[Convert.ToInt32(index)].Cells[1].Value)) * (Convert.ToInt64(dataGridView1.Rows[Convert.ToInt32(index)].Cells[2].Value));
                               dataGridView1.Rows[Convert.ToInt32(index)].Cells[3].Value = total;
                               totalinv += Convert.ToInt32(textBox2.Text) * Convert.ToInt32(dataGridView1.Rows[Convert.ToInt32(index)].Cells[2].Value);
                               label3.Text = totalinv + "";
                               usd = Convert.ToDecimal(label3.Text) / 1500;
                               usd = Math.Round(usd, 2);
                               label4.Text = usd + "";
                               textBox2.Text = "1";
                               f = dataGridView1.Rows.Count;
                               //  dataGridView1.Rows[1].ReadOnly = true;
                           }//
                           else
                           {
                           }
                       }
                       if (condition == 1)
                       {
                           string codeid = item_np[2];
                           this.dataGridView1.FirstDisplayedScrollingRowIndex = b;
                           total = (Convert.ToInt32(textBox2.Text)) * (Convert.ToInt32(item_np[1]));
                           dataGridView1.Rows.Add();
                           dataGridView1.Rows[i].Cells[0].Value = item_np[0];
                           dataGridView1.Rows[i].Cells[1].Value = textBox2.Text;
                           dataGridView1.Rows[i].Cells[2].Value = item_np[1];
                           dataGridView1.Rows[i].Cells[3].Value = total;
                           
                           dataGridView1.Rows[i].Cells[4].Value = "Delete";
                           //dataGridView1.Rows[1].ReadOnly = true;
                           b++;
                           button12.Enabled = true;
                           totalinv += Convert.ToInt32(dataGridView1.Rows[i].Cells[3].Value);
                           //   number_of_items++;
                          // label9.Enabled = false;
                           label3.Text = totalinv + "";
                           barcodes.Add(codeid);
                           textBox1.Text = null;
                           usd = Convert.ToDecimal(label3.Text) / 1500;
                           usd = Math.Round(usd, 2);
                           label4.Text = usd + "";
                           i++;
                           textBox2.Text = "1";
                       }
                       else { }
                   }
                   catch (Exception e)
                   {
                       MessageBox.Show(e.Message);
                   }
               }

           }
           ///button11.Enabled = false;   
       }

       catch
       {
           textBox1.Text = "";
           textBox1.Focus();
       }
    }

    private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
    {
        e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
    }


    private void textBox1_TextChanged_3(object sender, EventArgs e)
    {

    }

    private void textBox2_TextChanged(object sender, EventArgs e)
    {

    }

    private void button2_Click(object sender, EventArgs e)
    {
        textBox2.Text = "";
            textBox2.Text=textBox2.Text+"1";
        
    }

    private void button3_Click(object sender, EventArgs e)
    {
        textBox2.Text = "";
        textBox2.Text = textBox2.Text + "2";
    }

    private void button4_Click(object sender, EventArgs e)
    {
        textBox2.Text = "";
        textBox2.Text = textBox2.Text + "3";
       // button11.Enabled = true;
    }

    private void button5_Click(object sender, EventArgs e)
    {
        textBox2.Text = "";
        textBox2.Text = textBox2.Text + "4";
       // button11.Enabled = true;
    }

    private void button6_Click(object sender, EventArgs e)
    {
        textBox2.Text = "";
        textBox2.Text = textBox2.Text + "5";
       //button11.Enabled = true;
    }

    private void button7_Click(object sender, EventArgs e)
    {
        textBox2.Text = "";
        textBox2.Text = textBox2.Text + "6";
     //   button11.Enabled = true;
    }

    private void button8_Click(object sender, EventArgs e)
    {
        textBox2.Text = "";
        textBox2.Text = textBox2.Text + "7";
     //  button11.Enabled = true;
    }

    private void button9_Click(object sender, EventArgs e)
    {
        textBox2.Text = "";
        textBox2.Text = textBox2.Text + "8";
       // button11.Enabled = true;
    }

    private void button10_Click(object sender, EventArgs e)
    {
        textBox2.Text = "";
        textBox2.Text = textBox2.Text + "9";
        //button11.Enabled = true;
    }

    //private void button11_Click(object sender, EventArgs e)
    //{
    //    if (textBox2.Text == "")
    //    {
    //      //  textBox2.Text = textBox2;
    //    }
    //    else
    //    {
    //        textBox2.Text = textBox2.Text + "0";
    //    }
    //}

    private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
    {
        var senderGrid = (DataGridView)sender;

        if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn &&
            e.RowIndex >= 0)
        {
          //  dataGridView1.Rows[i].Cells[0].Value = item_np[0];
            int row_index = dataGridView1.CurrentCell.RowIndex;
            if (string.IsNullOrEmpty(dataGridView1.Rows[row_index].Cells[0].Value as string))
            {
               
            }
            else
            {
                //MessageBox.Show(dataGridView1.CurrentCell.RowIndex.ToString());
              
              //  dataGridView1.RefreshEdit();
                string totalItem = dataGridView1.Rows[row_index].Cells[3].Value.ToString();
                string BigTotal = label3.Text;
                long toBeWithdrawn = Convert.ToInt64(totalItem);
                long currentTotallbp = Convert.ToInt64(BigTotal);
                totalinv = currentTotallbp - toBeWithdrawn;
                label3.Text = totalinv + "";
                dataGridView1.Rows.RemoveAt(row_index);
                b--;
                i--;
                barcodes.RemoveAt(row_index);
                 // we should remocve the barcode of the item deleted from barcodes array
                usd = Convert.ToDecimal(label3.Text) / 1500;
                usd = Math.Round(usd, 2);
                label4.Text = usd + "";
            }
        }

        else
        {
           
        }
    

       
    }

    private void label3_Click(object sender, EventArgs e)
    {

    }

    private void label4_Click(object sender, EventArgs e)
    {

    }

    private void dataGridView1_ColumnAdded(object sender, DataGridViewColumnEventArgs e)
    {

    }

    private void label8_Click(object sender, EventArgs e)
    {

    }

    private void button12_Click(object sender, EventArgs e)
    {
        double total= 0;
        string cashInString = "";
        bool isNumeric = false;
            while (!isNumeric)
            {
                 cashInString = Interaction.InputBox("Enter the Cash In", "Cash In", "");
                int test;
                isNumeric= int.TryParse(cashInString, out test);
            }

            cashin = Convert.ToDouble(cashInString);
            
         discount = 1;
         if (textBox3.Text.CompareTo("") == 0 || textBox3.Text.CompareTo("0") == 0)
        {
            discount = 1;
             total = Convert.ToDouble(label3.Text);
        }
        else
        {
            discount = Convert.ToDouble(textBox3.Text)/100;
            double discountOutputUSD = Convert.ToDouble(label4.Text) -(Convert.ToDouble(label4.Text) * discount);
            double discountOutputLBP = Convert.ToDouble(label3.Text) - (Convert.ToDouble(label3.Text) * discount);
            MessageBox.Show("Total in USD: " + label4.Text + "$\r\n" + "Total with discount in USD:" + discountOutputUSD + "$\r\n" + "Total in LBP: " + label3.Text + "LBP\r\n" + "Total with discount in LBP:" + discountOutputLBP + "LBP\r\n");
            total = discountOutputLBP;
        }
         cashout = cashin - total;
       MessageBox.Show("Cashout: "+ cashout +" LBP");

        //comboBox1.Items.Add(onholdInvoiceName);
        string[] items = new string[dataGridView1.Rows.Count-1];
        string[] barcodearray = new string[dataGridView1.Rows.Count-1];
        string[] quantityarray = new string[dataGridView1.Rows.Count-1];
        string[] invoice_number = { label8.Text };

        for (int z = 0; z < dataGridView1.Rows.Count-1; z++)
        {
            barcodearray[z] = barcodes[z].ToString();
        }

        for (int l = 0; l < dataGridView1.Rows.Count-1; l++)
        {
            items[l] = dataGridView1.Rows[l].Cells[0].Value.ToString();
        }

        for (int k = 0; k < dataGridView1.Rows.Count-1; k++)
        {
            quantityarray[k] = dataGridView1.Rows[k].Cells[1].Value.ToString();
        }
        B1.submit(items, barcodearray,quantityarray,invoice_number,discount,cashin,cashout);
        if (enablePrinting == 1)
        {
            printDocument1.Print();
        }
        else
        {
        }
        maxinvoice = B1.select_invoice() + 1;
        label8.Text = maxinvoice.ToString();

        barcodes.Clear();
        dataGridView1.Rows.Clear();
        b = 0;
        i = 0;
       // number_of_items = 0;
        label3.Text = "0";
        label4.Text = "0";
        textBox3.Text = "";
        totalinv = 0;
        button12.Enabled = false;
        B1.dicreaseitems(barcodearray,quantityarray);
        MessageBox.Show("Successful !" );
      //  button11.Enabled = false;
        textBox1.Focus();
        comboBox2.SelectedIndex = -1;
       // button11.Enabled = false;
    }

 

    private void button13_Click(object sender, EventArgs e)
    {
        if (dataGridView1.Rows.Count > 1)
        {
            MessageBox.Show("Please submit or delete the invoice");
        }
        else if (dataGridView1.Rows.Count == 1)
        {
            this.Hide();
        }

    }

    private void button14_Click(object sender, EventArgs e)
    {
        textBox2.Text = "1";
    }

    private void textBox1_KeyDown(object sender, KeyEventArgs e)
    {
        //if (!System.Text.RegularExpressions.Regex.IsMatch(textBox1.Text, "[0-9]") && textBox1.Text != "")
        //{
        //    MessageBox.Show("You can only add numbers!");
        //    condition2 = 1;
        //    textBox1.Text = "";
        //}

        if (condition2 == 0 && textBox1.Text != "")
        {
            if (textBox2.Text == "0" || textBox2.Text == "")
            {
                MessageBox.Show("Please change the quantity");
                textBox2.Text = "1";
                textBox1.Text = "";
            }
            else
            {
                if (e.KeyCode == Keys.Enter)
                {
                    itemsread();
                  //  label9.Hide();
                    condition = 1;
                }
            }
        }
        condition2 = 0;
        button11.Enabled = true;
    }

    private void button11_Click(object sender, EventArgs e)
    {
        if (dataGridView1.Rows.Count == 1)
        {
            MessageBox.Show("No items to hold");
        }
        else
        {
            onHoldButton();
           // button11.Enabled = false;
            textBox1.Focus();
        }
    }

    private void onHoldButton()
    {
        onholdInvoiceName = Interaction.InputBox("Please enter the name of your OnHold invoice", "Onhold", "");

        if (onholdInvoiceName.Length > 0)
        {
            cnx con = new cnx();
            MySqlConnection conn2 = con.conx();
            string query;
            query = "show tables from supermarket like 't_" + onholdInvoiceName + "';";
            MySqlCommand cmd = new MySqlCommand(query, conn2);
            conn2.Open();
            MySqlDataReader MyReader2;
            MyReader2 = cmd.ExecuteReader();
            conn2.Close();
            if (MyReader2.HasRows)
            {
                //MessageBox.Show("Please choose another name");
                onHoldButton();
                button11.Enabled = true;
            }
            else
            {
                cnx con2 = new cnx();
                MySqlConnection conn4 = con.conx();
                string query1;
                query1 = "CREATE TABLE t_" + onholdInvoiceName + " ( itemname varchar(100), quantity bigint(100), PricePerUnit bigint(100), Total bigint(100),Barcode bigint(100));";
                MySqlCommand cmd1 = new MySqlCommand(query1, conn4);
                conn4.Open();
                cmd1.ExecuteReader();
                conn4.Close();
                insertOnHoldData();
            }
        }
        else 
        {
            DialogResult dialogResult = MessageBox.Show("Are you sure?", "You are cancelling on hold", MessageBoxButtons.YesNo);
           
            if (dialogResult == DialogResult.No)
            {
                onHoldButton();
            }
        }
    }


    private void insertOnHoldData()
    {
        cnx con = new cnx();
        MySqlConnection conn2 = con.conx();
        string query = "";
        for (int y = 0; y < dataGridView1.Rows.Count - 1; y++)
        {
            conn2.Open();
            query = "INSERT into t_" + onholdInvoiceName + " (itemname,quantity,PricePerUnit,Total,Barcode) VALUES ('" + dataGridView1.Rows[y].Cells[0].Value.ToString() + "'," + dataGridView1.Rows[y].Cells[1].Value.ToString() + "," + dataGridView1.Rows[y].Cells[2].Value.ToString() + "," + dataGridView1.Rows[y].Cells[3].Value.ToString() + "," + barcodes[y] + ");";
            MySqlCommand cmd = new MySqlCommand(query, conn2);

            cmd.ExecuteReader();
            conn2.Close();
        }

        comboBox1.Items.Add(onholdInvoiceName);
        // maxinvoice++;
        //  label8.Text = maxinvoice.ToString();
        barcodes.Clear();
        dataGridView1.Rows.Clear();
        b = 0;
        i = 0;
        //  number_of_items = 0;
        label3.Text = "0";
        label4.Text = "0";
        totalinv = 0;
        button12.Enabled = false;
        //B1.dicreaseitems(barcodearray, quantityarray);
        // MessageBox.Show("Successful !");
       // 
    }

    private void label10_Click(object sender, EventArgs e)
    {

    }

    private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
         
    private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    private void button15_Click(object sender, EventArgs e)
    {
        if (comboBox2.Text == "")
        {
            MessageBox.Show("Select an Item");
        }

        else
        {

            withbarcode = 0;
            itemsread();
            withbarcode = 1;
            condition = 1;
        }

        textBox1.Focus();
    }

    private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
    {
        //MessageBox.Show("comboBox1_SelectedIndexChanged");
       //totalinv= selesum();
        loaddatatothegrid();
        b = dataGridView1.Rows.Count-1;
        i = dataGridView1.Rows.Count - 1;
        textBox1.Focus();
    }

    private void DeleteonHoldTable()
    {
        cnx con = new cnx();
        MySqlConnection conn2 = con.conx();
        int IndextobeRemoved = comboBox1.SelectedIndex;
        string query;
        query = "drop table t_" + comboBox1.Text + ";";
        MySqlCommand cmd = new MySqlCommand(query, conn2);
        conn2.Open();
        cmd.ExecuteReader();
        conn2.Close();
    }

    private void loaddatatothegrid()
    {
        if (dataGridView1.Rows.Count > 1)
        {
            onHoldButton();
            cnx con = new cnx();
            MySqlConnection conn2 = con.conx();
            string query;
          //  query = "SELECT `itemname`, `quantity`, `PricePerUnit`, `Total`, `Barcode`,sumtotal FROM " + comboBox1.Text + " LEFT JOIN (SELECT SUM(Total) as sumtotal From " + comboBox1.Text + " ) as sum_table ON 1 WHERE 1";
           query = "select * from t_" + comboBox1.Text + ";";
            MySqlCommand cmd = new MySqlCommand(query, conn2);
            MySqlDataReader MyReader2;
            conn2.Open();
            MyReader2 = cmd.ExecuteReader();
            int Loop = 0;
            int total = 0;
            if (MyReader2.HasRows)
            {
                while (MyReader2.Read())
                {
                    dataGridView1.Rows.Add();
                    dataGridView1.Rows[Loop].Cells[0].Value = MyReader2[0];
                    dataGridView1.Rows[Loop].Cells[1].Value = MyReader2[1];
                    dataGridView1.Rows[Loop].Cells[2].Value = MyReader2[2];
                    dataGridView1.Rows[Loop].Cells[3].Value = MyReader2[3];
                    dataGridView1.Rows[Loop].Cells[4].Value = "Delete";
                    barcodes.Add(MyReader2[4]); 
                    total += Convert.ToInt32(MyReader2[3].ToString());
                    Loop++;
                    totalinv = total;
                }
                conn2.Close();
            }
            conn2.Close();
            button12.Enabled = true;
            button11.Enabled = true;
            // label8.Text = comboBox1.Text; 
            int IndextobeRemoved = comboBox1.SelectedIndex;
            DeleteonHoldTable();
            comboBox1.Items.RemoveAt(IndextobeRemoved);
            label3.Text = total + "";
            usd = Convert.ToDecimal(label3.Text) / 1500;
            usd = Math.Round(usd, 2);
            label4.Text = usd.ToString();
        }
        else
        {
            cnx con = new cnx();
            MySqlConnection conn2 = con.conx();
            string query;
            query = "select * from t_" + comboBox1.Text + ";";
            MySqlCommand cmd = new MySqlCommand(query, conn2);
            MySqlDataReader MyReader2;
            conn2.Open();
            MyReader2 = cmd.ExecuteReader();
            int Loop = 0;
            int total = 0;
            if (MyReader2.HasRows)
            {
                while (MyReader2.Read())
                {
                    dataGridView1.Rows.Add();
                    dataGridView1.Rows[Loop].Cells[0].Value = MyReader2[0];
                    dataGridView1.Rows[Loop].Cells[1].Value = MyReader2[1];
                    dataGridView1.Rows[Loop].Cells[2].Value = MyReader2[2];
                    dataGridView1.Rows[Loop].Cells[3].Value = MyReader2[3];
                    dataGridView1.Rows[Loop].Cells[4].Value = "Delete";
                    barcodes.Add(MyReader2[4]);
                    total += Convert.ToInt32(MyReader2[3].ToString());
                    Loop++;
                    totalinv = total;
                }
                conn2.Close();
            }
            conn2.Close();
            button12.Enabled = true;
            // label8.Text = comboBox1.Text; 
            int IndextobeRemoved = comboBox1.SelectedIndex;
            DeleteonHoldTable();
            comboBox1.Items.RemoveAt(IndextobeRemoved);
            label3.Text = total + "";
            usd = Convert.ToDecimal(label3.Text) / 1500;
            usd = Math.Round(usd, 2);
            label4.Text = usd.ToString();
        }
    }

    private void comboBox1_SelectedValueChanged(object sender, EventArgs e)
    {
       // MessageBox.Show("comboBox1_SelectedValueChanged");
       // if (dataGridView1.Rows.Count == 1)
       // {
       //     loaddatatothegrid();

       // }

       // else
       // {
        
       //     barcodes.Clear();
       //     dataGridView1.Rows.Clear();
       //     b = 0;
       //     i = 0;
       ////     number_of_items = 0;
       //     label3.Text = "0";
       //     label4.Text = "0";
       //     totalinv = 0;
       //     button12.Enabled = false;
       //     cnx con = new cnx();
       //     MySqlConnection conn2 = con.conx();
       //     string query;
       //     query = "CREATE TABLE t_" + label8.Text + " ( itemname varchar(100), quantity bigint(100), PricePerUnit bigint(100), Total bigint(100) );";
       //     MySqlCommand cmd = new MySqlCommand(query, conn2);
       //     conn2.Open();
       //     cmd.ExecuteReader();
       //     conn2.Close();
       //     insertOnHoldData();
       //     loaddatatothegrid();
       // }
    }

    private void dataGridView1_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
    {

    }

        

    private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
    {
        DataTable dtInvoice = new DataTable();
        int rowcount = 0;
        
        dtInvoice.Columns.Add("ITEM", typeof(string));
        dtInvoice.Columns.Add(" ", typeof(string));
        dtInvoice.Columns.Add("PRICE", typeof(long));
        

        for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
        {
            DataRow itemPrice = dtInvoice.NewRow();

            itemPrice["ITEM"] = dataGridView1.Rows[i].Cells[0].Value.ToString() + " * " + dataGridView1.Rows[i].Cells[1].Value.ToString();
          //  itemPrice["QTY"] = "* "+dataGridView1.Rows[i].Cells[1].Value.ToString();
            itemPrice["PRICE"] = Convert.ToInt64(dataGridView1.Rows[i].Cells[3].Value.ToString());
          
            dtInvoice.Rows.Add(itemPrice);
        }

        string header = "Katia's Diet Center"+"\r\n";
        header += "Inv# : " + label8.Text + "\r\n";
        header += "Phone : 03/534801  " + "\r\n";
        header += "Address : Kfarhim " + "\r\n" + "\r\n";
        string footer = string.Empty;
        int columnCount = dtInvoice.Columns.Count;
        int maxRows = dtInvoice.Rows.Count;
        double total = 0;




        using (Graphics g = e.Graphics)
        {
            Brush brush = new SolidBrush(Color.Black);
            Pen pen = new Pen(brush);
            Font font = new Font("Arial", 11);
            SizeF size;

            int x = 60, y = 10, width = 115;
         

            // Here title is written, sets to top-middle position of the page
            size = g.MeasureString(header, font);
            
            g.DrawString(header, font, brush, x ,y);

            x = 0;
            y += 90;

            // Writes out all column names in designated locations, aligned as a table
            foreach (DataColumn column in dtInvoice.Columns)
            {
                size = g.MeasureString(column.ColumnName, font);
              //  x += 10;
                g.DrawString(column.ColumnName, font, brush, x,y);
                x += width;
            }

            x = 0;
            //y += 0;
            g.DrawString("-----------------------------------------------------------", font, brush, x, y + 15);
            // Process each row and place each item under correct column.
            foreach (DataRow row in dtInvoice.Rows)
            {
                rowcount++;

                for (int i = 0; i < columnCount; i++)
                {
                    size = g.MeasureString(row[i].ToString(), font);
                 

                    g.DrawString(row[i].ToString(), font, brush, x , y + 40);
                    x += width;
                }
               
               // e.HasMorePages = rowcount - 1 < maxRows;

                x = 0;
                y += 22;
            }
            y += 30;
            
            g.DrawString("-----------------------------------------------------------", font, brush, x, y);
           // x = 10;
            g.DrawString("          Total (LBP): "+label3.Text, font, brush, x, y + 30);
          //  x += width+90;
            y += 30;
            //g.DrawString(label3.Text , font, brush, x,y);

            //g.DrawString(label4.Text, font, brush, x, y + 20);
            x = 0;
            y += 20;
            g.DrawString("          Total (USD): "+label4.Text, font, brush, x, y);



            if (textBox3.Text.CompareTo("") == 0 || textBox3.Text.CompareTo("0") == 0)
            {

            }
            else
            {
                y += 20;
                discount = Convert.ToDouble(textBox3.Text)/100;
                double discountOutputUSD = Convert.ToDouble(label4.Text) - (Convert.ToDouble(label4.Text) * discount);
                double discountOutputLBP = Convert.ToDouble(label3.Text) - (Convert.ToDouble(label3.Text) * discount);
               // MessageBox.Show("Total in USD: " + label4.Text + "$\r\n" + "Total with discount in USD:" + discountOutputUSD + "$\r\n" + "Total in LBP: " + label3.Text + "LBP\r\n" + "Total with discount in LBP:" + discountOutputLBP + "LBP\r\n");
                total = discountOutputLBP;
                y += 30;

                g.DrawString("********** Discount : " + discount * 100 + "% ********** ", font, brush, x + 30, y);
                y += 40;
                g.DrawString("          Total after discount (LBP): " + discountOutputLBP , font, brush, x, y);
                y += 15;
                //g.DrawString(discountOutputUSD+"", font, brush, x, y);
                y += 15;
                g.DrawString("          Total  after discount (USD): " + discountOutputUSD , font, brush, x, y);
            }
            g.DrawString("-----------------------------------------------------------", font, brush, x, y+30);
            g.DrawString("Cash In : " +cashin+ " LBP", font, brush, x, y + 60);
            g.DrawString("Cash Out : " + cashout + " LBP", font, brush, x, y + 90);
           // g.DrawString("-----------------------------------------------------------", font, brush, x, y + 60);


            footer = "Date " + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")+"\r\n";
            //y += 30;
            footer+= "              Welcome!\r\n";
            g.DrawString(footer, font, brush, x =60, y + 150);
        }
    }

    private void button1_Click_2(object sender, EventArgs e)
    {
        //   maxinvoice++;
        label8.Text = maxinvoice.ToString();
        barcodes.Clear();
        dataGridView1.Rows.Clear();
        b = 0;
        i = 0;
        //  number_of_items = 0;
        label3.Text = "0";
        label4.Text = "0";
        totalinv = 0;
        button12.Enabled = false;
        //B1.dicreaseitems(barcodearray, quantityarray);
        // MessageBox.Show("Successful !");
      //  button11.Enabled = false;
        textBox1.Focus();
    }

    private void button17_Click(object sender, EventArgs e)
    {
        string userloged;
        userloged = Login.userid_loged;

        if (enablePrinting == 1)
        {
          //  MessageBox.Show("dp");
            enablePrinting = 0;
            button17.Text = "Enable Printing";
          //  button17.BackColor = Color.Green;
            int status = 0;
            B1.updateprintstatus(status,userloged);
        }

        else
        {
        //    MessageBox.Show("p");
            enablePrinting = 1;
            button17.Text = "Disable Printing";
          //  button17.BackColor = Color.Red;
            int status = 1;
            B1.updateprintstatus(status,userloged);
        }
        textBox1.Focus();
    }

    private void textBox3_TextChanged(object sender, EventArgs e)
    {
      //  MessageBox.Show("changed");
        if (!System.Text.RegularExpressions.Regex.IsMatch(textBox3.Text, "[0-9]"))
        {
            textBox3.Clear();
            // MessageBox.Show("The cost should be an integer");

        }
    }

    private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
    {
        e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
    }

    private void textBox4_TextChanged(object sender, EventArgs e)
    {

    }




    }
}
