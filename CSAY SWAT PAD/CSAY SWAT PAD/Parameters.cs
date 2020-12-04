using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;

namespace CSAY_SWAT_PAD
{
    public partial class FrmParameters : Form
    {
        public FrmParameters()
        {
            InitializeComponent();
        }

        private void BtnParametersRecord_Click(object sender, EventArgs e)
        {
            SQLiteConnection ConnectDb = new SQLiteConnection("Data Source = SWAT_PAD_PARA.sqlite3");
            ConnectDb.Open();

            string query = "SELECT * FROM SWAT_Parameters";
            SQLiteDataAdapter DataAdptr = new SQLiteDataAdapter(query, ConnectDb);

            DataTable Dt = new DataTable();
            DataAdptr.Fill(Dt);
            dataGridViewAllPara.DataSource = Dt;

            dataGridViewAllPara.Columns[0].Width = 60; //ID
            dataGridViewAllPara.Columns[1].Width = 82; //Method
            dataGridViewAllPara.Columns[2].Width = 100; //Name
            dataGridViewAllPara.Columns[3].Width = 80; //File
            dataGridViewAllPara.Columns[4].Width = 275;  //Definition
            dataGridViewAllPara.Columns[5].Width = 70;  //min
            dataGridViewAllPara.Columns[6].Width = 70; //max
            dataGridViewAllPara.Columns[7].Width = 70; //Default
            dataGridViewAllPara.Columns[8].Width = 130; //Variables
            dataGridViewAllPara.Columns[9].Width = 280; //Remark

            /*int rindex = 0;
            string value;
            dataGridViewAllPara.Rows.Clear();
            rindex = dataGridViewAllPara.Rows.Add();
            foreach(DataRow row in Dt.Rows)
            {
                for (int i =0; i<10; i++)
                {
                    value = row[i].ToString();
                    dataGridViewAllPara.Rows[rindex].Cells[i].Value = value;
                }
                rindex++;
                dataGridViewAllPara.Rows.Add();
            }
            dataGridViewAllPara.Rows.RemoveAt(rindex);*/

            ConnectDb.Close();
            //MessageBox.Show("Parameters Data Loaded Successfully.", "Load Parameters");
            LblDbLog.Text = "Recent Activity: Parameters Data Loaded Successfully";
            int rcount = Dt.Rows.Count;
            LblRecordNo.Text = "Record No: " + rcount.ToString();

            //LblMsg.ForeColor = Color.Chartreuse;
            //LblMsg.Text = "LAST ACTIVITY: Viewing Database table";
        }

        private void BtnAddPara_Click(object sender, EventArgs e)
        {
             string Method = TxtAddMethod.Text;
             string Name = TxtAddName.Text;
             string File = TxtAddFile.Text;
             string Definition = TxtAddDefinition.Text;
             string Min = TxtAddMin.Text;
             string Max = TxtAddMax.Text;
             string Default = TxtAddDefault.Text;
             string Variable = TxtAddVariable.Text;
             string Remarks = TxtAddRemarks.Text;

            SQLiteConnection ConnectDb = new SQLiteConnection("Data Source = SWAT_PAD_PARA.sqlite3");
            ConnectDb.Open();
            string query = "INSERT INTO SWAT_Parameters(Method,Name,File,Definition,Min,Max,DefaultVal,Variable,Remarks) VALUES('" + Method + "','" + Name + "','" + File + "','" + Definition + "','" + Min + "','" + Max + "','" + Default + "','" + Variable + "','" + Remarks + "')";
                
            SQLiteCommand Cmd = new SQLiteCommand(query, ConnectDb);
            Cmd.ExecuteNonQuery();

            ConnectDb.Close();
            /*if (Method == "" || Name == "" || File == "" || Definition == "" || Min == "" ||
            Max == "" || Default == "" || Variable == "" || Remarks == "")
            {
                LblAddMsg.ForeColor = Color.Red;
                LblAddMsg.Text = "WARNING: Some or all data missing...";
            }*/

            TxtAddMethod.Text = "";
            TxtAddName.Text = "";
            TxtAddFile.Text = "";
            TxtAddDefinition.Text = "";
            TxtAddMin.Text = "";
            TxtAddMax.Text = "";
            TxtAddDefault.Text = "";
            TxtAddVariable.Text = "";
            TxtAddRemarks.Text = "";

            LblAddMsg.ForeColor = Color.Green;
            LblAddMsg.Text = "Activity: Parameter Successfully Added : " + Name + File;
                
        }

