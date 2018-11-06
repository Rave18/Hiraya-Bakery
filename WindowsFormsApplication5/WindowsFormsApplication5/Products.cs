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
    public partial class Products : Form
    {

        internal static DataGridView e = null;
        
        internal static string quantity = null;
        MySQL r = new MySQL();
        public Products()
        {
            
            InitializeComponent();
   
            Inventory();
            Quantity();
            //expire();

        }
        //private void expire()
      //  {

     //       foreach (DataGridViewRow dtg in dataGridView3.Rows)
          //  {
            //    var now = DateTime.Now;
               // var expirationdate = DateTime.Parse(dtg.Cells["ExpirationDate"].Value.ToString());

             //   if (now > expirationdate)
              //  {
              //      r.Connect();
                //    r.cmd = new MySqlCommand("Delete from tblInventory where ID=@ID", r.con);
       //             r.cmd.Parameters.Add(new MySqlParameter("ID", dtg.Cells["ID"].Value));
                 //   r.cmd.ExecuteNonQuery();
                  // r.Disconnect();
                   // foreach (DataGridViewRow dtgs in dtgAddProduct.SelectedRows)
                  //  {

                       // {
                        
                            
                            //r.Connect();
                            //r.cmd = new MySqlCommand("UPDATE tblAddProduct SET QuantityLeft= QuantityLeft - @Quantity where ID=@ID", r.con);
                            //r.cmd.Parameters.Add(new MySqlParameter("Quantity", dtgs.Cells["Quantity"].Value));
                            //r.cmd.Parameters.Add(new MySqlParameter("ID", dtgs.Cells["ID"].Value));
                            //r.cmd.ExecuteNonQuery();
                            //r.Disconnect();
                            
                  //      }
                //    }
              //  }
            //}
       //}
        private void Inventory()
        {
            r.Connect();
            MySqlDataAdapter da = new MySqlDataAdapter("SELECT tblInventory.ID,tblInventory.TransactionID as 'TransactionCode',tblAddProduct.ProductName,tblAddProduct.ID As 'ProductID',tblInventory.Quantity,tblAddProduct.Unit,tblInventory.Price,tblInventory.DateMade,tblInventory.ExpirationDate from tblInventory left join tblAddProduct on tblAddProduct.ID=tblInventory.ProductID ", r.con);
            r.cmd = da.SelectCommand;
            r.cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            da.Fill(dt);
            r.Disconnect();
            //dataGridView3.DataSource = dt;
          
        }


        
        private void Quantity()
        {
            r.Connect();
            MySqlDataAdapter da = new MySqlDataAdapter("select * from tblAddProduct", r.con);
            r.cmd = da.SelectCommand;
            r.cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            da.Fill(dt);
            r.Disconnect();
            dtgAddProduct.DataSource = dt;
            dataGridView1.DataSource = dt;
            dtgAddProduct2.DataSource = dt;

        }

        private void txtPrice_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            new SalesReport().Show();
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            new Inventory().Show();
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

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblTime.Text = DateTime.Now.ToString("hh:mm:ss");
            lblDate.Text = DateTime.Now.ToString("MMMM dd,yyyy");
        }

        private void button3_Click(object sender, EventArgs e)
        {
              r.Connect();
                MySqlDataAdapter da = new MySqlDataAdapter("select * from tblAddProduct where `ProductCode` = @ProdID", r.con);
                r.cmd = da.SelectCommand;
                r.cmd.Parameters.Add(new MySqlParameter("ProdID", txtPID.Text));
                r.dr = r.cmd.ExecuteReader();

                if (r.dr.Read())
                {
                   MessageBox.Show("You cannot have the same product code!","Error!",MessageBoxButtons.OK,MessageBoxIcon.Error);
                }
                else if (txtPrice.Text == "0")
                {
                    MessageBox.Show("Cannot be zero!!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (comboBox1.Text.Equals(""))
            {
                MessageBox.Show("Please fill up all the fields!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            else if (txtPID.Text.Equals(""))
            {
                MessageBox.Show("Please fill up all the fields!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (txtProdName.Text.Equals(""))
            {
                MessageBox.Show("Please fill up  all the fields!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (txtPrice.Text.Equals(""))
            {
                MessageBox.Show("Please fill up all the fields!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                r.Connect();
                r.cmd = new MySqlCommand("Insert into tblAddProduct VALUES(NULL,@ProductCode,@ProductName,@QuantityOrdered,@QuantityLeft,@Unit,@Price)", r.con);
                r.cmd.Parameters.Add(new MySqlParameter("ProductCode", txtPID.Text));
                r.cmd.Parameters.Add(new MySqlParameter("ProductName", txtProdName.Text));
                r.cmd.Parameters.Add(new MySqlParameter("QuantityOrdered", '0'));
                r.cmd.Parameters.Add(new MySqlParameter("QuantityLeft", '0'));
                r.cmd.Parameters.Add(new MySqlParameter("Unit", comboBox1.Text));
                r.cmd.Parameters.Add(new MySqlParameter("Price", txtPrice.Text));      
            
                r.dr = r.cmd.ExecuteReader();
                r.Disconnect();
                Quantity();
                MessageBox.Show("You added an order successfully!", "Congratulations!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtPID.Clear();
                txtPrice.Clear();
                txtProdName.Clear();
                comboBox1.SelectedIndex = -1;
            }
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = -1;
            txtPID.Text = "";
            txtProdName.Text = "";
            txtPrice.Text = "";
        }

        private void Products_Load(object sender, EventArgs e)
        {
  
            dtgAddProduct.CurrentCell = null;
            dtgAddProduct2.ClearSelection();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            if (dtgAddProduct2.CurrentCell == null)
            {
                MessageBox.Show("Please select an item!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (dtgAddProduct2.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select an item!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (textBox1.Text == "0")
            {
                MessageBox.Show("Cannot be zero!!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }






            else if (textBox1.Text.Equals(""))
            {
                MessageBox.Show("Please fill up the price of the item!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            else if (dtgAddProduct2.SelectedRows.Count > 0)
            {
                r.Connect();
                r.cmd = new MySqlCommand("Update tblAddProduct SET Price=@Price where ID=@ID", r.con);

                r.cmd.Parameters.Add(new MySqlParameter("Price", textBox1.Text));
                r.cmd.Parameters.Add(new MySqlParameter("ID", dtgAddProduct.SelectedRows[0].Cells["ID"].Value));
                r.dr = r.cmd.ExecuteReader();
                r.Disconnect();
                Quantity();
                MessageBox.Show("You edited a product successfully!", "Congratulations!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                lblPID.Text = "";
                lblProdName.Text = "";
                lblUnit.Text = "";
                textBox1.Text = "";

                dtgAddProduct2.CurrentCell = null;
            }
        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void dtgAddProduct2_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (dtgAddProduct2.SelectedRows.Count > 0)
            {
                DataGridViewRow dtgr = dtgAddProduct2.SelectedRows[0];

                lblPID.Text = dtgr.Cells["ID"].Value.ToString();
                lblProdName.Text = dtgr.Cells["ProductName"].Value.ToString();
                lblUnit.Text = dtgr.Cells["Unit"].Value.ToString();
                textBox1.Text = dtgr.Cells["Price"].Value.ToString();
            }
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        
            {
                string alphabet = "/,\';[]!@#$%&*()_=|}]{*/`/.,<>? .-qwertyuiopasdfghjklzxcvbnmQWERTYUIOPSADFGHJJKLZXCVBNM";

                if (alphabet.Contains(((char)e.KeyValue).ToString().ToLower()) || e.Shift)
                {
                    e.SuppressKeyPress = true;
                }
            }
        

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtPrice_KeyDown_1(object sender, KeyEventArgs e)
        {
            {
                string alphabet = "/,\';[]!@#$%&*()_=|}]{*/`/.,<>? .-qwertyuiopasdfghjklzxcvbnmQWERTYUIOPSADFGHJJKLZXCVBNM";

                if (alphabet.Contains(((char)e.KeyValue).ToString().ToLower()) || e.Shift)
                {
                    e.SuppressKeyPress = true;
                }
            }
        }

        private void txtProdName_KeyDown(object sender, KeyEventArgs e)
        {
            string allow = "qwertyuiopsadfghjklzxvcbnmQWERTYUIOPASDFGHJKLZXVCBNM .";
            if (!allow.Contains((char)e.KeyValue) && e.KeyCode != Keys.Back)
            {
                e.SuppressKeyPress = true;
            }
        }

        private void txtProdName_TextChanged(object sender, EventArgs e)
        {

        }

        private void button11_Click(object sender, EventArgs e)
        {
            Quantity();
            lblPID.Text = "";
            lblProdName.Text = "";
            lblUnit.Text = "";
            textBox1.Text = "";
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            Quantity();
            r.Connect();
            MySqlDataAdapter da = new MySqlDataAdapter("SELECT * from tblAddProduct WHERE ProductName LIKE @search", r.con);
            r.cmd = da.SelectCommand;
            r.cmd.Parameters.Add(new MySqlParameter("search", "%" + textBox3.Text + "%"));
            r.cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            da.Fill(dt);
            r.Disconnect();
            dtgAddProduct2.DataSource = dt;
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            Quantity();
            r.Connect();
            MySqlDataAdapter da = new MySqlDataAdapter("SELECT * from tblAddProduct WHERE ProductName LIKE @search", r.con);
            r.cmd = da.SelectCommand;
            r.cmd.Parameters.Add(new MySqlParameter("search", "%" + textBox2.Text + "%"));
            r.cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            da.Fill(dt);
            r.Disconnect();
            dtgAddProduct.DataSource = dt;
        }

        private void dtgAddProduct_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {
            
        }

        private void tbQuantity_TextChanged(object sender, EventArgs e)
        {

        }

        private void tabPage3_Click(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            frmQuantity.a = dataGridView1.SelectedRows[0];
            new frmQuantity().Show();
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

           
            }

        private void button20_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you want to log out?", "Logout?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Close();
                new Form1().Show();
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button19_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void button7_Click_1(object sender, EventArgs e)
        {
            
  
        }

        private void tabPage4_Click(object sender, EventArgs e)
        {

        }

        private void button13_Click(object sender, EventArgs e)
        {

        }
        private void tabPage6_Click(object sender, EventArgs e)
        {

        }

        private void button14_Click(object sender, EventArgs e)
        {
 

                       new frmIngredients().ShowDialog();

        }

        private void button15_Click(object sender, EventArgs e)
        {

   
        }

        private void txtSize_KeyDown(object sender, KeyEventArgs e)
        {
            string alphabet = "/,\';[]!@#$%&*()_=|}]{*/`/.,<>? .-qwertyuiopasdfghjklzxcvbnmQWERTYUIOPSADFGHJJKLZXCVBNM";

            if (alphabet.Contains(((char)e.KeyValue).ToString().ToLower()) || e.Shift)
            {
                e.SuppressKeyPress = true;
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

        private void button16_Click(object sender, EventArgs e)
        {
            ViewProducts.a = dataGridView1.SelectedRows[0];
            ViewProducts.b = dtgAddProduct;
            new ViewProducts().ShowDialog();
        }

        private void tabPage5_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {

        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you want to log out?", "Logout?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Close();
                new Form1().Show();
            }
        }

        private void dtgAddProduct2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void txtPrice_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtPrice_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtPID_KeyDown(object sender, KeyEventArgs e)
        {
            string allow = "qwertyuiopsadfghjklzxvcbnmQWERTYUIOPASDFGHJKLZXVCBNM123456789 .";
            if (!allow.Contains((char)e.KeyValue) && e.KeyCode != Keys.Back)
            {
                e.SuppressKeyPress = true;
            }
        }
        }
    }

