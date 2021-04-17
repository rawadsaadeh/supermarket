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
using System.Net.NetworkInformation;
using System.Configuration;
using System.Diagnostics;
using Supermarketapp;

namespace Supermarketapp
{
    public partial class Reports : Form
    {
        ArrayList item_idtobedeleted = new ArrayList();
        ArrayList data = new ArrayList();
        ArrayList details = new ArrayList();
        Reportsclass rc = new Reportsclass();
        double totalday=0;
        long totalinv = 0;
        decimal usd = 0;
        decimal usd2;
        string cost;
        double profit;
       // int costdaily;
        double cost2;
        string dailyprofit="";
        double cost3;
        public Reports()
        {
            InitializeComponent();
            string imgUrl = ConfigurationManager.AppSettings["BackImg"].ToString();
            Bitmap myimage = new Bitmap(imgUrl);
            Bitmap resized = new Bitmap(myimage, new Size(1366, 768));
            this.BackgroundImage = resized;
            dataGridView2.Hide();
            label3.Hide();
            label9.Hide();
            label10.Hide();
            label11.Hide();
            label12.Hide();
            label15.Hide();
            label16.Hide();
            dateTimePicker1.Text =DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") ;
            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;
        }
       

        internal static void ActiveForm()
        {
            //throw new NotImplementedException();
            Reports R1 = new Reports();
            R1.ShowDialog();
        }

        public int searchdate(){
            string date;
            date = dateTimePicker1.Text.ToString(); 
           // comboBox1.Items.Clear();
            dataGridView1.Rows.Clear();
            dataGridView2.Rows.Clear();
            
            dataGridView1.Rows.Clear();
            int c1=Convert.ToInt32(rc.checkInvoiceCountbydate(date));
       /*     if (c1 == 0) { 
                MessageBox.Show("No invoices for the selected date!");
                
            }
            else
            {*/
            data = rc.reports(date);
            dailyprofit = rc.getdailyprofit(date);
            try
            {
                double dailyprofit1 = Convert.ToDouble(dailyprofit);
                label17.Text = "0";
                label13.Text = "0";
                cost3 = 0;

                totalday = 0;
                label13.Text = "Profit : " + dailyprofit + " LBP";
                dailyprofit = "0";
                int length = 0;
                length = data.Count;
                if (dataGridView2.Rows.Count==0) 
                {
                    label4.Text = "";
                }
                int y = 0;
                for (int i = 0; i < length; i ++)
                {
                    reportsDataModel reportDataModel = new reportsDataModel();
                    reportDataModel = data[i] as reportsDataModel;
                    reportDataModel =  reportDataModel.get();
                    dataGridView1.Rows.Add();
                    dataGridView1.Rows[y].Cells[0].Value = reportDataModel.invoice_id;
                    dataGridView1.Rows[y].Cells[1].Value = reportDataModel.total_price;
                    dataGridView1.Rows[y].Cells[2].Value = reportDataModel.datetime;
                    dataGridView1.Rows[y].Cells[3].Value = reportDataModel.added_by_username;
                    dataGridView1.Rows[y].Cells[4].Value = reportDataModel.discount;
                    dataGridView1.Rows[y].Cells[5].Value = reportDataModel.cash_in;
                    dataGridView1.Rows[y].Cells[6].Value = reportDataModel.cash_out;
                    dataGridView1.Rows[y].Cells[7].Value = "Show Details";
                    dataGridView1.Rows[y].Cells[8].Value = "Delete";
                    dataGridView1.Rows[y].Cells[9].Value = "Print";
                    totalday += Convert.ToDouble(dataGridView1.Rows[y].Cells[1].Value);

                    label8.Text = totalday + "";
                    usd = Convert.ToDecimal(label8.Text) / 1500;
                    usd = Math.Round(usd, 2);
                    label6.Text = usd + "";
                   // comboBox1.Items.Add(dataGridView1.Rows[y].Cells[0].Value.ToString());
                    //comboBox1.Select(1, 2);
                    y++;
                }
                cost3 = totalday - dailyprofit1;
                label17.Text = "Cost: " +cost3 ;
                if (y != 0)
                {
                    dataGridView2.Show();
                  //  button2.Show();
                    label3.Show();
                   // label4.Show();
                    label9.Show();
                    label10.Show();
                    label11.Show();
                    label12.Show();
                 //   label4.Show();
                 //   comboBox1.Show();
                  //  button2.Enabled = false;
                    label12.Text = "0";
                    label10.Text = "0";
                    label15.Text = "0";
                    label16.Text = "0";
                    label4.Show();
                }
                return 1;

            }
            catch
            {
                dataGridView2.Hide();
               // button2.Hide();
                label3.Hide();
             //   label4.Hide();
                label9.Hide();
                label10.Hide();
                label11.Hide();
                label12.Hide();

                label6.Text = "";
                label8.Text = "";
                usd = 0;
                usd2 = 0;
                totalday = 0;
                totalinv = 0;
                label15.Text = "0";
                label16.Text = "0";
                cost2 = 0;
                profit = 0;
                label16.Hide();
                label17.Text = "0";
                label13.Text = "0";
                label15.Hide();
                label4.Hide();
                label4.Text = "";
                MessageBox.Show("No invoices for the selected date!");
                return 0;
            }
       // }
    }


