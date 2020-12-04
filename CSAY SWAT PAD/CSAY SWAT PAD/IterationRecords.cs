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
    public partial class FrmIterationRecords : Form
    {
        public FrmIterationRecords()
        {
            InitializeComponent();
        }

        private void BtnIterationRecords_Click(object sender, EventArgs e)
        {
            SQLiteConnection ConnectDb = new SQLiteConnection("Data Source = SWAT_PAD_ITERATION.sqlite3");
            ConnectDb.Open();

            string query = "SELECT * FROM SWAT_Iterations";
            SQLiteDataAdapter DataAdptr = new SQLiteDataAdapter(query, ConnectDb);

            DataTable Dt = new DataTable();
            DataAdptr.Fill(Dt);
            dataGridViewAllPara.DataSource = Dt;

            dataGridViewAllPara.Columns[0].Width = 60; //ID
            dataGridViewAllPara.Columns[1].Width = 180; //ProjetName
            dataGridViewAllPara.Columns[2].Width = 80; //IterationNo
            dataGridViewAllPara.Columns[3].Width = 200; //Parameters
            dataGridViewAllPara.Columns[4].Width = 290;  //Remark
            dataGridViewAllPara.Columns[5].Width = 290;  //Findings
            dataGridViewAllPara.Columns[6].Width = 120;  //Findings

            ConnectDb.Close();
            LblDbLog.Text = "Recent Activity: Iteration Record Loaded Successfully";

            int rcount = Dt.Rows.Count;
            LblRecordNo.Text = "Record No: " + rcount.ToString();
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void BtnFilter_Click(object sender, EventArgs e)
        {
            SQLiteConnection ConnectDb = new SQLiteConnection("Data Source = SWAT_PAD_ITERATION.sqlite3");
            ConnectDb.Open();

            //string query1 = "SELECT * FROM SWAT_Parameters";
            string query = "SELECT * FROM SWAT_Iterations where " + ComboBoxFilterBy.Text + " = '" + ComboBoxFilterDistinctValues.Text + "'";
            SQLiteDataAdapter DataAdptr = new SQLiteDataAdapter(query, ConnectDb);

            DataTable Dt = new DataTable();
            DataAdptr.Fill(Dt);
            dataGridViewAllPara.DataSource = Dt;

            dataGridViewAllPara.Columns[0].Width = 60; //ID
            dataGridViewAllPara.Columns[1].Width = 180; //ProjetName
            dataGridViewAllPara.Columns[2].Width = 80; //IterationNo
            dataGridViewAllPara.Columns[3].Width = 200; //Parameters
            dataGridViewAllPara.Columns[4].Width = 290;  //Remark
            dataGridViewAllPara.Columns[5].Width = 290;  //Findings
            dataGridViewAllPara.Columns[6].Width = 120;  //Findings

            ConnectDb.Close();
            //MessageBox.Show("Parameters Data Loaded Successfully.", "Load Parameters");
            int rcount = Dt.Rows.Count;
            LblRecordNo.Text = "Record No: " + rcount.ToString();
        }

        private void FrmIterationRecords_Load(object sender, EventArgs e)
        {
            //Add ---> Final Verdict
            ComboBoxAddFinalVerdict.Items.Add("Worst");
            ComboBoxAddFinalVerdict.Items.Add("Bad");
            ComboBoxAddFinalVerdict.Items.Add("Improving");
            ComboBoxAddFinalVerdict.Items.Add("Good");
            ComboBoxAddFinalVerdict.Items.Add("Best");

            //Update ---> Final Verdict
            ComboBoxUpdateFinalVerdict.Items.Add("Worst");
            ComboBoxUpdateFinalVerdict.Items.Add("Bad");
            ComboBoxUpdateFinalVerdict.Items.Add("Improving");
            ComboBoxUpdateFinalVerdict.Items.Add("Good");
            ComboBoxUpdateFinalVerdict.Items.Add("Best");

            //Filter --> Heading --> Level 1
            ComboBoxFilterBy.Items.Add("ID");
            ComboBoxFilterBy.Items.Add("ProjectName");
            ComboBoxFilterBy.Items.Add("IterationNo");
            ComboBoxFilterBy.Items.Add("FinalVerdict");

            //Filter --> Heading --> Level 2
            ComboBoxFilterByLevel2.Items.Add("IterationNo");
            ComboBoxFilterByLevel2.Items.Add("FinalVerdict");

        }

        private void ComboBoxFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            string value;
            SQLiteConnection ConnectDb = new SQLiteConnection("Data Source = SWAT_PAD_ITERATION.sqlite3");
            ConnectDb.Open();

            //for unique value
            string query = "SELECT DISTINCT " + ComboBoxFilterBy.Text + " FROM SWAT_Iterations";
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

            /*if (ComboBoxFilterBy.Text == "ProjectName")
            {
                BtnDeleteProject.Enabled = true;
            }
            else
            {
                BtnDeleteProject.Enabled = false;
            }*/
            /*if(ComboBoxFilterBy.Text == "ID" || ComboBoxFilterBy.Text == "IterationNo")
            {
                TxtFilterDeleteProject.Text = "";
                TxtFinalVerdictValues.Text = "";
            }*/
        }

        private void BtnAddIterationRecord_Click(object sender, EventArgs e)
        {
            string ProjectName= TxtAddProjecName.Text;
            string IterationNo = TxtAddIterationNo.Text;
            string Parameters = TxtAddParameters.Text;
            string Remark = TxtAddRemarks.Text;
            string Findings = TxtAddFindings.Text;
            string FinalVerdict = TxtAddFinalVerdict.Text;

            SQLiteConnection ConnectDb = new SQLiteConnection("Data Source = SWAT_PAD_ITERATION.sqlite3");
            ConnectDb.Open();
            string query = "INSERT INTO SWAT_Iterations(ProjectName,IterationNo,Parameters,Remark,Findings,FinalVerdict) VALUES('" + ProjectName + "','" + IterationNo + "','" + Parameters + "','" + Remark + "','" + Findings + "','" + FinalVerdict + "')";

            SQLiteCommand Cmd = new SQLiteCommand(query, ConnectDb);
            Cmd.ExecuteNonQuery();

            ConnectDb.Close();

            //TxtAddProjecName.Text = "";
            TxtAddIterationNo.Text = "";
            TxtAddParameters.Text = "";
            TxtAddRemarks.Text = "";
            TxtAddFindings.Text = "";
            TxtAddFinalVerdict.Text = "";

            LblAddMsg.Text = "Activity: Parameter Successfully Added : " + IterationNo + ProjectName;
        }

        private void BtnDisplay_Click(object sender, EventArgs e)
        {
            if (TxtUpdateID.Text == "")
            {
                LblUpdateMsg.Text = "Enter ID to Display";
            }
            else
            {
                SQLiteConnection ConnectDb = new SQLiteConnection("Data Source = SWAT_PAD_ITERATION.sqlite3");
                ConnectDb.Open();

                string query = "SELECT * FROM SWAT_Iterations where ID = '" + TxtUpdateID.Text + "'";

                SQLiteDataAdapter DataAdptr = new SQLiteDataAdapter(query, ConnectDb);

                DataTable Dt = new DataTable();
                DataAdptr.Fill(Dt);
                string value;
                foreach (DataRow row in Dt.Rows) //there is only one row here
                {
                    value = row[1].ToString();
                    TxtUpdateProjectName.Text = value;
                    value = row[2].ToString();
                    TxtUpdateIterationNo.Text = value;
                    value = row[3].ToString();
                    TxtUpdateParameters.Text = value;
                    value = row[4].ToString();
                    TxtUpdateRemarks.Text = value;
                    value = row[5].ToString();
                    TxtUpdateFindings.Text = value;
                    value = row[6].ToString();
                    TxtUpdateFinalVerdict.Text = value;
                }
                ConnectDb.Close();
                LblUpdateMsg.Text = "Selected ID Displayed" + " : " + TxtUpdateProjectName.Text + " : " + TxtUpdateIterationNo.Text;
            }
        }

        private void BtnUpdate_Click(object sender, EventArgs e)
        {
            string ID = TxtUpdateID.Text;
            string ProjectName = TxtUpdateProjectName.Text;
            string IterationNo = TxtUpdateIterationNo.Text;
            string Parameters = TxtUpdateParameters.Text;
            string Remark = TxtUpdateRemarks.Text;
            string Findings = TxtUpdateFindings.Text;
            string FinalVerdict = TxtUpdateFinalVerdict.Text;

            DialogResult dr = MessageBox.Show("Are you sure, you want to Modify?", "Modify", MessageBoxButtons.YesNo);
            if (dr == DialogResult.Yes)
            {
                //Modify
                SQLiteConnection ConnectDb = new SQLiteConnection("Data Source = SWAT_PAD_ITERATION.sqlite3");
                ConnectDb.Open();

                string query = "REPLACE INTO SWAT_Iterations(ID,ProjectName,IterationNo,Parameters,Remark,Findings,FinalVerdict) VALUES('" + ID + "','" + ProjectName + "','" + IterationNo + "','" + Parameters + "','" + Remark + "','" + Findings + "','" + FinalVerdict + "')";

                SQLiteCommand Cmd = new SQLiteCommand(query, ConnectDb);
                Cmd.ExecuteNonQuery();

                ConnectDb.Close();

                LblUpdateMsg.Text = "Existing Data Successfully updated in the database : " + ProjectName + " : " + IterationNo;
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
                    SQLiteConnection ConnectDb = new SQLiteConnection("Data Source =SWAT_PAD_ITERATION.sqlite3");
                    ConnectDb.Open();

                    string query = "DELETE FROM  SWAT_Iterations WHERE ID ='" + TxtUpdateID.Text + "' ";
                    SQLiteCommand Cmd = new SQLiteCommand(query, ConnectDb);
                    Cmd.ExecuteNonQuery();

                    ConnectDb.Close();

                    TxtUpdateID.Text = "";
                    string value = "";
                    TxtUpdateProjectName.Text = value;
                    TxtUpdateIterationNo.Text = value;
                    TxtUpdateParameters.Text = value;
                    TxtUpdateRemarks.Text = value;
                    TxtUpdateFindings.Text = value;
                    TxtUpdateFinalVerdict.Text = value;

                    LblUpdateMsg.Text = "Selected Data Successfully Deleted from database : " + TxtUpdateID.Text;
                }
                else if (dr == DialogResult.No)
                {
                    //Nothing to do
                }

            }
        }

        private void ComboBoxFilterDistinctValues_SelectedIndexChanged(object sender, EventArgs e)
        {
            TxtFilterMsg.Text = "1 : Filter By :   " + ComboBoxFilterBy.Text + "  For Distinct Value  :  " + ComboBoxFilterDistinctValues.Text;
            /*if(ComboBoxFilterBy.Text == "ProjectName")
            {
                TxtFilterDeleteProject.Text = ComboBoxFilterDistinctValues.Text;
            }
            else if (ComboBoxFilterBy.Text == "FinalVerdict")
            {
                TxtFinalVerdictValues.Text = ComboBoxFilterDistinctValues.Text;
            }*/
        }

        private void ComboBoxAddFinalVerdict_SelectedIndexChanged(object sender, EventArgs e)
        {
            TxtAddFinalVerdict.Text = ComboBoxAddFinalVerdict.Text;
        }

        private void ComboBoxUpdateFinalVerdict_SelectedIndexChanged(object sender, EventArgs e)
        {
            TxtUpdateFinalVerdict.Text = ComboBoxUpdateFinalVerdict.Text;
        }

        private void BtnDeleteProject_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Are You Sure, you want to delete the Complete Project?", "Delete Project", MessageBoxButtons.YesNo);
            if (dr == DialogResult.Yes)
            {
                //delete
                SQLiteConnection ConnectDb = new SQLiteConnection("Data Source =SWAT_PAD_ITERATION.sqlite3");
                ConnectDb.Open();

                string query = "DELETE FROM  SWAT_Iterations WHERE ProjectName ='" + ComboBoxFilterDistinctValues.Text + "' ";
                SQLiteCommand Cmd = new SQLiteCommand(query, ConnectDb);
                Cmd.ExecuteNonQuery();

                ConnectDb.Close();

                LblFilterDelete.Text = "Selected Project Successfully Deleted from database : " + ComboBoxFilterDistinctValues.Text;
            }
            else if (dr == DialogResult.No)
            {
                //Nothing to do
            }
        }

        private void BtnDeleteFinalVerdictOfProject_Click(object sender, EventArgs e)
        {

        }

        private void ComboBoxFilterByLevel2_SelectedIndexChanged(object sender, EventArgs e)
        {
            string value;
            SQLiteConnection ConnectDb = new SQLiteConnection("Data Source = SWAT_PAD_ITERATION.sqlite3");
            ConnectDb.Open();

            //for unique value
            string query = "SELECT DISTINCT " + ComboBoxFilterByLevel2.Text + " FROM SWAT_Iterations";
            SQLiteDataAdapter DataAdptr = new SQLiteDataAdapter(query, ConnectDb);

            DataTable Dt = new DataTable();
            DataAdptr.Fill(Dt);

            ComboBoxFilterDistinctValuesLevel2.Items.Clear();
            foreach (DataRow row in Dt.Rows)
            {
                value = row[0].ToString();
                ComboBoxFilterDistinctValuesLevel2.Items.Add(value);
            }

            ConnectDb.Close();
        }

        private void BtnDeleteSthOfProject_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Are You Sure, you want to the Final Verdict of the Project?", "Delete Final Verdict", MessageBoxButtons.YesNo);
            if (dr == DialogResult.Yes)
            {
                //delete
                SQLiteConnection ConnectDb = new SQLiteConnection("Data Source =SWAT_PAD_ITERATION.sqlite3");
                ConnectDb.Open();

                string query = "DELETE FROM  SWAT_Iterations WHERE ProjectName ='" + ComboBoxFilterDistinctValues.Text + "' AND " + ComboBoxFilterByLevel2.Text + " ='" + ComboBoxFilterDistinctValuesLevel2.Text + "'";
                SQLiteCommand Cmd = new SQLiteCommand(query, ConnectDb);
                Cmd.ExecuteNonQuery();

                ConnectDb.Close();

                LblFilterDelete.Text = ComboBoxFilterDistinctValues.Text + " : " + ComboBoxFilterDistinctValuesLevel2.Text + " Successfully Deleted";
            }
            else if (dr == DialogResult.No)
            {
                //Nothing to do
            }
        }

        private void BtnFilterLevel2_Click(object sender, EventArgs e)
        {
            SQLiteConnection ConnectDb = new SQLiteConnection("Data Source = SWAT_PAD_ITERATION.sqlite3");
            ConnectDb.Open();

            //string query1 = "SELECT * FROM SWAT_Parameters";
            string query = "SELECT * FROM SWAT_Iterations where " + ComboBoxFilterBy.Text + " = '" + ComboBoxFilterDistinctValues.Text + "'AND " + ComboBoxFilterByLevel2.Text + " ='" + ComboBoxFilterDistinctValuesLevel2.Text + "'";
            SQLiteDataAdapter DataAdptr = new SQLiteDataAdapter(query, ConnectDb);

            DataTable Dt = new DataTable();
            DataAdptr.Fill(Dt);
            dataGridViewAllPara.DataSource = Dt;

            dataGridViewAllPara.Columns[0].Width = 60; //ID
            dataGridViewAllPara.Columns[1].Width = 180; //ProjetName
            dataGridViewAllPara.Columns[2].Width = 80; //IterationNo
            dataGridViewAllPara.Columns[3].Width = 200; //Parameters
            dataGridViewAllPara.Columns[4].Width = 290;  //Remark
            dataGridViewAllPara.Columns[5].Width = 290;  //Findings
            dataGridViewAllPara.Columns[6].Width = 120;  //Findings

            ConnectDb.Close();
            //MessageBox.Show("Parameters Data Loaded Successfully.", "Load Parameters");
            int rcount = Dt.Rows.Count;
            LblRecordNo.Text = "Record No: " + rcount.ToString();
        }

        private void ComboBoxFilterDistinctValuesLevel2_SelectedIndexChanged(object sender, EventArgs e)
        {
            TxtFilterMsgLevel2.Text = "2 : Filter By :   " + ComboBoxFilterByLevel2.Text + "  For Distinct Value  :  " + ComboBoxFilterDistinctValuesLevel2.Text;
        }
    }
}
