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
    public partial class frmOrder : Form
    {
        MySQL r = new MySQL();
        public frmOrder()
        {
            InitializeComponent();
            LoadProduct();
            LoadTable();
            AddToCart();
          
        }
        private void LoadProduct()
        {
            r.Connect();
            MySqlDataAdapter da = new MySqlDataAdapter("SELECT * from tblAddClient", r.con);
            r.cmd = da.SelectCommand;
            r.cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            da.Fill(dt);
            r.Disconnect();
            dtgAddClient.DataSource = dt;
            dtgAddClient.ClearSelection();
            lblItemOrdered.Text = "";
            lblPrice.Text = "";
            lblTotal.Text = "";
            lblProductID.Text = "";
            dtgAddProduct.CurrentCell = null;
            textBox4.Text = "";
         
            lblUnit.Text = "";
            textBox1.Visible = true;
            textBox4.Visible = false;
            txtProdName.Text = "";
            textBox4.Visible = false;
            dateTimePicker1.Enabled = true;
            dateTimePicker2.Enabled = true;
          
            comboBox1.Enabled = true;
            dtgAddProduct.Enabled = true;
            txtProdName.Visible = true;
            txtProdName.Text = "";
            dtgAddClient.Enabled=true;
            dtgAddClient.CurrentCell = null;
            txtClientName.Text = "";
            txtContNum.Text= "";
            txtAddress.Text="";
            txtEmail.Text="";
            comboBox1.SelectedIndex = -1;
            txtTranscode.Text = "";
            txtTranscode.ReadOnly = false;
        }
        private void LoadTable()
        {
            r.Connect();
            MySqlDataAdapter da = new MySqlDataAdapter("select ID,ProductCode,ProductName,Unit,Price from tblAddProduct", r.con);
            r.cmd = da.SelectCommand;
            r.cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            da.Fill(dt);
            r.Disconnect();
            dtgAddProduct.DataSource = dt;
            dtgAddProduct.ClearSelection();
        }
        private void AddToCart()
        {
            r.Connect();
            MySqlDataAdapter da = new MySqlDataAdapter("select tblAddToCart.ID,tblAddToCart.ClientName,tblAddToCart.ProductID,tblAddProduct.ProductCode,tblAddProduct.ProductName,tblAddProduct.Unit,tblAddToCart.Quantity,tblAddToCart.Price from tblAddToCart left join tblAddProduct on tblAddProduct.ID=tblAddToCart.ProductID;", r.con);
            r.cmd = da.SelectCommand;
            r.cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            da.Fill(dt);
            r.Disconnect();
            dataGridView1.DataSource = dt;
            dataGridView1.ClearSelection();
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedValueChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_MouseClick(object sender, MouseEventArgs e)
        {

        }

        private void button8_Click_1(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click_1(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dtgAddProduct_RowEnter(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dtgAddProduct_Click(object sender, EventArgs e)
        {

        }

        private void dtgAddProduct_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void dtgAddProduct_MouseClick(object sender, MouseEventArgs e)
        {

        }

        private void panel6_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            new Inventory().Show();
            this.Hide();
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            new Products().Show();
            this.Hide();

        }

        private void button8_Click(object sender, EventArgs e)
        {
            new frmOrderList().Show();
            this.Hide();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblTime.Text = DateTime.Now.ToString("hh:mm:ss");
            lblDate.Text = DateTime.Now.ToString("MMMM dd,yyyy");
        }

        private void frmOrder_Load(object sender, EventArgs e)
        {
            dtgAddClient.ClearSelection();
            dtgAddProduct.ClearSelection();
            textBox4.Visible = false;
            txtMeetup.Visible = false;
            textBox4.Visible = false;

         

            dateTimePicker2.Value = DateTime.Today.AddDays(1);
        }

        private void button11_Click(object sender, EventArgs e)
        {
            r.Connect();
            MySqlDataAdapter dm = new MySqlDataAdapter("select * from tblOrderedProducts where `TransactionCode` = @Transcode", r.con);
            r.cmd = dm.SelectCommand;
            r.cmd.Parameters.Add(new MySqlParameter("Transcode", txtTranscode.Text));
            r.dr = r.cmd.ExecuteReader();

            if (r.dr.Read())
            {
                MessageBox.Show("You entered an existing transaction code!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
          else if (txtClientName.Text == "")
            {
                MessageBox.Show("Please choose a client!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (lblProductID.Text.Equals(""))
            {
                MessageBox.Show("Please select a product!", "error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (dtgAddProduct.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a product!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (comboBox1.Text.Equals(""))
            {
                MessageBox.Show("Please choose a service option!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            else if (textBox4.Text.Equals(""))
            {
                MessageBox.Show("Please choose a quantity!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            else if (dateTimePicker2.Value < dateTimePicker1.Value)
            {
                MessageBox.Show("You inputted a wrong pick up date!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (dateTimePicker1.Value.Date > DateTime.Now.Date)
            {
                MessageBox.Show("Cannot be greater than today!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (comboBox1.Text == "Delivery" && txtMeetup.Text == "")
            {

                MessageBox.Show("Please choose a meetup point!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);



            }
            else if (textBox4.Text == "0")
            {
                MessageBox.Show("Quantity cannot be 0!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);

            } else if (txtTranscode.Text == "")
            {
                MessageBox.Show("You have to fill up the transaction code!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (MessageBox.Show("Do you want to add this to cart?", "Order?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {

                r.Connect();
                MySqlDataAdapter da = new MySqlDataAdapter("select * from tblAddToCart where `ProductCode` = @ProdCode", r.con);
                r.cmd = da.SelectCommand;
                r.cmd.Parameters.Add(new MySqlParameter("ProdCode", lblProductID.Text));
                r.dr = r.cmd.ExecuteReader();

                if (r.dr.Read())
                {
                    string ID = r.dr["ID"].ToString();
                    string Quantity = r.dr["Quantity"].ToString();
                    string Price = r.dr["Price"].ToString();


                    int Quantity1 = Convert.ToInt16(Quantity);
                    int AddQuantity = Convert.ToInt16(textBox4.Text);
                    int TotalQuantity = Quantity1 + AddQuantity;
                    
                    double Price1 = Convert.ToDouble(Price);

                    string PriceCompute = lblTotal.Text;
                    double Price2 = Convert.ToDouble(PriceCompute);
                    double TotalPrice = Price2 + Price1;

                    r.Connect();
                    r.cmd = new MySqlCommand("Update tblAddToCart SET ClientName=@ClientName,ProductID = @ProductID,ProductCode=@ProductCode,Quantity=@Quantity,Price=@Price where ID=@ID", r.con);
                    r.cmd.Parameters.Add(new MySqlParameter("ClientName", txtClientName.Text));
                    r.cmd.Parameters.Add(new MySqlParameter("ProductID", dtgAddProduct.SelectedRows[0].Cells["ID"].Value));
                    r.cmd.Parameters.Add(new MySqlParameter("ProductCode", lblProductID.Text));
                    r.cmd.Parameters.Add(new MySqlParameter("Quantity", TotalQuantity.ToString()));
                    r.cmd.Parameters.Add(new MySqlParameter("Price", TotalPrice.ToString()));
               
                    r.cmd.Parameters.Add(new MySqlParameter("ID", ID));
                    r.dr = r.cmd.ExecuteReader();
                    r.Disconnect();
                    AddToCart();
                    MessageBox.Show("You added this to the cart!", "Congratulations!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    lblItemOrdered.Text = "";
                    lblPrice.Text = "";
                    lblTotal.Text = "";
                    lblProductID.Text = "";
                    dtgAddProduct.CurrentCell = null;
                    textBox4.Text = "";
     
                    lblUnit.Text = "";
                    txtMeetup.ReadOnly = true;
                    textBox4.Visible = false;
                    txtProdName.Text = "";
                    textBox4.Visible = false;
                    dateTimePicker1.Enabled = false;
                    dateTimePicker2.Enabled = false;
           
                    comboBox1.Enabled = false;
                    dtgAddProduct.Enabled = true;
                    txtProdName.Visible = true;
                    txtProdName.Text = "";
                    txtTranscode.ReadOnly = true;

                }
                else
                {
                    

                        r.Connect();
                        r.cmd = new MySqlCommand("Insert into tblAddToCart VALUES(NULL,@ClientName,@ProductID,@ProductCode,@Quantity,@Price)", r.con);
                        r.cmd.Parameters.Add(new MySqlParameter("ClientName", txtClientName.Text));
                        r.cmd.Parameters.Add(new MySqlParameter("ProductID", dtgAddProduct.SelectedRows[0].Cells["ID"].Value));

                        r.cmd.Parameters.Add(new MySqlParameter("ProductCode", lblProductID.Text));
                        r.cmd.Parameters.Add(new MySqlParameter("Quantity", textBox4.Text));
                        r.cmd.Parameters.Add(new MySqlParameter("Price", lblTotal.Text));
                       
                        r.cmd.ExecuteNonQuery();
                        r.Disconnect();

                        AddToCart();
                        MessageBox.Show("You added this to the cart!", "Congratulations!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        lblItemOrdered.Text = "";
                        lblPrice.Text = "";
                        lblTotal.Text = "";
                        lblProductID.Text = "";
                        dtgAddProduct.CurrentCell = null;
                        textBox4.Text = "";
   
                        lblUnit.Text = "";
                        txtMeetup.ReadOnly = true;
                        textBox4.Visible = false;
                        txtProdName.Text = "";
                        textBox4.Visible = false;
                        dateTimePicker1.Enabled = false;
                        dateTimePicker2.Enabled = false;
                      
                        comboBox1.Enabled = false;
                        dtgAddProduct.Enabled = true;
                        txtProdName.Visible = true;
                        txtProdName.Text = "";
                        txtTranscode.ReadOnly = true;
                    }
                

                
            }
        }

        private void dtgAddClient_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (dtgAddClient.SelectedRows.Count > 0)
            {
                DataGridViewRow dtgr = dtgAddClient.SelectedRows[0];

                txtClientName.Text = dtgr.Cells["ClientName"].Value.ToString();
                txtEmail.Text = dtgr.Cells["Email"].Value.ToString();
                txtAddress.Text = dtgr.Cells["Address"].Value.ToString();
                txtContNum.Text = dtgr.Cells["ContactNumber"].Value.ToString();

            }
        }

        private void dtgAddClient_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dtgAddProduct_RowEnter_1(object sender, DataGridViewCellEventArgs e)
        {
            if (dtgAddProduct.SelectedRows.Count > 0)
            {
                textBox4.Visible = true;

         

                DataGridViewRow dtgr = dtgAddProduct.SelectedRows[0];
                lblProductID.Text = dtgr.Cells["ProductCode"].Value.ToString();
                lblUnit.Text = dtgr.Cells["Unit"].Value.ToString();
                lblItemOrdered.Text = dtgr.Cells["ProductName"].Value.ToString();
                lblPrice.Text = dtgr.Cells["Price"].Value.ToString();
            }
        }

        private void textBox4_TextChanged_1(object sender, EventArgs e)
        {
            
            try
            {
                lblTotal.Text = (Convert.ToDouble(textBox4.Text) * Convert.ToDouble(lblPrice.Text)).ToString();

            }
            catch
            {
                lblTotal.Text = "";
            }
        }

        private void comboBox1_SelectedValueChanged_1(object sender, EventArgs e)
        {
            if (comboBox1.Text == "Delivery")
            {
                txtMeetup.Visible = true;

            }
            else
            {

                txtMeetup.Text = "";
                txtMeetup.Visible = false;
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentCell == null)
            {

                MessageBox.Show("There are no items in the cart!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (MessageBox.Show("Do you want to clear the cart?", "Order?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                r.Connect();
                r.cmd = new MySqlCommand("Truncate table tblAddToCart", r.con);
                r.cmd.ExecuteNonQuery();
                r.Disconnect();
                AddToCart();
                MessageBox.Show("Cart Cleared!", "Congratulations!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                lblItemOrdered.Text = "";
                lblPrice.Text = "";
                lblTotal.Text = "";
                lblProductID.Text = "";
                dtgAddProduct.CurrentCell = null;
                textBox4.Text = "";
        
                lblUnit.Text = "";


            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you want to reset field?", "Reset?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
               
                dateTimePicker2.Value = DateTime.Today.AddDays(1);

                txtTranscode.ReadOnly = false;
                comboBox1.SelectedIndex = -1;
                dateTimePicker1.Enabled = true;
                dateTimePicker2.Enabled = true;
                txtMeetup.ReadOnly = false;
                comboBox1.Enabled = true;
                txtMeetup.Text = "";
                MessageBox.Show("Field Cleared!", "Congratulations!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    

        private void button3_Click(object sender, EventArgs e)
        {
             if (MessageBox.Show("Do you want to reset this form? All your orders will be cleared", "Continue?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                r.Connect();
                r.cmd = new MySqlCommand("Truncate table tblAddToCart", r.con);
                r.cmd.ExecuteNonQuery();
                r.Disconnect();
                AddToCart();
                MessageBox.Show("Field Cleared!", "Congratulations!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dateTimePicker1.Value = DateTime.Today;
                dateTimePicker2.Value = DateTime.Today.AddDays(1);
                dtgAddClient.CurrentCell = null;
                txtClientName.Text = "";
                txtAddress.Text = "";
                txtContNum.Text = "";
                txtEmail.Text = "";
                dtgAddClient.Enabled = true;
                lblUnit.Text = "";
                comboBox1.SelectedIndex = -1;
                dateTimePicker1.Enabled = true;
                dateTimePicker2.Enabled = true;
                txtMeetup.ReadOnly = false;
                comboBox1.Enabled = true;
                lblItemOrdered.Text = "";
                lblPrice.Text = "";
                lblTotal.Text = "";
                lblProductID.Text = "";
                dtgAddProduct.CurrentCell = null;
                textBox4.Text = "";
                textBox1.Visible = true;
                txtMeetup.Text = "";

                txtTranscode.ReadOnly = false;
            }
       
        }

        private void button13_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select an item on the cart!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (dataGridView1.SelectedRows.Count > 0)
            {

                if (MessageBox.Show("Do you want to remove an item from the cart?", "Delete?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    r.Connect();
                    r.cmd = new MySqlCommand("Delete from tblAddToCart where ID=@ID", r.con);
                    r.cmd.Parameters.Add(new MySqlParameter("ID", dataGridView1.SelectedRows[0].Cells["ID"].Value));
                    r.dr = r.cmd.ExecuteReader();
                    r.Disconnect();
                    AddToCart();
                    MessageBox.Show("You removed an item from the cart successfully!", "Congratulations!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void button14_Click(object sender, EventArgs e)
        {
            
            
            if (dtgAddClient.CurrentCell == null)
            {
                MessageBox.Show("Please choose a client!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            else if (comboBox1.Text == "")
            {
                MessageBox.Show("Please choose a service option!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (dataGridView1.CurrentCell == null)
            {

                MessageBox.Show("There are no items in the cart!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            else
            {
       
                Reciept.a = dtgAddClient.SelectedRows[0];
                Reciept.b = dtgAddProduct;
                Reciept.d = dataGridView1;
                Reciept.service = comboBox1.Text;
                Reciept.meetup = txtMeetup.Text;
                Reciept.transcode = txtTranscode.Text;
                Reciept.Dateordered = dateTimePicker1.Value;
                Reciept.Pickup = dateTimePicker2.Value;

                Reciept.quantity = textBox4.Text;
                Reciept.price = lblPrice.Text;
                Reciept.total = lblTotal.Text;
         
                lblItemOrdered.Text = "";
                lblPrice.Text = "";
                lblTotal.Text = "";
                lblProductID.Text = "";
                dtgAddProduct.CurrentCell = null;
                textBox4.Text = "";
              
                lblUnit.Text = "";
                txtMeetup.ReadOnly = true;
                textBox4.Visible = false;
                txtProdName.Text = "";
                textBox4.Visible = false;
                dateTimePicker1.Enabled = false;
                dateTimePicker2.Enabled = false;

                comboBox1.Enabled = false;
                dtgAddProduct.Enabled = true;
                txtProdName.Visible = true;
                txtProdName.Text = "";
                
                new Reciept().ShowDialog();
                AddToCart();
                LoadProduct();
                
            }
        }

        private void textBox2_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            LoadTable();
            r.Connect();
            MySqlDataAdapter da = new MySqlDataAdapter("SELECT * from tblAddProduct WHERE ProductName LIKE @search", r.con);
            r.cmd = da.SelectCommand;
            r.cmd.Parameters.Add(new MySqlParameter("search", "%" + txtProdName.Text + "%"));
            r.cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            da.Fill(dt);
            r.Disconnect();
            dtgAddProduct.DataSource = dt;
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            LoadProduct();
            r.Connect();
            MySqlDataAdapter da = new MySqlDataAdapter("SELECT * from tblAddClient WHERE ClientName LIKE @search", r.con);
            r.cmd = da.SelectCommand;
            r.cmd.Parameters.Add(new MySqlParameter("search", "%" + textBox1.Text + "%"));
            r.cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            da.Fill(dt);
            r.Disconnect();
            dtgAddClient.DataSource = dt;
        }

        private void button16_Click(object sender, EventArgs e)
        {
            r.Connect();
            MySqlDataAdapter da = new MySqlDataAdapter("select * from tblAddClient where `ClientName` = @ClientName", r.con);
            r.cmd = da.SelectCommand;
            r.cmd.Parameters.Add(new MySqlParameter("ClientName", txtClientName.Text));
            r.dr = r.cmd.ExecuteReader();

            if (r.dr.Read())
            {
                MessageBox.Show("You cannot have the same client name!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (txtClientName.Text.Equals(""))
            {
                MessageBox.Show("Please fill up the client's name!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (txtContNum.Text == "")
            {
                MessageBox.Show("You must atleast fill up a contact number!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                r.Connect();
                r.cmd = new MySqlCommand("Insert into tblAddClient VALUES(NULL,@ClientName,@Email,@ContNum,@Address)", r.con);
                r.cmd.Parameters.Add(new MySqlParameter("ClientName", txtClientName.Text));
                r.cmd.Parameters.Add(new MySqlParameter("Email", txtEmail.Text));
                r.cmd.Parameters.Add(new MySqlParameter("ContNum", txtContNum.Text));
                r.cmd.Parameters.Add(new MySqlParameter("Address", txtAddress.Text));
                r.dr = r.cmd.ExecuteReader();
                r.Disconnect();
                LoadProduct();

                MessageBox.Show("You added a client successfully!", "Congratulations!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void button18_Click(object sender, EventArgs e)
        {
            
            if (dtgAddClient.SelectedRows.Count ==0)
            {
                MessageBox.Show("Please choose a client!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (txtClientName.Text != "")
            {
                MessageBox.Show("You already selected a client!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            else if (MessageBox.Show("Do you want to select this client?", "Select?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (dtgAddClient.SelectedRows.Count > 0)
                {
                    DataGridViewRow dtgr = dtgAddClient.SelectedRows[0];
                  
                    txtClientName.Text = dtgr.Cells["ClientName"].Value.ToString();
                    txtEmail.Text = dtgr.Cells["Email"].Value.ToString();
                    txtAddress.Text = dtgr.Cells["Address"].Value.ToString();
                    txtContNum.Text = dtgr.Cells["ContactNumber"].Value.ToString();

                }
               
                textBox1.Visible = false;
                textBox1.Text = "";
                MessageBox.Show("You selected a client!", "Congratulations!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void dtgAddProduct_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button7_Click_1(object sender, EventArgs e)
        {
            
         
        }

        private void tabPage2_Click(object sender, EventArgs e)
        {
        
        
        }

        private void txtProdName_TextChanged(object sender, EventArgs e)
        {

        }

        private void lblTotal_Click(object sender, EventArgs e)
        {

        }


        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void textBox4_KeyDown_1(object sender, KeyEventArgs e)
        {
            string alphabet = "/,\';[]!@#$%&*()_=|}]{*/`/.,<>? .-qwertyuiopasdfghjklzxcvbnmQWERTYUIOPSADFGHJJKLZXCVBNM";

            if (alphabet.Contains(((char)e.KeyValue).ToString().ToLower()) || e.Shift)
            {
                e.SuppressKeyPress = true;
            }
        }

        private void button15_Click(object sender, EventArgs e)
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

        private void textBox4_KeyDown_2(object sender, KeyEventArgs e)
        {
            string alphabet = "/,\';[]!@#$%&*()_=|}]{*/`/.,<>? .-qwertyuiopasdfghjklzxcvbnmQWERTYUIOPSADFGHJJKLZXCVBNM";

            if (alphabet.Contains(((char)e.KeyValue).ToString().ToLower()) || e.Shift)
            {
                e.SuppressKeyPress = true;
            }
        }

        private void lblTotal_Click_1(object sender, EventArgs e)
        {

        }

        private void button7_Click_2(object sender, EventArgs e)
        {
            txtAddress.Text = "";
            txtClientName.Text = "";
            txtContNum.Text = "";
            txtEmail.Text = "";
        }

        private void txtContNum_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtContNum_KeyDown(object sender, KeyEventArgs e)
        {
            string alphabet = "/,\';[]!@#$%&*()_=|}]{*/`/.,<>? .-qwertyuiopasdfghjklzxcvbnmQWERTYUIOPSADFGHJJKLZXCVBNM";

            if (alphabet.Contains(((char)e.KeyValue).ToString().ToLower()) || e.Shift)
            {
                e.SuppressKeyPress = true;
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {

        }

        private void button9_Click_1(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you want to log out?", "Logout?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Close();
                new Form1().Show();
            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            new SalesReport().Show();
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void txtContNum_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void txtClientName_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtClientName_KeyPress(object sender, KeyPressEventArgs e)
        {
           if((char.IsDigit(e.KeyChar)))
           {
           e.Handled = false;
           }
        }

        private void txtClientName_KeyDown(object sender, KeyEventArgs e)
        {
            string allow = "qwertyuiopsadfghjklzxvcbnmQWERTYUIOPASDFGHJKLZXVCBNM.";
            if(!allow.Contains((char)e.KeyValue) && e.KeyCode !=Keys.Back)
            {
                e.SuppressKeyPress=true;
            }
        }

        private void txtTranscode_KeyDown(object sender, KeyEventArgs e)
        {
            string allow = "qwertyuiopsadfghjklzxvcbnmQWERTYUIOPASDFGHJKLZXVCBNM123456789 .";
            if (!allow.Contains((char)e.KeyValue) && e.KeyCode != Keys.Back)
            {
                e.SuppressKeyPress = true;
            }
        }

        private void txtMeetup_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtMeetup_KeyDown(object sender, KeyEventArgs e)
        {
            string allow = "qwertyuiopsadfghjklzxvcbnmQWERTYUIOPASDFGHJKLZXVCBNM.";
            if (!allow.Contains((char)e.KeyValue) && e.KeyCode != Keys.Back)
            {
                e.SuppressKeyPress = true;
            }
        }

        private void txtAddress_KeyDown(object sender, KeyEventArgs e)
        {
            string allow = "qwertyuiopsadfghjklzxvcbnmQWERTYUIOPASDFGHJKLZXVCBNM123456789 .";
            if (!allow.Contains((char)e.KeyValue) && e.KeyCode != Keys.Back)
            {
                e.SuppressKeyPress = true;
            }
        }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }
}
}

    
