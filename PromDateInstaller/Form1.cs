using Octokit;
using PromDate.Installer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PromDateInstaller
{
    public partial class Form1 : Form
    {
        private List<Release> releaseNumbers = new List<Release>();
        public Form1()
        {
            InitializeComponent();
            GetReleaseNumbers();
        }

        private async void GetReleaseNumbers()
        {
            releaseNumbers = await Installer.GetReleasesAsync();
            foreach (Release release in releaseNumbers)
            {
                urlDropdown.Items.Add(release.TagName);
            }
            urlDropdown.SelectedIndex = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult result = folderDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                string file = folderDialog.SelectedPath;
                fileBox.Text = file;
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Installer.InstallPromDate(fileBox.Text, releaseNumbers[urlDropdown.SelectedIndex]);
            status.Text = "Installed!";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Installer.UninstallPromDate(fileBox.Text);
            status.Text = "Uninstalled!";
        }
    }
}
