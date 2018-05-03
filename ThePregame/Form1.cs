using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ThePregame
{
    public partial class Form1 : Form
    {

        public Form1()
        {


            InitializeComponent();
            string[] characters = System.IO.Directory.GetDirectories(Environment.CurrentDirectory + @"/../../data/images", "*", SearchOption.AllDirectories).Select(directory => Path.GetFileName(directory)).ToArray();
            char11.Items.AddRange(characters);
            char12.Items.AddRange(characters);
            char13.Items.AddRange(characters);
            char21.Items.AddRange(characters);
            char22.Items.AddRange(characters);
            char23.Items.AddRange(characters);
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {

        }

        private void scenestyle_SelectedIndexChanged(object sender, EventArgs e)
        {
            Debug.Print("Selected: " + scenestyle.Text);
            char12.Enabled = false;
            char13.Enabled = false;
            char21.Enabled = false;
            char22.Enabled = false;
            char23.Enabled = false;
            switch(scenestyle.Text)
            {
                case "solo":

                        break;
                case "duo":
                    char12.Enabled = true;
                    break;
                case "trio":
                    char12.Enabled = true;
                    char13.Enabled = true;
                    break;
                case "quartet":
                    char12.Enabled = true;
                    char13.Enabled = true;
                    char21.Enabled = true;
                    break;
                case "1v2":
                    char21.Enabled = true;
                    char22.Enabled = true;
                    break;
                case "2v1":
                    char12.Enabled = true;
                    char21.Enabled = true;
                    break;
                case "2v2":
                    char12.Enabled = true;
                    char21.Enabled = true;
                    char22.Enabled = true;
                    break;
                case "1v3":
                    char21.Enabled = true;
                    char22.Enabled = true;
                    char23.Enabled = true;
                    break;
                case "2v3":
                    char12.Enabled = true;
                    char21.Enabled = true;
                    char22.Enabled = true;
                    char23.Enabled = true;
                    break;
                case "3v1":
                    char12.Enabled = true;
                    char13.Enabled = true;
                    char21.Enabled = true;
                    break;
                case "3v2":
                    char12.Enabled = true;
                    char13.Enabled = true;
                    char21.Enabled = true;
                    char22.Enabled = true;
                    break;
                case "3v3":
                    char12.Enabled = true;
                    char13.Enabled = true;
                    char21.Enabled = true;
                    char22.Enabled = true;
                    char23.Enabled = true;
                    break;

            }
        }

        private void char11_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void char12_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void char13_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void char21_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void char22_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void char23_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void charPos12_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void charPos13_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void charPos21_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void charPos22_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void charPos23_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
