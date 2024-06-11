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
    public partial class Form28 : Form
    {
        private Form1 form1;

        public void loadFormRef(Form1 f1)
        {
            form1 = f1;
            ShowStaffData();
        }

        public Form28()
        {
            InitializeComponent();
        }

        private void ShowStaffData()
        {
            try
            {
                form1.con.Open();

                string sqlQuery = "SELECT * FROM HOSPITAL.STAFF";

                OracleCommand cmd = new OracleCommand(sqlQuery, form1.con);
                DataTable dt = new DataTable();

                OracleDataAdapter da = new OracleDataAdapter(cmd);
                da.Fill(dt);

                form1.con.Close();

                dataGridView1.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
                form1.con.Close();
            }
        }

        private void Form28_Load(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            // Validation checks
            if (string.IsNullOrWhiteSpace(textBox11.Text) ||
                string.IsNullOrWhiteSpace(textBox10.Text) ||
                string.IsNullOrWhiteSpace(textBox9.Text) ||
                string.IsNullOrWhiteSpace(textBox6.Text) ||
                string.IsNullOrWhiteSpace(textBox8.Text) ||
                string.IsNullOrWhiteSpace(comboBox1.Text))
            {
                MessageBox.Show("Please fill out all fields.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Collect user input data
            string name = textBox11.Text;
            string email = textBox10.Text;
            string phoneNumber = textBox9.Text;
            string role = comboBox1.SelectedItem.ToString();
            string salary = textBox6.Text;
            string idProof = textBox8.Text;

            // Generate a random staff ID greater than the last staff ID
            string lastStaffIdQuery = "SELECT MAX(STAFF_ID) FROM HOSPITAL.STAFF";
            string lastUserIdQuery = "SELECT MAX(USER_ID) FROM HOSPITAL.USER_T";

            int staffId = GetNextId(lastStaffIdQuery);
            int userId = GetNextId(lastUserIdQuery);

            // Generate a random password for the user
            string password = GenerateRandomPassword();

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
                staffCmd.Parameters.Add(":staffId", OracleDbType.Int32).Value = staffId;
                staffCmd.Parameters.Add(":userId", OracleDbType.Int32).Value = userId;
                staffCmd.Parameters.Add(":role", OracleDbType.Varchar2).Value = role;
                staffCmd.Parameters.Add(":salary", OracleDbType.Varchar2).Value = salary;
                staffCmd.ExecuteNonQuery();

                // Show user ID and password in a message box
                MessageBox.Show($"Staff added successfully!\n\nUser ID: {userId}\nPassword: {password}", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Close the add staff form
                this.Close();

                // Optionally, navigate to another form or perform any other action
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                // Close connection to the database
                form1.con.Close();
            }



        }

        // Method to generate a random password for the user
        private string GenerateRandomPassword()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            Random random = new Random();
            return new string(Enumerable.Repeat(chars, 8).Select(s => s[random.Next(s.Length)]).ToArray());
        }

        // Method to get the next available ID by querying the database
        private int GetNextId(string query)
        {
            form1.con.Open();
            OracleCommand cmd = new OracleCommand(query, form1.con);
            object result = cmd.ExecuteScalar();
            form1.con.Close();
            return result != null && result != DBNull.Value ? Convert.ToInt32(result) + 1 : 1;
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form9 adminpage = new Form9();
            adminpage.loadFormRef(form1);

            // Show the AdminSignUp form
            adminpage.Show();

            // Optionally, hide the current form if needed
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                // Open the database connection
                form1.con.Open();

                // Start a transaction
                OracleTransaction transaction = form1.con.BeginTransaction();

                try
                {
                    // Retrieve the staff ID from textBox4
                    int staffId;
                    if (!int.TryParse(textBox4.Text, out staffId))
                    {
                        MessageBox.Show("Please enter a valid staff ID.");
                        return;
                    }

                    // Define the SQL query to retrieve the user ID associated with the staff ID
                    string userIdQuery = "SELECT USER_ID FROM HOSPITAL.STAFF WHERE STAFF_ID = :staffId";

                    // Create a command object for retrieving the user ID
                    OracleCommand userIdCmd = new OracleCommand(userIdQuery, form1.con);
                    userIdCmd.Parameters.Add(":staffId", OracleDbType.Int32).Value = staffId;

                    // Execute the query to retrieve the user ID
                    object userIdObj = userIdCmd.ExecuteScalar();

                    // Check if a user ID was found
                    if (userIdObj == null || userIdObj == DBNull.Value)
                    {
                        MessageBox.Show("User ID not found for the given staff ID.");
                        return;
                    }

                    int userId = Convert.ToInt32(userIdObj);

                    // Define the SQL queries to delete the staff and user records
                    string staffDeleteQuery = "DELETE FROM HOSPITAL.STAFF WHERE STAFF_ID = :staffId";
                    string userDeleteQuery = "DELETE FROM HOSPITAL.USER_T WHERE USER_ID = :userId";

                    // Create command objects for deleting staff and user records
                    OracleCommand staffCmd = new OracleCommand(staffDeleteQuery, form1.con);
                    staffCmd.Parameters.Add(":staffId", OracleDbType.Int32).Value = staffId;

                    OracleCommand userCmd = new OracleCommand(userDeleteQuery, form1.con);
                    userCmd.Parameters.Add(":userId", OracleDbType.Int32).Value = userId;

                    // Execute the DELETE queries
                    int staffRowsDeleted = staffCmd.ExecuteNonQuery();
                    int userRowsDeleted = userCmd.ExecuteNonQuery();

                    // Commit the transaction if both deletions are successful
                    if (staffRowsDeleted > 0 && userRowsDeleted > 0)
                    {
                        transaction.Commit();
                        MessageBox.Show("Staff and user records deleted successfully!");
                    }
                    else
                    {
                        // Rollback the transaction if any deletion fails
                        transaction.Rollback();
                        MessageBox.Show("Failed to delete staff and user records.");
                    }
                }
                catch (Exception ex)
                {
                    // Rollback the transaction in case of an error
                    transaction.Rollback();
                    MessageBox.Show("Error: " + ex.Message);
                }
                finally
                {
                    // Close the database connection
                    form1.con.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }


        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                // Open the database connection
                form1.con.Open();

                // Start a transaction
                OracleTransaction transaction = form1.con.BeginTransaction();

                try
                {
                    // Retrieve the staff ID from textBox1
                    int staffId;
                    if (!int.TryParse(textBox12.Text, out staffId))
                    {
                        MessageBox.Show("Please enter a valid staff ID.");
                        return;
                    }

                    // Retrieve the role entered by the user from comboBox1
                    string role = comboBox2.Text;

                    // Retrieve the salary entered by the user from textBox3
                    string salary = textBox2.Text;

                    // Retrieve other user information
                    string name = textBox7.Text;
                    string email = textBox5.Text;
                    string phoneNumber = textBox3.Text;
                    string idProof = textBox1.Text;

                    // Define the SQL query to update the staff record
                    string staffUpdateQuery = "UPDATE HOSPITAL.STAFF " +
                                              "SET ROLE = :role, SALARY = :salary " +
                                              "WHERE STAFF_ID = :staffId";

                    // Define the SQL query to update the user record
                    string userUpdateQuery = "UPDATE HOSPITAL.USER_T " +
                                             "SET NAME = :name, EMAIL = :email, PHONE_NUMBER = :phoneNumber, ID_PROOF = :idProof " +
                                             "WHERE USER_ID = (SELECT USER_ID FROM HOSPITAL.STAFF WHERE STAFF_ID = :staffId)";

                    // Create command objects with the SQL queries and connection
                    OracleCommand staffCmd = new OracleCommand(staffUpdateQuery, form1.con);
                    OracleCommand userCmd = new OracleCommand(userUpdateQuery, form1.con);

                    // Set parameters for the staff command
                    staffCmd.Parameters.Add(":role", OracleDbType.Varchar2).Value = role;
                    staffCmd.Parameters.Add(":salary", OracleDbType.Varchar2).Value = salary;
                    staffCmd.Parameters.Add(":staffId", OracleDbType.Int32).Value = staffId;

                    // Set parameters for the user command
                    userCmd.Parameters.Add(":name", OracleDbType.Varchar2).Value = name;
                    userCmd.Parameters.Add(":email", OracleDbType.Varchar2).Value = email;
                    userCmd.Parameters.Add(":phoneNumber", OracleDbType.Varchar2).Value = phoneNumber;
                    userCmd.Parameters.Add(":idProof", OracleDbType.Varchar2).Value = idProof;
                    userCmd.Parameters.Add(":staffId", OracleDbType.Int32).Value = staffId;

                    // Execute the staff and user update queries
                    int staffRowsUpdated = staffCmd.ExecuteNonQuery();
                    int userRowsUpdated = userCmd.ExecuteNonQuery();

                    // Commit the transaction if both updates are successful
                    if (staffRowsUpdated > 0 && userRowsUpdated > 0)
                    {
                        transaction.Commit();
                        MessageBox.Show("Staff and user information updated successfully!");
                    }
                    else
                    {
                        // Rollback the transaction if any update fails
                        transaction.Rollback();
                        MessageBox.Show("Failed to update staff and user information.");
                    }
                }
                catch (Exception ex)
                {
                    // Rollback the transaction in case of an error
                    transaction.Rollback();
                    MessageBox.Show("Error: " + ex.Message);
                }
                finally
                {
                    // Close the database connection
                    form1.con.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
    }
}
