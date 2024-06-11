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
    public partial class Form10 : Form
    {
        public Form1 form1;

        public void loadFormRef(Form1 f1)
        {
            form1 = f1;
        }
        public Form10()
        {
            InitializeComponent();
        }

        private void Form10_Load(object sender, EventArgs e)
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
            Form18 viewsalary = new Form18();
            viewsalary.loadFormRef(form1);

            // Show the AdminSignUp form
            viewsalary.Show();

            // Optionally, hide the current form if needed
            this.Hide();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Form16 customersupport = new Form16();
            customersupport.loadFormRef(form1);

            // Show the AdminSignUp form
            customersupport.Show();

            // Optionally, hide the current form if needed
            this.Hide();
        }
    }
}
