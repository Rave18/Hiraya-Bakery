using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace WindowsFormsApplication5
{
    public partial class frmStocksOut : Form
    {
        internal static DataGridViewRow d = null;
        MySQL r = new MySQL();
        public frmStocksOut()
        {
            InitializeComponent();

        }

        private void button1_Click(object sender, EventArgs e)
        {


        }

        private void frmStocksOut_Load(object sender, EventArgs e)
        {
            lblUnit.Text = d.Cells["Unit"].Value.ToString();
            lblTotalPrice.Text = d.Cells["TotalStocksIN"].Value.ToString();
            lblIngCode.Text = d.Cells["IngredientCode"].Value.ToString();
            lblIngName.Text = d.Cells["IngredientName"].Value.ToString();
            lblPrice.Text = d.Cells["UnitPrice"].Value.ToString();
            lblSize.Text = d.Cells["Size"].Value.ToString();
            lblTotal.Text = d.Cells["StocksOUT"].Value.ToString();
            lblStocksIn.Text = d.Cells["StocksIN"].Value.ToString();
            lblPrice2.Text = d.Cells["TotalStocksIN"].Value.ToString();

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                lblTotal.Text = (Convert.ToDouble(lblSize.Text) * Convert.ToDouble(textBox1.Text)).ToString();

            }
            catch
            {
                lblTotal.Text = "";
            }

        }

        private void lblTotal_TextChanged(object sender, EventArgs e)
        {
            try
            {
                lblTotalPrice.Text = (Convert.ToDouble(lblPrice.Text) * Convert.ToDouble(lblTotal.Text)).ToString();

            }
            catch
            {
                lblTotalPrice.Text = "";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string price1 = lblPrice.Text;
            string price3 = lblTotal.Text;

            double price = Convert.ToDouble(price1);
            double price2 = Convert.ToDouble(price3);
            double totalprice = price * price2;


            if (Convert.ToDouble(lblTotal.Text) > Convert.ToDouble(lblStocksIn.Text))
            {
                MessageBox.Show("You do not have enough stocks!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            else if (textBox1.Text == "0")
            {
                MessageBox.Show("Cannot be zero!!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (textBox1.Text == "")
            {
                MessageBox.Show("Please fill up the quantity!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                r.Connect();
                r.cmd = new MySqlCommand("UPDATE tblIngredients SET QuantityOUT =QuantityOUT+@QuantityOUT,StocksOUT=StocksOUT +@StocksOUT,TotalStocksOUT=TotalStocksOUT +@TotalStocksOUT where ID=@ID", r.con);
                r.cmd.Parameters.Add(new MySqlParameter("QuantityOUT", textBox1.Text));
                r.cmd.Parameters.Add(new MySqlParameter("StocksOUT", lblTotal.Text));
                r.cmd.Parameters.Add(new MySqlParameter("TotalStocksOUT", totalprice));
                r.cmd.Parameters.Add(new MySqlParameter("ID", d.Cells["ID"].Value));
                r.cmd.ExecuteNonQuery();
                r.Disconnect();
                r.Connect();
                r.cmd = new MySqlCommand("UPDATE tblIngredients SET StocksIN=StocksIN -@StocksIN,TotalStocksIN=TotalStocksIN -@TotalStocksIN,QuantityIN =QuantityIN -@QuantityIN where ID=@ID", r.con);
                r.cmd.Parameters.Add(new MySqlParameter("StocksIN", lblTotal.Text));
                r.cmd.Parameters.Add(new MySqlParameter("TotalStocksIN", totalprice));
                r.cmd.Parameters.Add(new MySqlParameter("QuantityIN", textBox1.Text));
                r.cmd.Parameters.Add(new MySqlParameter("ID", d.Cells["ID"].Value));

                r.cmd.ExecuteNonQuery();
                r.Disconnect();

                MessageBox.Show("The quantity had been added successfully!", "Congratulations!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

   

     

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
    }
}
