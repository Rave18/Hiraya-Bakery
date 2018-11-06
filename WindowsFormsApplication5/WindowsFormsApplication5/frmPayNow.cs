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
    public partial class frmPayNow : Form
    {
        internal static DataGridViewRow a = null;
        MySQL r = new MySQL();
        public frmPayNow()
        {
               
           
            InitializeComponent();
    
            Inventory();
               
        }

        private void Inventory()
        {
            r.Connect();
            MySqlDataAdapter da = new MySqlDataAdapter("SELECT tblInventory.ID,tblInventory.TransactionID as 'TransactionCode',tblAddProduct.ProductName,tblAddProduct.ID As 'ProductID',tblInventory.Quantity,tblInventory.Price from tblInventory left join tblAddProduct on tblAddProduct.ID=tblInventory.ProductID where transactionid like @search; ", r.con);
            r.cmd = da.SelectCommand;
            r.cmd.Parameters.Add(new MySqlParameter("search", "%" + a.Cells["TransactionCode"].Value.ToString() + "%"));
            r.cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            da.Fill(dt);
            r.Disconnect();
            dataGridView1.DataSource = dt;

        }
     
        

        private void button1_Click(object sender, EventArgs e)
        {
            

       
         if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a product in the inventory!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (dataGridView1.SelectedRows.Count > 0)
            {
                string Quantityy = a.Cells["Quantity"].Value.ToString();
                string ProdID = a.Cells["ProductID"].Value.ToString();
                string Prodname1 = lblProdID.Text;
                int Quantity1 = int.Parse(label4.Text);
                int Quantity2 = Convert.ToInt16(Quantityy);
              
                if (Quantity1 < Quantity2)
                {
                    MessageBox.Show("You have insufficient product!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
                else if (ProdID != Prodname1)
                {
                    MessageBox.Show("That is not the product ordered!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (Quantity1 > Quantity2)
                {
                    MessageBox.Show("That is more than the product your customer ordered!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }

                else if (Quantity1 == Quantity2 && ProdID == Prodname1)
                {


                    r.Connect();
                    r.cmd = new MySqlCommand("Update tblOrderedProducts SET Balance=@Balance,DownPayment=@DownPayment,ProductStatus=@ProdStatus,PaymentStatus=@Status where ID=@ID", r.con);
                    r.cmd.Parameters.Add(new MySqlParameter("Balance", "0"));
                    r.cmd.Parameters.Add(new MySqlParameter("DownPayment", "0"));
                    r.cmd.Parameters.Add(new MySqlParameter("ProdStatus", "Sold"));
                    r.cmd.Parameters.Add(new MySqlParameter("Status", "Paid"));
                    r.cmd.Parameters.Add(new MySqlParameter("ID", a.Cells["ID"].Value));
                    r.dr = r.cmd.ExecuteReader();
                    r.Disconnect();

                    r.Connect();
                    r.cmd = new MySqlCommand("UPDATE tblAddProduct SET QuantityLeft= QuantityLeft - @Quantity where ID=@ID", r.con);
                    r.cmd.Parameters.Add(new MySqlParameter("Quantity", label4.Text));
                    r.cmd.Parameters.Add(new MySqlParameter("ID", a.Cells["ProductID"].Value));
                    r.cmd.ExecuteNonQuery();
                    r.Disconnect();
                    // Console.WriteLine("UPDATE tblAddProduct SET QuantityLeft= QuantityLeft + " + label4.Text + " where ProductID=" + lblProductID.Text);
                    //   AddQuantity();


                    r.Connect();
                    r.cmd = new MySqlCommand("UPDATE tblAddProduct SET QuantityOrdered= QuantityOrdered - @Quantity where ID=@ID", r.con);
                    r.cmd.Parameters.Add(new MySqlParameter("Quantity", label4.Text));
                    r.cmd.Parameters.Add(new MySqlParameter("ID", a.Cells["ProductID"].Value));
                    r.cmd.ExecuteNonQuery();
                    r.Disconnect();
                    //Console.WriteLine("UPDATE tblAddProduct SET QuantityOrdered= QuantityOrdered + " + label4.Text + " where ProductID=" + lblProductID.Text);

                    r.Connect();
                    r.cmd = new MySqlCommand("Delete from tblInventory where ID=@ID", r.con);
                    r.cmd.Parameters.Add(new MySqlParameter("Quantity", label4.Text));
                    r.cmd.Parameters.Add(new MySqlParameter("ID", dataGridView1.SelectedRows[0].Cells["ID"].Value));
                    r.cmd.ExecuteNonQuery();
                    r.Disconnect();
                    //  MinusQuantity();


                    MessageBox.Show("This order is paid successfully!", "Congratulations!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
            }

       



            
        }
        private void AddQuantity()
        {

            r.Connect();
            MySqlDataAdapter da = new MySqlDataAdapter("select * from tblAddProduct where `ProductCode` = @ProdCode", r.con);
            r.cmd = da.SelectCommand;
        //    r.cmd.Parameters.Add(new MySqlParameter("ProdCode", lblProductID.Text));
            r.dr = r.cmd.ExecuteReader();

            if (r.dr.Read())
            {
                string ID = r.dr["ProductCode"].ToString();
                string Quantity = r.dr["QuantityLeft"].ToString();



                int Quantity1 = Convert.ToInt16(Quantity);
                int AddQuantity = Convert.ToInt16(label4.Text);
                int TotalQuantity = Quantity1 + AddQuantity;
            }

        }
        private void MinusQuantity()
        {

            r.Connect();
            MySqlDataAdapter da = new MySqlDataAdapter("select * from tblAddProduct where `ProductCode` = @ProdCode", r.con);
            r.cmd = da.SelectCommand;
           // r.cmd.Parameters.Add(new MySqlParameter("ProdCode", lblProductID.Text));
            r.dr = r.cmd.ExecuteReader();

            if (r.dr.Read())
            {
                string ID = r.dr["ProductCode"].ToString();
                string Quantity = r.dr["QuantityOrdered"].ToString();



                int Quantity1 = Convert.ToInt16(Quantity);
                int AddQuantity = Convert.ToInt16(label4.Text);
                int TotalQuantity = Quantity1 + AddQuantity;
            }

        }
        private void frmPayNow_Load(object sender, EventArgs e)
        {
            
            dataGridView1.CurrentCell = null;
           
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {


                DataGridViewRow dtgr = dataGridView1.SelectedRows[0];

                label5.Text = a.Cells["ClientName"].Value.ToString();
                lblItemOrdered.Text = dtgr.Cells["ProductName"].Value.ToString();
                lblProdID.Text = dtgr.Cells["ProductID"].Value.ToString();
                label4.Text = dtgr.Cells["Quantity"].Value.ToString();

            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
