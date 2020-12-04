using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using Excel = Microsoft.Office.Interop.Excel;


namespace CSAY_SWAT_PAD
{
    public partial class FrmMain : Form
    {
        //Excel.Application sExcelApp;
        //Excel.Workbook sWorkbook;
        int i, j;
        public FrmMain()
        {
            InitializeComponent();
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void BtnTheissenPolySubbasin_Click(object sender, EventArgs e)
        {
            FrmTheissenPolygonCalc ftheissen = new FrmTheissenPolygonCalc();
            ftheissen.Show();
        }

        private void BtnParametersRecord_Click(object sender, EventArgs e)
        {
            FrmParameters fpara = new FrmParameters();
            fpara.Show();
        }

        private void BtnIterationRecord_Click(object sender, EventArgs e)
        {
            FrmIterationRecords firecord = new FrmIterationRecords();
            firecord.Show();
        }

        private void BtnAbout_Click(object sender, EventArgs e)
        {
            FrmAbout fabout = new FrmAbout();
            fabout.Show();
        }

        private void BtnWeatherGenInput_Click(object sender, EventArgs e)
        {
            FrmWeatherGenInput fwgeninput = new FrmWeatherGenInput();
            fwgeninput.Show();
        }
    }
}
