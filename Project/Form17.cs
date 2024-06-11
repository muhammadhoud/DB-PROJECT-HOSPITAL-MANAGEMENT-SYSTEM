using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form17 : Form
    {
        public Form1 form1;



        public void loadFormRef(Form1 f1)
        {
            form1 = f1;

            // Initialization for objects using form1
            print();
        }
        public Form17()
        {
            InitializeComponent();
        }

        private void Form17_Load(object sender, EventArgs e)
        {

        }

        void print()
        {
            dataGridView1.DataSource = null;
            try
            {
                // Open the database connection
                form1.con.Open();

                // Define the SQL query to retrieve customer support data
                string sqlQuery = "SELECT ISSUE_ID, DESCRIPTION, RESOLUTION, TIMESTAMP, PATIENT_ID, STAFF_ID FROM HOSPITAL.CUSTOMER_SUPPORT";

                // Create a command object with the SQL query and connection
                OracleCommand cmd = new OracleCommand(sqlQuery, form1.con);

                // Execute the SQL query and retrieve the data
                OracleDataReader reader = cmd.ExecuteReader();

                // Create a DataTable to hold the retrieved data
                DataTable dt = new DataTable();

                // Add columns to the DataTable
                dt.Columns.Add("ISSUE_ID");
                dt.Columns.Add("DESCRIPTION");
                dt.Columns.Add("RESOLUTION");
                dt.Columns.Add("TIMESTAMP");
                dt.Columns.Add("PATIENT_ID");
                dt.Columns.Add("STAFF_ID");

                // Load the data from the DataReader into the DataTable
                while (reader.Read())
                {
                    DataRow row = dt.NewRow();
                    row["ISSUE_ID"] = reader["ISSUE_ID"];
                    row["DESCRIPTION"] = reader["DESCRIPTION"];
                    row["RESOLUTION"] = reader["RESOLUTION"];
                    row["TIMESTAMP"] = reader["TIMESTAMP"];
                    row["PATIENT_ID"] = reader["PATIENT_ID"];
                    row["STAFF_ID"] = reader["STAFF_ID"];
                    dt.Rows.Add(row);
                }

                // Close the DataReader
                reader.Close();

                // Close the database connection
                form1.con.Close();

                // Bind the DataTable to the DataGridView to display the data
                dataGridView1.DataSource = dt;
            }
            catch (Exception ex)
            {
                // Display any errors that occur during the retrieval process
                MessageBox.Show("Error: " + ex.Message);

                // Close the database connection in case of an error
                if (form1.con.State == ConnectionState.Open)
                {
                    form1.con.Close();
                }
            }
        }

        

        private void button1_Click_1(object sender, EventArgs e)
        {
            Form12 CUSTOMERSUPPORTpage = new Form12();
            CUSTOMERSUPPORTpage.loadFormRef(form1);

            // Show the AdminSignUp form
            CUSTOMERSUPPORTpage.Show();

            // Optionally, hide the current form if needed
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // This button click event handles the solving of an issue.
            btnSolveIssue_Click(sender, e);
        }

        

        private void btnSolveIssue_Click(object sender, EventArgs e)
        {
            // This method solves the issue based on the input issue ID.
            string issueId = textBox1.Text.Trim();

            if (string.IsNullOrEmpty(issueId))
            {
                MessageBox.Show("Please enter the issue ID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Solve the issue here (for demonstration purposes, just showing a message)
            MessageBox.Show($"Issue ID {issueId} has been solved successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // Delete the issue from the CUSTOMER_SUPPORT table
            DeleteIssueFromCustomerSupport(issueId);
        }

        private void DeleteIssueFromCustomerSupport(string issueId)
        {
            try
            {
                // Open the database connection
                form1.con.Open();

                // Define the SQL query to delete the issue from the CUSTOMER_SUPPORT table
                string sqlDeleteQuery = "DELETE FROM HOSPITAL.CUSTOMER_SUPPORT WHERE ISSUE_ID = :issueId";

                // Create a command object with the SQL query and connection
                OracleCommand cmd = new OracleCommand(sqlDeleteQuery, form1.con);
                cmd.Parameters.Add(":issueId", OracleDbType.Varchar2).Value = issueId;

                // Execute the SQL query
                int rowsAffected = cmd.ExecuteNonQuery();

                // Check if the issue was successfully deleted
                if (rowsAffected <= 0)
                {
                    // Display a message if the issue ID was not found
                    MessageBox.Show($"Issue ID {issueId} not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                // Refresh the DataGridView to reflect the changes
                form1.con.Close();
                print();
            }
            catch (Exception ex)
            {
                // Display any errors that occur during the deletion process
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                form1.con.Close();
            }
            
        }


        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            // This method is called when the text in the TextBox changes, but we don't need to handle it here.
        }

        private void label1_Click(object sender, EventArgs e)
        {
            // This method is called when the label is clicked, but we don't need to handle it here.
        }
    }

   
}
