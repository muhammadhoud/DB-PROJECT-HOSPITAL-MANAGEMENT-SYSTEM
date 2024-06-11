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
    public partial class Form21 : Form
    {
        public Form1 form1;

        public void loadFormRef(Form1 f1)
        {
            form1 = f1;

            
        }
        public Form21()
        {
            InitializeComponent();
        }

        private void Form21_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form7 PATIENTpage = new Form7();
            PATIENTpage.loadFormRef(form1);

            // Show the AdminSignUp form
            PATIENTpage.Show();

            // Optionally, hide the current form if needed
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Get the details entered by the user
            string description = textBox4.Text;
            string resolution = textBox5.Text;

            try
            {
                // Open the database connection
                form1.con.Open();

                // Get the last issue ID from the table
                string lastIssueId = GetLastIssueId();

                // Generate a new issue ID by incrementing the last issue ID
                string issueId = GenerateNewIssueId(lastIssueId);

                // Retrieve the patient ID of the current user from the database
                int patientId = GetCurrentPatientId();

                // Define the SQL query to insert a new issue
                string sqlQuery = "INSERT INTO HOSPITAL.CUSTOMER_SUPPORT (ISSUE_ID, DESCRIPTION, RESOLUTION, TIMESTAMP, PATIENT_ID, STAFF_ID) " +
                                  "VALUES (:issueId, :description, :resolution, :timestamp, :patientId, :staffId)";

                // Create a command object with the SQL query and connection
                OracleCommand cmd = new OracleCommand(sqlQuery, form1.con);

                // Set parameters for the command
                cmd.Parameters.Add(":issueId", OracleDbType.Varchar2).Value = issueId;
                cmd.Parameters.Add(":description", OracleDbType.Varchar2).Value = description;
                cmd.Parameters.Add(":resolution", OracleDbType.Varchar2).Value = resolution;
                cmd.Parameters.Add(":timestamp", OracleDbType.TimeStamp).Value = DateTime.Now;
                cmd.Parameters.Add(":patientId", OracleDbType.Int32).Value = patientId;
                cmd.Parameters.Add(":staffId", OracleDbType.Int32).Value = 6;

                // Execute the SQL query to insert the new issue
                int rowsInserted = cmd.ExecuteNonQuery();

                // Display a message indicating the result of the issue creation
                if (rowsInserted > 0)
                {
                    MessageBox.Show("Issue submitted successfully!");
                }
                else
                {
                    MessageBox.Show("Failed to submit issue. Please try again.");
                }
            }
            catch (Exception ex)
            {
                // Display any errors that occur during the submission process
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                // Close the database connection
                if (form1.con.State == ConnectionState.Open)
                {
                    form1.con.Close();
                }
            }
        }

        // Method to get the last issue ID from the table
        private string GetLastIssueId()
        {
            // Define the SQL query to get the last issue ID from the table
            string sqlQuery = "SELECT MAX(ISSUE_ID) FROM HOSPITAL.CUSTOMER_SUPPORT";

            // Create a command object with the SQL query and connection
            OracleCommand cmd = new OracleCommand(sqlQuery, form1.con);

            // Execute the SQL query and retrieve the last issue ID
            object result = cmd.ExecuteScalar();

            // Convert the result to a string and return
            return result != null ? result.ToString() : null; // Return null if no last issue ID is found
        }

        private int GetCurrentPatientId()
        {
            // Define the SQL query to retrieve the patient ID of the current user
            string sqlQuery = "SELECT PATIENT_ID FROM HOSPITAL.PATIENT WHERE USER_ID = :userId";

            // Create a command object with the SQL query and connection
            OracleCommand cmd = new OracleCommand(sqlQuery, form1.con);

            // Set parameter for the command
            cmd.Parameters.Add(":userId", OracleDbType.Varchar2).Value = form1.fuser_id; // Assuming USER_ID is of type VARCHAR2

            // Execute the SQL query and retrieve the patient ID
            object result = cmd.ExecuteScalar();

            // Convert the result to an integer and return
            return result != null ? Convert.ToInt32(result) : -1; // Return -1 if patient ID is not found
        }

        // Method to generate a new issue ID by incrementing the last issue ID
        private string GenerateNewIssueId(string lastIssueId)
        {
            if (string.IsNullOrEmpty(lastIssueId))
            {
                // If there are no existing issue IDs, start with '1'
                return "1";
            }
            else
            {
                // Increment the last issue ID by one to generate a new issue ID
                int newIssueId = int.Parse(lastIssueId) + 1;
                return newIssueId.ToString();
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
    }
}
