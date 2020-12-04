using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace CSAY_SWAT_PAD
{
    public partial class FrmTheissenPolygonCalc : Form
    {
        int No_of_Subbasin, Unique_Met_st;
        int StartYear, Year_No;
        int TotalDataofOneStation, TotalNumberofDailyData;
        int[] This_Year = new int[100]; 
        int[] SubbasinWise_MetSt = new int[500];
        string[,] Met_St_Name_SubbasinWise = new string[500,100];
        string[] Met_St_Name_Unique = new string[500]; 
        double[,] Met_St_Area_SubbasinWise = new double[500, 100];
        //int TotalRows; // to calculate for total no. of days

        public FrmTheissenPolygonCalc()
        {
            InitializeComponent();
        }

        private void TxtSubbasinNo_TextChanged(object sender, EventArgs e)
        {
            try
            {
                GenerateSubbasinSummaryRows();
                GenerateColumnsForSubbasinDetail();
            }
            catch
            {

            }
        }
        
        
        public void GenerateSubbasinSummaryRows()
        {
            int ks;
            try
            {
                No_of_Subbasin = Convert.ToInt32(TxtSubbasinNo.Text);
            }
            catch
            {
                //MessageBox.Show("Del_t or N missing !!!");
            }

            dataGridViewSubbasinSummary.Rows.Clear(); // subbasin summary datagridview

            try
            {
                ks = 0;

                ks = dataGridViewSubbasinSummary.Rows.Add(); // subbasin summary datagridview

                // subbasin summary datagridview
                for (int i = 0; i < No_of_Subbasin; i++)
                {
                    dataGridViewSubbasinSummary.Rows[ks].Cells[0].Value ="Sb" + (i + 1).ToString();
                    ks++;
                    if(ks <= No_of_Subbasin - 1)
                    {
                        dataGridViewSubbasinSummary.Rows.Add();
                    }
                }
            }
            catch
            {

            }
        }

        public void GenerateColumnsForSubbasinDetail()
        {
            No_of_Subbasin = Convert.ToInt32(TxtSubbasinNo.Text);

            int TotalColumn = No_of_Subbasin * 2 + 1;
            int rindex = 0;
            dataGridView1.ColumnCount = TotalColumn;

            for(int i =1; i<TotalColumn; i = i + 2)
            {
                dataGridView1.Columns[i].Name = dataGridViewSubbasinSummary.Rows[rindex].Cells[0].Value.ToString()
                    + " Met Station Name";
                dataGridView1.Columns[i+1].Name = dataGridViewSubbasinSummary.Rows[rindex].Cells[0].Value.ToString()
                     + " Met Station Area";

                dataGridView1.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
                dataGridView1.Columns[i].Width = 60;

                dataGridView1.Columns[i+1].SortMode = DataGridViewColumnSortMode.NotSortable;
                dataGridView1.Columns[i+1].Width = 60;

                if (rindex % 2 != 0)
                {
                    dataGridView1.Columns[i].HeaderCell.Style.ForeColor = Color.Black;
                    dataGridView1.Columns[i].HeaderCell.Style.BackColor = Color.LightBlue;

                    dataGridView1.Columns[i+1].HeaderCell.Style.ForeColor = Color.Black;
                    dataGridView1.Columns[i+1].HeaderCell.Style.BackColor = Color.LightBlue;

                    dataGridView1.EnableHeadersVisualStyles = false;
                }
                else
                {
                    dataGridView1.Columns[i].HeaderCell.Style.ForeColor = Color.Black;
                    dataGridView1.Columns[i].HeaderCell.Style.BackColor = Color.LightGreen;

                    dataGridView1.Columns[i + 1].HeaderCell.Style.ForeColor = Color.Black;
                    dataGridView1.Columns[i + 1].HeaderCell.Style.BackColor = Color.LightGreen;

                    dataGridView1.EnableHeadersVisualStyles = false;
                }
                rindex++;
            }
        }
        public int FindMaximum(int no_of_data, int[] seriesdata)
        {
            int max = seriesdata[0];
            for(int i = 1; i < no_of_data; i++)
            {
                if(seriesdata[i] > max)
                {
                    max = seriesdata[i];
                }
            }
            return max;
        }

        public void GenerateRowsOfSubbasinDetail()
        {
            int Max_No_of_Met_st, ks;
            No_of_Subbasin = Convert.ToInt32(TxtSubbasinNo.Text);

            for (int i =0; i < No_of_Subbasin; i++)
            {
                SubbasinWise_MetSt[i] = Convert.ToInt32(dataGridViewSubbasinSummary.Rows[i].Cells[2].Value);
            }
            Max_No_of_Met_st = FindMaximum(No_of_Subbasin, SubbasinWise_MetSt);

            dataGridView1.Rows.Clear(); // subbasin detail datagridview

            try
            {
                ks = 0;

                ks = dataGridView1.Rows.Add(); // subbasin summary datagridview

                // subbasin summary datagridview
                for (int i = 0; i < Max_No_of_Met_st; i++)
                {
                    dataGridView1.Rows[ks].Cells[0].Value = (i + 1).ToString();
                    ks++;
                    if (ks <= Max_No_of_Met_st - 1)
                    {
                        dataGridView1.Rows.Add();
                    }
                }
            }
            catch
            {

            }
        }

        private void InputMetStationDetailToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                GenerateRowsOfSubbasinDetail();
            }
            catch
            {

            }
        }
        public void GetUniqueMetStation()
        {
            //input met station name and area from datagridview
            int colNameindex = 1, colAreaindex = 2;
            Unique_Met_st = 0;
            for(int i = 0; i < No_of_Subbasin; i++)
            {
                for(int j = 0; j < SubbasinWise_MetSt[i]; j++)
                {
                    Met_St_Name_SubbasinWise[i, j] = dataGridView1.Rows[j].Cells[colNameindex].Value.ToString();
                    Met_St_Area_SubbasinWise[i, j] = Convert.ToDouble(dataGridView1.Rows[j].Cells[colAreaindex].Value);
                }
                colNameindex += 2;
                colAreaindex += 2;
            }

            //find unique values
            dataGridView3.Rows.Clear();
            int ks = 0, checkDuplicate;
            ks = dataGridView3.Rows.Add();

            for (int j = 0; j < SubbasinWise_MetSt[0]; j++)
            {
                Met_St_Name_Unique[ks] = Met_St_Name_SubbasinWise[0, j];
                dataGridView3.Rows[ks].Cells[1].Value = Met_St_Name_Unique[ks];
                Unique_Met_st++;
                ks++;
                dataGridView3.Rows.Add();
            }

            for (int i = 1; i < No_of_Subbasin; i++)
            {
                for (int j = 0; j < SubbasinWise_MetSt[i]; j++)
                {
                    checkDuplicate = 0;
                    for(int k =0; k < i; k++)
                    {
                        for(int l = 0; l < SubbasinWise_MetSt[k]; l++)
                        {
                            if (Met_St_Name_SubbasinWise[i, j] == Met_St_Name_SubbasinWise[k, l])
                            {
                                checkDuplicate++;
                            }
                        }
                    }
                    if(checkDuplicate == 0)
                    {
                        Met_St_Name_Unique[ks] = Met_St_Name_SubbasinWise[i, j];
                        dataGridView3.Rows[ks].Cells[1].Value = Met_St_Name_Unique[ks];;
                        ks++;
                        Unique_Met_st++;
                        dataGridView3.Rows.Add();
                    }
                }
            }
            TxtUniqueMet_st.Text = Unique_Met_st.ToString();
        }

        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void ImportUniqueMetStationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int TotalNumberofDailyData = 366;
            int i, rowIndex = 0;
            string FolderPath = TxtFolderPath.Text;
            bool FileFound;

            //string path = @"F:\AY\VS_2017\C#\ReadFromExcel\ReadFromExcel\Example1.xlsx";
            //string[] filePaths = Directory.GetFiles(@"c:\1A\", "*.xls", SearchOption.AllDirectories);
            string[] filePaths = Directory.GetFiles(FolderPath + "\\", "*.csv", SearchOption.AllDirectories);
          
            foreach (string files in filePaths)
            {
                FileFound = false;
                for(int fl =0; fl < Unique_Met_st; fl++)
                {
                    if(files ==(FolderPath + "\\" + Met_St_Name_Unique[fl] + ".csv"))
                    {
                        FileFound = true;
                    }
                }
                if(FileFound == true)
                {
                    string[] lines = System.IO.File.ReadAllLines(files);

                    for (i = 1; i <= TotalNumberofDailyData; i++) //row; i=0 is header in *.csv file
                    {
                        string[] rowdata = lines[i].Split(',');
                        for (int onecolindex = 0; onecolindex < Year_No; onecolindex++)
                        {
                            //MessageBox.Show((rowIndex + i - 1).ToString() + " ; " + (onecolindex + 2).ToString()
                            //    + " ; " + rowdata[onecolindex]);
                            dataGridView2.Rows[rowIndex + i - 1].Cells[onecolindex + 2].Value = rowdata[onecolindex];
                        }
                    }
                    rowIndex += 366;
                }
            }
            //MessageBox.Show("Import Completed Successfully");
            TxtLog.AppendText(DateTime.Now.ToString("hh:mm:ss") + " ==>  Met Station Data Import Completed Successfully");
            TxtLog.AppendText(Environment.NewLine);
            TxtLog.AppendText("-------------------------------------------------------------------------------");
            TxtLog.AppendText(Environment.NewLine);
        }

        private void FindUniqueMetStationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                GetUniqueMetStation();
                TxtLog.AppendText(DateTime.Now.ToString("hh:mm:ss") + " ==>  Unique Met Station Found Successfully");
                TxtLog.AppendText(Environment.NewLine);
                TxtLog.AppendText("-------------------------------------------------------------------------------");
                TxtLog.AppendText(Environment.NewLine);
            }
            catch
            {

            }
        }

        private void GenerateRowsAndColumnsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void GenerateMetStationDetailToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                GenerateRowsOfSubbasinDetail();
                TxtLog.AppendText(DateTime.Now.ToString("hh:mm:ss") + " ==>  Met Station Detail Table Generated Successfully");
                TxtLog.AppendText(Environment.NewLine);
                TxtLog.AppendText("-------------------------------------------------------------------------------");
                TxtLog.AppendText(Environment.NewLine);
            }
            catch
            {

            }
        }

        private void GenerateMetStDataTableToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                GenerateUniqueMetStRows();
                GenerateUniqueMetStColumns();
                TxtLog.AppendText(DateTime.Now.ToString("hh:mm:ss") + " ==>  Yearwise Met Station Table Generated Successfully");
                TxtLog.AppendText(Environment.NewLine);
                TxtLog.AppendText("-------------------------------------------------------------------------------");
                TxtLog.AppendText(Environment.NewLine);
            }
            catch
            {

            }
        }

        public void GenerateUniqueMetStRows() //rows
        {
            int ks, rindex = 0;
            Unique_Met_st = Convert.ToInt32(TxtUniqueMet_st.Text);
            dataGridView2.Rows.Clear();
            ks = 0;
            ks = dataGridView2.Rows.Add();

            for(int i = 0; i < Unique_Met_st; i++)
            {
                //dataGridView2.Rows[rindex].Cells[0].Value = (i + 1).ToString();
                dataGridView2.Rows[rindex].Cells[0].Value = Met_St_Name_Unique[i];
                for (int j = 0; j < 366; j++)
                {
                    dataGridView2.Rows[rindex].Cells[1].Value = (j + 1).ToString();
                    ks++;
                    rindex++;
                    dataGridView2.Rows.Add();
                }
            }
        }

        private void GenerateMetStWiseDataTableToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                GenerateColumnsOfMetStWiseTable();
                TxtLog.AppendText(DateTime.Now.ToString("hh:mm:ss") + " ==>  Met Station wise Year Data Table Generated Successfully");
                TxtLog.AppendText(Environment.NewLine);
                TxtLog.AppendText("-------------------------------------------------------------------------------");
                TxtLog.AppendText(Environment.NewLine);
            }
            catch
            {

            }
        }

        public void GenerateUniqueMetStColumns() //columns
        {
            StartYear = Convert.ToInt32(TxtStartYear.Text);
            Year_No = Convert.ToInt32(TxtYearNo.Text);
            Unique_Met_st = Convert.ToInt32(TxtUniqueMet_st.Text);
            int HeadIndex = 2,Years;
            dataGridView2.ColumnCount =  Year_No + 2;
            Years = StartYear;
            for (int j = 0; j <= Year_No; j++)
            {
                This_Year[j] = Years;
                dataGridView2.Columns[HeadIndex].Name = This_Year[j].ToString();
                dataGridView2.Columns[HeadIndex].SortMode = DataGridViewColumnSortMode.NotSortable;
                dataGridView2.Columns[HeadIndex].Width = 60;
                HeadIndex++;
                Years++;
            }
        }

        public void Transform_Yearwiswe_to_MetStWise()
        {
            int indx, Current_Year, CountYr, TotalDaysinYr, StrtRow;
            int U_Met_St = Convert.ToInt32(TxtUniqueMet_st.Text);
            int TotalYrs = Convert.ToInt32(TxtYearNo.Text);
            int strtYear = Convert.ToInt32(TxtStartYear.Text);
            int LeapYrNo =0;
            for(int st = 0; st < U_Met_St; st++)
            {
                CountYr = 0; indx = 0;
                StrtRow = st * 366;
                for(int col = 0; col <TotalYrs; col++)
                {
                    Current_Year = strtYear + CountYr;
                    if (DateTime.IsLeapYear(Current_Year))
                    {
                        TotalDaysinYr = 366;
                        LeapYrNo++;
                    }
                    else
                    {
                        TotalDaysinYr = 365;
                    }

                    for(int ro = StrtRow; ro < StrtRow + TotalDaysinYr; ro++)
                    {
                        if(st == 0)
                        {
                            indx = dataGridView4.Rows.Add();
                        }
                        dataGridView4.Rows[indx].Cells[st + 2].Value = dataGridView2.Rows[ro].Cells[col+2].Value;
                        indx++;
                    }
                    CountYr++;
                }
                TotalDataofOneStation = indx;
                TotalNumberofDailyData = TotalDataofOneStation;
                //MessageBox.Show("Indx = " + TotalDataofOneStation.ToString() + " of station = " + st.ToString() + " leapyr = " + LeapYrNo.ToString());
            }
        }
        private void YearwiseToMetStWiseDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Transform_Yearwiswe_to_MetStWise();
                TxtLog.AppendText(DateTime.Now.ToString("hh:mm:ss") + " ==>  Transformation of Yearwise to Met Station wise data completed Successfully");
                TxtLog.AppendText(Environment.NewLine);
                TxtLog.AppendText("-------------------------------------------------------------------------------");
                TxtLog.AppendText(Environment.NewLine);
            }
            catch
            {
                
            }
        }
        public void GenerateSubbasinWiseData_Column()
        {
            int sub_no = Convert.ToInt32(TxtSubbasinNo.Text);

            dataGridView5.ColumnCount = sub_no + 2;
            for (int i = 0; i < sub_no; i++)
            {
                dataGridView5.Columns[i+2].Name = dataGridViewSubbasinSummary.Rows[i].Cells[1].Value.ToString();
                dataGridView5.Columns[i+2].SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            //TotalRows = 0; 
            for(int i =0; i < TotalDataofOneStation; i++)
            {
                i = dataGridView5.Rows.Add();
                //TotalRows++;
            }
        }

        private void GenerateSubbasinTableToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                GenerateSubbasinWiseData_Column();
                TxtLog.AppendText(DateTime.Now.ToString("hh:mm:ss") + " ==>  Subbasin Table Generated Successfully");
                TxtLog.AppendText(Environment.NewLine);
                TxtLog.AppendText("-------------------------------------------------------------------------------");
                TxtLog.AppendText(Environment.NewLine);
            }
            catch
            {

            }
        }

        public void Calculate_SubbasinWise_Data_Using_Thiessen_Poly()
        {
            int subbsn_no = Convert.ToInt32(TxtSubbasinNo.Text);
            int UniqueMet_no = Convert.ToInt32(TxtUniqueMet_st.Text);
            //int met_st_no;
            double sum;
            int[,] Met_st_ID = new int[100,100];
            double[] Subbsn_ToalArea = new double[100];

            for(int sb = 0; sb < subbsn_no; sb++)
            {
                Subbsn_ToalArea[sb] = Convert.ToDouble(dataGridViewSubbasinSummary.Rows[sb].Cells[3].Value);
                //met_st_no = Convert.ToInt32(dataGridViewSubbasinSummary.Rows[sb].Cells[2].Value);
                
                for (int mt_sb = 0; mt_sb < SubbasinWise_MetSt[sb]; mt_sb++)
                {
                    for(int j = 0; j < UniqueMet_no; j++)
                    {
                        if (Met_St_Name_SubbasinWise[sb,mt_sb] == Met_St_Name_Unique[j])
                        {
                            //store j
                            Met_st_ID[sb, mt_sb] = j;
                        }
                    }
                }

                for(int k =0; k < TotalDataofOneStation; k++)
                {
                    sum = 0;
                    for (int mt_sb = 0; mt_sb < SubbasinWise_MetSt[sb]; mt_sb++)
                    {
                        double OneData = Convert.ToDouble(dataGridView4.Rows[k].Cells[Met_st_ID[sb, mt_sb] + 2].Value);
                        double Prod = Met_St_Area_SubbasinWise[sb, mt_sb] * OneData;
                        sum += Prod;
                    }
                    dataGridView5.Rows[k].Cells[sb+2].Value = (sum / Subbsn_ToalArea[sb]).ToString();
                }
            }
        }

        private void CalcSubbasinWiseDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Calculate_SubbasinWise_Data_Using_Thiessen_Poly();
                TxtLog.AppendText(DateTime.Now.ToString("hh:mm:ss") + " ==>  Calculating Subbasin wise data using Theissen completed Successfully");
                TxtLog.AppendText(Environment.NewLine);
                TxtLog.AppendText("-------------------------------------------------------------------------------");
                TxtLog.AppendText(Environment.NewLine);
            }
            catch
            {

            }
        }

        private void PasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridViewSubbasinSummary.SelectedCells.Count < 1) return;

                string[] lines;

                int row = dataGridViewSubbasinSummary.SelectedCells[0].RowIndex;
                int col = dataGridViewSubbasinSummary.SelectedCells[0].ColumnIndex;

                //get copied values
                lines = Clipboard.GetText().Split(new string[] { Environment.NewLine }, StringSplitOptions.None);

                string[] values;
                for (int i = 0; i < lines.Length; i++)
                {
                    values = lines[i].Split('\t');

                    if (row >= dataGridViewSubbasinSummary.Rows.Count || dataGridViewSubbasinSummary.Rows[row].IsNewRow) continue;
                    //if (row >= dataGridViewMusking.Rows.Count || dataGridViewMusking.Rows[row].IsNewRow) dataGridViewMusking.Rows.Add();
                    for (int j = 0; j < values.Length; j++)
                    {
                        if (col + j >= dataGridViewSubbasinSummary.Columns.Count) continue;
                        dataGridViewSubbasinSummary.Rows[row].Cells[col + j].Value = values[j];
                    }

                    row++;
                }

            }
            catch
            {

            }
        }

        private void PasteToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.SelectedCells.Count < 1) return;

                string[] lines;

                int row = dataGridView1.SelectedCells[0].RowIndex;
                int col = dataGridView1.SelectedCells[0].ColumnIndex;

                //get copied values
                lines = Clipboard.GetText().Split(new string[] { Environment.NewLine }, StringSplitOptions.None);

                string[] values;
                for (int i = 0; i < lines.Length; i++)
                {
                    values = lines[i].Split('\t');

                    if (row >= dataGridView1.Rows.Count || dataGridView1.Rows[row].IsNewRow) continue;
                    //if (row >= dataGridViewMusking.Rows.Count || dataGridViewMusking.Rows[row].IsNewRow) dataGridViewMusking.Rows.Add();
                    for (int j = 0; j < values.Length; j++)
                    {
                        if (col + j >= dataGridView1.Columns.Count) continue;
                        dataGridView1.Rows[row].Cells[col + j].Value = values[j];
                    }

                    row++;
                }

            }
            catch
            {

            }
        }

        private void ToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView4.SelectedCells.Count < 1) return;

                string[] lines;

                int row = dataGridView4.SelectedCells[0].RowIndex;
                int col = dataGridView4.SelectedCells[0].ColumnIndex;

                //get copied values
                lines = Clipboard.GetText().Split(new string[] { Environment.NewLine }, StringSplitOptions.None);

                string[] values;
                for (int i = 0; i < lines.Length; i++)
                {
                    values = lines[i].Split('\t');

                    if (row >= dataGridView4.Rows.Count || dataGridView4.Rows[row].IsNewRow) continue;
                    //if (row >= dataGridViewMusking.Rows.Count || dataGridViewMusking.Rows[row].IsNewRow) dataGridViewMusking.Rows.Add();
                    for (int j = 0; j < values.Length; j++)
                    {
                        if (col + j >= dataGridView4.Columns.Count) continue;
                        dataGridView4.Rows[row].Cells[col + j].Value = values[j];
                    }

                    row++;
                }

            }
            catch
            {

            }
        }

        private void GeneratePrecipitationDataIntxtFormatToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                CreateWeatherGenFolderWiseTxtFile();
            }
            catch
            {

            }
        }

        public void GenerateColumnsOfMetStWiseTable()
        {
            Unique_Met_st = Convert.ToInt32(TxtUniqueMet_st.Text);

            dataGridView4.ColumnCount = Unique_Met_st + 2;
            int HeadIndex = 2;
            for (int j = 0; j < Unique_Met_st; j++)
            {
                dataGridView4.Columns[HeadIndex].Name = Met_St_Name_Unique[j];
                dataGridView4.Columns[HeadIndex].SortMode = DataGridViewColumnSortMode.NotSortable;
                dataGridView4.Columns[HeadIndex].Width = 60;
                HeadIndex++;
            }
        }

        public void GenerateRowsOfMetStWiseTable()
        {
            StartYear = Convert.ToInt32(TxtStartYear.Text);
            Year_No = Convert.ToInt32(TxtYearNo.Text);
        }

        public void CreateWeatherGenFolderWiseTxtFile()
        {
            string Folder;
            string root, ProjectName, ProjectFolder;
            //string DirPcp, DirTmp, DirSolar, DirRH, DirWnd;
            string DirPcp;

            int TotalStations = Convert.ToInt32(TxtSubbasinNo.Text);
            //creating directory

            //string ImagePath = Environment.CurrentDirectory + "\\CRAWFORD" + DateTime.Now.ToString("yyyyMMddTHHmmss") + ".png";
            Folder = TxtDirectoryPath.Text;
            if (Folder == "")
            {
                Folder = Environment.CurrentDirectory;
                TxtLog.AppendText(DateTime.Now.ToString("hh:mm:ss") + " ==>  Folder Path set to " + Folder);
                TxtLog.AppendText(Environment.NewLine);
                TxtLog.AppendText("-------------------------------------------------------------------------------");
                TxtLog.AppendText(Environment.NewLine);
            }
            ProjectName = TxtProjectName.Text;
            if (ProjectName == "")
            {
                ProjectName = "New Project_" + DateTime.Now.ToString("yyyyMMddTHHmmss") + "_";
                TxtLog.AppendText(DateTime.Now.ToString("hh:mm:ss") + " ==>  Project Name set to " + ProjectName);
                TxtLog.AppendText(Environment.NewLine);
                TxtLog.AppendText("-------------------------------------------------------------------------------");
                TxtLog.AppendText(Environment.NewLine);
            }

            ProjectFolder = Folder + "\\" + ProjectName;
            if (!Directory.Exists(ProjectFolder))
            {
                Directory.CreateDirectory(ProjectFolder);
            }

            root = ProjectFolder + "\\Weather Data";
            DirPcp = root + "\\Precipitation";
            //DirTmp = root + "\\Temperature";
            //DirSolar = root + "\\Solar";
            //DirRH = root + "\\RH";
            //DirWnd = root + "\\Wind";

            if (!Directory.Exists(root))
            {
                Directory.CreateDirectory(root);
            }
            if (!Directory.Exists(DirPcp))
            {
                Directory.CreateDirectory(DirPcp);
            }
            /*if (!Directory.Exists(DirTmp))
            {
                Directory.CreateDirectory(DirTmp);
            }
            if (!Directory.Exists(DirSolar))
            {
                Directory.CreateDirectory(DirSolar);
            }
            if (!Directory.Exists(DirRH))
            {
                Directory.CreateDirectory(DirRH);
            }
            if (!Directory.Exists(DirWnd))
            {
                Directory.CreateDirectory(DirWnd);
            } */

            //creating files in directory
            string txtFile, SYear, MainFile, TempName;
            int StepColumn;
            SYear = TxtStartYear.Text + "0101";
            // precipitation
            Application.DoEvents();
            TxtLog.AppendText(DateTime.Now.ToString("hh:mm:ss") + " ==>  Writing to Precipitation files...");
            TxtLog.AppendText(Environment.NewLine);
            StepColumn = 0;
            MainFile = DirPcp + "\\" + "Pcp" + ".txt";
            //TextWriter MainwriterP = new StreamWriter(MainFile);
            //MainwriterP.Write("ID,NAME,LAT,LONG,ELEVATION");
            for (int i = 0; i < TotalStations; i++) //TotalStations is equal to Total number of subbasin
            {
                
                txtFile = DirPcp + "\\" + dataGridViewSubbasinSummary.Rows[i].Cells[1].Value + ".txt";

                TempName = "Pcp_" + dataGridViewSubbasinSummary.Rows[i].Cells[1].Value; //subbasin name
                //for main pcp file containing records of pcp stations
                //MainwriterP.Write(Environment.NewLine);
                /*MainwriterP.Write((i + 1).ToString() + "," + TempName + "," + dataGridViewStation.Rows[i].Cells[2].Value
                    + "," + dataGridViewStation.Rows[i].Cells[3].Value + ","
                    + dataGridViewStation.Rows[i].Cells[4].Value);*/

                TextWriter writer = new StreamWriter(txtFile);

                //writing to each files
                writer.Write(SYear);
                for (int j = 0; j < TotalNumberofDailyData; j++)
                {
                    writer.Write(Environment.NewLine);
                    writer.Write(dataGridView5.Rows[j].Cells[2 + i].Value);
                }
                //StepColumn += EachStationColumn;
                writer.Close();
                TxtLog.AppendText(DateTime.Now.ToString("hh:mm:ss") + " ==>  Finished writing to file : " + txtFile);
                TxtLog.AppendText(Environment.NewLine);
                TxtLog.AppendText("................................................................................");
                TxtLog.AppendText(Environment.NewLine);
            }
            //MainwriterP.Close();

            /* //tmp mx, tmp mn
            Application.DoEvents();
            TxtLog.AppendText(DateTime.Now.ToString("hh:mm:ss") + " ==>  Writing to Temperature files...");
            TxtLog.AppendText(Environment.NewLine);
            StepColumn = 0;
            MainFile = DirTmp + "\\" + "Tmp" + ".txt";
            TextWriter MainwriterT = new StreamWriter(MainFile);
            MainwriterT.Write("ID,NAME,LAT,LONG,ELEVATION");
            for (int i = 0; i < TotalStations; i++)
            {
                txtFile = DirTmp + "\\" + "Tmp_" + dataGridViewStation.Rows[i].Cells[1].Value + ".txt";

                TempName = "Tmp_" + dataGridViewStation.Rows[i].Cells[1].Value;
                MainwriterT.Write(Environment.NewLine);
                MainwriterT.Write((i + 1).ToString() + "," + TempName + "," + dataGridViewStation.Rows[i].Cells[2].Value
                    + "," + dataGridViewStation.Rows[i].Cells[3].Value + ","
                    + dataGridViewStation.Rows[i].Cells[4].Value);

                TextWriter writer = new StreamWriter(txtFile);
                writer.Write(SYear);
                for (int j = 0; j < TotalNumberofDailyData; j++)
                {
                    writer.Write(Environment.NewLine);
                    writer.Write(dataGridViewMusking.Rows[j].Cells[3 + StepColumn].Value + "," + dataGridViewMusking.Rows[j].Cells[4 + StepColumn].Value);
                }
                StepColumn += EachStationColumn;
                writer.Close();
                TxtLog.AppendText(DateTime.Now.ToString("hh:mm:ss") + " ==>  Finished writing to file : " + txtFile);
                TxtLog.AppendText(Environment.NewLine);
                TxtLog.AppendText("................................................................................");
                TxtLog.AppendText(Environment.NewLine);
            }
            MainwriterT.Close();

            //solar
            Application.DoEvents();
            TxtLog.AppendText(DateTime.Now.ToString("hh:mm:ss") + " ==>  Writing to Solar files...");
            TxtLog.AppendText(Environment.NewLine);
            StepColumn = 0;
            MainFile = DirSolar + "\\" + "Solar" + ".txt";
            TextWriter MainwriterS = new StreamWriter(MainFile);
            MainwriterS.Write("ID,NAME,LAT,LONG,ELEVATION");
            for (int i = 0; i < TotalStations; i++)
            {
                txtFile = DirSolar + "\\" + "Solar_" + dataGridViewStation.Rows[i].Cells[1].Value + ".txt";

                TempName = "Solar_" + dataGridViewStation.Rows[i].Cells[1].Value;
                MainwriterS.Write(Environment.NewLine);
                MainwriterS.Write((i + 1).ToString() + "," + TempName + "," + dataGridViewStation.Rows[i].Cells[2].Value
                    + "," + dataGridViewStation.Rows[i].Cells[3].Value + ","
                    + dataGridViewStation.Rows[i].Cells[4].Value);

                TextWriter writer = new StreamWriter(txtFile);
                writer.Write(SYear);
                for (int j = 0; j < TotalNumberofDailyData; j++)
                {
                    writer.Write(Environment.NewLine);
                    writer.Write(dataGridViewMusking.Rows[j].Cells[5 + StepColumn].Value);
                }
                StepColumn += EachStationColumn;
                writer.Close();
                TxtLog.AppendText(DateTime.Now.ToString("hh:mm:ss") + " ==>  Finished writing to file : " + txtFile);
                TxtLog.AppendText(Environment.NewLine);
                TxtLog.AppendText("................................................................................");
                TxtLog.AppendText(Environment.NewLine);
            }
            MainwriterS.Close();

            //Relative Humidity
            Application.DoEvents();
            TxtLog.AppendText(DateTime.Now.ToString("hh:mm:ss") + " ==>  Writing to Relative Humidity files...");
            TxtLog.AppendText(Environment.NewLine);
            StepColumn = 0;
            MainFile = DirRH + "\\" + "RH" + ".txt";
            TextWriter MainwriterR = new StreamWriter(MainFile);
            MainwriterR.Write("ID,NAME,LAT,LONG,ELEVATION");
            for (int i = 0; i < TotalStations; i++)
            {
                txtFile = DirRH + "\\" + "RH_" + dataGridViewStation.Rows[i].Cells[1].Value + ".txt";

                TempName = "RH_" + dataGridViewStation.Rows[i].Cells[1].Value;
                MainwriterR.Write(Environment.NewLine);
                MainwriterR.Write((i + 1).ToString() + "," + TempName + "," + dataGridViewStation.Rows[i].Cells[2].Value
                    + "," + dataGridViewStation.Rows[i].Cells[3].Value + ","
                    + dataGridViewStation.Rows[i].Cells[4].Value);

                TextWriter writer = new StreamWriter(txtFile);
                writer.Write(SYear);
                for (int j = 0; j < TotalNumberofDailyData; j++)
                {
                    writer.Write(Environment.NewLine);
                    writer.Write(dataGridViewMusking.Rows[j].Cells[6 + StepColumn].Value);
                }
                StepColumn += EachStationColumn;
                writer.Close();
                TxtLog.AppendText(DateTime.Now.ToString("hh:mm:ss") + " ==>  Finished writing to file : " + txtFile);
                TxtLog.AppendText(Environment.NewLine);
                TxtLog.AppendText("................................................................................");
                TxtLog.AppendText(Environment.NewLine);
            }
            MainwriterR.Close();

            //Wind
            Application.DoEvents();
            TxtLog.AppendText(DateTime.Now.ToString("hh:mm:ss") + " ==>  Writing to Wind files...");
            TxtLog.AppendText(Environment.NewLine);
            StepColumn = 0;
            MainFile = DirWnd + "\\" + "Wind" + ".txt";
            TextWriter MainwriterW = new StreamWriter(MainFile);
            MainwriterW.Write("ID,NAME,LAT,LONG,ELEVATION");
            for (int i = 0; i < TotalStations; i++)
            {
                txtFile = DirWnd + "\\" + "Wind_" + dataGridViewStation.Rows[i].Cells[1].Value + ".txt";

                TempName = "Wind_" + dataGridViewStation.Rows[i].Cells[1].Value;
                MainwriterW.Write(Environment.NewLine);
                MainwriterW.Write((i + 1).ToString() + "," + TempName + "," + dataGridViewStation.Rows[i].Cells[2].Value
                    + "," + dataGridViewStation.Rows[i].Cells[3].Value + ","
                    + dataGridViewStation.Rows[i].Cells[4].Value);

                TextWriter writer = new StreamWriter(txtFile);
                writer.Write(SYear);
                for (int j = 0; j < TotalNumberofDailyData; j++)
                {
                    writer.Write(Environment.NewLine);
                    writer.Write(dataGridViewMusking.Rows[j].Cells[6 + StepColumn].Value);
                }
                StepColumn += EachStationColumn;
                writer.Close();
                TxtLog.AppendText(DateTime.Now.ToString("hh:mm:ss") + " ==>  Finished writing to file : " + txtFile);
                TxtLog.AppendText(Environment.NewLine);
                TxtLog.AppendText("................................................................................");
                TxtLog.AppendText(Environment.NewLine);
            }
            MainwriterW.Close();*/

            TxtLog.AppendText(DateTime.Now.ToString("hh:mm:ss") + " ==>  Writing to all files completed");
            TxtLog.AppendText(Environment.NewLine);
            TxtLog.AppendText(DateTime.Now.ToString("hh:mm:ss") + " ==>  Files written to " + root);
            TxtLog.AppendText(Environment.NewLine);
            TxtLog.AppendText("-------------------------------------------------------------------------------");
            TxtLog.AppendText(Environment.NewLine);
        }
    }
}
