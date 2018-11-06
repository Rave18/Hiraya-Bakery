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
    public partial class frmQuantity : Form
    {
        internal static DataGridViewRow a = null;
        internal static DataGridViewRow b = null;
        MySQL r = new MySQL();
        public frmQuantity()
        {
            InitializeComponent();

    
            
        }

        private void frmQuantity_Load(object sender, EventArgs e)
        {
            lblTransID.Text = a.Cells["TransactionCode"].Value.ToString();
            lblQuantity.Text = a.Cells["Quantity"].Value.ToString();
            lblPID.Text = a.Cells["ProductID"].Value.ToString();
            lblProdName.Text = a.Cells["ProductName"].Value.ToString();
            lblUnit.Text = a.Cells["Unit"].Value.ToString();
            label1.Text = a.Cells["Price"].Value.ToString();
            dateTimePicker2.Value = DateTime.Today.AddDays(1);
        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (dateTimePicker1.Value > dateTimePicker2.Value)
            {
                MessageBox.Show("You entered an invalid date!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {

                r.Connect();
                r.cmd = new MySqlCommand("UPDATE tblAddProduct SET QuantityLeft= QuantityLeft + @Quantity where ID=@ID", r.con);
                r.cmd.Parameters.Add(new MySqlParameter("Quantity", lblQuantity.Text));
                r.cmd.Parameters.Add(new MySqlParameter("ID", a.Cells["ProductID"].Value));
                r.cmd.ExecuteNonQuery();
                r.Disconnect();
                Console.WriteLine("UPDATE tblAddProduct SET QuantityLeft= QuantityLeft + " + lblQuantity.Text + " where ID=" + lblPID.Text);


                r.Connect();
                r.cmd = new MySqlCommand("UPDATE tblOrderedProducts SET ProductStatus=@ProductStatus where ID=@ID", r.con);
                r.cmd.Parameters.Add(new MySqlParameter("ProductStatus", "Available"));
                r.cmd.Parameters.Add(new MySqlParameter("ID", a.Cells["ID"].Value));
                r.cmd.ExecuteNonQuery();
                r.Disconnect();

                r.Connect();
                r.cmd = new MySqlCommand("Insert into tblInventory VALUES(NULL,@TransactionCode,@ProductID,@Price,@Quantity,@DateMade,@ExpirationDate,@Status)", r.con);
                r.cmd.Parameters.Add(new MySqlParameter("TransactionCode", lblTransID.Text));
                r.cmd.Parameters.Add(new MySqlParameter("ProductID", lblPID.Text));
                r.cmd.Parameters.Add(new MySqlParameter("Price", a.Cells["Price"].Value));
                r.cmd.Parameters.Add(new MySqlParameter("Quantity", lblQuantity.Text));
                r.cmd.Parameters.Add(new MySqlParameter("DateMade", dateTimePicker1.Value));
                r.cmd.Parameters.Add(new MySqlParameter("ExpirationDate", dateTimePicker2.Value));
                r.cmd.Parameters.Add(new MySqlParameter("Status", "Good"));
                r.cmd.ExecuteNonQuery();
                r.Disconnect();
                frmOrderList.quant = lblQuantity.Text;
                Products.quantity = lblQuantity.Text;
                MessageBox.Show("You added a quantity!", "Congratulations!", MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.Hide();
            }
            
        }
        private void AddQuantity()
        {
            
            r.Connect();
            MySqlDataAdapter da = new MySqlDataAdapter("select * from tblAddProduct where `ProductCode` = @ProdCode", r.con);
            r.cmd = da.SelectCommand;
            r.cmd.Parameters.Add(new MySqlParameter("ProdCode", lblPID.Text));
            r.dr = r.cmd.ExecuteReader();

            if (r.dr.Read())
            {
                string ID = r.dr["ProductCode"].ToString();
                string Quantity = r.dr["QuantityLeft"].ToString();



                int Quantity1 = Convert.ToInt16(Quantity);
                int AddQuantity = Convert.ToInt16(lblQuantity.Text);
                int TotalQuantity = Quantity1 + AddQuantity;
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
            this.Close();
        }
    }
}
