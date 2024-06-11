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
    public partial class Form9 : Form
    {
        public Form1 form1;

        public void loadFormRef(Form1 f1)
        {
            form1 = f1;
        }
        public Form9()
        {
            InitializeComponent();
        }

        private void Form9_Load(object sender, EventArgs e)
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

        private void button9_Click(object sender, EventArgs e)
        {
            // Create an instance of AdminSignUp form
            Form13 vieappoint = new Form13();
            vieappoint.loadFormRef(form1);

            // Show the AdminSignUp form
            vieappoint.Show();

            // Optionally, hide the current form if needed
            this.Hide();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            // Create an instance of AdminSignUp form
            Form14 viewrevenuereport = new Form14();
            viewrevenuereport.loadFormRef(form1);

            // Show the AdminSignUp form
            viewrevenuereport.Show();

            // Optionally, hide the current form if needed
            this.Hide();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            // Create an instance of AdminSignUp form
            Form15 viewfeedback = new Form15();
            viewfeedback.loadFormRef(form1);

            // Show the AdminSignUp form
            viewfeedback.Show();

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

        private void button2_Click(object sender, EventArgs e)
        {
            Form28 employeepage = new Form28();
            employeepage.loadFormRef(form1);

            // Show the AdminSignUp form
            employeepage.Show();

            // Optionally, hide the current form if needed
            this.Hide();
        }
    }
}
