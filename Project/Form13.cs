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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;

namespace WindowsFormsApp1
{
    public partial class Form13 : Form
    {
        public Form1 form1;
        
        

        public void loadFormRef(Form1 f1)
        {
            form1 = f1;

            // Initialization for objects using form1
            print();
        }
        public Form13()
        {
            InitializeComponent();

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form9 patientpage = new Form9();
            patientpage.loadFormRef(form1);

            // Show the AdminSignUp form
            patientpage.Show();

            // Optionally, hide the current form if needed
            this.Hide();
        }

        void print()
        {
            dataGridView1.DataSource = null;
            try
            {
                // Open the database connection
                form1.con.Open();

                // Define the SQL query to retrieve appointment data
                string sqlQuery = "SELECT * FROM HOSPITAL.APPOINTMENT_T";

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

        private void Form13_Load(object sender, EventArgs e)
        {

        }
    }
}
