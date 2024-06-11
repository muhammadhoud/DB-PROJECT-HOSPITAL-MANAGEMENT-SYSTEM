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
    public partial class Form6 : Form
    {
        Form1 form1;
        public Form6()
        {
            InitializeComponent();
        }
        public void loadFormRef(Form1 f1)
        {
            form1 = f1;
        }
        public void checkUserType(string user_id)
        {
            // Open connection to the database
            form1.con.Open();

            // Check if the user is an Admin
            string sqlQuery = "SELECT ADMIN_ID FROM HOSPITAL.ADMIN_T WHERE USER_ID = " + user_id;
            OracleCommand cmd = new OracleCommand(sqlQuery, form1.con);
            OracleDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                MessageBox.Show("User is an Admin!");
                // Show AdminSignUp form
                Form9 adminpage = new Form9();
                adminpage.loadFormRef(form1);
                adminpage.Show();
                this.Hide();

                // Close connection
                form1.con.Close();
                return;
            }

            // Check if the user is a Patient
            sqlQuery = "SELECT PATIENT_ID FROM HOSPITAL.PATIENT WHERE USER_ID = " + user_id;
            cmd.CommandText = sqlQuery;
            reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                MessageBox.Show("User is a Patient!");
                // Show Patient form
                Form7 patientpage = new Form7();
                patientpage.loadFormRef(form1);
                patientpage.Show();
                this.Hide();

                // Close connection
                form1.con.Close();
                return;
            }

            // Check if the user is a Staff member
            sqlQuery = "SELECT ROLE FROM HOSPITAL.STAFF WHERE USER_ID = " + user_id;
            cmd.CommandText = sqlQuery;
            reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                string role = reader.GetString(reader.GetOrdinal("ROLE"));

                // Navigate based on role
                switch (role)
                {
                    case "Doctor":
                    case "Nurse":
                        MessageBox.Show("User is a Doctor/Nurse!");
                        Form11 doctorNursePage = new Form11();
                        doctorNursePage.loadFormRef(form1);
                        doctorNursePage.Show();
                        this.Hide();
                        break;

                    case "Customer Support":
                        MessageBox.Show("User is a Customer Support!");
                        Form12 customerSupportPage = new Form12();
                        customerSupportPage.loadFormRef(form1);
                        customerSupportPage.Show();
                        this.Hide();
                        break;

                    case "Receptionist":
                        MessageBox.Show("User is a Receptionist!");
                        Form10 receptionistPage = new Form10();
                        receptionistPage.loadFormRef(form1);
                        receptionistPage.Show();
                        this.Hide();
                        break;

                    default:
                        MessageBox.Show("Unknown role!");
                        break;
                }
            }
            else
            {
                MessageBox.Show("User not found!");
            }

            // Close connection
            form1.con.Close();
        }
        private void Form6_Load(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            bool match = false;
            string sqlQuery = "SELECT User_ID, PASSWORD FROM HOSPITAL.USER_T";
            string user_id = "", password = "";
            form1.con.Open();
            OracleCommand cmd = new OracleCommand(sqlQuery, form1.con);
            OracleDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                user_id = reader.GetInt32(reader.GetOrdinal("USER_ID")).ToString();  //Select by a column name
                password = reader.GetString(reader.GetOrdinal("PASSWORD"));  //Select by a column name

                if(user_id == textBox1.Text && password == textBox2.Text)
                {
                    MessageBox.Show("Logged in!");
                    match = true;
                    form1.fuser_id = user_id;
                    break;
                }
            }

                form1.con.Close();
            if (match)
            {
                checkUserType(user_id);
            }
            else
            {
                MessageBox.Show("Wrong Username or Password!!");
                textBox2.Text = ""; 
            }

            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            form1.Show();


            this.Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
