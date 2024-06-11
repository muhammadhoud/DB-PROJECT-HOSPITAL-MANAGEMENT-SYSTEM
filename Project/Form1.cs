using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Oracle.ManagedDataAccess;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public string fuser_id;

        public OracleConnection con;
        public Form1()
        {
            InitializeComponent();

            string conStr = "DATA SOURCE = localhost:1521/xe;USER ID=houd;PASSWORD=1234;";
            con = new OracleConnection(conStr);
        }



        // Inside the event handler for the "Login" button click event
        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void Login_Click(object sender, EventArgs e)
        {
            Form3 secondPageForm = new Form3();

            // Show the second page form
            secondPageForm.Show();

            // Optionally, you can hide the first page form if needed
            this.Hide();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Create an instance of the second page form
            Form2 secondPageForm = new Form2();
            secondPageForm.loadFormRef(this);
            // Show the second page form
            secondPageForm.Show();

            // Optionally, you can hide the first page form if needed
            this.Hide();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }

        private void linkLabel1_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            Form6 loginPage = new Form6();
            loginPage.loadFormRef(this);

            loginPage.Show();

            this.Hide();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }
    }
}
