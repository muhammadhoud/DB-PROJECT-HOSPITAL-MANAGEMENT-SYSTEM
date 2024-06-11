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
    public partial class Form19 : Form
    {
        public Form1 form1;

        public void loadFormRef(Form1 f1)
        {
            form1 = f1;

            // Initialization for objects using form1
            print();
        }
        public Form19()
        {
            InitializeComponent();
        }

        void print()
        {
            dataGridView1.DataSource = null;
            try
            {
                // Open the database connection
                form1.con.Open();

                // Define the SQL query to retrieve patient record data
                string sqlQuery = "SELECT RECORD_ID, PATIENT_ID, MEDICAL_HISTORY, INSURANCE_DETAILS FROM HOSPITAL.PATIENT_RECORD";

                // Create a command object with the SQL query and connection
                OracleCommand cmd = new OracleCommand(sqlQuery, form1.con);

                // Execute the SQL query and retrieve the data
                OracleDataReader reader = cmd.ExecuteReader();

                // Create a DataTable to hold the retrieved data
                DataTable dt = new DataTable();

                // Add columns to the DataTable
                dt.Columns.Add("RECORD_ID");
                dt.Columns.Add("PATIENT_ID");
                dt.Columns.Add("MEDICAL_HISTORY");
                dt.Columns.Add("INSURANCE_DETAILS");

                // Load the data from the DataReader into the DataTable
                while (reader.Read())
                {
                    DataRow row = dt.NewRow();
                    row["RECORD_ID"] = reader["RECORD_ID"];
                    row["PATIENT_ID"] = reader["PATIENT_ID"];
                    row["MEDICAL_HISTORY"] = reader["MEDICAL_HISTORY"];
                    row["INSURANCE_DETAILS"] = reader["INSURANCE_DETAILS"];
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


        private void Form19_Load(object sender, EventArgs e)
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

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
