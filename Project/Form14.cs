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
    public partial class Form14 : Form
    {
        public Form1 form1;



        public void loadFormRef(Form1 f1)
        {
            form1 = f1;

            // Initialization for objects using form1
            print();
        }
        public Form14()
        {
            InitializeComponent();
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
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


    }
}
