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
    
    public partial class Form8 : Form
    {
        public Form1 form1;

        public void loadFormRef(Form1 f1)
        {
            form1 = f1;
        }
        public Form8()
        {
            InitializeComponent();
        }

        

        private void Form8_Load(object sender, EventArgs e)
        {
            // Load user data when the form is loaded
            LoadUserData(form1.fuser_id); // Replace "123" with the actual user ID
        }

        public void LoadUserData(string userId)
        {
            try
            {
                // Open connection to the database
                form1.con.Open();

                // Construct SQL query to retrieve user data
                string sqlQuery = "SELECT NAME, EMAIL, PHONE_NUMBER, ID_PROOF FROM HOSPITAL.USER_T WHERE USER_ID = :userId";

                // Create OracleCommand and set parameters
                OracleCommand cmd = new OracleCommand(sqlQuery, form1.con);
                cmd.Parameters.Add(":userId", OracleDbType.Int32).Value = Convert.ToInt32(userId);

                // Execute SQL query
                OracleDataReader reader = cmd.ExecuteReader();

                // Check if data was retrieved
                if (reader.Read())
                {
                    // Populate textboxes with user data
                    textBox1.Text = reader["NAME"].ToString();
                    textBox2.Text = reader["EMAIL"].ToString();
                    textBox3.Text = reader["PHONE_NUMBER"].ToString();
                    textBox4.Text = reader["ID_PROOF"].ToString();
                }
                else
                {
                    MessageBox.Show("User not found!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
            finally
            {
                // Close connection to the database
                form1.con.Close();
            }
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

    }
}
