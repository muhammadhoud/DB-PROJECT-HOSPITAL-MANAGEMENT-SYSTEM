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
    public partial class Form15 : Form
    {
        public Form1 form1;



        public void loadFormRef(Form1 f1)
        {
            form1 = f1;

            // Initialization for objects using form1
            print();
        }

        public Form15()
        {
            InitializeComponent();
        }

        private void Form15_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Get the user's role
            string userRole = GetUserRole(form1.fuser_id);

            // Navigate based on the user's role
            switch (userRole)
            {
                case "ADMIN":
                    // Navigate to Form9 for admins
                    Form9 adminPage = new Form9();
                    adminPage.loadFormRef(form1);
                    adminPage.Show();
                    break;
                case "Receptionist":
                case "Cashier":
                    // Navigate to Form10 for receptionists/cashiers
                    Form10 receptionistPage = new Form10();
                    receptionistPage.loadFormRef(form1);
                    receptionistPage.Show();
                    break;
                case "Doctor":
                case "Nurse":
                    // Navigate to Form11 for doctors/nurses
                    Form11 doctorPage = new Form11();
                    doctorPage.loadFormRef(form1);
                    doctorPage.Show();
                    break;
                case "PATIENT":
                    // Navigate to Form7 for patients
                    Form7 patientPage = new Form7();
                    patientPage.loadFormRef(form1);
                    patientPage.Show();
                    break;
                case "Customer Support":
                    // Navigate to Form12 for patient customer support
                    Form12 customerSupportPage = new Form12();
                    customerSupportPage.loadFormRef(form1);
                    customerSupportPage.Show();
                    break;
                default:
                    MessageBox.Show("Unknown role!");
                    break;
            }

            // Optionally, hide the current form
            this.Hide();
        }

        // Method to retrieve the user's role based on user ID
        private string GetUserRole(string userId)
        {
            try
            {
                // Open connection to the database
                form1.con.Open();

                // Check if the user is an admin
                string sqlAdminQuery = "SELECT 'ADMIN' FROM HOSPITAL.ADMIN_T WHERE USER_ID = :userId";
                OracleCommand adminCmd = new OracleCommand(sqlAdminQuery, form1.con);
                adminCmd.Parameters.Add(":userId", OracleDbType.Int32).Value = Convert.ToInt32(userId);
                object isAdmin = adminCmd.ExecuteScalar();

                if (isAdmin != null && isAdmin != DBNull.Value)
                {
                    return "ADMIN";
                }

                // Check if the user is a patient
                string sqlPatientQuery = "SELECT 'PATIENT' FROM HOSPITAL.PATIENT WHERE USER_ID = :userId";
                OracleCommand patientCmd = new OracleCommand(sqlPatientQuery, form1.con);
                patientCmd.Parameters.Add(":userId", OracleDbType.Int32).Value = Convert.ToInt32(userId);
                object isPatient = patientCmd.ExecuteScalar();

                if (isPatient != null && isPatient != DBNull.Value)
                {
                    return "PATIENT";
                }

                // Check if the user is a staff member
                string sqlStaffQuery = "SELECT ROLE FROM HOSPITAL.STAFF WHERE USER_ID = :userId";
                OracleCommand staffCmd = new OracleCommand(sqlStaffQuery, form1.con);
                staffCmd.Parameters.Add(":userId", OracleDbType.Int32).Value = Convert.ToInt32(userId);
                object result = staffCmd.ExecuteScalar();

                if (result != null && result != DBNull.Value)
                {
                    return result.ToString();
                }
                else
                {
                    return "UNKNOWN";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
                return "ERROR";
            }
            finally
            {
                // Close connection to the database
                form1.con.Close();
            }
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

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }
    }
}
