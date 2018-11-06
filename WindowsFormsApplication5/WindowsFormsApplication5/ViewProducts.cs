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
    public partial class ViewProducts : Form
    {
        internal static DataGridViewRow a = null;
        internal static DataGridView b = null;
        MySQL r = new MySQL();
        public ViewProducts()
        {
            InitializeComponent();
            Inventory();
            expire();
        }
        private void expire()
        {

            foreach (DataGridViewRow dtg in dataGridView1.Rows)
            {
                var now = DateTime.Now;
                var expirationdate = DateTime.Parse(dtg.Cells["ExpirationDate"].Value.ToString());

                if (now > expirationdate)
                {
         

                  r.Connect();
                  r.cmd = new MySqlCommand("UPDATE tblAddProduct SET QuantityLeft= QuantityLeft - @Quantity where ID=@ID AND ID NOT IN (SELECT ProductID FROM tblInventory WHERE Status = 'Expired')", r.con);
                  r.cmd.Parameters.Add(new MySqlParameter("Quantity", dtg.Cells["Quantity"].Value));
                  r.cmd.Parameters.Add(new MySqlParameter("ID", dtg.Cells["ProductID"].Value));
                  r.cmd.ExecuteNonQuery();
                  r.Disconnect();
                    

                  r.Connect();
                  r.cmd = new MySqlCommand("UPDATE tblInventory SET Status= @Status where ID=@ID", r.con);
                  r.cmd.Parameters.Add(new MySqlParameter("Status", "Expired"));
                  r.cmd.Parameters.Add(new MySqlParameter("ID", dtg.Cells["ID"].Value));
                  r.cmd.ExecuteNonQuery();
                  r.Disconnect();

                }
            }
        }
        private void Inventory()
        {
            r.Connect();
            MySqlDataAdapter da = new MySqlDataAdapter("SELECT tblInventory.ID,tblInventory.TransactionID as 'TransactionCode',tblAddProduct.ProductName,tblAddProduct.ID As 'ProductID',tblInventory.Quantity,tblAddProduct.Unit,tblInventory.Price,tblInventory.DateMade,tblInventory.ExpirationDate,tblInventory.Status from tblInventory left join tblAddProduct on tblAddProduct.ID=tblInventory.ProductID where ProductName like @search", r.con);
            r.cmd = da.SelectCommand;
            r.cmd.Parameters.Add(new MySqlParameter("search", "%" + a.Cells["ProductName"].Value.ToString() + "%"));
            r.cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            da.Fill(dt);
            r.Disconnect();
            dataGridView1.DataSource = dt;

        }
        private void ViewProducts_Load(object sender, EventArgs e)
        {
            
            foreach (DataGridViewRow dtg in dataGridView1.Rows)
            {
            
                
                lblProdName.Text = a.Cells["ProductName"].Value.ToString();
                lblQuantity.Text = dtg.Cells["Quantity"].Value.ToString();
                lblPrice.Text = a.Cells["Price"].Value.ToString();
                lblUnit.Text = a.Cells["Unit"].Value.ToString();
                lblDateMade.Text = dtg.Cells["DateMade"].Value.ToString();
                lblExpDate.Text = dtg.Cells["ExpirationDate"].Value.ToString();
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("There is no product!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {

                CancelledOrder.u = dataGridView1.SelectedRows[0];
                new CancelledOrder().ShowDialog();
                Inventory();
            }
      
        }
    }
}
