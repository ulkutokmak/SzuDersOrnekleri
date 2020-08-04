using DenyoSDK;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace DenyoUI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            var files = new DirectoryInfo("Plugs").GetFiles("*.dll");
            foreach (var file in files)
            {
                var types = Assembly.LoadFrom(file.FullName).GetTypes();
                var externalTypes = types.Where(
                    p => typeof(IStringProcess).IsAssignableFrom(p) && !p.IsInterface);
                foreach (var type in externalTypes)
                {

                    IStringProcess process = (IStringProcess)Activator.CreateInstance(type);

                    Button b = new Button();
                    b.Text = process.GetName();
                    b.Click += B_Click;
                    b.Tag = process;
                    flowLayoutPanel1.Controls.Add(b);
                }
            }

        }

        private void B_Click(object sender, EventArgs e)
        {
            IStringProcess process = (IStringProcess)(sender as Control).Tag;
            txtOutput.Text = process.Process(txtInput.Text);

        }
    }
}
