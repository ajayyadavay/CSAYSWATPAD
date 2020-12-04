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
using System.IO;

namespace CSAY_SWAT_PAD
{
    public partial class FrmWeatherGenInput : Form
    {
        int n, n1, n2,n3,n4,n5,n6,n_St;
        int k, k1, k2, k3, k4, k5, k6,ks;
        int i, j, start_Year, ny,ns;
        int TotalStations, TotalNumberofDailyData;
        int EachStationColumn = 7; //pcp, tmpmx, tmpmn, solar, rh, wind, dewtmp
        string path;
        int[,] RowIndex = new int[30, 5000];
        int[,] TotalMonthWiseDataNumber = new int[30, 5000];

        double[] DaysNumber = new double[5000];

        double[,] PcpDailyData = new double[30,5000];
        double[,] TmpMXDailyData = new double[30,5000];
        double[,] TmpMNDailyData = new double[30,5000]; 
        double[,] SolarDailyData = new double[30,5000];
        double[,] RHDailyData = new double[30,5000];
        double[,] WindDailyData = new double[30,5000];
        double[,] DewTmpDailyData = new double[30, 5000];

        double[,,] PcpMonthlyData = new double[30,15,5000];
        double[,,] TmpMXMonthlyData = new double[30,15, 5000];
        double[,,] TmpMNMonthlyData = new double[30,15, 5000];
        double[,,] SolarMonthlyData = new double[30,15, 5000];
        double[,,] RHMonthlyData = new double[30,15, 5000];
        double[,,] WindMonthlyData = new double[30,15, 5000];
        double[,,] DewTmpMonthlyData = new double[30,15, 5000];

        double[,] MeanMonthlyPcp = new double[30, 20];
        double[,] StdDevMonthlyPcp = new double[30, 20];
        double[,] SkewMonthly = new double[30, 20];
        double[,] PR_W1Monthly = new double[30, 20];
        double[,] PR_W2Monthly = new double[30, 20];
        double[,] PCPDMonthly = new double[30, 20];
        double[,] RAINHHMXMonthly = new double[30, 20]; 

        double[,] MeanMonthlyTmpMX = new double[30, 20];
        double[,] StdDevMonthlyTmpMX = new double[30, 20];

        double[,] MeanMonthlyTmpMN = new double[30, 20];
        double[,] StdDevMonthlyTmpMN= new double[30, 20];

        double[,] MeanMonthlySolar = new double[30, 20];

        double[,] MeanMonthlyDew = new double[30, 20];

        double[,] MeanMonthlyWind = new double[30, 20];

        string[] HeadingForInput = {"PCP_", "TMP MX_", "TMP MN_", "SOLAR _", "RH _", "WIND _","Dew Tmp_" };
        string[] MonthNames = { "JAN", "FEB", "MAR", "APR", "MAY", "JUN", "JUL", "AUG", "SEP", "OCT", "NOV", "DEC"};
        //double[] MonthAverage = new double[5000];
        //double[] MonthSum = new double[5000];

        DataGridView[] Datagridviews = new DataGridView[25]; 

        string[] MonthName = new string[5000]; 

        int[] Years = new int[5000];
        int[] DaysInYear = new int[5000];
        int SumOfDays;
        int[] step = new int[5000];

        private void TxtNoOfStation_TextChanged(object sender, EventArgs e)
        {
            try
            {
                GenerateStationRows();
                GenerateColumns();
                Generate_WGEN_Rows();
            }
            catch
            {

            }
        }

