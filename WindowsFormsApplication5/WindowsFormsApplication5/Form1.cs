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
    public partial class Form1 : Form
    {
        MySQL r = new MySQL();
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            r.Connect();
            r.cmd = new MySqlCommand("SELECT * from tblLogin WHERE Username= '" + txtUsername.Text + "' AND Password ='" + txtPassword.Text + "'", r.con);
            
            r.dr = r.cmd.ExecuteReader();
            if (r.dr.Read())
            {
                MessageBox.Show("Welcome, " + r.dr["Name"] + "'");
                new frmMain().Show();
                Hide();
            }
            else
            {
                new Frost_Controls.Frost_Notification().Show("Wrong username or password", "INVALID", "ACCOUNT",this,Frost_Controls.Frost_Notification.NotificationType.ERROR);
                MessageBox.Show("Invalid Account");

            }
            r.Disconnect();

        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtUsername_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void panel7_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