        private void button1_Click(object sender, EventArgs e)
        {
            searchdate();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var senderGrid = (DataGridView)sender;

            if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn &&
                e.RowIndex >= 0)
            {
                //  dataGridView1.Rows[i].Cells[0].Value = item_np[0];
                int row_index = dataGridView1.CurrentCell.RowIndex;
                int cell_index = dataGridView1.CurrentCell.ColumnIndex;
                if (string.IsNullOrEmpty(dataGridView1.Rows[row_index].Cells[0].Value as string))
                {
                    try
                    {
                        if (dataGridView1.Rows[row_index].Cells[cell_index].Value.ToString().CompareTo("Show Details") == 0)
                        {
                            int row_index2 = dataGridView1.CurrentCell.RowIndex;
                            string invoice_id = dataGridView1.Rows[row_index].Cells[0].Value.ToString();
                            showDetails(invoice_id);
                            label4.Text = dataGridView1.Rows[row_index].Cells[0].Value.ToString();
                        }
                        else if (dataGridView1.Rows[row_index].Cells[cell_index].Value.ToString().CompareTo("Delete") == 0)
                        {
                            DialogResult dialogResult = MessageBox.Show("Are you sure do you want to delete this invoice ?", "Some Title", MessageBoxButtons.YesNo);
                            if (dialogResult == DialogResult.Yes)
                            {
                                DeleteInvoice();
                                searchdate();
                            }
                            else if (dialogResult == DialogResult.No)
                            {

                            }
                        }
                        else if (dataGridView1.Rows[row_index].Cells[cell_index].Value.ToString().CompareTo("Print") == 0)
                        {
                            Printinvoice();
                        }
                    }

                    catch { }
                }
                else
                {
                    MessageBox.Show("sefsef");
                }
            }

            else
            {

            }
        }


        private void DeleteInvoice()
        {
            int row_index = dataGridView1.CurrentCell.RowIndex;
            string invoice_id = dataGridView1.Rows[row_index].Cells[0].Value.ToString();
            rc.getitemsbyinvoiceid(invoice_id);
          //  rc.updateqis();
            //rc.deleteallitemsfrominvoice();
        }

        public int count_items_in_invoice(int invoice_id) 
        {

            int x=Convert.ToInt32(rc.count_items_in_invoice(invoice_id));
            return x;
        }


        private void Printinvoice()
        {
            printDocument1.Print();
        }

