using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Reflection;

namespace smbxnpceditor
{
    public partial class Changelog : Form
    {
        public Changelog()
        {
            InitializeComponent();
        }

        private void Changelog_Load(object sender, EventArgs e)
        {
            label1.Text = "Current Version: " + FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location.ToString()).ProductVersion.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
