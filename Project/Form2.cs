using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form2 : Form
    {
        public Form1 form1;
        public Form2()
        {
            InitializeComponent();
        }

        public void loadFormRef(Form1 f1)
        {
            form1 = f1;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
           
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            // Create an instance of AdminSignUp form
            Form3 adminSignUpForm = new Form3();
            adminSignUpForm.loadFormRef(form1);

            // Show the AdminSignUp form
            adminSignUpForm.Show();

            // Optionally, hide the current form if needed
            this.Hide();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            form1.Show();

            
            this.Close();

            //// Create an instance of AdminSignUp form
            //Form1 adminSignUpForm = new Form1();

            //// Show the AdminSignUp form
            //adminSignUpForm.Show();

            //// Optionally, hide the current form if needed
            //this.Hide();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            // Create an instance of AdminSignUp form
            Form4 patientsignup = new Form4();
            patientsignup.loadFormRef(form1);

            // Show the AdminSignUp form
            patientsignup.Show();

            // Optionally, hide the current form if needed
            this.Hide();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

            Form5 staffsignup = new Form5();
            staffsignup.loadFormRef(form1);

            // Show the AdminSignUp form
            staffsignup.Show();

            // Optionally, hide the current form if needed
            this.Hide();

        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }
    }
}