        private int showDetails(string inv)
        {
           
            dataGridView2.Rows.Clear();
            dataGridView2.Show();
            details = rc.show_invoice_details(inv);

            cost = rc.getcost(inv);
            int lenght = 0;
            lenght = details.Count;
            int y = 0;
            double discount = 1;
            for (int i = 0; i < lenght; i += 4)
            {
                dataGridView2.Rows.Add();
                dataGridView2.Rows[y].Cells[0].Value = details[i];
                dataGridView2.Rows[y].Cells[1].Value = details[i + 1];
                dataGridView2.Rows[y].Cells[2].Value = details[i + 2];
                dataGridView2.Rows[y].Cells[3].Value = "Delete";
                totalinv += Convert.ToInt64(dataGridView2.Rows[y].Cells[1].Value) * Convert.ToInt64(dataGridView2.Rows[y].Cells[2].Value);
                discount = Convert.ToDouble(details[i + 3]);
                y++;
            }
            label12.Text = totalinv-(totalinv*discount) + ""; 
            usd2 = 0;
            usd2 = Convert.ToDecimal(label12.Text) / 1500;
            usd2 = Math.Round(usd2, 2);
            label10.Text = usd2 + "";
            usd2 = 0;
            // y = 0;
            label15.Show();
            label16.Show();
            label15.Text = "Cost: " + cost + " LBP";
            try
            {
                cost2 = Convert.ToDouble(cost);
                profit = totalinv - (totalinv * discount) - cost2;
                label16.Text = "Profit: " + profit + " LBP";
                totalinv = 0;
                profit = 0;
                return 1;
            }

            catch 
            {
               int xi= searchdate();
               if (xi == 0)
               {
                   return 0;
               }
               else 
               {
                   return 1;
               }
            }

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var senderGrid = (DataGridView)sender;
            int show_details_status;
            show_details_status = 1;

            if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn &&
          e.RowIndex >= 0)
            {
                try
                {

                    int row_index = dataGridView2.CurrentCell.RowIndex;
                    string item_name = dataGridView2.Rows[row_index].Cells[0].Value.ToString();
                    rc.deleteItem(Convert.ToInt64(label4.Text), item_name,dataGridView2.Rows[row_index].Cells[2].Value.ToString());
                  show_details_status=showDetails(label4.Text);

                }

                catch
                {
                }

                if (show_details_status == 1)
                {
                    searchdate();
                    int ct=count_items_in_invoice(Convert.ToInt32(label4.Text));
                    if (ct == 0)
                    {
                        label4.Text = "";
                        label4.Hide();
                        label3.Hide();
                        label9.Hide();
                        label11.Hide();
                        label12.Hide();
                        label10.Hide();
                        label15.Hide();
                        label16.Hide();
                        label16.Hide();
                        dataGridView2.Hide();
                    }
                    else
                    {
                        showDetails(label4.Text);
                    }
                   
                }

            }
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
          //  HomeMan.ActiveForm();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
           // button2.Enabled = true;
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            //dateTimePicker1.Text
        }

       

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void Reports_Load(object sender, EventArgs e)
        {

        }

        private void label15_Click(object sender, EventArgs e)
        {
            
        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void label17_Click(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_BindingContextChanged(object sender, EventArgs e)
        {

        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            int row_index = dataGridView1.CurrentCell.RowIndex;
            string invoice_id = dataGridView1.Rows[row_index].Cells[0].Value.ToString();
            // dataGridView2.Rows.Clear();
            details = rc.show_invoice_details(invoice_id);
            DataTable dtInvoice = new DataTable();
            int rowcount = 0;
            dtInvoice.Columns.Add("ITEM", typeof(string));
            dtInvoice.Columns.Add("PRICE", typeof(long));
            

            for (int i = 0; i < details.Count; i += 4)
            {
                DataRow itemPrice = dtInvoice.NewRow();
                
                itemPrice["ITEM"] = details[i].ToString() +" * "+details[i+2];
                long item_price=Convert.ToInt64(details[i + 1]);
                long item_quantity_purschsed=Convert.ToInt64(details[i + 2]);
                long total=item_price*item_quantity_purschsed;
                itemPrice["PRICE"] = total;
                dtInvoice.Rows.Add(itemPrice);
            }

            string header = "Diet Center" + "\r\n";
            header += "Inv# : " + invoice_id + "\r\n";
            header += "Phone : 03/xxxxxx  " + "\r\n";
            header += "Address : Beirut  " + "\r\n" + "\r\n";
            string footer = string.Empty;
            int columnCount = dtInvoice.Columns.Count;
            int maxRows = dtInvoice.Rows.Count;
            double discount = Convert.ToDouble(dataGridView1.Rows[row_index].Cells[4].Value);
            using (Graphics g = e.Graphics)
            {
                Brush brush = new SolidBrush(Color.Black);
                Pen pen = new Pen(brush);
                Font font = new Font("Arial", 11);
                SizeF size;

                int x = 60, y = 10, width = 200;


                // Here title is written, sets to top-middle position of the page
                size = g.MeasureString(header, font);

                g.DrawString(header, font, brush, x, y);

                x = 0;
                y += 90;

                // Writes out all column names in designated locations, aligned as a table
                foreach (DataColumn column in dtInvoice.Columns)
                {
                    size = g.MeasureString(column.ColumnName, font);
                    //  x += 10;
                    g.DrawString(column.ColumnName, font, brush, x, y);
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


                        g.DrawString(row[i].ToString(), font, brush, x, y + 40);
                        x += width;
                    }

                    // e.HasMorePages = rowcount - 1 < maxRows;

                    x = 0;
                    y += 22;
                }
                y += 30;

                g.DrawString("-----------------------------------------------------------", font, brush, x, y);
                // x = 10;
                y += 20;
                double totallbpwithoutdiscount = Convert.ToDouble(dataGridView1.Rows[row_index].Cells[1].Value.ToString())*100/discount;
                g.DrawString("Total : " + dataGridView1.Rows[row_index].Cells[1].Value.ToString() + " LBP", font, brush, x + 40, y);
              //  x += 50;
                //y += 30;
                double totalusd = Convert.ToDouble(dataGridView1.Rows[row_index].Cells[1].Value.ToString());
               
                totalusd = totalusd / 1500;
                totalusd = Math.Round(totalusd, 2);
                 double totalusdwithoutdiscount = totalusd *100 /discount;

                y += 20;
                g.DrawString("Total : " + totalusd + " USD", font, brush, x+40, y);


                if (discount > 0) 
                {
                    y += 20;
                    g.DrawString("Total Without discount: " + totallbpwithoutdiscount + " LBP", font, brush, x + 40, y);

                    y += 20;
                    g.DrawString("Total Without discount : " + totalusdwithoutdiscount + " USD", font, brush, x + 40, y);
                }


                footer = "    Date " + dataGridView1.Rows[row_index].Cells[2].Value.ToString() + "\r\n";
                //y += 30;
                footer += "              Welcome!";
                g.DrawString(footer, font, brush, x = 60, y + 50);
            }
        }

        private void label16_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            string urlForbckup = ConfigurationManager.AppSettings["BackUpLink"].ToString();
            string ChromeUrl = ConfigurationManager.AppSettings["ChromeUrl"].ToString();
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = ChromeUrl;
            startInfo.Arguments = urlForbckup;
            Process.Start(startInfo);
        }
    }
}
