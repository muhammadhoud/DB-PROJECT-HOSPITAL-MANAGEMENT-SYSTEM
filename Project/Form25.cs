using Oracle.ManagedDataAccess.Client;
using System;
using System.Data;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form25 : Form
    {
        public Form1 form1;


        public Form25()
        {
            InitializeComponent();
        }

        public void loadFormRef(Form1 f1)
        {
            form1 = f1;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form7 patientPage = new Form7();
            patientPage.loadFormRef(form1);

            // Show the patient page form
            patientPage.Show();

            // Optionally, hide the current form if needed
            this.Hide();
        }

        

        // Method to get the last prescription ID from the table
        private string GetLastPrescriptionId()
        {
            // Define the SQL query to get the last prescription ID from the table
            string sqlQuery = "SELECT MAX(PRESCRIPTION_ID) FROM HOSPITAL.PRESCRIPTION";

            // Create a command object with the SQL query and connection
            OracleCommand cmd = new OracleCommand(sqlQuery, form1.con);

            // Execute the SQL query and retrieve the last prescription ID
            object result = cmd.ExecuteScalar();

            // Convert the result to a string and return
            return result != null ? result.ToString() : null; // Return null if no last prescription ID is found
        }


        private int GetCurrentDoctorId()
        {
            // Define the SQL query to retrieve the doctor ID of the current user
            string sqlQuery = "SELECT STAFF_ID FROM HOSPITAL.STAFF WHERE USER_ID = :userId";

            // Create a command object with the SQL query and connection
            OracleCommand cmd = new OracleCommand(sqlQuery, form1.con);

            // Set parameter for the command
            cmd.Parameters.Add(":userId", OracleDbType.Varchar2).Value = form1.fuser_id; // Assuming USER_ID is of type VARCHAR2

            // Execute the SQL query and retrieve the doctor ID
            object result = cmd.ExecuteScalar();

            // Convert the result to an integer and return
            return result != null ? Convert.ToInt32(result) : -1; // Return -1 if doctor ID is not found
        }

        // Method to generate a new prescription ID by incrementing the last prescription ID
        private string GenerateNewPrescriptionId(string lastPrescriptionId)
        {
            if (string.IsNullOrEmpty(lastPrescriptionId))
            {
                // If there are no existing prescription IDs, start with '1'
                return "1";
            }
            else
            {
                // Increment the last prescription ID by one to generate a new prescription ID
                int newPrescriptionId = int.Parse(lastPrescriptionId) + 1;
                return newPrescriptionId.ToString();
            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            // Get the details entered by the user
            string medication = textBox1.Text;
            string dosage = textBox5.Text;

            try
            {
                // Open the database connection
                form1.con.Open();

                // Get the last prescription ID from the table
                string lastPrescriptionId = GetLastPrescriptionId();

                // Generate a new prescription ID by incrementing the last prescription ID
                string prescriptionId = GenerateNewPrescriptionId(lastPrescriptionId);

                // Retrieve the patient ID entered by the user from textBox4
                int patientId;
                if (!int.TryParse(textBox4.Text, out patientId))
                {
                    MessageBox.Show("Please enter a valid patient ID.");
                    return;
                }

                // Retrieve the doctor ID of the current user from the database
                int doctorId = GetCurrentDoctorId();

                // Define the SQL query to insert a new prescription
                string sqlQuery = "INSERT INTO HOSPITAL.PRESCRIPTION (PRESCRIPTION_ID, PATIENT_ID, DOCTOR_ID, MEDICATION, DOSAGE) " +
                                  "VALUES (:prescriptionId, :patientId, :doctorId, :medication, :dosage)";

                // Create a command object with the SQL query and connection
                OracleCommand cmd = new OracleCommand(sqlQuery, form1.con);

                // Set parameters for the command
                cmd.Parameters.Add(":prescriptionId", OracleDbType.Varchar2).Value = prescriptionId;
                cmd.Parameters.Add(":patientId", OracleDbType.Int32).Value = patientId;
                cmd.Parameters.Add(":doctorId", OracleDbType.Int32).Value = doctorId;
                cmd.Parameters.Add(":medication", OracleDbType.Varchar2).Value = medication;
                cmd.Parameters.Add(":dosage", OracleDbType.Varchar2).Value = dosage;

                // Execute the SQL query to insert the new prescription
                int rowsInserted = cmd.ExecuteNonQuery();

                // Display a message indicating the result of the prescription creation
                if (rowsInserted > 0)
                {
                    MessageBox.Show("Prescription added successfully!");
                }
                else
                {
                    MessageBox.Show("Failed to add prescription.");
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

        private void Form25_Load(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Form11 doctorpage = new Form11();
            doctorpage.loadFormRef(form1);

            // Show the AdminSignUp form
            doctorpage.Show();

            // Optionally, hide the current form if needed
            this.Hide();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
