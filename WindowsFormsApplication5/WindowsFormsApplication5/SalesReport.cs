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
    
    public partial class SalesReport : Form
    {
        internal static DataTable dtReports=new DataTable();
        MySQL r = new MySQL();
        public SalesReport()
        {
            InitializeComponent();
        }
        private void LoadTable()
        {
            DataTable dtb = new DataTable();
            dtb.Columns.Add("Day");
            dtb.Columns.Add("Total Sales");
            dtb.Columns.Add("No. Of Transactions");

            string lastDay = "";
            double Price = 0;
            int totalNo = 0;
            DateTime currentDate = new DateTime();

            r.Connect();
          MySqlDataAdapter da=new MySqlDataAdapter("SELECT DATE(DateOrdered) AS CurrentDate FROM tblOrderedProducts WHERE DATE(DateOrdered) BETWEEN DATE(@start) and DATE(@end)", r.con);
            r.cmd =da.SelectCommand;
            r.cmd.Parameters.Add(new MySqlParameter("start", dtpStart.Value));
            r.cmd.Parameters.Add(new MySqlParameter("end", dtpEnd.Value));
            r.cmd.ExecuteNonQuery();
            DataTable dates = new DataTable();
            da.Fill(dates);
            r.Disconnect();
            DataTableReader dtDates = new DataTableReader(dates);


            if (rbtDaily.Checked)
            {

                while (dtDates.Read())
                {
                    Price = 0;
                    totalNo = 0;
                    r.Connect();
                    r.cmd = new MySqlCommand("Select * from tblOrderedProducts where DATE(DateOrdered) = DATE(@date)", r.con);
                    r.cmd.Parameters.Add(new MySqlParameter("date", dtDates["CurrentDate"]));
                    r.dr = r.cmd.ExecuteReader();
                    while (r.dr.Read())
                    {
                        Price += double.Parse(r.dr["Price"].ToString());
                        totalNo++;
                    }

                    DateTime.TryParse(dtDates["CurrentDate"].ToString(), out currentDate);
                    r.Disconnect();
                    if (!lastDay.Equals(currentDate.ToString("MMMM dd, yyyy")))
                    {
                        lastDay = currentDate.ToString("MMMM dd, yyyy");
                        dtb.Rows.Add(new object[] { currentDate.ToString("MMMM dd, yyyy"), Price, totalNo });
                    }
                }
            }
            else if (rbtMonthly.Checked)
            {
                while (dtDates.Read())
                {
                    Price = 0;
                    totalNo = 0;
                    r.Connect();
                    r.cmd = new MySqlCommand("Select * from tblOrderedProducts where CONCAT(YEAR(DateOrdered),',',MONTH(DateOrdered)) = CONCAT(YEAR(@date),',',MONTH(@date)) ", r.con);
                    r.cmd.Parameters.Add(new MySqlParameter("date", dtDates["CurrentDate"]));
                    r.dr = r.cmd.ExecuteReader();
                    while (r.dr.Read())
                    {
                        Price += double.Parse(r.dr["Price"].ToString());
                        totalNo++;
                    }

                    DateTime.TryParse(dtDates["CurrentDate"].ToString(), out currentDate);
                    r.Disconnect();
                    if (!lastDay.Equals(currentDate.ToString("MMMM, yyyy")))
                    {
                        lastDay = currentDate.ToString("MMMM, yyyy");
                        dtb.Rows.Add(new object[] { currentDate.ToString("MMMM, yyyy"), Price, totalNo });
                    }

                }
            }
            else if (rbtAnually.Checked)
            {
                while (dtDates.Read())
                {
                    Price = 0;
                    totalNo = 0;
                    r.Connect();
                    r.cmd = new MySqlCommand("Select * from tblOrderedProducts where YEAR(DateOrdered) = YEAR(@date)", r.con);
                    r.cmd.Parameters.Add(new MySqlParameter("date", dtDates["CurrentDate"]));
                    r.dr = r.cmd.ExecuteReader();
                    while (r.dr.Read())
                    {
                        Price += double.Parse(r.dr["Price"].ToString());
                        totalNo++;
                    }

                    DateTime.TryParse(dtDates["CurrentDate"].ToString(), out currentDate);
                    r.Disconnect();
                    if (!lastDay.Equals(currentDate.ToString("yyyy")))
                    {
                        lastDay = currentDate.ToString("yyyy");
                        dtb.Rows.Add(new object[] { currentDate.ToString("yyyy"), Price, totalNo });
                    }

                }
            }


            dtgSales.DataSource = dtb;

        }
        

     
        private void button3_Click(object sender, EventArgs e)
        {
            LoadTable();
        }

        private void SalesReport_Load(object sender, EventArgs e)
        {

        }

        private void SalesReport_Shown(object sender, EventArgs e)
        {
            LoadTable();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MySQL.dtReports = new DataTable();
            MySQL.dtReports.Columns.Add("Day");
            MySQL.dtReports.Columns.Add("No. Of Transactions");
            MySQL.dtReports.Columns.Add("Total Sales");
            foreach (DataGridViewRow dtgr in dtgSales.Rows)
            {
                MySQL.dtReports.Rows.Add(new object[]{
                     dtgr.Cells["Day"].Value,dtgr.Cells["No. Of Transactions"].Value,dtgr.Cells["Total Sales"].Value});
            }
            ReportViewer.rptReport = new rptReport();
            new ReportViewer().Show();
        }

        private void dtgSales_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
