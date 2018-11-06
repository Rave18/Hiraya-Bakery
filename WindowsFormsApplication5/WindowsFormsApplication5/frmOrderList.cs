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
    public partial class frmOrderList : Form
    {
        internal static string quant = null;

        MySQL r = new MySQL();
        public frmOrderList()
        {
            InitializeComponent();
            LoadTable();
            AddToCart();
        }
        private void LoadTable()
        {
            r.Connect();
            MySqlDataAdapter da = new MySqlDataAdapter("select tblOrderedProducts.ID,tblAddClient.ClientName,tblOrderedProducts.ProductID, tblAddProduct.ProductName,tblAddProduct.Unit,tblOrderedProducts.ServiceOption,tblOrderedProducts.MeetupPoint,tblOrderedProducts.DateOrdered,tblOrderedProducts.Pickup,tblOrderedProducts.Quantity,tblOrderedProducts.Price,tblOrderedProducts.DownPayment,tblOrderedProducts.Balance,tblOrderedProducts.TransactionCode,tblOrderedProducts.ProductStatus,tblOrderedProducts.PaymentStatus from tblOrderedProducts left join tblAddClient on tblOrderedProducts.ClientID= tblAddClient.ID left join tblAddProduct on tblAddProduct.ID=tblOrderedProducts.ProductID", r.con);
            r.cmd = da.SelectCommand;
            r.cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            da.Fill(dt);
            r.Disconnect();
            dtgOrder.DataSource = dt;
           
        }
        private void AddToCart()
        {
            r.Connect();
            MySqlDataAdapter da = new MySqlDataAdapter("select tblOrderedProducts.ID,tblAddClient.ClientName,tblOrderedProducts.ProductID, tblAddProduct.ProductName,tblAddProduct.Unit,tblOrderedProducts.ServiceOption,tblOrderedProducts.MeetupPoint,tblOrderedProducts.DateOrdered,tblOrderedProducts.Pickup,tblOrderedProducts.Quantity,tblOrderedProducts.Price,tblOrderedProducts.DownPayment,tblOrderedProducts.Balance,tblOrderedProducts.TransactionCode,tblOrderedProducts.ProductStatus,tblOrderedProducts.PaymentStatus from tblOrderedProducts left join tblAddClient on tblOrderedProducts.ClientID= tblAddClient.ID left join tblAddProduct on tblAddProduct.ID=tblOrderedProducts.ProductID where PaymentStatus='Pending'", r.con);
            r.cmd = da.SelectCommand;
            r.cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            da.Fill(dt);
            r.Disconnect();
            dataGridView2.DataSource = dt;
            dataGridView2.CurrentCell = null;
        }
        
        private void button4_Click(object sender, EventArgs e)
        {
            new Inventory().Show();
            this.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            new Products().Show();
            this.Hide();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            new frmOrder().Show();
            this.Hide();
        }

        private void button8_Click(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblTime.Text = DateTime.Now.ToString("hh:mm:ss");
            lblDate.Text = DateTime.Now.ToString("MMMM dd,yyyy");
        }

        private void frmOrderList_Load(object sender, EventArgs e)
        {
          
                dateTimePicker1.Enabled = false;
            dtPicker.Enabled = false;
            comboBox2.Enabled = false;
          
            txtMeetup.Enabled = false;
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            LoadTable();
            r.Connect();
            MySqlDataAdapter da = new MySqlDataAdapter("select tblOrderedProducts.ID,tblAddClient.ClientName,tblOrderedProducts.ProductID, tblAddProduct.ProductName,tblAddProduct.Unit,tblOrderedProducts.ServiceOption,tblOrderedProducts.MeetupPoint,tblOrderedProducts.DateOrdered,tblOrderedProducts.Pickup,tblOrderedProducts.Quantity,tblOrderedProducts.Price,tblOrderedProducts.DownPayment,tblOrderedProducts.Balance,tblOrderedProducts.TransactionCode,tblOrderedProducts.ProductStatus,tblOrderedProducts.PaymentStatus from tblOrderedProducts left join tblAddClient on tblOrderedProducts.ClientID= tblAddClient.ID left join tblAddProduct on tblAddProduct.ID=tblOrderedProducts.ProductID WHERE ClientName LIKE @search", r.con);
            r.cmd = da.SelectCommand;
            r.cmd.Parameters.Add(new MySqlParameter("search", "%" + textBox2.Text + "%"));
            r.cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            da.Fill(dt);
            r.Disconnect();
            dtgOrder.DataSource = dt;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (dtgOrder.SelectedRows.Count > 0)
            {
              
                    if (dtgOrder.SelectedRows[0].Cells["PaymentStatus"].Value.ToString() == "Paid")
                    {
                        MessageBox.Show("This order is already paid!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }


                    else if (dtgOrder.SelectedRows[0].Cells["PaymentStatus"].Value.ToString() == "Cancelled")
                    {
                        MessageBox.Show("This order is cancelled!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    else
                    {
                        
                        frmPayNow.a = dtgOrder.SelectedRows[0];
                        new frmPayNow().ShowDialog();
                        LoadTable();
                     
                    
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dtgOrder.SelectedRows.Count > 0)
            {

                if (MessageBox.Show("Do you want to cancel this order?", "Cancel?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (dtgOrder.SelectedRows[0].Cells["PaymentStatus"].Value.ToString() == "Cancelled")
                    {
                        MessageBox.Show("This order is already cancelled!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else if (dtgOrder.SelectedRows[0].Cells["PaymentStatus"].Value.ToString() == "Paid")
                    {
                        MessageBox.Show("This order is already paid!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    else
                    {
                        foreach (DataGridViewRow dtgr in dtgOrder.SelectedRows)
                        {
                            r.Connect();
                            r.cmd = new MySqlCommand("Update tblOrderedProducts SET Balance=@Balance,DownPayment=@DownPayment,PaymentStatus=@Status where ID=@ID", r.con);
                            r.cmd.Parameters.Add(new MySqlParameter("Balance", "0"));
                            r.cmd.Parameters.Add(new MySqlParameter("DownPayment", "0"));
                            r.cmd.Parameters.Add(new MySqlParameter("Status", "Cancelled"));
                            r.cmd.Parameters.Add(new MySqlParameter("ID", dtgOrder.SelectedRows[0].Cells["ID"].Value));
                            r.dr = r.cmd.ExecuteReader();
                            r.Disconnect();

                            r.Connect();
                            r.cmd = new MySqlCommand("UPDATE tblAddProduct SET QuantityOrdered= (QuantityOrdered - @Quantity) where ID=@ID", r.con);
                            r.cmd.Parameters.Add(new MySqlParameter("Quantity", dtgr.Cells["Quantity"].Value));
                            r.cmd.Parameters.Add(new MySqlParameter("ID", dtgr.Cells["ProductID"].Value));
                            r.cmd.ExecuteNonQuery();
                            r.Disconnect();
                        }

                        AddToCart();
                            LoadTable();
                            MessageBox.Show("You cancelled an order successfully!", "Congratulations!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }


        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.Text == "Pending")
            {
                LoadTable();
                r.Connect();
                MySqlDataAdapter da = new MySqlDataAdapter("select tblOrderedProducts.ID,tblAddClient.ClientName,tblOrderedProducts.ProductID, tblAddProduct.ProductName,tblAddProduct.Unit,tblOrderedProducts.ServiceOption,tblOrderedProducts.MeetupPoint,tblOrderedProducts.DateOrdered,tblOrderedProducts.Pickup,tblOrderedProducts.Quantity,tblOrderedProducts.Price,tblOrderedProducts.DownPayment,tblOrderedProducts.Balance,tblOrderedProducts.TransactionCode,tblOrderedProducts.ProductStatus,tblOrderedProducts.PaymentStatus from tblOrderedProducts left join tblAddClient on tblOrderedProducts.ClientID= tblAddClient.ID left join tblAddProduct on tblAddProduct.ID=tblOrderedProducts.ProductID WHERE PaymentStatus LIKE @Status", r.con);
                r.cmd = da.SelectCommand;
                r.cmd.Parameters.Add(new MySqlParameter("Status", "%" + "Pending" + "%"));
                r.cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                da.Fill(dt);
                r.Disconnect();
                dtgOrder.DataSource = dt;
            }
            if (comboBox1.Text == "Paid")
            {
                LoadTable();
                r.Connect();
                MySqlDataAdapter da = new MySqlDataAdapter("select tblOrderedProducts.ID,tblAddClient.ClientName,tblOrderedProducts.ProductID, tblAddProduct.ProductName,tblAddProduct.Unit,tblOrderedProducts.ServiceOption,tblOrderedProducts.MeetupPoint,tblOrderedProducts.DateOrdered,tblOrderedProducts.Pickup,tblOrderedProducts.Quantity,tblOrderedProducts.Price,tblOrderedProducts.DownPayment,tblOrderedProducts.Balance,tblOrderedProducts.TransactionCode,tblOrderedProducts.ProductStatus,tblOrderedProducts.PaymentStatus from tblOrderedProducts left join tblAddClient on tblOrderedProducts.ClientID= tblAddClient.ID left join tblAddProduct on tblAddProduct.ID=tblOrderedProducts.ProductID WHERE PaymentStatus LIKE @Status", r.con);
                r.cmd = da.SelectCommand;
                r.cmd.Parameters.Add(new MySqlParameter("Status", "%" + "Paid" + "%"));
                r.cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                da.Fill(dt);
                r.Disconnect();
                dtgOrder.DataSource = dt;
            }

            if (comboBox1.Text == "Cancelled")
            {
                
                LoadTable();
                r.Connect();
                MySqlDataAdapter da = new MySqlDataAdapter("select tblOrderedProducts.ID,tblAddClient.ClientName,tblOrderedProducts.ProductID, tblAddProduct.ProductName,tblAddProduct.Unit,tblOrderedProducts.ServiceOption,tblOrderedProducts.MeetupPoint,tblOrderedProducts.DateOrdered,tblOrderedProducts.Pickup,tblOrderedProducts.Quantity,tblOrderedProducts.Price,tblOrderedProducts.DownPayment,tblOrderedProducts.Balance,tblOrderedProducts.TransactionCode,tblOrderedProducts.ProductStatus,tblOrderedProducts.PaymentStatus from tblOrderedProducts left join tblAddClient on tblOrderedProducts.ClientID= tblAddClient.ID left join tblAddProduct on tblAddProduct.ID=tblOrderedProducts.ProductID WHERE PaymentStatus LIKE @Status", r.con);
                r.cmd = da.SelectCommand;
                r.cmd.Parameters.Add(new MySqlParameter("Status", "%" + "Cancelled" + "%"));
                r.cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                da.Fill(dt);
                r.Disconnect();
                dtgOrder.DataSource = dt;
            }
            else if(comboBox1.Text=="All")
            {
                r.Connect();
            MySqlDataAdapter da = new MySqlDataAdapter("select tblOrderedProducts.ID,tblAddClient.ClientName,tblOrderedProducts.ProductID, tblAddProduct.ProductName,tblAddProduct.Unit,tblOrderedProducts.ServiceOption,tblOrderedProducts.MeetupPoint,tblOrderedProducts.DateOrdered,tblOrderedProducts.Pickup,tblOrderedProducts.Quantity,tblOrderedProducts.Price,tblOrderedProducts.DownPayment,tblOrderedProducts.Balance,tblOrderedProducts.TransactionCode,tblOrderedProducts.ProductStatus,tblOrderedProducts.PaymentStatus from tblOrderedProducts left join tblAddClient on tblOrderedProducts.ClientID= tblAddClient.ID left join tblAddProduct on tblAddProduct.ID=tblOrderedProducts.ProductID", r.con);
            r.cmd = da.SelectCommand;
            r.cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            da.Fill(dt);
            r.Disconnect();
            dtgOrder.DataSource = dt;
        }
        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView2_RowEnter(object sender, DataGridViewCellEventArgs e)
        {

            if (dataGridView2.SelectedRows.Count > 0)
            {
                dateTimePicker1.Enabled = true;
                dtPicker.Enabled = true;
                comboBox2.Enabled = true;
                txtMeetup.Enabled = true;
                DataGridViewRow dtgr = dataGridView2.SelectedRows[0];

                lblClientName.Text = dtgr.Cells["ClientName"].Value.ToString();

         
                lblUnit.Text = dtgr.Cells["Unit"].Value.ToString();
                lblItemOrdered.Text = dtgr.Cells["ProductName"].Value.ToString();
                txtMeetup.Text = dtgr.Cells["MeetupPoint"].Value.ToString();
                dateTimePicker1.Text = dtgr.Cells["DateOrdered"].Value.ToString();
                dtPicker.Text = dtgr.Cells["Pickup"].Value.ToString();
                comboBox2.Text = dtgr.Cells["ServiceOption"].Value.ToString();
            }
        }

        private void comboBox2_SelectedValueChanged(object sender, EventArgs e)
        {
            if (comboBox2.Text == "Delivery")
            {
                txtMeetup.Visible = true;

            }
            else
            {

                txtMeetup.Text = "";
                txtMeetup.Visible = false;
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (dataGridView2.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please choose an order!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (comboBox2.Text.Equals(""))
            {
                MessageBox.Show("Please choose a service option!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (dateTimePicker1.Value > DateTime.Today)
            {
                MessageBox.Show("The date ordered you entered is invalid!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            else if (dtPicker.Value < dateTimePicker1.Value)
            {
                MessageBox.Show("You inputted a wrong pick up date!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (comboBox2.Text == "Delivery" && txtMeetup.Text == "")
            {

                MessageBox.Show("Please choose a meetup point!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);



            }
            else if (dataGridView2.SelectedRows.Count > 0)
            {
                dateTimePicker1.Enabled = true;
                dtPicker.Enabled = true;

                r.Connect();

                r.cmd = new MySqlCommand("Update tblOrderedProducts SET ServiceOption=@ServiceOption,MeetupPoint=@MeetupPoint,DateOrdered=@DateOrdered,Pickup=@Pickup where TransactionCode=@Transcode", r.con);
                r.cmd.Parameters.Add(new MySqlParameter("ServiceOption", comboBox2.Text));
                r.cmd.Parameters.Add(new MySqlParameter("MeetupPoint", txtMeetup.Text));
                r.cmd.Parameters.Add(new MySqlParameter("DateOrdered", dateTimePicker1.Value));
                r.cmd.Parameters.Add(new MySqlParameter("Pickup", dtPicker.Value));
                r.cmd.Parameters.Add(new MySqlParameter("Transcode", dataGridView2.SelectedRows[0].Cells["TransactionCode"].Value));
                r.dr = r.cmd.ExecuteReader();
                r.Disconnect();
                LoadTable();
                AddToCart();
                MessageBox.Show("You changed an order successfully!", "Congratulations!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                comboBox2.SelectedIndex = -1;
                txtMeetup.Text = "";
                dateTimePicker1.ResetText();
                dtPicker.ResetText();
                dateTimePicker1.Enabled = false;
                dtPicker.Enabled = false;
                comboBox2.Enabled = false;
                lblUnit.Text = "";
                lblItemOrdered.Text = "";
                txtMeetup.Enabled = false;
                dataGridView2.CurrentCell = null;


            }

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {
            dtgOrder.CurrentCell = null;
        }

        private void panel5_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel6_Paint(object sender, PaintEventArgs e)
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

        private void button11_Click(object sender, EventArgs e)
        {
            if (dtgOrder.SelectedRows[0].Cells["ProductStatus"].Value.ToString() == "Available")
            {
                MessageBox.Show("That order is already made!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (dtgOrder.SelectedRows[0].Cells["PaymentStatus"].Value.ToString() == "Paid")
            {
                MessageBox.Show("That order is already paid!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (dtgOrder.SelectedRows[0].Cells["PaymentStatus"].Value.ToString() == "Cancelled")
            {
                MessageBox.Show("That order is already cancelled!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                Products.e = dtgOrder;
                frmQuantity.a = dtgOrder.SelectedRows[0];
                new frmQuantity().ShowDialog();
                LoadTable();
            }
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button19_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox3.Text == "Pending")
            {
                LoadTable();
                r.Connect();
                MySqlDataAdapter da = new MySqlDataAdapter("select tblOrderedProducts.ID,tblAddClient.ClientName,tblOrderedProducts.ProductID, tblAddProduct.ProductName,tblAddProduct.Unit,tblOrderedProducts.ServiceOption,tblOrderedProducts.MeetupPoint,tblOrderedProducts.DateOrdered,tblOrderedProducts.Pickup,tblOrderedProducts.Quantity,tblOrderedProducts.Price,tblOrderedProducts.DownPayment,tblOrderedProducts.Balance,tblOrderedProducts.TransactionCode,tblOrderedProducts.ProductStatus,tblOrderedProducts.PaymentStatus from tblOrderedProducts left join tblAddClient on tblOrderedProducts.ClientID= tblAddClient.ID left join tblAddProduct on tblAddProduct.ID=tblOrderedProducts.ProductID WHERE ProductStatus LIKE @Status", r.con);
                r.cmd = da.SelectCommand;
                r.cmd.Parameters.Add(new MySqlParameter("Status", "%" + "Pending" + "%"));
                r.cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                da.Fill(dt);
                r.Disconnect();
                dtgOrder.DataSource = dt;
            }
            else if (comboBox3.Text == "Available")
            {
                LoadTable();
                r.Connect();
                MySqlDataAdapter da = new MySqlDataAdapter("select tblOrderedProducts.ID,tblAddClient.ClientName,tblOrderedProducts.ProductID, tblAddProduct.ProductName,tblAddProduct.Unit,tblOrderedProducts.ServiceOption,tblOrderedProducts.MeetupPoint,tblOrderedProducts.DateOrdered,tblOrderedProducts.Pickup,tblOrderedProducts.Quantity,tblOrderedProducts.Price,tblOrderedProducts.DownPayment,tblOrderedProducts.Balance,tblOrderedProducts.TransactionCode,tblOrderedProducts.ProductStatus,tblOrderedProducts.PaymentStatus from tblOrderedProducts left join tblAddClient on tblOrderedProducts.ClientID= tblAddClient.ID left join tblAddProduct on tblAddProduct.ID=tblOrderedProducts.ProductID WHERE ProductStatus LIKE @Status", r.con);
                r.cmd = da.SelectCommand;
                r.cmd.Parameters.Add(new MySqlParameter("Status", "%" + "Available" + "%"));
                r.cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                da.Fill(dt);
                r.Disconnect();
                dtgOrder.DataSource = dt;
            }
            else if (comboBox3.Text == "Sold")
            {
                LoadTable();
                r.Connect();
                MySqlDataAdapter da = new MySqlDataAdapter("select tblOrderedProducts.ID,tblAddClient.ClientName,tblOrderedProducts.ProductID, tblAddProduct.ProductName,tblAddProduct.Unit,tblOrderedProducts.ServiceOption,tblOrderedProducts.MeetupPoint,tblOrderedProducts.DateOrdered,tblOrderedProducts.Pickup,tblOrderedProducts.Quantity,tblOrderedProducts.Price,tblOrderedProducts.DownPayment,tblOrderedProducts.Balance,tblOrderedProducts.TransactionCode,tblOrderedProducts.ProductStatus,tblOrderedProducts.PaymentStatus from tblOrderedProducts left join tblAddClient on tblOrderedProducts.ClientID= tblAddClient.ID left join tblAddProduct on tblAddProduct.ID=tblOrderedProducts.ProductID WHERE ProductStatus LIKE @Status", r.con);
                r.cmd = da.SelectCommand;
                r.cmd.Parameters.Add(new MySqlParameter("Status", "%" + "Sold" + "%"));
                r.cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                da.Fill(dt);
                r.Disconnect();
                dtgOrder.DataSource = dt;
            }
            else if (comboBox3.Text == "All")
            {
                r.Connect();
                MySqlDataAdapter da = new MySqlDataAdapter("select tblOrderedProducts.ID,tblAddClient.ClientName,tblOrderedProducts.ProductID, tblAddProduct.ProductName,tblAddProduct.Unit,tblOrderedProducts.ServiceOption,tblOrderedProducts.MeetupPoint,tblOrderedProducts.DateOrdered,tblOrderedProducts.Pickup,tblOrderedProducts.Quantity,tblOrderedProducts.Price,tblOrderedProducts.DownPayment,tblOrderedProducts.Balance,tblOrderedProducts.TransactionCode,tblOrderedProducts.ProductStatus,tblOrderedProducts.PaymentStatus from tblOrderedProducts left join tblAddClient on tblOrderedProducts.ClientID= tblAddClient.ID left join tblAddProduct on tblAddProduct.ID=tblOrderedProducts.ProductID", r.con);
                r.cmd = da.SelectCommand;
                r.cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                da.Fill(dt);
                r.Disconnect();
                dtgOrder.DataSource = dt;
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

        private void lblTime_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            new SalesReport().Show();
        }
    }
}