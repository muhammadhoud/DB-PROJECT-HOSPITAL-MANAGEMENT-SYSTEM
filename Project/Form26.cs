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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace WindowsFormsApp1
{
    public partial class Form26 : Form
    {
        public Form1 form1;

        public void loadFormRef(Form1 f1)
        {
            form1 = f1;

            ShowMedicalRecordData();
        }
        public Form26()
        {
            InitializeComponent();
           
        }

        private void Form26_Load(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        

        private void ShowMedicalRecordData()
        {
            try
            {
                // Open the database connection
                form1.con.Open();

                // Define the SQL query to retrieve medical record data
                string sqlQuery = "SELECT * FROM HOSPITAL.MEDICAL_RECORD";

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

       

        private void button2_Click(object sender, EventArgs e)
        {
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // Get the details entered by the user
            string recordId = textBox10.Text;
            string patientId = textBox9.Text;
            string doctorId = textBox8.Text;
            string diagnosis = textBox7.Text;
            string medication = textBox6.Text;

            try
            {
                // Open the database connection
                form1.con.Open();

                // Define the SQL query to update the medical record
                string sqlQuery = "UPDATE HOSPITAL.MEDICAL_RECORD " +
                                  "SET PATIENT_ID = :patientId, " +
                                  "DOCTOR_ID = :doctorId, " +
                                  "DIAGNOSIS = :diagnosis, " +
                                  "MEDICATION = :medication " +
                                  "WHERE RECORD_ID = :recordId";

                // Create a command object with the SQL query and connection
                OracleCommand cmd = new OracleCommand(sqlQuery, form1.con);

                // Set parameters for the command
                cmd.Parameters.Add(":patientId", OracleDbType.Int32).Value = int.Parse(patientId);
                cmd.Parameters.Add(":doctorId", OracleDbType.Int32).Value = int.Parse(doctorId);
                cmd.Parameters.Add(":diagnosis", OracleDbType.Varchar2).Value = diagnosis;
                cmd.Parameters.Add(":medication", OracleDbType.Varchar2).Value = medication;
                cmd.Parameters.Add(":recordId", OracleDbType.Varchar2).Value = recordId;

                // Execute the SQL query to update the medical record
                int rowsUpdated = cmd.ExecuteNonQuery();

                // Display a message indicating the result of the update operation
                if (rowsUpdated > 0)
                {
                    MessageBox.Show("Medical record updated successfully!");
                }
                else
                {
                    MessageBox.Show("Failed to update medical record.");
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

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {
            ShowMedicalRecordData();
        }

        private void tabPage2_Click(object sender, EventArgs e)
        {
            ShowMedicalRecordData();
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

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
