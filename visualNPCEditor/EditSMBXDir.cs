using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Utility.ModifyRegistry;

namespace smbxnpceditor
{
    public partial class EditSMBXDir : Form
    {
        ModifyRegistry mr = new ModifyRegistry();

        public EditSMBXDir()
        {
            InitializeComponent();
        }

        private void selDirButton_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            DialogResult dr = fbd.ShowDialog();
            switch (dr)
            {
                case(DialogResult.OK):
                    dir.Text = fbd.SelectedPath;
                    break;
                case(DialogResult.Cancel):
                    break;
            }
        }

        private void EditSMBXDir_Load(object sender, EventArgs e)
        {
            try
            {
                dir.Text = mr.Read("SMBXDIRECTORY");
            }
            catch (Exception ex)
            {
                dir.Text = @"C:\SMBX";
                Console.WriteLine("Note: couldn't load the SMBXDIRECTORY reg key due to an exception.\n{0}", ex.Message);
            }
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            try
            {
                mr.Write("SMBXDIRECTORY", dir.Text + @"\graphics\npc");
                MessageBox.Show("Saved successfully!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("There was an error saving!\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