        private void DailyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                TransformDailyToMonthly();
                TxtLog.AppendText(DateTime.Now.ToString("hh:mm:ss") + " ==>  Daily Data transformed to Monthly data");
                TxtLog.AppendText(Environment.NewLine);
                TxtLog.AppendText("-------------------------------------------------------------------------------");
                TxtLog.AppendText(Environment.NewLine);
            }
            catch
            {

            }
        }

        private void ExitToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Close();
        }
        public void Generate_WGEN_Column()
        {
            int indexMonthly;
            dataGridView13.ColumnCount = 174;

            dataGridView13.Columns[1].Name = "STATION";
            dataGridView13.Columns[2].Name = "WLATITUDE";
            dataGridView13.Columns[3].Name = "WLONGITUDE";
            dataGridView13.Columns[4].Name = "WELEV";
            dataGridView13.Columns[5].Name = "RAIN_YRS";

            //TMPMX
            indexMonthly = 1;
            for(int i = 6; i <=17; i++)
            {
                dataGridView13.Columns[i].Name = "TMPMX_" + indexMonthly.ToString();
                indexMonthly++;
            }

            //TMPMN
            indexMonthly = 1;
            for (int i = 18; i <= 29; i++)
            {
                dataGridView13.Columns[i].Name = "TMPMN_" + indexMonthly.ToString();
                indexMonthly++;
            }

            //TMPSTDMX
            indexMonthly = 1;
            for (int i = 30; i <= 41; i++)
            {
                dataGridView13.Columns[i].Name = "TMPSTDMX_" + indexMonthly.ToString();
                indexMonthly++;
            }

            //TMPSTDMN
            indexMonthly = 1;
            for (int i = 42; i <= 53; i++)
            {
                dataGridView13.Columns[i].Name = "TMPSTDMN_" + indexMonthly.ToString();
                indexMonthly++;
            }

            //PCPMM
            indexMonthly = 1;
            for (int i = 54; i <= 65; i++)
            {
                dataGridView13.Columns[i].Name = "PCPMM_" + indexMonthly.ToString();
                indexMonthly++;
            }

            //PCPSTD
            indexMonthly = 1;
            for (int i = 66; i <= 77; i++)
            {
                dataGridView13.Columns[i].Name = "PCPSTD_" + indexMonthly.ToString();
                indexMonthly++;
            }

            //PCPSKW
            indexMonthly = 1;
            for (int i = 78; i <= 89; i++)
            {
                dataGridView13.Columns[i].Name = "PCPSKW_" + indexMonthly.ToString();
                indexMonthly++;
            }

            //PR_W1_
            indexMonthly = 1;
            for (int i = 90; i <= 101; i++)
            {
                dataGridView13.Columns[i].Name = "PR_W1_" + indexMonthly.ToString();
                indexMonthly++;
            }

            //PR_W2_
            indexMonthly = 1;
            for (int i = 102; i <= 113; i++)
            {
                dataGridView13.Columns[i].Name = "PR_W2_" + indexMonthly.ToString();
                indexMonthly++;
            }

            //PCPD
            indexMonthly = 1;
            for (int i = 114; i <= 125; i++)
            {
                dataGridView13.Columns[i].Name = "PCPD_" + indexMonthly.ToString();
                indexMonthly++;
            }

            //RAINHHMX
            indexMonthly = 1;
            for (int i = 126; i <= 137; i++)
            {
                dataGridView13.Columns[i].Name = "RAINHHMX_" + indexMonthly.ToString();
                indexMonthly++;
            }

            //SOLARAV
            indexMonthly = 1;
            for (int i = 138; i <= 149; i++)
            {
                dataGridView13.Columns[i].Name = "SOLARAV_" + indexMonthly.ToString();
                indexMonthly++;
            }

            //DEWPT
            indexMonthly = 1;
            for (int i = 150; i <= 161; i++)
            {
                dataGridView13.Columns[i].Name = "DEWPT_" + indexMonthly.ToString();
                indexMonthly++;
            }

            //WNDAV
            indexMonthly = 1;
            for (int i = 162; i <= 173; i++)
            {
                dataGridView13.Columns[i].Name = "WNDAV_" + indexMonthly.ToString();
                indexMonthly++;
            }
        }
        private void FrmWeatherGenInput_Load(object sender, EventArgs e)
        {
            GenerateRowsOfParameters();
            Generate_WGEN_Column();

            Datagridviews[0] = dataGridView1;
            Datagridviews[1] = dataGridView2;
            Datagridviews[2] = dataGridView3;
            Datagridviews[3] = dataGridView4;
            Datagridviews[4] = dataGridView5;
            Datagridviews[5] = dataGridView6;

            Datagridviews[6] = dataGridView7;
            Datagridviews[7] = dataGridView8;
            Datagridviews[8] = dataGridView9;
            Datagridviews[9] = dataGridView10;
            Datagridviews[10] = dataGridView11;
            Datagridviews[11] = dataGridView12;

           
        }

        private void DewTemperatureToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                CalculateDewTemperature();
                TxtLog.AppendText(DateTime.Now.ToString("hh:mm:ss") + " ==>  Dew Point Temperature Calculated");
                TxtLog.AppendText(Environment.NewLine);
                TxtLog.AppendText("-------------------------------------------------------------------------------");
                TxtLog.AppendText(Environment.NewLine);
            }
            catch
            {

            }
        }
        public void CreateWeatherGenFolderWiseTxtFile()
        {
            string Folder;
            string root, ProjectName, ProjectFolder;
            string DirPcp, DirTmp, DirSolar, DirRH, DirWnd;

            TotalStations = Convert.ToInt32(TxtNoOfStation.Text);
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
            DirTmp = root + "\\Temperature";
            DirSolar = root + "\\Solar";
            DirRH = root + "\\RH";
            DirWnd = root + "\\Wind";

            if (!Directory.Exists(root))
            {
                Directory.CreateDirectory(root);
            }
            if (!Directory.Exists(DirPcp))
            {
                Directory.CreateDirectory(DirPcp);
            }
            if (!Directory.Exists(DirTmp))
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
            }

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
            TextWriter MainwriterP = new StreamWriter(MainFile);
            MainwriterP.Write("ID,NAME,LAT,LONG,ELEVATION");
            for (int i = 0; i < TotalStations; i++)
            {
                txtFile = DirPcp + "\\" + "Pcp_" + dataGridViewStation.Rows[i].Cells[1].Value + ".txt";

                TempName = "Pcp_" + dataGridViewStation.Rows[i].Cells[1].Value;
                MainwriterP.Write(Environment.NewLine);
                MainwriterP.Write((i + 1).ToString() + "," + TempName + "," + dataGridViewStation.Rows[i].Cells[2].Value
                    + "," + dataGridViewStation.Rows[i].Cells[3].Value + ","
                    + dataGridViewStation.Rows[i].Cells[4].Value);

                TextWriter writer = new StreamWriter(txtFile);
                writer.Write(SYear);
                for (int j = 0; j < TotalNumberofDailyData; j++)
                {
                    writer.Write(Environment.NewLine);
                    writer.Write(dataGridViewMusking.Rows[j].Cells[2 + StepColumn].Value);
                }
                StepColumn += EachStationColumn;
                writer.Close();
                TxtLog.AppendText(DateTime.Now.ToString("hh:mm:ss") + " ==>  Finished writing to file : " + txtFile);
                TxtLog.AppendText(Environment.NewLine);
                TxtLog.AppendText("................................................................................");
                TxtLog.AppendText(Environment.NewLine);
            }
            MainwriterP.Close();

            //tmp mx, tmp mn
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
            MainwriterW.Close();

            TxtLog.AppendText(DateTime.Now.ToString("hh:mm:ss") + " ==>  Writing to all files completed");
            TxtLog.AppendText(Environment.NewLine);
            TxtLog.AppendText(DateTime.Now.ToString("hh:mm:ss") + " ==>  Files written to " + root);
            TxtLog.AppendText(Environment.NewLine);
            TxtLog.AppendText("-------------------------------------------------------------------------------");
            TxtLog.AppendText(Environment.NewLine);
        }
        private void Button1_Click(object sender, EventArgs e)
        {
            CreateWeatherGenFolderWiseTxtFile();
        }

        private void ImportDailyDatacsvToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                //int TotalNumberofDailyData_ = 366;
                int i, ColStart = 0, CsvFileCount = 0;
                string FolderPath = TxtDailyDataCsvFolderPath.Text; //
                                                                    //bool FileFound;
                int Year_No = Convert.ToInt32(TxtYearNo.Text);
                int T_St = Convert.ToInt32(TxtNoOfStation.Text);

                //string path = @"F:\AY\VS_2017\C#\ReadFromExcel\ReadFromExcel\Example1.xlsx";
                //string[] filePaths = Directory.GetFiles(@"c:\1A\", "*.xls", SearchOption.AllDirectories);
                string[] filePaths = Directory.GetFiles(FolderPath + "\\", "*.csv", SearchOption.AllDirectories);

                foreach (string files in filePaths)
                {
                    //FileFound = false;
                    /*for (int fl = 0; fl < Unique_Met_st; fl++)
                    {
                        if (files == (FolderPath + "\\" + Met_St_Name_Unique[fl] + ".csv"))
                        {
                            FileFound = true;
                        }
                    }*/
                    //if (FileFound == true)
                    // {
                    CsvFileCount++;
                    if (CsvFileCount <= T_St)
                    {
                        string[] lines = System.IO.File.ReadAllLines(files);

                        for (i = 1; i <= TotalNumberofDailyData; i++) //row; i=0 is header in *.csv file
                        {
                            string[] rowdata = lines[i].Split(',');
                            //for (int onecolindex = 0; onecolindex < Year_No; onecolindex++)
                            for (int onecolindex = 0; onecolindex < 7; onecolindex++) // 7 = pcp, tmpmx, tmpmn, solar, rh, wind, dewtmp
                            {
                                //MessageBox.Show((rowIndex + i - 1).ToString() + " ; " + (onecolindex + 2).ToString()
                                //    + " ; " + rowdata[onecolindex]);
                                //dataGridViewMusking.Rows[rowIndex + i - 1].Cells[onecolindex + 2].Value = rowdata[onecolindex];
                                dataGridViewMusking.Rows[i - 1].Cells[ColStart + onecolindex + 2].Value = rowdata[onecolindex];
                            }
                        }
                        ColStart += 7; //7 = pcp, tmpmx, tmpmn, solar, rh, wind, dewtmp
                    }
                    //rowIndex += 366;
                    // }
                }
                //MessageBox.Show("Import Completed Successfully From .CSV file");

                TxtLog.AppendText(DateTime.Now.ToString("hh:mm:ss") + " ==>  Daily Data import Completed Successfully From .csv");
                TxtLog.AppendText(Environment.NewLine);
                TxtLog.AppendText("Number of Files imported = Number of station ==> " + T_St.ToString());
                TxtLog.AppendText(Environment.NewLine);
                TxtLog.AppendText("-------------------------------------------------------------------------------");
                TxtLog.AppendText(Environment.NewLine);
            }
            catch
            {

            }
            
        }

        private void ImportStationRecordcsvToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                int i, CsvFileCount = 0;
                string FolderPath = TxtStationRecordCsvFolderPath.Text;
                int T_St = Convert.ToInt32(TxtNoOfStation.Text);

                string[] filePaths = Directory.GetFiles(FolderPath + "\\", "*.csv", SearchOption.AllDirectories);

                foreach (string files in filePaths)
                {
                    CsvFileCount++;
                    if (CsvFileCount <= 1)
                    {
                        string[] lines = System.IO.File.ReadAllLines(files);

                        for (i = 1; i <= T_St; i++) //row; i=0 is header in *.csv file
                        {
                            string[] rowdata = lines[i].Split(',');
                            for (int onecolindex = 0; onecolindex < 5; onecolindex++) // 5 = station, wlatitude, wlongitude, welev, rain_yrs
                            {
                                dataGridViewStation.Rows[i - 1].Cells[onecolindex + 1].Value = rowdata[onecolindex];
                            }
                        }
                    }
                }
                //MessageBox.Show("Import Completed Successfully From .CSV file");
                TxtLog.AppendText(DateTime.Now.ToString("hh:mm:ss") + " ==>  Station Record import Completed Successfully From .csv");
                TxtLog.AppendText(Environment.NewLine);
                TxtLog.AppendText("Number of stations imported  = " + T_St.ToString());
                TxtLog.AppendText(Environment.NewLine);
                TxtLog.AppendText("-------------------------------------------------------------------------------");
                TxtLog.AppendText(Environment.NewLine);
            }
            catch
            {

            }
            
        }

        public void Calculate_WGEN_Parameters()
        {
            int Yr_no = Convert.ToInt32(TxtYearNo.Text);
            TotalStations = Convert.ToInt32(TxtNoOfStation.Text);
            double[] tempData1 = new double[5000];
            
            double[] tempData2 = new double[5000];
            double[] tempData3 = new double[5000];
            double[] tempData4 = new double[5000];
            double[] tempData5 = new double[5000];
            double[] tempData6 = new double[5000];

            int indx, colindex = 1;

            for (int StationNo = 0; StationNo < TotalStations; StationNo++)
            {
                for (j = 0; j < 12; j++)
                {
                    indx = 0;
                    for(int monthdata = 0; monthdata < TotalMonthWiseDataNumber[StationNo, j]; monthdata++)
                    {
                        tempData1[indx] = PcpMonthlyData[StationNo, j, monthdata];
                        tempData2[indx] = TmpMXMonthlyData[StationNo, j, monthdata];
                        tempData3[indx] = TmpMNMonthlyData[StationNo, j, monthdata];
                        tempData4[indx] = SolarMonthlyData[StationNo, j, monthdata];
                        tempData5[indx] = DewTmpMonthlyData[StationNo, j, monthdata];
                        tempData6[indx] = WindMonthlyData[StationNo, j, monthdata];

                        indx++;
                    }
                    //pcp
                    MeanMonthlyPcp[StationNo, j] = FindMean(TotalMonthWiseDataNumber[StationNo, j], tempData1);
                    StdDevMonthlyPcp[StationNo, j] = FindStandardDeviation(TotalMonthWiseDataNumber[StationNo, j], tempData1);
                    SkewMonthly[StationNo, j] = FindSkewCoefficient(TotalMonthWiseDataNumber[StationNo, j], tempData1);
                    PR_W1Monthly[StationNo, j] = FindPR_W1(TotalMonthWiseDataNumber[StationNo, j], tempData1);
                    PR_W2Monthly[StationNo, j] = FindPR_W2(TotalMonthWiseDataNumber[StationNo, j], tempData1);
                    PCPDMonthly[StationNo, j] =  FindPCPD(TotalMonthWiseDataNumber[StationNo, j],Yr_no,tempData1);
                    RAINHHMXMonthly[StationNo, j] = FindRainHHMX(TotalMonthWiseDataNumber[StationNo, j], tempData1);

                    dataGridView7.Rows[0].Cells[colindex].Value = MeanMonthlyPcp[StationNo, j].ToString("0.0000");
                    dataGridView7.Rows[1].Cells[colindex].Value = StdDevMonthlyPcp[StationNo, j].ToString("0.0000");
                    dataGridView7.Rows[2].Cells[colindex].Value = SkewMonthly[StationNo, j].ToString("0.0000");
                    dataGridView7.Rows[3].Cells[colindex].Value = PR_W1Monthly[StationNo, j].ToString("0.0000");
                    dataGridView7.Rows[4].Cells[colindex].Value = PR_W2Monthly[StationNo, j].ToString("0.0000");
                    dataGridView7.Rows[5].Cells[colindex].Value = PCPDMonthly[StationNo, j].ToString("0.0000");
                    dataGridView7.Rows[6].Cells[colindex].Value = RAINHHMXMonthly[StationNo, j].ToString("0.0000");

                    //Tmp MX
                    MeanMonthlyTmpMX[StationNo, j] = FindMean(TotalMonthWiseDataNumber[StationNo, j], tempData2);
                    StdDevMonthlyTmpMX[StationNo, j] = FindStandardDeviation(TotalMonthWiseDataNumber[StationNo, j], tempData2);

                    dataGridView8.Rows[0].Cells[colindex].Value = MeanMonthlyTmpMX[StationNo, j].ToString("0.0000");
                    dataGridView8.Rows[1].Cells[colindex].Value = StdDevMonthlyTmpMX[StationNo, j].ToString("0.0000");

                    //Tmp MN
                    MeanMonthlyTmpMN[StationNo, j] = FindMean(TotalMonthWiseDataNumber[StationNo, j], tempData3);
                    StdDevMonthlyTmpMN[StationNo, j] = FindStandardDeviation(TotalMonthWiseDataNumber[StationNo, j], tempData3);

                    dataGridView9.Rows[0].Cells[colindex].Value = MeanMonthlyTmpMN[StationNo, j].ToString("0.0000");
                    dataGridView9.Rows[1].Cells[colindex].Value = StdDevMonthlyTmpMN[StationNo, j].ToString("0.0000");

                    //Solar
                    MeanMonthlySolar[StationNo, j] = FindMean(TotalMonthWiseDataNumber[StationNo, j], tempData4);

                    dataGridView10.Rows[0].Cells[colindex].Value = MeanMonthlySolar[StationNo, j].ToString("0.0000");

                    //Dew
                    MeanMonthlyDew[StationNo, j] = FindMean(TotalMonthWiseDataNumber[StationNo, j], tempData5);

                    dataGridView11.Rows[0].Cells[colindex].Value = MeanMonthlyDew[StationNo, j].ToString("0.0000");

                    //Wind
                    MeanMonthlyWind[StationNo, j] = FindMean(TotalMonthWiseDataNumber[StationNo, j], tempData6);

                    dataGridView12.Rows[0].Cells[colindex].Value = MeanMonthlyWind[StationNo, j].ToString("0.0000");

                    colindex++;
                    //TotalMonthWiseDataNumber[StationNo, j] = RowIndex[StationNo, j];
                }
            }

        }

        private void CalculateWGENParametersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Calculate_WGEN_Parameters();
                TxtLog.AppendText(DateTime.Now.ToString("hh:mm:ss") + " ==>  WGEN Parameters Calculated");
                TxtLog.AppendText(Environment.NewLine);
                TxtLog.AppendText("-------------------------------------------------------------------------------");
                TxtLog.AppendText(Environment.NewLine);
            }
            catch
            {

            }
            
        }

        private void InputFilesIntxtFormatToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                CreateWeatherGenFolderWiseTxtFile();
            }
            catch
            {

            }
           
        }

        private void ImportDailyDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                int ColStIndex = 0, CountSheet = 0;
                TotalStations = Convert.ToInt32(TxtNoOfStation.Text);

                OpenFileDialog openfiledialog1 = new OpenFileDialog();
                openfiledialog1.Filter = "Excel Sheet(*.xlsx)|*.xlsx|Excel Sheet(*.xls)|*.xls|All Files(*.*)|*.*";
                openfiledialog1.FilterIndex = 1;

                if (openfiledialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    path = openfiledialog1.FileName;
                }
                else if (openfiledialog1.ShowDialog() == System.Windows.Forms.DialogResult.Cancel) return;

                dataGridViewMusking.DataSource = null;

               /* for (int j = 0; j < dataGridViewMusking.Rows.Count - 1; j++)
                {
                    dataGridViewMusking.Rows.RemoveAt(j);
                    j--;
                    while (dataGridViewMusking.Rows.Count == 0)
                        continue;
                }*/
                //string path = @"F:\AY\VS_2017\C#\Temp\CSAY SWAT PAD\CSAY SWAT PAD\Example Station Data1.xlsx";
                Excel.Application app = new Excel.Application();
                Excel.Workbooks workbooks = app.Workbooks;

                Excel.Workbook workbook = workbooks.Open(path,Type.Missing,true);
                //Excel.Worksheet worksheet = workbook.ActiveSheet;

                //dataGridViewMusking.Rows.Add();
                //dataGridViewMusking.Rows.Add();
                //dataGridViewMusking.Rows.Add();
                //dataGridViewMusking.Rows.Add();

                //j = 2;
                /* for(i = 0; i < TotalNumberofDailyData; i++)
                 {
                     dataGridViewMusking.Rows.Add();
                 }*/
                TxtLog.Text = DateTime.Now.ToString("hh:mm:ss") + " ==>  Starting Data Import...Please Wait...";
                TxtLog.AppendText(Environment.NewLine);
                Application.DoEvents();
                foreach (Excel.Worksheet worksheet in workbook.Worksheets)
                {
                    CountSheet++;
                    if(CountSheet <= TotalStations)
                    {
                        // MessageBox.Show("Importing From Sheet: " + worksheet.Name.ToString(), "Import Daily Data...");
                        TxtLog.AppendText(DateTime.Now.ToString("hh:mm:ss")+" ==>  Importing From Sheet:  " + worksheet.Name.ToString());
                        TxtLog.AppendText(Environment.NewLine);

                        dataGridViewStation.Rows[CountSheet-1].Cells[1].Value = worksheet.Cells[2, 2].value; //Station name
                        dataGridViewStation.Rows[CountSheet - 1].Cells[2].Value = worksheet.Cells[3, 2].value; //Wlatitude
                        dataGridViewStation.Rows[CountSheet - 1].Cells[3].Value = worksheet.Cells[4, 2].value; //WLongitude
                        dataGridViewStation.Rows[CountSheet - 1].Cells[4].Value = worksheet.Cells[5, 2].value; //WElevation
                        dataGridViewStation.Rows[CountSheet - 1].Cells[5].Value = worksheet.Cells[7, 2].value; //Rain_Yrs.
                        for (i = 0; i < TotalNumberofDailyData; i++)
                        {
                            //dataGridViewMusking.Rows.Add();
                            for (int j = 0; j < EachStationColumn; j++) // EachStationColumn = 7 is used for 7 heading: pcp, tmpmx, tmpmn, solar, rh, wind, dewtemp
                            {
                                dataGridViewMusking.Rows[i].Cells[j + 2 + ColStIndex].Value = worksheet.Cells[i + 10, j + 1].value; // add 2 to j to account for column "Year" and "Day"

                            }
                        }
                        //Marshal.ReleaseComObject(worksheet);
                        //j++;
                        ColStIndex += EachStationColumn;
                    }
                    Marshal.ReleaseComObject(worksheet);
                }

                //worksheet.cells[rows, column].value; here rows column starts from 1 and rows starts from 1 of excel.

                workbook.Close();
                //app.Quit();
                Marshal.ReleaseComObject(workbook);
                Marshal.ReleaseComObject(workbooks);
                app.Quit();
                //Marshal.ReleaseComObject(worksheet);

                //MessageBox.Show("Data import Completed Successfully", "Import Daily Data...");
                TxtLog.AppendText(DateTime.Now.ToString("hh:mm:ss") + " ==>  Data import Completed Successfully");
                TxtLog.AppendText(Environment.NewLine);
                TxtLog.AppendText("-------------------------------------------------------------------------------");
                TxtLog.AppendText(Environment.NewLine);
            }
            catch
            {

            }
        }

        public void Generate_WGEN_Table()
        {
            int colindx;
            TotalStations = Convert.ToInt32(TxtNoOfStation.Text);

            //station records
            for (int ts = 0; ts < TotalStations; ts++)
            {
                for(int i = 1; i <= 5; i++)
                {
                    dataGridView13.Rows[ts].Cells[i].Value = dataGridViewStation.Rows[ts].Cells[i].Value;
                }
            }

            //Temperature Max -- Mean
            colindx = 1;
            for (int ts = 0; ts < TotalStations; ts++)
            {
                for (int i = 6; i <= 17; i++)
                {
                    dataGridView13.Rows[ts].Cells[i].Value = dataGridView8.Rows[0].Cells[colindx].Value;
                    colindx++;
                }
            }

            //Temperature Min -- Mean
            colindx = 1;
            for (int ts = 0; ts < TotalStations; ts++)
            {
                for (int i = 18; i <= 29; i++)
                {
                    dataGridView13.Rows[ts].Cells[i].Value = dataGridView9.Rows[0].Cells[colindx].Value;
                    colindx++;
                }
            }

            //Temperature Max -- Standard Deviation
            colindx = 1;
            for (int ts = 0; ts < TotalStations; ts++)
            {
                for (int i = 30; i <= 41; i++)
                {
                    dataGridView13.Rows[ts].Cells[i].Value = dataGridView8.Rows[1].Cells[colindx].Value;
                    colindx++;
                }
            }

            //Temperature Min -- Standard Deviation
            colindx = 1;
            for (int ts = 0; ts < TotalStations; ts++)
            {
                for (int i = 42; i <= 53; i++)
                {
                    dataGridView13.Rows[ts].Cells[i].Value = dataGridView9.Rows[1].Cells[colindx].Value;
                    colindx++;
                }
            }

            //Precipitation -- Mean in milimeter (MM)
            colindx = 1;
            for (int ts = 0; ts < TotalStations; ts++)
            {
                for (int i = 54; i <= 65; i++)
                {
                    dataGridView13.Rows[ts].Cells[i].Value = dataGridView7.Rows[0].Cells[colindx].Value;
                    colindx++;
                }
            }

            //Precipitation -- standard deviation
            colindx = 1;
            for (int ts = 0; ts < TotalStations; ts++)
            {
                for (int i = 66; i <= 77; i++)
                {
                    dataGridView13.Rows[ts].Cells[i].Value = dataGridView7.Rows[1].Cells[colindx].Value;
                    colindx++;
                }
            }

            //Precipitation -- skewness
            colindx = 1;
            for (int ts = 0; ts < TotalStations; ts++)
            {
                for (int i = 78; i <= 89; i++)
                {
                    dataGridView13.Rows[ts].Cells[i].Value = dataGridView7.Rows[2].Cells[colindx].Value;
                    colindx++;
                }
            }

            //Precipitation -- PR_W1
            colindx = 1;
            for (int ts = 0; ts < TotalStations; ts++)
            {
                for (int i = 90; i <= 101; i++)
                {
                    dataGridView13.Rows[ts].Cells[i].Value = dataGridView7.Rows[3].Cells[colindx].Value;
                    colindx++;
                }
            }

            //Precipitation -- PR_W2
            colindx = 1;
            for (int ts = 0; ts < TotalStations; ts++)
            {
                for (int i = 102; i <= 113; i++)
                {
                    dataGridView13.Rows[ts].Cells[i].Value = dataGridView7.Rows[4].Cells[colindx].Value;
                    colindx++;
                }
            }

            //Precipitation -- PCPD
            colindx = 1;
            for (int ts = 0; ts < TotalStations; ts++)
            {
                for (int i = 114; i <= 125; i++)
                {
                    dataGridView13.Rows[ts].Cells[i].Value = dataGridView7.Rows[5].Cells[colindx].Value;
                    colindx++;
                }
            }

            //Precipitation -- PCPD
             colindx = 1;
             for (int ts = 0; ts < TotalStations; ts++)
             {
                 for (int i = 126; i <= 137; i++)
                 {
                     dataGridView13.Rows[ts].Cells[i].Value = dataGridView7.Rows[6].Cells[colindx].Value;
                     colindx++;
                 }
             }

            //Solar radiation-- mean
            colindx = 1;
             for (int ts = 0; ts < TotalStations; ts++)
             {
                 for (int i = 138; i <= 149; i++)
                 {
                     dataGridView13.Rows[ts].Cells[i].Value = dataGridView10.Rows[0].Cells[colindx].Value;
                     colindx++;
                 }
             }

            //Dew Point Temperature-- mean
            colindx = 1;
            for (int ts = 0; ts < TotalStations; ts++)
            {
                for (int i = 150; i <= 161; i++)
                {
                    dataGridView13.Rows[ts].Cells[i].Value = dataGridView11.Rows[0].Cells[colindx].Value;
                    colindx++;
                }
            }

            //Wind-- mean
            colindx = 1;
            for (int ts = 0; ts < TotalStations; ts++)
            {
                for (int i = 162; i <= 173; i++)
                {
                    dataGridView13.Rows[ts].Cells[i].Value = dataGridView12.Rows[0].Cells[colindx].Value;
                    colindx++;
                }
            }
        }
        private void GenerateWGENTableToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Generate_WGEN_Table();
                TxtLog.AppendText(DateTime.Now.ToString("hh:mm:ss") + " ==>  WGEN Table Generated");
                TxtLog.AppendText(Environment.NewLine);
                TxtLog.AppendText("-------------------------------------------------------------------------------");
                TxtLog.AppendText(Environment.NewLine);
            }
            catch
            {

            }
            
        }

        public void CalculateDewTemperature()
        {
            //calculation according to Allen (1998)
            //Also in FAO website: http://www.fao.org/3/X0490E/x0490e07.htm
            //es(T) = 0.6108*exp((17.27*T)/(T+237.3)) ----saturated vapor pressure      [1]
            //unit for saturated vapor pressue (es) is kPa and 1 mbar = kPa * 10
            //ea = RH * es/100-----According to Hackel, 1999                        [2]  
            //Dew = (234.18*log10(ea)-184.2)/(8.204 - log10(ea))                    [3]
            //where Dew = dew point temperature in degree celcius
            //es in mbar, ea in mbar, T = air temperature in degree celcius
            //RH = relative humidity in %
            //steps
            //use daily Min and Max Temp to find esmin, esmax and take their mean
            //then use esmean in the equation [2] to find ea and use ea in [3] to find dew point temperature

            //GetDataFromDailyDataGridView();

            try
            {
                //double[,] esDailyData = new double[30, 5000];
                double esmin, esmax, esmean, dewpoint,ea;

                TotalStations = Convert.ToInt32(TxtNoOfStation.Text);
                int StepInputColIndex = 0; //column 0 = Years and column 1 = Days and each station records data after 6 column

                //storing data into Array from Datagridview; only temp max and temp min are required here...
                for (int StationNo = 0; StationNo < TotalStations; StationNo++)
                {
                    for (i = 0; i < n; i++) //n is total number of days of entire years considered
                    {
                        //DaysNumber[i] = Convert.ToDouble(dataGridViewMusking.Rows[i].Cells["ColDay"].Value);
                        TmpMXDailyData[StationNo, i] = Convert.ToDouble(dataGridViewMusking.Rows[i].Cells[3 + StepInputColIndex].Value);
                        TmpMNDailyData[StationNo, i] = Convert.ToDouble(dataGridViewMusking.Rows[i].Cells[4 + StepInputColIndex].Value);
                        RHDailyData[StationNo, i] = Convert.ToDouble(dataGridViewMusking.Rows[i].Cells[6 + StepInputColIndex].Value);

                        //using equation [1] with T = Tmp MX and Tmp MN
                        esmax = 0.6108 * Math.Exp((17.27 * TmpMXDailyData[StationNo, i]) / (TmpMXDailyData[StationNo, i] + 237.3));
                        esmin = 0.6108 * Math.Exp((17.27 * TmpMNDailyData[StationNo, i]) / (TmpMNDailyData[StationNo, i] + 237.3));
                        esmean = 10 * (esmax + esmin) / 2; //multiplying by 10 to convert kpa into mbar i.e. 1 mbar = 10 * 1kpa

                        //using equation [2]
                        ea = RHDailyData[StationNo, i] * esmean / 100;

                        //using equation [3]
                        //dewpoint = (234.18 * Math.Log10(esmean) - 184.2) / (8.204 - Math.Log10(esmean));
                        dewpoint = (234.18 * Math.Log10(ea) - 184.2) / (8.204 - Math.Log10(ea));
                        dataGridViewMusking.Rows[i].Cells[8 + StepInputColIndex].Value = dewpoint.ToString("0.000");
                    }
                    StepInputColIndex += EachStationColumn; //EachStationColumn=7 headings are: pcp, tmpmx, tmpmn, solar, rh, wind, dewtmp. Column gets repeated after these 7 column
                }
            }
            catch
            {

            }
        }

        private void FileImportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                ImportStationRecordcsvToolStripMenuItem_Click(sender, e);
                ImportDailyDatacsvToolStripMenuItem_Click(sender, e);
            }
            catch
            {

            }
        }

        private void ProcessingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                DewTemperatureToolStripMenuItem_Click(sender, e);
                DailyToolStripMenuItem_Click(sender,  e);
                CalculateWGENParametersToolStripMenuItem_Click( sender,  e);
                GenerateWGENTableToolStripMenuItem_Click(sender, e);
            }
            catch
            {

            }
        }

        private void FileAndProcessingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                ImportStationRecordcsvToolStripMenuItem_Click(sender, e);
                ImportDailyDatacsvToolStripMenuItem_Click(sender, e);
                DewTemperatureToolStripMenuItem_Click(sender, e);
                DailyToolStripMenuItem_Click(sender, e);
                CalculateWGENParametersToolStripMenuItem_Click(sender, e);
                GenerateWGENTableToolStripMenuItem_Click(sender, e);
            }
            catch
            {

            }
        }

        private void SettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmSetting fset = new FrmSetting();
            fset.Show();
        }

        public void GetDataFromDailyDataGridView()
        {
            try
            {
                TotalStations = Convert.ToInt32(TxtNoOfStation.Text);
                int StepInputColIndex = 0; //column 0 = Years and column 1 = Days and each station records data after 7 column

                //storing data into Array from Datagridview
                for (int StationNo = 0; StationNo < TotalStations; StationNo++)
                {
                    for (i = 0; i < n; i++) //n is total number of days of entire years considered
                    {
                        DaysNumber[i] = Convert.ToDouble(dataGridViewMusking.Rows[i].Cells["ColDay"].Value);

                        /*PcpDailyData[StationNo, i] = Convert.ToDouble(dataGridViewMusking.Rows[i].Cells["ColPcp"].Value);
                        TmpMXDailyData[StationNo, i] = Convert.ToDouble(dataGridViewMusking.Rows[i].Cells["ColTmpMX"].Value);
                        TmpMNDailyData[StationNo, i] = Convert.ToDouble(dataGridViewMusking.Rows[i].Cells["ColTmpMN"].Value);
                        SolarDailyData[StationNo, i] = Convert.ToDouble(dataGridViewMusking.Rows[i].Cells["ColSolar"].Value);
                        RHDailyData[StationNo, i] = Convert.ToDouble(dataGridViewMusking.Rows[i].Cells["ColRH"].Value);
                        WindDailyData[StationNo, i] = Convert.ToDouble(dataGridViewMusking.Rows[i].Cells["ColWind"].Value);*/

                        PcpDailyData[StationNo, i] = Convert.ToDouble(dataGridViewMusking.Rows[i].Cells[2 + StepInputColIndex].Value);
                        TmpMXDailyData[StationNo, i] = Convert.ToDouble(dataGridViewMusking.Rows[i].Cells[3 + StepInputColIndex].Value);
                        TmpMNDailyData[StationNo, i] = Convert.ToDouble(dataGridViewMusking.Rows[i].Cells[4 + StepInputColIndex].Value);
                        SolarDailyData[StationNo, i] = Convert.ToDouble(dataGridViewMusking.Rows[i].Cells[5 + StepInputColIndex].Value);
                        RHDailyData[StationNo, i] = Convert.ToDouble(dataGridViewMusking.Rows[i].Cells[6 + StepInputColIndex].Value);
                        WindDailyData[StationNo, i] = Convert.ToDouble(dataGridViewMusking.Rows[i].Cells[7 + StepInputColIndex].Value);
                        DewTmpDailyData[StationNo, i] = Convert.ToDouble(dataGridViewMusking.Rows[i].Cells[8 + StepInputColIndex].Value);
                    }
                    StepInputColIndex += EachStationColumn; //EachStationColumn=7 headings are: pcp, tmpmx, tmpmn, solar, rh, wind, dewtmp. Column gets repeated after these 7 column
                }
            }
            catch
            {

            }
        }
         
        public void TransformDailyToMonthly()
        {
            int start, stop, dayindex;
            int index;
            
            try
            {
               /* MonthName[0] = "JAN"; MonthName[1] = "FEB"; MonthName[2] = "MAR"; MonthName[3] = "APR";
                MonthName[4] = "MAY"; MonthName[5] = "JUN"; MonthName[6] = "JUL"; MonthName[7] = "AUG";
                MonthName[8] = "SEP"; MonthName[9] = "OCT"; MonthName[10] = "NOV"; MonthName[11] = "DEC";*/

                step[0] = 31;
                step[2] = 31; step[3] = 30; step[4] = 31; step[5] = 30; step[6] = 31;
                step[7] = 31; step[8] = 30; step[9] = 31; step[10] = 30; step[11] = 31;

                GetDataFromDailyDataGridView();

                /*index = 0; //index is total number of days of entire years considered

                //Initialize Row index for each month to zero for first year 
                //and then for next years, use the starting value as -> (ending value of previous year + 1)
                for (j = 0; i < 12; j++)
                {
                    RowIndex[j] = 0;
                }*/

                int StepMonthlyColIndex = 1;
                for (int StationNo = 0; StationNo < TotalStations; StationNo++)
                {
                    index = 0; //index is total number of days of entire years considered

                    //Initialize Row index for each month to zero for first year 
                    //and then for next years, use the starting value as -> (ending value of previous year + 1)
                    for (j = 0; j < 12; j++)
                    {
                        RowIndex[StationNo,j] = 0;
                    }

                    for (i = 0; i < ny; i++) //ny is number of years
                    { 
                        //Determine leap year to know if days in Feb is 28 or 29 and days in year is 365 or 366
                        if (DateTime.IsLeapYear(Years[i]))
                        {
                            step[1] = 29;
                            DaysInYear[i] = 366;
                        }
                        else
                        {
                            step[1] = 28;
                            DaysInYear[i] = 365;
                        }

                        start = 1;
                        
                        for (j = 0; j < 12; j++) //for 12 months
                        {
                            stop = start + step[j] - 1;

                            for (dayindex = start; dayindex <= stop; dayindex++) //to find monthly value
                            {
                                /*dataGridView1.Rows[RowIndex[StationNo,j]].Cells[j + StepMonthlyColIndex].Value = PcpDailyData[StationNo, index];
                                PcpMonthlyData[StationNo, j, RowIndex[StationNo, j]] 
                                    =Convert.ToDouble(dataGridView1.Rows[RowIndex[StationNo, j]].Cells[j + StepMonthlyColIndex].Value);*/

                                /*dataGridView2.Rows[RowIndex[StationNo,j]].Cells[j + StepMonthlyColIndex].Value = TmpMXDailyData[StationNo, index];
                                dataGridView3.Rows[RowIndex[StationNo,j]].Cells[j + StepMonthlyColIndex].Value = TmpMNDailyData[StationNo, index];
                                dataGridView4.Rows[RowIndex[StationNo,j]].Cells[j + StepMonthlyColIndex].Value = SolarDailyData[StationNo, index];
                                //dataGridView5.Rows[RowIndex[StationNo,j]].Cells[j + StepMonthlyColIndex].Value = RHDailyData[StationNo, index];
                                dataGridView5.Rows[RowIndex[StationNo, j]].Cells[j + StepMonthlyColIndex].Value = DewTmpDailyData[StationNo, index];
                                dataGridView6.Rows[RowIndex[StationNo,j]].Cells[j + StepMonthlyColIndex].Value = WindDailyData[StationNo, index];*/

                                PcpMonthlyData[StationNo, j, RowIndex[StationNo, j]] = PcpDailyData[StationNo, index];
                                dataGridView1.Rows[RowIndex[StationNo, j]].Cells[j + StepMonthlyColIndex].Value = PcpMonthlyData[StationNo, j, RowIndex[StationNo, j]];

                                TmpMXMonthlyData[StationNo, j, RowIndex[StationNo, j]] = TmpMXDailyData[StationNo, index];
                                dataGridView2.Rows[RowIndex[StationNo, j]].Cells[j + StepMonthlyColIndex].Value = TmpMXMonthlyData[StationNo, j, RowIndex[StationNo, j]];

                                TmpMNMonthlyData[StationNo, j, RowIndex[StationNo, j]] = TmpMNDailyData[StationNo, index];
                                dataGridView3.Rows[RowIndex[StationNo, j]].Cells[j + StepMonthlyColIndex].Value = TmpMNMonthlyData[StationNo, j, RowIndex[StationNo, j]];

                                SolarMonthlyData[StationNo, j, RowIndex[StationNo, j]] = SolarDailyData[StationNo, index];
                                dataGridView4.Rows[RowIndex[StationNo, j]].Cells[j + StepMonthlyColIndex].Value = SolarMonthlyData[StationNo, j, RowIndex[StationNo, j]];

                                DewTmpMonthlyData[StationNo, j, RowIndex[StationNo, j]] = DewTmpDailyData[StationNo, index];
                                dataGridView5.Rows[RowIndex[StationNo, j]].Cells[j + StepMonthlyColIndex].Value = DewTmpMonthlyData[StationNo, j, RowIndex[StationNo, j]];

                                WindMonthlyData[StationNo, j, RowIndex[StationNo, j]] = WindDailyData[StationNo, index];
                                dataGridView6.Rows[RowIndex[StationNo, j]].Cells[j + StepMonthlyColIndex].Value = WindMonthlyData[StationNo, j, RowIndex[StationNo, j]];

                                index++; //index is total number of days of entire years considered
                                RowIndex[StationNo,j]++;
                            }
                            start = stop + 1;
                        }
                    }
                    StepMonthlyColIndex += 12; //the column gets repeated after 12 months
                }
                for (int StationNo = 0; StationNo < TotalStations; StationNo++)
                {
                    for (j = 0; j < 12; j++)
                    {
                        //MessageBox.Show(StationNo.ToString() + "," + j.ToString() + " : " + RowIndex[StationNo, j].ToString());
                        TotalMonthWiseDataNumber[StationNo, j] = RowIndex[StationNo, j];
                    }
                }
            }
            catch
            {

            }
        }


        private void PasteToGidCellsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridViewMusking.SelectedCells.Count < 1) return;

                string[] lines;

                int row = dataGridViewMusking.SelectedCells[0].RowIndex;
                int col = dataGridViewMusking.SelectedCells[0].ColumnIndex;

                //get copied values
                lines = Clipboard.GetText().Split(new string[] { Environment.NewLine }, StringSplitOptions.None);

                string[] values;
                for (int i = 0; i < lines.Length; i++)
                {
                    values = lines[i].Split('\t');

                    if (row >= dataGridViewMusking.Rows.Count || dataGridViewMusking.Rows[row].IsNewRow) continue;
                    //if (row >= dataGridViewMusking.Rows.Count || dataGridViewMusking.Rows[row].IsNewRow) dataGridViewMusking.Rows.Add();
                    for (int j = 0; j < values.Length; j++)
                    {
                        if (col + j >= dataGridViewMusking.Columns.Count) continue;
                        dataGridViewMusking.Rows[row].Cells[col + j].Value = values[j];
                    }

                    row++;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
        }

        private void EXITToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        public FrmWeatherGenInput()
        {
            InitializeComponent();
        }

        private void TxtYearNo_TextChanged(object sender, EventArgs e)
        {
            try
            {
                GenerateDailyInputrows();
            }
            catch
            {

            }
        }

        private void TxtStartYear_TextChanged(object sender, EventArgs e)
        {
            try
            {
                GenerateDailyInputrows();
            }
            catch
            {

            }
        }

        public void GenerateDailyInputrows()
        {
            try
            {
                SumOfDays = 0;
                ny = Convert.ToInt32(TxtYearNo.Text);
                start_Year = Convert.ToInt32(TxtStartYear.Text);

                Years[0] = start_Year;
                if (DateTime.IsLeapYear(Years[0])) DaysInYear[0] = 366;
                else DaysInYear[0] = 365;

                SumOfDays = SumOfDays + DaysInYear[0];

                for (i = 1; i < ny; i++)
                {
                    Years[i] = Years[i - 1] + 1;
                    if (DateTime.IsLeapYear(Years[i])) DaysInYear[i] = 366;
                    else DaysInYear[i] = 365;

                    SumOfDays = SumOfDays + DaysInYear[i];
                }
                TotalNumberofDailyData = SumOfDays;
                
            }
            catch
            {
                //MessageBox.Show("Del_t or N missing !!!");
            }

            dataGridViewMusking.Rows.Clear(); //Daily input datagridview

            dataGridView1.Rows.Clear(); //Montly datagridview ---Pcp
            dataGridView2.Rows.Clear(); //Montly datagridview ---Tmp MX
            dataGridView3.Rows.Clear(); //Montly datagridview ---Tmp MN
            dataGridView4.Rows.Clear(); //Montly datagridview ---Solar
            dataGridView5.Rows.Clear(); //Montly datagridview ---RH
            dataGridView6.Rows.Clear(); //Montly datagridview ---Wind
            //n = SumOfDays;

            try
            {
                k = 0; k1 = 0; k2 = 0; k3 = 0; k4 = 0; k5 = 0; k6=0;
                
                n = 0; n1 = 0; n2 = 0; n3 = 0; n4 = 0; n5 = 0; n6 = 0;

                k = dataGridViewMusking.Rows.Add(); //Daily input datagridview
                k1 = dataGridView1.Rows.Add(); //Montly datagridview ---Pcp
                k2 = dataGridView2.Rows.Add(); //Montly datagridview ---Tmp MX
                k3 = dataGridView3.Rows.Add(); //Montly datagridview ---Tmp MN
                k4 = dataGridView4.Rows.Add(); //Montly datagridview ---Solar
                k5 = dataGridView5.Rows.Add(); //Montly datagridview ---RH
                k6 = dataGridView6.Rows.Add(); //Montly datagridview ---Wind

                //Daily input datagridview
                for (i = 0; i < ny; i++)
                {
                    //k = dataGridViewMusking.Rows.Add();
                    dataGridViewMusking.Rows[k].Cells[0].Value = Years[i];

                    for (j = 0; j < DaysInYear[i]; j++)
                    {
                        dataGridViewMusking.Rows[k].Cells[1].Value = j + 1;
                        k++;
                        n++;
                        k = dataGridViewMusking.Rows.Add();
                    }
                }
                
                // Montly datagridview ---Pcp
                for (i = 0; i < ny * 31; i++)
                {
                    //dataGridView1.Rows[k1].Cells[0].Value = i;
                    k1++;
                    n1++;
                    k1 = dataGridView1.Rows.Add();
                }

                // Montly datagridview ---Tmp MX
                for (i = 0; i < ny * 31; i++)
                {
                    //dataGridView1.Rows[k1].Cells[0].Value = i;
                    k2++;
                    n2++; 
                    k2 = dataGridView2.Rows.Add();
                }

                // Montly datagridview ---Tmp MN
                for (i = 0; i < ny * 31; i++)
                {
                    //dataGridView1.Rows[k1].Cells[0].Value = i;
                    k3++;
                    n3++;
                    k3 = dataGridView3.Rows.Add();
                }

                // Montly datagridview ---Solar
                for (i = 0; i < ny * 31; i++)
                {
                    //dataGridView1.Rows[k1].Cells[0].Value = i;
                    k4++;
                    n4++;
                    k4 = dataGridView4.Rows.Add();
                }

                // Montly datagridview ---RH
                for (i = 0; i < ny * 31; i++)
                {
                    //dataGridView1.Rows[k1].Cells[0].Value = i;
                    k5++;
                    n5++;
                    k5 = dataGridView5.Rows.Add();
                }

                // Montly datagridview ---Wind
                for (i = 0; i < ny * 31; i++)
                {
                    //dataGridView1.Rows[k1].Cells[0].Value = i;
                    k6++;
                    n6++;
                    k6 = dataGridView6.Rows.Add();
                }
            }
            catch
            {

            }
        }
        public void Generate_WGEN_Rows()
        {
            try 
            {
                ns = Convert.ToInt32(TxtNoOfStation.Text);
            }
            catch
            {
                //MessageBox.Show("Del_t or N missing !!!");
            }

            dataGridView13.Rows.Clear(); //station datagridview

            try
            {
                ks = 0;

                ks = dataGridView13.Rows.Add(); //station datagridview

                // station datagridview
                for (i = 0; i < ns; i++)
                {
                    dataGridView13.Rows[ks].Cells[0].Value = i + 1;
                    ks++;
                    dataGridView13.Rows.Add();
                }
            }
            catch
            {

            }
        }

        public void GenerateStationRows()
        {
            try
            {
                ns = Convert.ToInt32(TxtNoOfStation.Text);
            }
            catch
            {
                //MessageBox.Show("Del_t or N missing !!!");
            }

            dataGridViewStation.Rows.Clear(); //station datagridview

            try
            {
                ks = 0;

                ks = dataGridViewStation.Rows.Add(); //station datagridview

                // station datagridview
                for (i = 0; i < ns; i++)
                {
                    dataGridViewStation.Rows[ks].Cells[0].Value = i+1;
                    ks++;
                    dataGridViewStation.Rows.Add();
                }
            }
            catch
            {

            }
        }

        public void GenerateColumns()
        {
            int StIndex = 1, HeadIndex = 2;
            int MonthHeadIndex = 1, MonthStIndex = 1;
            try
            {
                ns = Convert.ToInt32(TxtNoOfStation.Text);

                //dataGridView.Rows.Clear();
                int TotalColCount = ns * 12 + 1; //for parameter datagridview
                /* dataGridViewMusking.ColumnCount = ns * 6 + 2;
                 dataGridView1.ColumnCount = TotalColCount; //Montly datagridview ---Pcp
                 dataGridView2.ColumnCount = TotalColCount; //Montly datagridview ---Tmp MX
                 dataGridView3.ColumnCount = TotalColCount; //Montly datagridview ---Tmp MN
                 dataGridView4.ColumnCount = TotalColCount; //Montly datagridview ---Solar
                 dataGridView5.ColumnCount = TotalColCount; //Montly datagridview ---RH
                 dataGridView6.ColumnCount = TotalColCount; //Montly datagridview ---Wind */

                dataGridViewMusking.ColumnCount = ns * EachStationColumn + 2; //for daily input datagridview
                for (int GIndex = 0; GIndex <= 11; GIndex++)
                {
                    Datagridviews[GIndex].ColumnCount = TotalColCount;
                }

                for (i = 2; i <= (ns+1); i++)
                {
                    //i = dataGridView.Columns.Add();
                    for(int j =0; j <= 6; j++) //7 headings are:pcp, tmpmx, tmpmn, solar, rh, wind, Dewtmp
                    {
                        dataGridViewMusking.Columns[HeadIndex].Name = HeadingForInput[j] + StIndex.ToString();
                        dataGridViewMusking.Columns[HeadIndex].SortMode = DataGridViewColumnSortMode.NotSortable;
                        dataGridViewMusking.Columns[HeadIndex].Width = 60;

                        if (StIndex % 2 != 0)
                        {
                            dataGridViewMusking.Columns[HeadIndex].HeaderCell.Style.ForeColor = Color.Black;
                            dataGridViewMusking.Columns[HeadIndex].HeaderCell.Style.BackColor = Color.LightBlue;
                            dataGridViewMusking.EnableHeadersVisualStyles = false;
                        }
                        else
                        {
                            dataGridViewMusking.Columns[HeadIndex].HeaderCell.Style.ForeColor = Color.Black;
                            dataGridViewMusking.Columns[HeadIndex].HeaderCell.Style.BackColor = Color.LightGreen;
                            dataGridViewMusking.EnableHeadersVisualStyles = false;
                        }
                        HeadIndex++;
                    }
                    StIndex++;
                }

                //for monthly datagridview-----------------------------------------
                for (i = 1; i <= ns; i++) //ns is number of stations
                {
                    //i = dataGridView.Columns.Add();
                    for (int j = 0; j <= 11; j++)
                    {
                        for(int GridIndex =0; GridIndex <=11; GridIndex++)//12 datagridview (6 for monthly data and remaining 6 for parameters of data)
                        {
                            Datagridviews[GridIndex].Columns[MonthHeadIndex].Name = MonthNames[j] + MonthStIndex.ToString();
                            Datagridviews[GridIndex].Columns[MonthHeadIndex].SortMode = DataGridViewColumnSortMode.NotSortable;
                            Datagridviews[GridIndex].Columns[MonthHeadIndex].Width = 60;

                            if (MonthStIndex % 2 != 0)
                            {
                                Datagridviews[GridIndex].Columns[MonthHeadIndex].HeaderCell.Style.ForeColor = Color.Black;
                                Datagridviews[GridIndex].Columns[MonthHeadIndex].HeaderCell.Style.BackColor = Color.LightBlue;
                                Datagridviews[GridIndex].EnableHeadersVisualStyles = false;
                            }
                            else
                            {
                                Datagridviews[GridIndex].Columns[MonthHeadIndex].HeaderCell.Style.ForeColor = Color.Black;
                                Datagridviews[GridIndex].Columns[MonthHeadIndex].HeaderCell.Style.BackColor = Color.LightGreen;
                                Datagridviews[GridIndex].EnableHeadersVisualStyles = false;
                            }
                        }
                        MonthHeadIndex++;
                    }
                    MonthStIndex++;
                }
            }
            catch
            {

            }
        }

        public void GenerateRowsOfParameters()
        {
            int PcpParaIndex;
            dataGridView7.Rows.Clear();
            dataGridView8.Rows.Clear();
            dataGridView9.Rows.Clear();
            dataGridView10.Rows.Clear();
            dataGridView11.Rows.Clear();
            dataGridView12.Rows.Clear();

            try
            {
                PcpParaIndex = 0;
                //Pcp Parameter datagridview
                PcpParaIndex = dataGridView7.Rows.Add(); 

                dataGridView7.Rows[0].Cells[0].Value = "Mean";
                dataGridView7.Rows.Add();
                dataGridView7.Rows[1].Cells[0].Value = "SDev";
                dataGridView7.Rows.Add();
                dataGridView7.Rows[2].Cells[0].Value = "Skew";
                dataGridView7.Rows.Add();
                dataGridView7.Rows[3].Cells[0].Value = "PR_W1";
                dataGridView7.Rows.Add();
                dataGridView7.Rows[4].Cells[0].Value = "PR_W2";
                dataGridView7.Rows.Add();
                dataGridView7.Rows[5].Cells[0].Value = "PCPD";
                dataGridView7.Rows.Add();
                dataGridView7.Rows[6].Cells[0].Value = "RAINHHMX";

                //TMP MX Parameter datagridview
                PcpParaIndex = dataGridView8.Rows.Add();

                dataGridView8.Rows[0].Cells[0].Value = "Mean";
                dataGridView8.Rows.Add();
                dataGridView8.Rows[1].Cells[0].Value = "SDev";

                //TMP MN Parameter datagridview
                PcpParaIndex = dataGridView9.Rows.Add();

                dataGridView9.Rows[0].Cells[0].Value = "Mean";
                dataGridView9.Rows.Add();
                dataGridView9.Rows[1].Cells[0].Value = "SDev";

                //Solar Parameter datagridview
                PcpParaIndex = dataGridView10.Rows.Add();
                dataGridView10.Rows[0].Cells[0].Value = "Mean";

                //RH Parameter datagridview
                PcpParaIndex = dataGridView11.Rows.Add();
                dataGridView11.Rows[0].Cells[0].Value = "Mean";

                //Wind Parameter datagridview
                PcpParaIndex = dataGridView12.Rows.Add();
                dataGridView12.Rows[0].Cells[0].Value = "Mean";

            }
            catch
            {

            }
        }

        public double FindMean(int NumOfMeanData, double[] MeanData)
        {
            double SumMean = 0;
            for(int i = 0; i<NumOfMeanData; i++)
            {
                SumMean += MeanData[i];
            }
            return (SumMean/NumOfMeanData);
        }

        public double FindStandardDeviation(int NumOfStdData, double[] StdData)
        {
            double TempMean, SumStd = 0;
            TempMean = FindMean(NumOfStdData, StdData);
            for(int i = 0; i< NumOfStdData; i++)
            {
                SumStd += (StdData[i] - TempMean) * (StdData[i] - TempMean);
            }
            return (Math.Sqrt(SumStd/(NumOfStdData - 1)));
        }

        public double FindSkewCoefficient(int NumOfSkewData, double[] SkewData)
        {
            double TempMean, TempStd, SumSkew = 0, Denominator;
            TempMean = FindMean(NumOfSkewData, SkewData);
            TempStd = FindStandardDeviation(NumOfSkewData, SkewData);

            for (int i = 0; i < NumOfSkewData; i++)
            {
                SumSkew += (SkewData[i] - TempMean) * (SkewData[i] - TempMean) * (SkewData[i] - TempMean);
            }
            Denominator = (NumOfSkewData - 1) * (NumOfSkewData - 2) * TempStd * TempStd * TempStd;
            return (NumOfSkewData * SumSkew / Denominator);
        }

        public double FindPR_W1(int NumofPData1, double[] PData1)
        {
            //this calculates probability of wet day following a dry day in a month
            //how many days are there such that if day1 is dry then day2 is wet
            //Dry day means precipitation = 0 mm and Wet day means precipitation > 0 mm
            int CountTotalDry = 0, CountWetAfterDry = 0;
            for(int i = 0; i < (NumofPData1 - 1); i++)
            {
                if(PData1[i] ==0)
                {
                    CountTotalDry++;
                    if(PData1[i+1] > 0)
                    {
                        CountWetAfterDry++;
                    }
                }
            }
            if (CountTotalDry == 0)
            {
                 return 0;
            }
            else
            {
                double num = Convert.ToDouble(CountWetAfterDry);
                double den = Convert.ToDouble(CountTotalDry);
                //return (CountWetAfterDry / CountTotalDry);
                return (num/den);
            }
        }

        public double FindPR_W2(int NumofPData2, double[] PData2)
        {
            //this calculates probability of wet day following a wet day in a month
            //how many days are there such that if day1 is wet then day2 is also wet
            //Dry day means precipitation = 0 mm and Wet day means precipitation > 0 mm
            int CountTotalWet = 0, CountWetAfterWet = 0;
            for (int i = 0; i < (NumofPData2 - 1); i++)
            {
                if (PData2[i] > 0)
                {
                    CountTotalWet++;
                    if (PData2[i + 1] > 0)
                    {
                        CountWetAfterWet++;
                    }
                }
            }

            double num = Convert.ToDouble(CountWetAfterWet);
            double den = Convert.ToDouble(CountTotalWet);

            if (CountTotalWet == 0) return 0;
            //else return (CountWetAfterWet / CountTotalWet);
            else return (num / den);
        }
         
        public double FindPCPD(int NumofPCPDData, int NumOfYrs, double[] PCPDData)
        {
            //this calculates average number of days of precipitation in a month
            //i.e. Total number of wet days/total number of days in a month for entire period of observation
            //Dry day means precipitation = 0 mm and Wet day means precipitation > 0 mm
            int CountPCPDTotalWet = 0;
            for (int i = 0; i < NumofPCPDData; i++)
            {
                if (PCPDData[i] > 0)
                {
                    CountPCPDTotalWet++;
                }
            }
            double num = Convert.ToDouble(CountPCPDTotalWet);
            double den = Convert.ToDouble(NumOfYrs);
            //return (CountPCPDTotalWet/NumOfYrs);
            return (num / den);
        }

        public double FindRainHHMX(int NumOfRainData, double[] RainData) 
        {
            double MaxRain = RainData[0];
            for (int i = 1; i < NumOfRainData; i++)
            { 
               if(RainData[i] > MaxRain)
               {
                    MaxRain = RainData[i];
               }
            }
            return (MaxRain/3);
        }
    }
}
