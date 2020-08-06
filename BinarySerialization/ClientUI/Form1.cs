using CommonObjects;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;

namespace ClientUI
{
    public partial class Form1 : Form
    {
        List<Employee> employees;

        public Form1()
        {
            InitializeComponent();
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "employees.dat");

            using (var stream = File.OpenRead(path))
            {
                BinaryFormatter bf = new BinaryFormatter();
                employees = (List<Employee>)bf.Deserialize(stream);
            }

            dataGridView1.DataSource = employees;

            showMessage("Data loaded !");
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            using (var stream = File.OpenWrite(@"d:\employees.txt"))
            {
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(stream, employees);
            }

            showMessage("Data saved !");
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblMessage.Text = "-";
            timer1.Stop();
        }

        void showMessage(string message)
        {
            lblMessage.Text = message;
            timer1.Start();
        }
    }
}
