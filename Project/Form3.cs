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
    public partial class Form3 : Form
    {
        public Form1 form1;
        public Form3()
        {
            InitializeComponent();
            textBox4.PasswordChar = '*';
            textBox5.PasswordChar = '*';

        }
        public void loadFormRef(Form1 f1)
        {
            form1 = f1;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Create an instance of AdminSignUp form
            Form2 adminSignUpForm = new Form2();
            adminSignUpForm.loadFormRef(form1);
            // Show the AdminSignUp form
            adminSignUpForm.Show();

            // Optionally, hide the current form if needed
            this.Hide();
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            // Create an instance of AdminSignUp form
            Form2 adminSignUpForm = new Form2();
            adminSignUpForm.loadFormRef(form1);

            // Show the AdminSignUp form
            adminSignUpForm.Show();

            // Optionally, hide the current form if needed
            this.Hide();
        }

        private void Form3_Load(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Validation checks
            if (string.IsNullOrWhiteSpace(textBox1.Text) ||
                string.IsNullOrWhiteSpace(textBox2.Text) ||
                string.IsNullOrWhiteSpace(textBox3.Text) ||
                string.IsNullOrWhiteSpace(textBox4.Text) ||
                string.IsNullOrWhiteSpace(textBox5.Text))
            {
                MessageBox.Show("Please fill out all fields.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (textBox4.Text != textBox5.Text)
            {
                MessageBox.Show("Password and Confirm Password do not match.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }
            // Collect user input data
            string name = textBox1.Text;
            string email = textBox2.Text;
            string password = textBox4.Text;
            string phoneNumber = textBox3.Text;
            string idProof = textBox6.Text;
            // Generate a random user ID greater than 15
            Random random = new Random();
            int userId;
            do
            {
                userId = random.Next(16, 100);
            } while (IsUserIdExists(userId));

            // Construct SQL INSERT statement
            string sqlQuery = "INSERT INTO HOSPITAL.USER_T (USER_ID, NAME, EMAIL, PASSWORD, PHONE_NUMBER, ID_PROOF) " +
                              "VALUES (:userId, :name, :email, :password, :phoneNumber, :idProof)";

            // Construct SQL INSERT statement for ADMIN_T table
            string adminInsertQuery = "INSERT INTO HOSPITAL.ADMIN_T (ADMIN_ID, USER_ID) VALUES (:adminId, :userId)";

            try
            {
                // Open connection to the database
                form1.con.Open();

                // Create OracleCommand and set parameters
                OracleCommand cmd = new OracleCommand(sqlQuery, form1.con);
                cmd.Parameters.Add(":userId", OracleDbType.Int32).Value = userId;
                cmd.Parameters.Add(":name", OracleDbType.Varchar2).Value = name;
                cmd.Parameters.Add(":email", OracleDbType.Varchar2).Value = email;
                cmd.Parameters.Add(":password", OracleDbType.Varchar2).Value = password;
                cmd.Parameters.Add(":phoneNumber", OracleDbType.Varchar2).Value = phoneNumber;
                cmd.Parameters.Add(":idProof", OracleDbType.Varchar2).Value = idProof;

                // Execute INSERT command
                int rowsInserted = cmd.ExecuteNonQuery();

                // Insert user ID into ADMIN_T table
                OracleCommand adminCmd = new OracleCommand(adminInsertQuery, form1.con);
                adminCmd.Parameters.Add(":adminId", OracleDbType.Int32).Value = userId; // Assigning ADMIN_ID same as USER_ID
                adminCmd.Parameters.Add(":userId", OracleDbType.Int32).Value = userId;
                adminCmd.ExecuteNonQuery();

                // Check if insertion was successful
                if (rowsInserted > 0)
                {
                    MessageBox.Show($"User signed up successfully! Your user ID is: {userId}");

                    // Close the sign-up form
                    this.Close();

                    // Open the login form
                    Form6 loginPage = new Form6();
                    loginPage.loadFormRef(form1);

                    loginPage.Show();

                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Failed to sign up user. Please try again.");
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
        private bool IsUserIdExists(int userId)
        {
            // Check if the given user ID already exists in the table
            string sqlQuery = "SELECT COUNT(*) FROM HOSPITAL.USER_T WHERE USER_ID = :userId";

            try
            {
                // Open connection to the database
                form1.con.Open();

                OracleCommand cmd = new OracleCommand(sqlQuery, form1.con);
                cmd.Parameters.Add(":userId", OracleDbType.Int32).Value = userId;

                int count = Convert.ToInt32(cmd.ExecuteScalar());
                return count > 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
                return false;
            }
            finally
            {
                // Close connection to the database
                form1.con.Close();
            }
        }


        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
