using Oracle.ManagedDataAccess.Client;
using System;
using System.Data;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form22 : Form
    {
        public Form1 form1;

        public void loadFormRef(Form1 f1)
        {
            form1 = f1;
            ShowPreviousAppointments();
        }

        public Form22()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Form22_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form11 doctorpage = new Form11();
            doctorpage.loadFormRef(form1);

            // Show the AdminSignUp form
            doctorpage.Show();

            // Optionally, hide the current form if needed
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Show previous appointments where status is 'done'
            ShowPreviousAppointments();
        }

        private void ShowPreviousAppointments()
        {
            try
            {
                // Open the database connection
                form1.con.Open();

                // Get the patient ID of the current user
                int patientId = GetCurrentPatientId();

                if (patientId != -1)
                {
                    // Define the SQL query to retrieve previous appointments of the logged-in patient
                    string sqlQuery = "SELECT APPOINTMENT_ID, \"Date\", \"Time\", STATUS, PATIENT_ID, DOCTOR_ID " +
                                      "FROM HOSPITAL.APPOINTMENT_T " +
                                      "WHERE PATIENT_ID = :patientId AND STATUS = 'Done'";

                    // Create a command object with the SQL query and connection
                    OracleCommand cmd = new OracleCommand(sqlQuery, form1.con);

                    // Set parameter for the command
                    cmd.Parameters.Add(":patientId", OracleDbType.Int32).Value = patientId;

                    // Create a DataTable to hold the retrieved data
                    DataTable dt = new DataTable();

                    // Load the data from the DataReader into the DataTable
                    using (OracleDataReader reader = cmd.ExecuteReader())
                    {
                        dt.Load(reader);
                    }

                    // Bind the DataTable to the DataGridView to display the data
                    dataGridView1.DataSource = dt;
                }
                else
                {
                    MessageBox.Show("Patient ID not found for the current user.");
                }
            }
            catch (Exception ex)
            {
                // Display any errors that occur during the retrieval process
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

        private int GetCurrentPatientId()
        {
            try
            {
                // Define the SQL query to retrieve the patient ID of the current user
                string sqlQuery = "SELECT PATIENT_ID FROM HOSPITAL.PATIENT WHERE USER_ID = :userId";

                // Create a command object with the SQL query and connection
                OracleCommand cmd = new OracleCommand(sqlQuery, form1.con);

                // Set parameter for the command
                cmd.Parameters.Add(":userId", OracleDbType.Varchar2).Value = form1.fuser_id;

                // Execute the SQL query and retrieve the patient ID
                object result = cmd.ExecuteScalar();

                // Convert the result to an integer and return
                return result != null ? Convert.ToInt32(result) : -1; // Return -1 if patient ID is not found
            }
            catch (Exception ex)
            {
                // Display any errors that occur during the retrieval process
                MessageBox.Show("Error getting patient ID: " + ex.Message);
                return -1;
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Form7 PATIENTpage = new Form7();
            PATIENTpage.loadFormRef(form1);

            // Show the AdminSignUp form
            PATIENTpage.Show();

            // Optionally, hide the current form if needed
            this.Hide();
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }
    }
}
