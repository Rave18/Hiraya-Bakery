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
    public partial class frmMain : Form
    {
        MySQL r = new MySQL();
        public frmMain()
        {
            InitializeComponent();
            
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblTime.Text = DateTime.Now.ToString("hh:mm:ss");
            lblDate.Text = DateTime.Now.ToString("MMMM dd,yyyy");
        }

        private void button3_Click(object sender, EventArgs e)
        {
           
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
        
        }

        private void button5_Click(object sender, EventArgs e)
        {
         
        }

        private void button6_Click(object sender, EventArgs e)
        {
          
        }

        private void button8_Click(object sender, EventArgs e)
        {
           
        }

        private void button9_Click(object sender, EventArgs e)
        {
         
        }

        private void button7_Click(object sender, EventArgs e)
        {
        
        }

        private void button10_Click(object sender, EventArgs e)
        {
         

        }

        private void button12_Click(object sender, EventArgs e)
        {
       

        }

        private void button15_Click(object sender, EventArgs e)
        {
  
        }

        private void button18_Click(object sender, EventArgs e)
        {
        

        }

        private void button11_Click(object sender, EventArgs e)
        {
         

        }

        private void button13_Click(object sender, EventArgs e)
        {
         
        }

        private void button14_Click(object sender, EventArgs e)
        {
          

        }

        private void button16_Click(object sender, EventArgs e)
        {
           
        }

        private void button17_Click(object sender, EventArgs e)
        {
           
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            
        }

        private void panel6_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button10_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button19_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void button6_Click_1(object sender, EventArgs e)
        {
            r.Connect();
            r.cmd = new MySqlCommand("Truncate table tblAddToCart", r.con);
            r.cmd.ExecuteNonQuery();
            r.Disconnect();
            new frmOrder().Show();
            this.Close();

        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            new Inventory().Show();
            this.Close();
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            new Products().Show();
            this.Close();

        }

        private void button8_Click_1(object sender, EventArgs e)
        {
            new frmOrderList().Show();
            this.Close();

        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            r.Connect();
            r.cmd = new MySqlCommand("Truncate table tblAddToCart", r.con);
            r.cmd.ExecuteNonQuery();
            r.Disconnect();

           // r.Connect();
            //r.cmd = new MySqlCommand("SELECT * FROM tblIngredients", r.con);
            //r.dr = r.cmd.ExecuteReader();
      //      while (r.dr.Read())
        //    {
          //      string quantity = r.dr["QuantityIN"].ToString();
               // int quantity1 = Convert.ToInt16(quantity);
                //if (quantity1 < 5)
                //{
                 //   new Frost_Controls.Frost_Notification().Show(r.dr["IngredientName"] + " is now " + quantity + " quantity of ur stock", "Critical", "Level", this, Frost_Controls.Frost_Notification.NotificationType.ERROR);
                //}
              //  else if (quantity1 == 0)
                //{
                  //  new Frost_Controls.Frost_Notification().Show(r.dr["IngredientName"] + " is now " + quantity + " quantity of ur stock", "Critical", "Level", this, Frost_Controls.Frost_Notification.NotificationType.ERROR);
               // }
            //}
            //r.Disconnect();
            //r.Connect();
            //r.cmd = new MySqlCommand("SELECT tblInventory.ID,tblInventory.TransactionID as 'TransactionCode',tblAddProduct.ProductName,tblAddProduct.ID As 'ProductID',tblInventory.Quantity,tblAddProduct.Unit,tblInventory.Price,tblInventory.DateMade,tblInventory.ExpirationDate,tblInventory.Status from tblInventory left join tblAddProduct on tblAddProduct.ID=tblInventory.ProductID", r.con);
            //r.dr = r.cmd.ExecuteReader();
            //while (r.dr.Read())
            //{
              //  string quantity = r.dr["ExpirationDate"].ToString();
                //DateTime expdate = DateTime.Parse(quantity).AddDays(2);
                //DateTime tomorrow = DateTime.Now;
                //if (expdate >tomorrow)
                //{
                 //   new Frost_Controls.Frost_Notification().Show(r.dr["ProductName"] + " is about to expire on "+quantity, "Expiration", "Warning", this, Frost_Controls.Frost_Notification.NotificationType.ERROR);
                //}
                //if (expdate < tomorrow)
                //{
                  //  new Frost_Controls.Frost_Notification().Show(r.dr["ProductName"] + " is now expired  ", "Expiration", "Warning", this, Frost_Controls.Frost_Notification.NotificationType.ERROR);

//                }

  //          }
    //        r.Disconnect();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            new SalesReport().Show();
            
        }

        private void button20_Click(object sender, EventArgs e)
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
    }
}
