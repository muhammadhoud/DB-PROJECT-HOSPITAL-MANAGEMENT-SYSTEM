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
    public partial class Form18 : Form
    {
        public Form1 form1;

        public void loadFormRef(Form1 f1)
        {
            form1 = f1;

            // Initialization for objects using form1
            print();
        }
        public Form18()
        {
            InitializeComponent();
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }


        void print()
        {
            dataGridView1.DataSource = null;
            try
            {
                // Open the database connection
                form1.con.Open();

                // Define the SQL query to retrieve data from USER_T and STAFF tables
                string sqlQuery = "SELECT " +
                                  "U.USER_ID, U.NAME, " +
                                  "S.STAFF_ID, S.ROLE, S.SALARY " +
                                  "FROM HOSPITAL.USER_T U " +
                                  "INNER JOIN HOSPITAL.STAFF S ON U.USER_ID = S.USER_ID";

                // Create a command object with the SQL query and connection
                OracleCommand cmd = new OracleCommand(sqlQuery, form1.con);

                // Execute the SQL query and retrieve the data
                OracleDataReader reader = cmd.ExecuteReader();

                // Create a DataTable to hold the retrieved data
                DataTable dt = new DataTable();

                // Add columns to the DataTable
                dt.Columns.Add("USER_ID");
                dt.Columns.Add("NAME");
                dt.Columns.Add("STAFF_ID");
                dt.Columns.Add("ROLE");
                dt.Columns.Add("SALARY");

                // Load the data from the DataReader into the DataTable
                while (reader.Read())
                {
                    DataRow row = dt.NewRow();
                    row["USER_ID"] = reader["USER_ID"];
                    row["NAME"] = reader["NAME"];
                    row["STAFF_ID"] = reader["STAFF_ID"];
                    row["ROLE"] = reader["ROLE"];
                    row["SALARY"] = reader["SALARY"];
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


        private void Form18_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form10 CASHIERpage = new Form10();
            CASHIERpage.loadFormRef(form1);

            // Show the AdminSignUp form
            CASHIERpage.Show();

            // Optionally, hide the current form if needed
            this.Hide();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
