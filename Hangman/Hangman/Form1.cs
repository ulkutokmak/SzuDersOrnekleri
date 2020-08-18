using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hangman
{
    public partial class Form1 : Form
    {
        int imageIndex = 0;
        int wrongCount = 11;
        int rightCount = 0;
        int letterCount = 0;

        string mainWord = string.Empty;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            mainWord = "GERGEDAN";

            BuildWordLabels(mainWord, "Hayvan");
            BuildKeyboard();
        }

        private void Button_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            button.Enabled = false;

            string letter = button.Text;

            if (mainWord.Contains(letter))
            {
                foreach (Label label in flowLayoutPanel1.Controls)
                {
                    if (label.Tag.ToString() == letter)
                    {
                        label.Text = letter;
                        rightCount += 1;
                    }
                }

                if (rightCount == mainWord.Length)
                    Finish("Kazandın");
            }
            else
            {
                pictureBox1.Image = imageList1.Images[imageIndex++];

                if (imageIndex == wrongCount)
                {
                    Finish("Kaybettin");
                    OpenWord();
                    return;
                }
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            string title = "HANGMAN";

            Graphics g = e.Graphics;

            g.DrawString(
                title,
                new Font("verdana", FindFontSize(g, title, "verdana", panel1.Width)),
                new LinearGradientBrush(new PointF(0, 0), new PointF(panel1.Width, panel1.Height), Color.Red, Color.Orange),
                panel1.ClientRectangle);
        }

        void BuildWordLabels(string word, string info)
        {
            label1.Text = $"Bu bir {info}";
            letterCount = word.Length;

            int labelWidth = Math.Abs(flowLayoutPanel1.Width / letterCount);

            foreach (var w in word)
            {
                Label label = new Label();
                label.AutoSize = false;
                label.Size = new Size(labelWidth, 30);
                label.Text = "-";
                label.Tag = w;
                label.Margin = new Padding(0);

                flowLayoutPanel1.Controls.Add(label);
            }
        }

        void BuildKeyboard()
        {
            string keys = "QWERTYUIOPĞÜ-ASDFGHJKLŞİ-ZXCVBNMÖÇ";
            Control breaker = null;

            foreach (var key in keys)
            {
                if (key == '-')
                {
                    flowLayoutPanel2.SetFlowBreak(breaker, true);
                    continue;
                }
                else
                {
                    Button button = new Button();
                    button.TabStop = false;
                    button.Size = new Size(30, 30);
                    button.Text = key.ToString();
                    button.Click += Button_Click;

                    breaker = button;

                    flowLayoutPanel2.Controls.Add(button);
                }
            }
        }

        float FindFontSize(Graphics g, string text, string fontName, int width)
        {
            float fontSize = 0;
            float calculatedWidth;

            do
            {
                calculatedWidth = g.MeasureString(text, new Font(fontName, ++fontSize)).Width;
            } while (calculatedWidth < width);

            return fontSize - 1;
        }

        void OpenWord()
        {
            foreach (Label label in flowLayoutPanel1.Controls)
                label.Text = label.Tag.ToString();
        }

        void Finish(string message)
        {
            MessageBox.Show(message);
            flowLayoutPanel2.Enabled = false;
        }
    }
}
