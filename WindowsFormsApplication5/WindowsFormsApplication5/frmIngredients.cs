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
    public partial class frmIngredients : Form
    {
        internal static DataGridViewRow b = null;
        MySQL r = new MySQL();
        public frmIngredients()
        {
            InitializeComponent();
        }

        private void frmIngredients_Load(object sender, EventArgs e)
        {
            lblPrice2.Text = b.Cells["TotalStocksOut"].Value.ToString();
            lblStackOut.Text = b.Cells["StocksOut"].Value.ToString();
            lblUnit.Text = b.Cells["Unit"].Value.ToString();
            lblTotalPrice.Text = b.Cells["TotalStocksIN"].Value.ToString();
            lblIngCode.Text = b.Cells["IngredientCode"].Value.ToString();
            lblIngName.Text = b.Cells["IngredientName"].Value.ToString();
            lblPrice.Text = b.Cells["UnitPrice"].Value.ToString();
            lblSize.Text = b.Cells["Size"].Value.ToString();
            lblTotal.Text = b.Cells["StocksIN"].Value.ToString();
            
       
        }

        private void button1_Click(object sender, EventArgs e)
        {


        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
 
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

        private void lblTotal_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Quantity_Click(object sender, EventArgs e)
        {

        }

        private void lblSize_Click(object sender, EventArgs e)
        {

        }

        private void lblPrice_Click(object sender, EventArgs e)
        {

        }

        private void lblIngCode_Click(object sender, EventArgs e)
        {

        }

        private void lblIngName_Click(object sender, EventArgs e)
        {

        }

        private void label20_Click(object sender, EventArgs e)
        {

        }

        private void label21_Click(object sender, EventArgs e)
        {

        }

        private void label22_Click(object sender, EventArgs e)
        {

        }

        private void label23_Click(object sender, EventArgs e)
        {

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
            if (textBox1.Text == "")
            {
                MessageBox.Show("Please fill up the quantity!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (textBox1.Text == "0")
            {
                MessageBox.Show("Cannot be zero!!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                string price1 = lblPrice.Text;
                string price3 = lblTotal.Text;

                double price = Convert.ToDouble(price1);
                double price2 = Convert.ToDouble(price3);
                double totalprice = price * price2;
                r.Connect();
                r.cmd = new MySqlCommand("UPDATE tblIngredients SET StocksIN=StocksIN +@Stocks,TotalStocksIn=TotalStocksIN+@TotalStocks,QuantityIN =QuantityIN+@QuantityIN where ID=@ID", r.con);
                r.cmd.Parameters.Add(new MySqlParameter("Stocks", lblTotal.Text));
                r.cmd.Parameters.Add(new MySqlParameter("TotalStocks", totalprice));
                r.cmd.Parameters.Add(new MySqlParameter("QuantityIN", textBox1.Text));
                r.cmd.Parameters.Add(new MySqlParameter("ID", b.Cells["ID"].Value));

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

   
    }
}
