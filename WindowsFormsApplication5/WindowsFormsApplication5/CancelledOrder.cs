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
   
    public partial class CancelledOrder : Form
    {
        internal static DataGridViewRow u = null;
        MySQL r = new MySQL();
        public CancelledOrder()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            r.Connect();
            r.cmd = new MySqlCommand("Update tblInventory SET TransactionID=@TransactionCode,ProductID=@ProductID,Price=@Price,Quantity=@Quantity,DateMade=@DateMade,ExpirationDate=@ExpirationDate where ID=@ID", r.con);
            r.cmd.Parameters.Add(new MySqlParameter("TransactionCode", txtTrans.Text));
            r.cmd.Parameters.Add(new MySqlParameter("ProductID", lblPID.Text));
            r.cmd.Parameters.Add(new MySqlParameter("Price", u.Cells["Price"].Value));
            r.cmd.Parameters.Add(new MySqlParameter("Quantity", lblQuantity.Text));
            r.cmd.Parameters.Add(new MySqlParameter("DateMade", DateTime.Parse(lblDateMade.Text)    ));
            r.cmd.Parameters.Add(new MySqlParameter("ExpirationDate", DateTime.Parse(lblExpDate.Text)));
            r.cmd.Parameters.Add(new MySqlParameter("ID", u.Cells["ID"].Value));
            r.cmd.ExecuteNonQuery();
            r.Disconnect();
            MessageBox.Show("You updated the transaction code!", "Congratulations!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Hide();

        }

        private void CancelledOrder_Load(object sender, EventArgs e)
        {
            lblQuantity.Text = u.Cells["Quantity"].Value.ToString();
            lblPID.Text = u.Cells["ProductID"].Value.ToString();
            lblProdName.Text = u.Cells["ProductName"].Value.ToString();
            lblUnit.Text = u.Cells["Unit"].Value.ToString();
            lblPrice.Text = u.Cells["Price"].Value.ToString();
            lblDateMade.Text = u.Cells["DateMade"].Value.ToString();
            lblExpDate.Text = u.Cells["ExpirationDate"].Value.ToString();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtTrans_KeyDown(object sender, KeyEventArgs e)
        {
            string allow = "qwertyuiopsadfghjklzxvcbnmQWERTYUIOPASDFGHJKLZXVCBNM123456789 .";
            if (!allow.Contains((char)e.KeyValue) && e.KeyCode != Keys.Back)
            {
                e.SuppressKeyPress = true;
            }
        }
    }
}
