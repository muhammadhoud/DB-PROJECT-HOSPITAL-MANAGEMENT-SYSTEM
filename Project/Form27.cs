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
    public partial class Form27 : Form
    {
        public Form1 form1;

        public void loadFormRef(Form1 f1)
        {
            form1 = f1;

            ShowAppointmentData();

        }

        public Form27()
        {
            InitializeComponent();

        }

        private void Form27_Load(object sender, EventArgs e)
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

        private void ShowAppointmentData()
        {
            try
            {
                // Open the database connection
                form1.con.Open();

                // Define the SQL query to retrieve medical record data
                string sqlQuery = "SELECT * FROM HOSPITAL.APPOINTMENT_T";

                // Create a command object with the SQL query and connection
                OracleCommand cmd = new OracleCommand(sqlQuery, form1.con);

                // Create a DataTable to hold the retrieved data
                DataTable dt = new DataTable();

                // Load the data into the DataTable
                OracleDataAdapter da = new OracleDataAdapter(cmd);
                da.Fill(dt);

                // Close the database connection
                form1.con.Close();

                // Bind the DataTable to the DataGridView to display the data
                dataGridView1.DataSource = dt;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
                form1.con.Close();
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

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

        // Method to get the last appointment ID from the table
        private string GetLastAppointmentId()
        {
            // Define the SQL query to get the last appointment ID from the table
            string sqlQuery = "SELECT MAX(APPOINTMENT_ID) FROM HOSPITAL.APPOINTMENT_T";

            // Create a command object with the SQL query and connection
            OracleCommand cmd = new OracleCommand(sqlQuery, form1.con);

            // Execute the SQL query and retrieve the last appointment ID
            object result = cmd.ExecuteScalar();

            // Convert the result to a string and return
            return result != null ? result.ToString() : null; // Return null if no last appointment ID is found
        }

        // Method to generate a new appointment ID by incrementing the last appointment ID
        private string GenerateNewAppointmentId(string lastAppointmentId)
        {
            if (string.IsNullOrEmpty(lastAppointmentId))
            {
                // If there are no existing appointment IDs, start with '1'
                return "1";
            }
            else
            {
                // Increment the last appointment ID by one to generate a new appointment ID
                int newAppointmentId = int.Parse(lastAppointmentId) + 1;
                return newAppointmentId.ToString();
            }
        }


        private void tabPage1_Click(object sender, EventArgs e)
        {
            ShowAppointmentData();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                // Open the database connection
                form1.con.Open();

                // Get the last appointment ID from the table
                string lastAppointmentId = GetLastAppointmentId();

                // Generate a new appointment ID by incrementing the last appointment ID
                string appointmentId = GenerateNewAppointmentId(lastAppointmentId);

                // Retrieve the patient ID entered by the user from textBox8
                int patientId;
                if (!int.TryParse(textBox8.Text, out patientId))
                {
                    MessageBox.Show("Please enter a valid patient ID.");
                    return;
                }

                // Retrieve the doctor ID of the current user from the form1.fuser_id
                int doctorId = GetCurrentDoctorId();

                // Get the appointment date from dateTimePicker1
                DateTime appointmentDate = dateTimePicker1.Value.Date;

                string status = textBox9.Text;

                string sqlQuery = "INSERT INTO HOSPITAL.APPOINTMENT_T (APPOINTMENT_ID, \"Date\", \"Time\", STATUS, PATIENT_ID, DOCTOR_ID) " +
                  "VALUES (:appointmentId, :date1, :time1, :status, :patientId, :doctorId)";

                // Create a command object with the SQL query and connection
                OracleCommand cmd = new OracleCommand(sqlQuery, form1.con);

                // Set parameters for the command
                cmd.Parameters.Add(":appointmentId", OracleDbType.Int32).Value = appointmentId;
                cmd.Parameters.Add(":date1", OracleDbType.Date).Value = appointmentDate;
                cmd.Parameters.Add(":time1", OracleDbType.TimeStamp).Value = DateTime.Now; // Use the correct appointment time
                cmd.Parameters.Add(":status", OracleDbType.Varchar2).Value = status;
                cmd.Parameters.Add(":patientId", OracleDbType.Int32).Value = patientId;
                cmd.Parameters.Add(":doctorId", OracleDbType.Int32).Value = doctorId;


                // Execute the SQL query to insert the new appointment
                int rowsInserted = cmd.ExecuteNonQuery();

                // Display a message indicating the result of the appointment creation
                if (rowsInserted > 0)
                {
                    MessageBox.Show("Appointment added successfully!");
                }
                else
                {
                    MessageBox.Show("Failed to add appointment.");
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

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                // Open the database connection
                form1.con.Open();

                // Retrieve the appointment ID from textBox1
                int appointmentId;
                if (!int.TryParse(textBox1.Text, out appointmentId))
                {
                    MessageBox.Show("Please enter a valid appointment ID.");
                    return;
                }

                // Retrieve the patient ID entered by the user from textBox2
                int patientId;
                if (!int.TryParse(textBox2.Text, out patientId))
                {
                    MessageBox.Show("Please enter a valid patient ID.");
                    return;
                }

                // Retrieve the status entered by the user from textBox3
                string status = textBox3.Text;

                // Get the appointment date from dateTimePicker2
                DateTime appointmentDate = dateTimePicker2.Value.Date;

                // Define the SQL query to update the appointment record
                string sqlQuery = "UPDATE HOSPITAL.APPOINTMENT_T " +
                                  "SET \"Date\" = :date1, STATUS = :status, PATIENT_ID = :patientId " +
                                  "WHERE APPOINTMENT_ID = :appointmentId";

                // Create a command object with the SQL query and connection
                OracleCommand cmd = new OracleCommand(sqlQuery, form1.con);

                // Set parameters for the command
                cmd.Parameters.Add(":date1", OracleDbType.Date).Value = appointmentDate;
                cmd.Parameters.Add(":status", OracleDbType.Varchar2).Value = status;
                cmd.Parameters.Add(":patientId", OracleDbType.Int32).Value = patientId;
                cmd.Parameters.Add(":appointmentId", OracleDbType.Int32).Value = appointmentId;

                // Execute the SQL query to update the appointment record
                int rowsUpdated = cmd.ExecuteNonQuery();

                // Display a message indicating the result of the update operation
                if (rowsUpdated > 0)
                {
                    MessageBox.Show("Appointment updated successfully!");
                }
                else
                {
                    MessageBox.Show("Failed to update appointment.");
                }
            }
            catch (Exception ex)
            {
                // Display any errors that occur during the update process
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

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                // Open the database connection
                form1.con.Open();

                // Retrieve the appointment ID from textBox4
                int appointmentId;
                if (!int.TryParse(textBox4.Text, out appointmentId))
                {
                    MessageBox.Show("Please enter a valid appointment ID.");
                    return;
                }

                // Define the SQL query to delete the appointment record
                string sqlQuery = "DELETE FROM HOSPITAL.APPOINTMENT_T WHERE APPOINTMENT_ID = :appointmentId";

                // Create a command object with the SQL query and connection
                OracleCommand cmd = new OracleCommand(sqlQuery, form1.con);

                // Set parameters for the command
                cmd.Parameters.Add(":appointmentId", OracleDbType.Int32).Value = appointmentId;

                // Execute the SQL query to delete the appointment record
                int rowsDeleted = cmd.ExecuteNonQuery();

                // Display a message indicating the result of the deletion operation
                if (rowsDeleted > 0)
                {
                    MessageBox.Show("Appointment deleted successfully!");
                }
                else
                {
                    MessageBox.Show("Failed to delete appointment. Appointment ID not found.");
                }
            }
            catch (Exception ex)
            {
                // Display any errors that occur during the deletion process
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

        private void tabPage3_Click(object sender, EventArgs e)
        {

        }
    }
}
