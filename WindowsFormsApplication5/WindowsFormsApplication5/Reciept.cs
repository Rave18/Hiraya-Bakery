using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Threading;
namespace WindowsFormsApplication5
{
    public partial class Reciept : Form
    {
        internal static DataGridView b = null;
        internal static DataGridViewRow a = null;
        internal static string transcode = null;
        internal static DataGridView d = null;
        internal static DateTime Dateordered = new DateTime();
        internal static DateTime Pickup = new DateTime();
        internal static string notes = null;
        internal static string quantity = null;
        internal static string price = null;
        internal static string total = null;
        internal static string service = null;
        internal static string meetup = null;
        internal static string email = null;
        internal static string Expiration = null;
        MySQL r = new MySQL();
        public Reciept()
        {
        
            InitializeComponent();
            lblTotal.Text = total;
            label4.Text = transcode;
            lblMeetup.Text = meetup;
            lblService.Text = service;
            lblClientName.Text = a.Cells["ClientName"].Value.ToString();
            lblClientName.Tag = a.Cells["ID"].Value;
           
            
        
            int t = label11.Top + label11.Height;

            double Total = 0;

            foreach (DataGridViewRow dtg in d.Rows)
            {

                Label Quantity = new Label();
                Quantity.Font = new Font("Franklin Gothic", 10, FontStyle.Regular);
                Quantity.Text = dtg.Cells["Quantity"].Value.ToString();
                Quantity.Top = t;
                Quantity.Left = label11.Left;
                this.Controls.Add(Quantity);

                Label Unit = new Label();
                Unit.Font = new Font("Franklin Gothic", 10, FontStyle.Regular);
                Unit.Text = dtg.Cells["Unit"].Value.ToString();
                Unit.Top = t;
                Unit.Left = label10.Left;
                this.Controls.Add(Unit);

                Label ItemOrdered = new Label();
                ItemOrdered.Font = new Font("Franklin Gothic", 10, FontStyle.Regular);
                ItemOrdered.Text = dtg.Cells["ProductName"].Value.ToString();
                ItemOrdered.Top = t;
                ItemOrdered.Left = label7.Left;

                this.Controls.Add(ItemOrdered);

                Label Price = new Label();
                Price.Font = new Font("Franklin Gothic", 10, FontStyle.Regular);
                Price.Text = dtg.Cells["Price"].Value.ToString();
                Price.Top = t;
                Price.Left = label12.Left;
                this.Controls.Add(Price);

                Label PID = new Label();
                PID.Font = new Font("Franklin Gothic", 10, FontStyle.Regular);
                PID.Text = dtg.Cells["ProductCode"].Value.ToString();
                PID.Top = t;
                PID.Left = label6.Left;
                this.Controls.Add(PID);




                t += Quantity.Height;
                Total += double.Parse(Price.Text);


            }
            lblTotal.Text = Total.ToString("0.00");

        }
   
        private void Reciept_Load(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                label18.Text = lblTotal.Text;
            }


            if (lblMeetup.Text == "")
            {
                label17.Visible = false;
                label14.Visible = false;
                textBox1.Text = "0";
                textBox1.Visible = false;

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                textBox1.Text = "0";
            }


