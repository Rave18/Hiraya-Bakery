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
    public partial class Inventory : Form
    {
        MySQL r = new MySQL();
        public Inventory()
        {
            InitializeComponent();
            Ingredients();
        }
        private void Ingredients()
        {
            r.Connect();
            MySqlDataAdapter da = new MySqlDataAdapter("SELECT * from tblIngredients ", r.con);
            r.cmd = da.SelectCommand;
            r.cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            da.Fill(dt);
            r.Disconnect();
            dtgIngredients.DataSource = dt;
            dataGridView1.DataSource = dt;
            dataGridView2.DataSource = dt;
        }
        private void button13_Click(object sender, EventArgs e)
        {
            if (txtIngCode.Text == "")
            {
                MessageBox.Show("Please fill up the ingredient code!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (txtIngName.Text == "")
            {
                MessageBox.Show("Please fill up the ingredient name!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (comboBox2.Text == "")
            {
                MessageBox.Show("Please fill up the unit!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (txtSize.Text == "")
            {
                MessageBox.Show("Please fill up the size!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (txtPrice2.Text == "")
            {
                MessageBox.Show("Please fill up the unit price!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                r.Connect();
                MySqlDataAdapter da = new MySqlDataAdapter("select * from tblIngredients where `IngredientCode` = @IngredientCode", r.con);
                r.cmd = da.SelectCommand;
                r.cmd.Parameters.Add(new MySqlParameter("IngredientCode", txtIngCode.Text));
                r.dr = r.cmd.ExecuteReader();

                if (r.dr.Read())
                {
                    MessageBox.Show("You cannot have the same product code!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    r.Connect();
                    r.cmd = new MySqlCommand("Insert into tblIngredients VALUES(NULL,@IngredientCode,@IngredientName,@Size,@Unit,@UnitPrice,@QuantityIN,@Stocks,@TotalStocksIn,@QuantityOUT,@StocksOut,@TotalStocksOut)", r.con);
                    r.cmd.Parameters.Add(new MySqlParameter("IngredientCode", txtIngCode.Text));
                    r.cmd.Parameters.Add(new MySqlParameter("IngredientName", txtIngName.Text));
                    r.cmd.Parameters.Add(new MySqlParameter("Size", txtSize.Text));
                    r.cmd.Parameters.Add(new MySqlParameter("Unit", comboBox2.Text));
                    r.cmd.Parameters.Add(new MySqlParameter("UnitPrice", txtPrice2.Text));
                    r.cmd.Parameters.Add(new MySqlParameter("QuantityIN", '0'));
                    r.cmd.Parameters.Add(new MySqlParameter("Stocks", '0'));
                    r.cmd.Parameters.Add(new MySqlParameter("TotalStocksIn", '0'));
                    r.cmd.Parameters.Add(new MySqlParameter("QuantityOUT", '0'));
                    r.cmd.Parameters.Add(new MySqlParameter("StocksOut", '0'));
                    r.cmd.Parameters.Add(new MySqlParameter("TotalStocksOut", '0'));


                    r.dr = r.cmd.ExecuteReader();
                    r.Disconnect();
                    MessageBox.Show("The ingredient has been added!", "Congratulations!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Ingredients();
                }
            }
        }

        private void button15_Click(object sender, EventArgs e)
        {
            frmStocksOut.d = dtgIngredients.SelectedRows[0];
            new frmStocksOut().ShowDialog();
            Ingredients();
        }

        private void button14_Click(object sender, EventArgs e)
        {


            frmIngredients.b = dtgIngredients.SelectedRows[0];
            new frmIngredients().ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            
        }

        private void button5_Click(object sender, EventArgs e)
        {
            new Products().Show();
            this.Close();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            new frmOrder().Show();
            this.Close();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            new frmOrderList().Show();
            this.Close();
        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView2_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            foreach (DataGridViewRow dtg in dataGridView2.SelectedRows)
            {
                lblIngCode.Text = dtg.Cells["IngredientCode"].Value.ToString();
                lblIngName.Text = dtg.Cells["IngredientName"].Value.ToString();
                lblSize.Text = dtg.Cells["Size"].Value.ToString();
                lblUnit.Text = dtg.Cells["Unit"].Value.ToString();
                


            }
        }

        private void tabPage3_Click(object sender, EventArgs e)
        
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (txtUnitPrice.Text == "")
            {
                MessageBox.Show("Please fill up the required field!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (dataGridView2.CurrentCell== null)
            {
                MessageBox.Show("Please choose an ingredient!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {

                r.Connect();
                r.cmd = new MySqlCommand("Update tblIngredients SET UnitPrice=@UnitPrice where ID=@ID", r.con);

                r.cmd.Parameters.Add(new MySqlParameter("UnitPrice", txtUnitPrice.Text));
                r.cmd.Parameters.Add(new MySqlParameter("ID", dataGridView2.SelectedRows[0].Cells["ID"].Value));
                r.dr = r.cmd.ExecuteReader();
                r.Disconnect();
                MessageBox.Show("You updated the ingredient!", "Congratulations!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Ingredients();
            }
            

        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you want to log out?", "Logout?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Close();
                new Form1().Show();
            }
        }

        private void txtPrice2_KeyDown(object sender, KeyEventArgs e)
        {
            string alphabet = "/,\';[]!@#$%&*()_=|}]{*/`/.,<>? .-qwertyuiopasdfghjklzxcvbnmQWERTYUIOPSADFGHJJKLZXCVBNM";

            if (alphabet.Contains(((char)e.KeyValue).ToString().ToLower()) || e.Shift)
            {
                e.SuppressKeyPress = true;
            }
        }

        private void txtSize_KeyDown(object sender, KeyEventArgs e)
        {
            string alphabet = "/,\';[]!@#$%&*()_=|}]{*/`/.,<>? .-qwertyuiopasdfghjklzxcvbnmQWERTYUIOPSADFGHJJKLZXCVBNM";

            if (alphabet.Contains(((char)e.KeyValue).ToString().ToLower()) || e.Shift)
            {
                e.SuppressKeyPress = true;
            }
        }

        private void txtUnitPrice_TextChanged(object sender, EventArgs e)
        {

        }

        private void button19_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblTime.Text = DateTime.Now.ToString("hh:mm:ss");
            lblDate.Text = DateTime.Now.ToString("MMMM dd,yyyy");
        }

        private void dtgIngredients_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {
           

        }

        private void button3_Click(object sender, EventArgs e)
        {
            MySQL.dtReports = new DataTable();
            MySQL.dtReports.Columns.Add("IngredientCode");
            MySQL.dtReports.Columns.Add("IngredientName");
            MySQL.dtReports.Columns.Add("Size");
            MySQL.dtReports.Columns.Add("Unit");
            MySQL.dtReports.Columns.Add("UnitPrice");
            MySQL.dtReports.Columns.Add("QuantityIN");
            MySQL.dtReports.Columns.Add("StocksIN");
            MySQL.dtReports.Columns.Add("TotalStocksIN");
            MySQL.dtReports.Columns.Add("QuantityOUT");
            MySQL.dtReports.Columns.Add("StocksOUT");
            MySQL.dtReports.Columns.Add("TotalStocksOUT");
            MySQL.dtReports.Columns.Add("TotalExpenseUsed");
            MySQL.dtReports.Columns.Add("TotalIngredientsStocks");
            double total1 = 0;
            double total = 0;
            foreach (DataGridViewRow dtgr in dtgIngredients.Rows)
            {
                total += double.Parse((dtgr.Cells["TotalStocksOut"].Value.ToString()));
                total1 += double.Parse((dtgr.Cells["TotalStocksIN"].Value.ToString()));
                MySQL.dtReports.Rows.Add(new object[]{
                    dtgr.Cells["IngredientCode"].Value, dtgr.Cells["IngredientName"].Value,dtgr.Cells["Size"].Value,dtgr.Cells["Unit"].Value,dtgr.Cells["UnitPrice"].Value,dtgr.Cells["QuantityIN"].Value,dtgr.Cells["StocksIN"].Value,dtgr.Cells["TotalStocksIN"].Value,dtgr.Cells["QuantityOUT"].Value,dtgr.Cells["StocksOUT"].Value,dtgr.Cells["TotalStocksOUT"].Value,total,total1});
            }
            ReportViewer.rptReport = new rptInventory();
            new ReportViewer().Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            new SalesReport().Show();
        }

        

        private void txtPrice2_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtSize_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void txtPrice2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar)
      && !char.IsDigit(e.KeyChar)
      && e.KeyChar != '.')
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if (e.KeyChar == '.'
                && (sender as TextBox).Text.IndexOf('.') > -1)
            {
                e.Handled = true;
            }
        }

        private void txtUnitPrice_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar)
      && !char.IsDigit(e.KeyChar)
      && e.KeyChar != '.')
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if (e.KeyChar == '.'
                && (sender as TextBox).Text.IndexOf('.') > -1)
            {
                e.Handled = true;
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you want to reset the stocks out?", "Reset?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {

            foreach (DataGridViewRow dtgvr in dtgIngredients.Rows)
            {
            

                    r.Connect();
                    r.cmd = new MySqlCommand("Update tblIngredients SET StocksOUT=0", r.con);

                    r.dr = r.cmd.ExecuteReader();
                    r.Disconnect();
                    Ingredients();

                    r.Connect();
                    r.cmd = new MySqlCommand("Update tblIngredients SET QuantityOUT=0", r.con);

                    r.dr = r.cmd.ExecuteReader();
                    r.Disconnect();
                    Ingredients();

                    r.Connect();
                    r.cmd = new MySqlCommand("Update tblIngredients SET TotalStocksOut=0", r.con);

                    r.dr = r.cmd.ExecuteReader();
                    r.Disconnect();
                    Ingredients();
                }
            }
        }

        private void txtIngCode_KeyDown(object sender, KeyEventArgs e)
        {
            string allow = "qwertyuiopsadfghjklzxvcbnmQWERTYUIOPASDFGHJKLZXVCBNM123456789 .";
            if (!allow.Contains((char)e.KeyValue) && e.KeyCode != Keys.Back)
            {
                e.SuppressKeyPress = true;
            }
        }

        private void txtIngName_KeyDown(object sender, KeyEventArgs e)
        {
            string allow = "qwertyuiopsadfghjklzxvcbnmQWERTYUIOPASDFGHJKLZXVCBNM .";
            if (!allow.Contains((char)e.KeyValue) && e.KeyCode != Keys.Back)
            {
                e.SuppressKeyPress = true;
            }
        }
    }
}

        
       
