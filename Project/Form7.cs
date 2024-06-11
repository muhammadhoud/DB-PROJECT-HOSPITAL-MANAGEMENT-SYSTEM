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
    public partial class Form7 : Form
    {
        public Form1 form1;

        public void loadFormRef(Form1 f1)
        {
            form1 = f1;

        }
        public Form7()
        {
            InitializeComponent();
        }

        private void Form7_Load(object sender, EventArgs e)
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
            Form20 bookappointment = new Form20();
            bookappointment.loadFormRef(form1);

            // Show the AdminSignUp form
            bookappointment.Show();

            // Optionally, hide the current form if needed
            this.Hide();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Form21 customersupport = new Form21();
            customersupport.loadFormRef(form1);

            // Show the AdminSignUp form
            customersupport.Show();

            // Optionally, hide the current form if needed
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form22 previousAppointment = new Form22();
            previousAppointment.loadFormRef(form1);

            // Show the AdminSignUp form
            previousAppointment.Show();

            // Optionally, hide the current form if needed
            this.Hide();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Form23 previousAppointment = new Form23();
            previousAppointment.loadFormRef(form1);

            // Show the AdminSignUp form
            previousAppointment.Show();

            // Optionally, hide the current form if needed
            this.Hide();
        }
    }
}
