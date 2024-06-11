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
    public partial class Form11 : Form
    {
        public Form1 form1;

        public void loadFormRef(Form1 f1)
        {
            form1 = f1;
        }
        public Form11()
        {
            InitializeComponent();
        }

        private void Form11_Load(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            form1.Show();


            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form8 checkprofile = new Form8();
            checkprofile.loadFormRef(form1);

            // Show the AdminSignUp form
            checkprofile.Show();

            // Optionally, hide the current form if needed
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form19 patientrecord = new Form19();
            patientrecord.loadFormRef(form1);

            // Show the AdminSignUp form
            patientrecord.Show();

            // Optionally, hide the current form if needed
            this.Hide();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Form25 patientrecord = new Form25();
            patientrecord.loadFormRef(form1);

            // Show the AdminSignUp form
            patientrecord.Show();

            // Optionally, hide the current form if needed
            this.Hide();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Form26 medicalrecord = new Form26();
            medicalrecord.loadFormRef(form1);

            // Show the AdminSignUp form
            medicalrecord.Show();

            // Optionally, hide the current form if needed
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form27 manageappointments = new Form27();
            manageappointments.loadFormRef(form1);

            // Show the AdminSignUp form
            manageappointments.Show();

            // Optionally, hide the current form if needed
            this.Hide();
        }
    }
}
