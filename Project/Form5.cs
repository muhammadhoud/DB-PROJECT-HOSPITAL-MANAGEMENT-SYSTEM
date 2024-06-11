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
    public partial class Form5 : Form
    {

        public Form1 form1;

        public void loadFormRef(Form1 f1)
        {
            form1 = f1;
        }

        public Form5()
        {
            InitializeComponent();
            textBox5.PasswordChar = '*';
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Create an instance of AdminSignUp form
            Form2 signuppage = new Form2();
            signuppage.loadFormRef(form1);
            // Show the AdminSignUp form
            signuppage.Show();

            // Optionally, hide the current form if needed
            this.Hide();
        }

        private void Form5_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Validation checks
            if (string.IsNullOrWhiteSpace(textBox1.Text) ||
                string.IsNullOrWhiteSpace(textBox2.Text) ||
                string.IsNullOrWhiteSpace(textBox3.Text) ||
                string.IsNullOrWhiteSpace(textBox4.Text) ||
                string.IsNullOrWhiteSpace(textBox5.Text) ||
                string.IsNullOrWhiteSpace(textBox7.Text))
            {
                MessageBox.Show("Please fill out all fields.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (textBox5.Text != textBox7.Text)
            {
                MessageBox.Show("Password and Confirm Password do not match.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Collect user input data
            string name = textBox1.Text;
            string email = textBox2.Text;
            string phoneNumber = textBox3.Text;
            string role = comboBox1.SelectedItem.ToString();
            string salary = textBox4.Text;
            string password = textBox7.Text;
            string idProof = textBox8.Text;

            // Generate a random user ID greater than 15
            Random random = new Random();
            int userId;
            do
            {
                userId = random.Next(16, 100);
            } while (IsUserIdExists(userId));

            // Construct SQL INSERT statement for USER_T table
            string userInsertQuery = "INSERT INTO HOSPITAL.USER_T (USER_ID, NAME, EMAIL, PASSWORD, PHONE_NUMBER, ID_PROOF) " +
                                     "VALUES (:userId, :name, :email, :password, :phoneNumber, :idProof)";

            // Construct SQL INSERT statement for STAFF table
            string staffInsertQuery = "INSERT INTO HOSPITAL.STAFF (STAFF_ID, USER_ID, ROLE, SALARY) " +
                                      "VALUES (:staffId, :userId, :role, :salary)";

            try
            {
                // Open connection to the database
                form1.con.Open();

                // Insert user data into USER_T table
                OracleCommand userCmd = new OracleCommand(userInsertQuery, form1.con);
                userCmd.Parameters.Add(":userId", OracleDbType.Int32).Value = userId;
                userCmd.Parameters.Add(":name", OracleDbType.Varchar2).Value = name;
                userCmd.Parameters.Add(":email", OracleDbType.Varchar2).Value = email;
                userCmd.Parameters.Add(":password", OracleDbType.Varchar2).Value = password;
                userCmd.Parameters.Add(":phoneNumber", OracleDbType.Varchar2).Value = phoneNumber;
                userCmd.Parameters.Add(":idProof", OracleDbType.Varchar2).Value = idProof;
                userCmd.ExecuteNonQuery();

                // Insert user ID into STAFF table
                OracleCommand staffCmd = new OracleCommand(staffInsertQuery, form1.con);
                staffCmd.Parameters.Add(":staffId", OracleDbType.Int32).Value = userId; // Assigning STAFF_ID same as USER_ID
                staffCmd.Parameters.Add(":userId", OracleDbType.Int32).Value = userId;
                staffCmd.Parameters.Add(":role", OracleDbType.Varchar2).Value = role;
                staffCmd.Parameters.Add(":salary", OracleDbType.Varchar2).Value = salary;
                staffCmd.ExecuteNonQuery();

                // Show user ID in a message box
                MessageBox.Show($"User signed up successfully! Your user ID is: {userId}");

                // Close the sign-up form
                this.Close();

                // Open the login form
                Form6 loginPage = new Form6();
                loginPage.loadFormRef(form1);
                loginPage.Show();
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

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