            else if (Convert.ToDouble(textBox1.Text) > Convert.ToDouble(lblTotal.Text))
            {

                MessageBox.Show("The downpayment cannot be higher than the total!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            else if (MessageBox.Show("Do you want to order this product?", "Order?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                foreach (DataGridViewRow dtg in d.Rows)
                {
                 
                  

                    r.Connect();
                    r.cmd = new MySqlCommand("Insert into tblOrderedProducts VALUES(NULL,@ClientName,@ProductID,@ServiceOption,@MeetupPoint,@DateOrdered,@Pickup,@Quantity,@Price,@DownPayment,@Balance,@TransactionCode,@ProductStatus, @PaymentStatus)", r.con);
                    r.cmd.Parameters.Add(new MySqlParameter("ClientName", lblClientName.Tag));
                    r.cmd.Parameters.Add(new MySqlParameter("ProductID", dtg.Cells["ProductID"].Value));
                    r.cmd.Parameters.Add(new MySqlParameter("ServiceOption", lblService.Text));
                    r.cmd.Parameters.Add(new MySqlParameter("MeetupPoint", lblMeetup.Text));
                    r.cmd.Parameters.Add(new MySqlParameter("DateOrdered", Dateordered));
                    r.cmd.Parameters.Add(new MySqlParameter("Pickup", Pickup));
                 
                    r.cmd.Parameters.Add(new MySqlParameter("Quantity", dtg.Cells["Quantity"].Value));
                    r.cmd.Parameters.Add(new MySqlParameter("Price", dtg.Cells["Price"].Value));            
                    r.cmd.Parameters.Add(new MySqlParameter("DownPayment", textBox1.Text));
                    r.cmd.Parameters.Add(new MySqlParameter("Balance", label18.Text));
                    r.cmd.Parameters.Add(new MySqlParameter("TransactionCode", label4.Text));
                    r.cmd.Parameters.Add(new MySqlParameter("ProductStatus", "Pending"));
                    r.cmd.Parameters.Add(new MySqlParameter("PaymentStatus", "Pending"));
                    r.dr = r.cmd.ExecuteReader();
                    r.Disconnect();



                    r.Connect();
                    r.cmd = new MySqlCommand("UPDATE tblAddProduct SET QuantityOrdered= QuantityOrdered + @Quantity where ID=@ID", r.con);
                    r.cmd.Parameters.Add(new MySqlParameter("Quantity", dtg.Cells["Quantity"].Value));
                    r.cmd.Parameters.Add(new MySqlParameter("ID", dtg.Cells["ProductID"].Value));
                    r.cmd.ExecuteNonQuery();
                    r.Disconnect();
                
                }
                r.Connect();
                r.cmd = new MySqlCommand("Truncate table tblAddToCart", r.con);
                r.cmd.ExecuteNonQuery();
                r.Disconnect();

                Thread m = new Thread(new ThreadStart(new ThreadStart(SplashScreen)));
                m.Start();
                Thread.Sleep(3800);


                m.Abort();
                MessageBox.Show("Transaction Completed!", "Congratulations!", MessageBoxButtons.OK, MessageBoxIcon.Information);
        
                this.Close();
          

            }
        }
    
        public void SplashScreen()
        {
            Application.Run(new frmLoad());
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                label18.Text = (Convert.ToDouble(lblTotal.Text) - Convert.ToDouble(textBox1.Text)).ToString();

            }
            catch
            {
                label18.Text = "";
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

        private void button10_Click(object sender, EventArgs e)
        {
            r.Connect();
            r.cmd = new MySqlCommand("Truncate table tblAddToCart", r.con);
            r.cmd.ExecuteNonQuery();
            r.Disconnect();
            
            this.Close();
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            
            MySQL.dtReports = new DataTable();
            MySQL.dtReports.Columns.Add("ClientName");
            MySQL.dtReports.Columns.Add("ProductName");
            MySQL.dtReports.Columns.Add("ProductCode");
            MySQL.dtReports.Columns.Add("Unit");
            MySQL.dtReports.Columns.Add("Quantity");
            MySQL.dtReports.Columns.Add("Price");

 
            foreach (DataGridViewRow dtgr in d.Rows)
            {
                MySQL.dtReports.Rows.Add(new object[]{
                    dtgr.Cells["ClientName"].Value, dtgr.Cells["ProductName"].Value,dtgr.Cells["ProductCode"].Value,dtgr.Cells["Unit"].Value,dtgr.Cells["Quantity"].Value,dtgr.Cells["Price"].Value});
            }
            ReportViewer.rptReport = new rptReciept();
            new ReportViewer().Show();
        }

        private void lblTotal_Click(object sender, EventArgs e)
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
    }
}