        private void FrmParameters_Load(object sender, EventArgs e)
        {
            //Add --> Method
            ComboBoxAddMethod.Items.Add("relative (r)");
            ComboBoxAddMethod.Items.Add("absolute (a)");
            ComboBoxAddMethod.Items.Add("replace (v)");

            //Add --> Variable
            ComboBoxAddVariable.Items.Add("Flow and Sediment");
            ComboBoxAddVariable.Items.Add("Flow Only");
            ComboBoxAddVariable.Items.Add("Sediment Only");
            ComboBoxAddVariable.Items.Add("Others");

            //Update and Delete --> Method
            ComboBoxUpdateMethod.Items.Add("relative (r)");
            ComboBoxUpdateMethod.Items.Add("absolute (a)");
            ComboBoxUpdateMethod.Items.Add("replace (v)");

            //Update and Delete --> Variable
            ComboBoxUpdateVariable.Items.Add("Flow and Sediment");
            ComboBoxUpdateVariable.Items.Add("Flow Only");
            ComboBoxUpdateVariable.Items.Add("Sediment Only");
            ComboBoxUpdateVariable.Items.Add("Others");

            //Filter --> Heading
            ComboBoxFilterBy.Items.Add("ID");
            ComboBoxFilterBy.Items.Add("Method");
            ComboBoxFilterBy.Items.Add("Name");
            ComboBoxFilterBy.Items.Add("File");
            ComboBoxFilterBy.Items.Add("Variable");

        }

        private void ComboBoxAddMethod_SelectedIndexChanged(object sender, EventArgs e)
        {
            TxtAddMethod.Text = ComboBoxAddMethod.Text;
        }

        private void ComboBoxAddVariable_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(ComboBoxAddVariable.Text == "Others")
            {
                TxtAddVariable.Text = "";
            }
            else
            {
                TxtAddVariable.Text = ComboBoxAddVariable.Text;
            }
        }

        private void BtnDisplay_Click(object sender, EventArgs e)
        {
            if(TxtUpdateID.Text =="")
            {
                LblUpdateMsg.Text = "Enter ID to Display";
            }
            else
            {
                SQLiteConnection ConnectDb = new SQLiteConnection("Data Source = SWAT_PAD_PARA.sqlite3");
                ConnectDb.Open();

                string query = "SELECT * FROM SWAT_Parameters where ID = '"+ TxtUpdateID.Text +"'";

                SQLiteDataAdapter DataAdptr = new SQLiteDataAdapter(query, ConnectDb);

                DataTable Dt = new DataTable();
                DataAdptr.Fill(Dt);
                string value;
                foreach (DataRow row in Dt.Rows) //there is only one row here
                {
                    value = row[1].ToString();
                    TxtUpdateMethod.Text = value;
                    value = row[2].ToString();
                    TxtUpdateName.Text = value;
                    value = row[3].ToString();
                    TxtUpdateFile.Text = value;
                    value = row[4].ToString();
                    TxtUpdateDefinition.Text = value;
                    value = row[5].ToString();
                    TxtUpdateMin.Text = value;
                    value = row[6].ToString();
                    TxtUpdateMax.Text = value;
                    value = row[7].ToString();
                    TxtUpdateDefault.Text = value;
                    value = row[8].ToString();
                    TxtUpdateVariable.Text = value;
                    value = row[9].ToString();
                    TxtUpdateRemarks.Text = value;
                }

                 ConnectDb.Close();
               LblUpdateMsg.Text = "Selected ID Displayed" + " : " + TxtUpdateName.Text + TxtUpdateFile.Text;
            }
        }

