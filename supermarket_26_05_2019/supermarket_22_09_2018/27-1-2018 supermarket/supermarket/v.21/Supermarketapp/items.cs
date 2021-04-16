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
    public partial class items : Form
    {
        Itemsclass i1 = new Itemsclass();
        int i = -1;
        string[,] Iq2;
        string availability;
        string[] itt2 = new string[5];
        public items()
        {
            InitializeComponent();
            button2.Enabled = false;
            button4.Enabled = false;
            button1.Enabled = false;
            comboBox1.Items.Add("%");
            comboBox1.Items.Add("LBP");
            comboBox1.Text = "LBP";
            checkBox1.Checked = true; addNoBarcodeItems();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "" || textBox5.Text == "")
                {
                    MessageBox.Show("Please fill all the data");
                }
                else
                {
                    if (comboBox1.Text.CompareTo("%") == 0)
                    {
                        long price = calculatePrice(Convert.ToInt32(textBox5.Text));
                        i1.additem(textBox1.Text, textBox2.Text, textBox3.Text, textBox4.Text, price.ToString(), checkBox1.Checked);
                        // button1.Enabled = false;
                    }

                    else
                    {
                        i1.additem(textBox1.Text, textBox2.Text, textBox3.Text, textBox4.Text, textBox5.Text, checkBox1.Checked);
                        // button1.Enabled = false;
                    }
                }
            }

            else
            {
                if (textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "" || textBox5.Text == "")
                {
                    MessageBox.Show("Please fill all the data");
                }
                else
                {
                    if (comboBox1.Text.CompareTo("%") == 0)
                    {
                        long price = calculatePrice(Convert.ToInt32(textBox5.Text));
                        i1.additem(textBox1.Text, textBox2.Text, textBox3.Text, textBox4.Text, price.ToString(), checkBox1.Checked);
                        // button1.Enabled = false;
                    }

                    else
                    {
                        i1.additem(textBox1.Text, textBox2.Text, textBox3.Text, textBox4.Text, textBox5.Text, checkBox1.Checked);
                        //   button1.Enabled = false;
                    }
                }
            }

            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            addNoBarcodeItems();
        }

        internal static void ActiveForm()
        {
            //throw new NotImplementedException();
            items i2 = new items();
            i2.ShowDialog();
        }

        private long calculatePrice(int percentage)
        {
            long price = percentage * (Convert.ToInt32(textBox4.Text) / 100);
            return price + Convert.ToInt64(textBox4.Text);
        }


        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            Iq2 = i1.Showitems();
            int height = Iq2.GetLength(0);
            int width = Iq2.GetLength(1);
            for (int i = 0; i < height; i++)
            {
                dataGridView1.Rows.Add();
                for (int j = 0; j < width; j++)
                {

                    dataGridView1.Rows[i].Cells[j].Value = Iq2[i, j];
                    dataGridView1.Rows[i].Cells[j].Value = Iq2[i, j];
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

            if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "" || textBox5.Text == "")
            {
                MessageBox.Show("Please fill all the data");
            }
            else
            {
                string itemid = textBox1.Text;
                string itemname = textBox2.Text;
                string qis = textBox3.Text;
                string cost = textBox4.Text;
                string price = textBox5.Text;
                i1.updateitem(itemid, itemname, qis, cost, price);
                textBox1.Text = "";
                textBox1.Enabled = true;
                textBox2.Text = "";
                textBox3.Text = "";
                textBox4.Text = "";
                textBox5.Text = "";
                button2.Enabled = false;
                checkBox1.Checked = true;
            }
            comboBox2.SelectedIndex = -1;
            comboBox2.Items.Clear();
            addNoBarcodeItems();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }



        private void button4_Click(object sender, EventArgs e)
        {
            textBox1.Enabled = false;
            string item_id = textBox1.Text.ToString();
            itt2 = i1.edititems(item_id);
            textBox1.Text = itt2[0];
            textBox2.Text = itt2[1];
            textBox3.Text = itt2[2];
            textBox4.Text = itt2[3];
            textBox5.Text = itt2[4];
            button2.Enabled = true;
            button4.Enabled = false;

        }
        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            //  e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            if (!System.Text.RegularExpressions.Regex.IsMatch(textBox4.Text, "[0-9]"))
            {
                textBox4.Clear();
                // MessageBox.Show("The cost should be an integer");

            }
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            if (!System.Text.RegularExpressions.Regex.IsMatch(textBox5.Text, "[0-9]"))
            {
                textBox5.Clear();
                // MessageBox.Show("The price should be an intmesseger");
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            button1.Enabled = false;
            button2.Enabled = false;
            button4.Enabled = false;
            comboBox2.SelectedIndex = -1;
            checkBox1.Checked = true;

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                textBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";
                textBox4.Text = "";
                textBox5.Text = "";
                textBox1.Enabled = true;
                button1.Enabled = false;
                comboBox2.Enabled = false;
            }

            else
            {
                textBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";
                textBox4.Text = "";
                textBox5.Text = "";
                textBox1.Text = "";
                textBox1.Enabled = false;
                button1.Enabled = true;
                comboBox2.Enabled = true;
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void textBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

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

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void items_Load(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (comboBox2.SelectedIndex == -1) { }//do nothing
            else
            {
                string[] item_details = i1.getInfoForNoBarcodeItem(comboBox2.Text);
                textBox1.Text = item_details[0].ToString();
                textBox2.Text = item_details[1].ToString();
                textBox3.Text = item_details[2].ToString();
                textBox4.Text = item_details[3].ToString();
                textBox5.Text = item_details[4].ToString();
                button1.Enabled = false;
                button4.Enabled = false;
                button2.Enabled = true;
            }
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (!System.Text.RegularExpressions.Regex.IsMatch(textBox1.Text, "[0-9]"))
            {
                textBox1.Clear();
            }
            else
            {
                if (e.KeyCode == Keys.Enter)
                {
                    if (textBox1.Text != "")
                    {
                        availability = i1.checkbarcode(textBox1.Text);
                        if (availability.CompareTo("1") == 0)
                        {
                            button1.Enabled = false;
                            button4.Enabled = true;
                        }
                        else
                        {
                            button1.Enabled = true;
                            button4.Enabled = false;
                        }
                    }

                    else
                    {
                        button1.Enabled = true;
                        button4.Enabled = false;
                    }

                }
            }
        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int colIndex = Convert.ToInt32(e.ColumnIndex);
            if (colIndex == 0)
            {

                string itemName = dataGridView1[e.ColumnIndex, e.RowIndex].Value.ToString();
                Reportsclass rc = new Reportsclass();
                Tuple<long, string, int> tuple = rc.fetchitemid(itemName);
                textBox1.Text = tuple.Item1 + "";
                int item3 =Convert.ToInt32(tuple.Item3);
                if (item3 == 1)
                {
                    //checkBox1.Checked = false;
                    //comboBox2.Enabled = true;
                    comboBox2.Text = tuple.Item2.ToString();
                }
                else 
                {
                    comboBox2.Enabled = false;
                    //checkBox1.Checked = true;
                }

                button4.Enabled = true;
                button4.PerformClick();

            }
        }

    }
}


