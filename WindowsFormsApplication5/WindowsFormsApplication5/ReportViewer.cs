using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;
namespace WindowsFormsApplication5
{
    public partial class ReportViewer : Form
    {
        internal static ReportDocument rptReport;
     
        public ReportViewer()
        {
            InitializeComponent();
            rptReport.SetDataSource(MySQL.dtReports);
            crystalReportViewer1.ReportSource = rptReport;

        }

        private void crystalReportViewer1_Load(object sender, EventArgs e)
        {

        }
    }
}
