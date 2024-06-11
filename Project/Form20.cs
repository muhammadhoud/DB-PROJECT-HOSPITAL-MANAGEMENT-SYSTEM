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
    public partial class Form20 : Form
    {
        public Form1 form1;


        public void loadFormRef(Form1 f1)
        {
            form1 = f1;

            // Initialization for objects using form1
            print();
        }

        public Form20()
        {
            InitializeComponent();
        }

        private void Form20_Load(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        void print()
        {
            dataGridView1.DataSource = null;
            try
            {
                // Open the database connection
                form1.con.Open();

                // Define the SQL query to retrieve appointment data
                string sqlQuery = "SELECT APPOINTMENT_ID, \"Date\", \"Time\", STATUS, PATIENT_ID, DOCTOR_ID FROM HOSPITAL.APPOINTMENT_T";

                // Create a command object with the SQL query and connection
                OracleCommand cmd = new OracleCommand(sqlQuery, form1.con);

                // Execute the SQL query and retrieve the data
                OracleDataReader reader = cmd.ExecuteReader();

                // Create a DataTable to hold the retrieved data
                DataTable dt = new DataTable();

                // Add columns to the DataTable
                dt.Columns.Add("APPOINTMENT_ID");
                dt.Columns.Add("Date");
                dt.Columns.Add("Time");
                dt.Columns.Add("STATUS");
                dt.Columns.Add("PATIENT_ID");
                dt.Columns.Add("DOCTOR_ID");

                // Load the data from the DataReader into the DataTable
                while (reader.Read())
                {
                    DataRow row = dt.NewRow();
                    row["APPOINTMENT_ID"] = reader["APPOINTMENT_ID"];
                    row["Date"] = reader["Date"];
                    row["Time"] = reader["Time"];
                    row["STATUS"] = reader["STATUS"];
                    row["PATIENT_ID"] = reader["PATIENT_ID"];
                    row["DOCTOR_ID"] = reader["DOCTOR_ID"];
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


        private void button1_Click(object sender, EventArgs e)
        {
            Form7 patientpage = new Form7();
            patientpage.loadFormRef(form1);

            // Show the AdminSignUp form
            patientpage.Show();

            // Optionally, hide the current form if needed
            this.Hide();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Get the selected date from the DateTimePicker
            DateTime selectedDate = dateTimePicker1.Value.Date;

            try
            {
                // Open the database connection
                form1.con.Open();

                // Get the last appointment ID from the table
                string lastAppointmentId = GetLastAppointmentId();

                // Generate a new appointment ID by incrementing the last appointment ID
                string appointmentId = GenerateNewAppointmentId(lastAppointmentId);

                // Retrieve a random doctor ID from the database
                int randomDoctorId = GetRandomDoctorId();

                // Retrieve the patient ID of the current user from the database
                int patientId = GetCurrentPatientId();

                // Define the SQL query to insert a new appointment
                string sqlQuery = "INSERT INTO HOSPITAL.APPOINTMENT_T (APPOINTMENT_ID, \"Date\", \"Time\", STATUS, PATIENT_ID, DOCTOR_ID) " +
                                  "VALUES (:appointmentId, :date1, :time1, :status1, :patientId, :doctorId)";

                // Create a command object with the SQL query and connection
                OracleCommand cmd = new OracleCommand(sqlQuery, form1.con);

                // Set parameters for the command
                cmd.Parameters.Add(":appointmentId", OracleDbType.Varchar2).Value = appointmentId;
                cmd.Parameters.Add(":date1", OracleDbType.Date).Value = selectedDate;
                cmd.Parameters.Add(":time1", OracleDbType.TimeStamp).Value = DateTime.Now; // You need to provide a valid time value
                cmd.Parameters.Add(":status1", OracleDbType.Varchar2).Value = "Scheduled"; // You need to specify the status
                cmd.Parameters.Add(":patientId", OracleDbType.Int32).Value = patientId;
                cmd.Parameters.Add(":doctorId", OracleDbType.Int32).Value = randomDoctorId;

                // Execute the SQL query to insert the new appointment
                int rowsInserted = cmd.ExecuteNonQuery();

                // Display a message indicating the result of the appointment booking
                if (rowsInserted > 0)
                {
                    MessageBox.Show("Appointment booked successfully!");
                }
                else
                {
                    MessageBox.Show("Failed to book appointment. Please try again.");
                }
            }
            catch (Exception ex)
            {
                // Display any errors that occur during the booking process
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
            print();
        }



        // Method to retrieve a random doctor ID from the database
        private int GetRandomDoctorId()
        {
            try
            {
                // Define the SQL query to retrieve a random doctor ID
                string sqlQuery = "SELECT STAFF_ID FROM HOSPITAL.STAFF WHERE ROLE = 'Doctor' ORDER BY DBMS_RANDOM.RANDOM";

                // Create a command object with the SQL query and connection
                OracleCommand cmd = new OracleCommand(sqlQuery, form1.con);

                // Execute the SQL query and retrieve the first row
                object result = cmd.ExecuteScalar();

                // Convert the result to an integer and return
                return result != null ? Convert.ToInt32(result) : -1; // Return -1 if no doctor ID is found
            }
            catch (Exception ex)
            {
                // Handle any errors that occur during the retrieval process
                MessageBox.Show("Error getting random doctor ID: " + ex.Message);
                return -1;
            }
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


        // Method to retrieve the patient ID of the current user from the database
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


        private void label1_Click(object sender, EventArgs e)
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