        private void BtnUpdate_Click(object sender, EventArgs e)
        {
            string ID = TxtUpdateID.Text;
            string Method = TxtUpdateMethod.Text;
            string Name = TxtUpdateName.Text;
            string File = TxtUpdateFile.Text;
            string Definition = TxtUpdateDefinition.Text;
            string Min = TxtUpdateMin.Text;
            string Max = TxtUpdateMax.Text;
            string Default = TxtUpdateDefault.Text;
            string Variable = TxtUpdateVariable.Text;
            string Remarks = TxtUpdateRemarks.Text;

            DialogResult dr = MessageBox.Show("Are you sure, you want to Modify?", "Modify", MessageBoxButtons.YesNo);
            if (dr == DialogResult.Yes)
            {
                //Modify
                SQLiteConnection ConnectDb = new SQLiteConnection("Data Source = SWAT_PAD_PARA.sqlite3");
                ConnectDb.Open();

                string query = "REPLACE INTO SWAT_Parameters(ID,Method,Name,File,Definition,Min,Max,DefaultVal,Variable,Remarks) VALUES('" + ID + "','" + Method + "','" + Name + "','" + File + "','" + Definition + "','" + Min + "','" + Max + "','" + Default + "','" + Variable + "','" + Remarks + "')";

                SQLiteCommand Cmd = new SQLiteCommand(query, ConnectDb);
                Cmd.ExecuteNonQuery();

                ConnectDb.Close();

                LblUpdateMsg.Text = "Existing Data Successfully updated in the database : " + Name + File;
            }
            else if (dr == DialogResult.No)
            {
                //Nothing to do
            }
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {

            if (TxtUpdateID.Text == "")
            {
                LblUpdateMsg.Text = "Enter ID to Delete";
            }
            else
            {
                DialogResult dr = MessageBox.Show("Are You Sure, you want to delete?", "Delete", MessageBoxButtons.YesNo);
                if (dr == DialogResult.Yes)
                {
                    //delete
                    SQLiteConnection ConnectDb = new SQLiteConnection("Data Source = SWAT_PAD_PARA.sqlite3");
                    ConnectDb.Open();

                    string query = "DELETE FROM  SWAT_Parameters WHERE ID ='" + TxtUpdateID.Text + "' ";
                    SQLiteCommand Cmd = new SQLiteCommand(query, ConnectDb);
                    Cmd.ExecuteNonQuery();

                    ConnectDb.Close();

                    TxtUpdateID.Text = "";
                    string value = "";
                    TxtUpdateMethod.Text = value;
                    TxtUpdateName.Text = value;
                    TxtUpdateFile.Text = value;
                    TxtUpdateDefinition.Text = value;
                    TxtUpdateMin.Text = value;
                    TxtUpdateMax.Text = value;
                    TxtUpdateDefault.Text = value;
                    TxtUpdateVariable.Text = value;
                    TxtUpdateRemarks.Text = value;

                    LblUpdateMsg.Text = "Selected Data Successfully Deleted from database: ID = " + TxtUpdateID.Text;
                }
                else if (dr == DialogResult.No)
                {
                    //Nothing to do
                }
               
            }
        }

        private void BtnFilter_Click(object sender, EventArgs e)
        {
            SQLiteConnection ConnectDb = new SQLiteConnection("Data Source = SWAT_PAD_PARA.sqlite3");
            ConnectDb.Open();

            //string query1 = "SELECT * FROM SWAT_Parameters";
            string query = "SELECT * FROM SWAT_Parameters where "+ComboBoxFilterBy.Text+" = '" + ComboBoxFilterDistinctValues.Text + "'";
            SQLiteDataAdapter DataAdptr = new SQLiteDataAdapter(query, ConnectDb);

            DataTable Dt = new DataTable();
            DataAdptr.Fill(Dt); 
            dataGridViewAllPara.DataSource = Dt;

            dataGridViewAllPara.Columns[0].Width = 60; //ID
            dataGridViewAllPara.Columns[1].Width = 82; //Method
            dataGridViewAllPara.Columns[2].Width = 100; //Name
            dataGridViewAllPara.Columns[3].Width = 80; //File
            dataGridViewAllPara.Columns[4].Width = 275;  //Definition
            dataGridViewAllPara.Columns[5].Width = 70;  //min
            dataGridViewAllPara.Columns[6].Width = 70; //max
            dataGridViewAllPara.Columns[7].Width = 70; //Default
            dataGridViewAllPara.Columns[8].Width = 130; //Variables
            dataGridViewAllPara.Columns[9].Width = 280; //Remark

            ConnectDb.Close();
            //MessageBox.Show("Parameters Data Loaded Successfully.", "Load Parameters");
            int rcount = Dt.Rows.Count;
            LblRecordNo.Text = "Record No: " + rcount.ToString();
        }

        private void ComboBoxFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            string value;
            SQLiteConnection ConnectDb = new SQLiteConnection("Data Source = SWAT_PAD_PARA.sqlite3");
            ConnectDb.Open();

            //for unique value
            string query = "SELECT DISTINCT " + ComboBoxFilterBy.Text + " FROM SWAT_Parameters";
            SQLiteDataAdapter DataAdptr = new SQLiteDataAdapter(query, ConnectDb);

            DataTable Dt = new DataTable();
            DataAdptr.Fill(Dt);

            ComboBoxFilterDistinctValues.Items.Clear();
            foreach (DataRow row in Dt.Rows)
            {
                value = row[0].ToString();
                ComboBoxFilterDistinctValues.Items.Add(value);
            }

            ConnectDb.Close();
        }

        private void ComboBoxUpdateVariable_SelectedIndexChanged(object sender, EventArgs e)
        {
            TxtUpdateVariable.Text = ComboBoxUpdateVariable.Text;
        }

        private void ComboBoxUpdateMethod_SelectedIndexChanged(object sender, EventArgs e)
        {
            TxtUpdateMethod.Text = ComboBoxUpdateMethod.Text;
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void ComboBoxFilterDistinctValues_SelectedIndexChanged(object sender, EventArgs e)
        {
            TxtFilterMsg.Text = "Filter By :   " + ComboBoxFilterBy.Text + "  For Distinct Value  :  " + ComboBoxFilterDistinctValues.Text;
        }
    }
}
