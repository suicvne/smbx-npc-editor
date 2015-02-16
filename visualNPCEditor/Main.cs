using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using visualNPCEditor.Controls;
using System.IO;
using System.Diagnostics;
using System.Windows;
using smbxnpceditor;
using Utility.ModifyRegistry;
using Setting;
using System.Reflection;
using System.Drawing.Imaging;
using Microsoft.VisualBasic;

namespace visualNPCEditor
{
    public partial class Main : Form
    {
        public bool hasSaved;
        public string workingFile;
        public string imagePath;
        public string executableLocation = Environment.CurrentDirectory;
        public FileStream fs;
        public Image animatedImage;
        public Image fromFile;
        public int totalFrames;
        public int CurFrame;
        public int gfxWidth;
        public int gfxHeight;
        public int framesSpeed;
        public Rectangle SR;
        public Bitmap bmp;
        public bool isTimerOn;
        bool validData;
        public string smbxDirectory = null;
        public bool showAnimationPane = true;
        public string curNpcId = "blank";
        public decimal osversion;
        IniFile defaultConfig;
        ModifyRegistry mr = new ModifyRegistry();


        public Main()
        {
            InitializeComponent();
            Font f = new Font(SystemFonts.MessageBoxFont.FontFamily, 9); //Gets the system font and sets it to 9 point so everything looks ok
            Font = f;
        }

        private void npcHCb_CheckedChanged(object sender, EventArgs e)
        {
            npcGfxHeight.Enabled = npcHCb.Checked;
            //npcHitBoxHeight.npcHbH.Enabled = npcHCb.Checked;
        }
        //All the reading aka absolute chaos
        private void menuItem2_Click(object sender, EventArgs e)
        {
            OpenFileDialog of = new OpenFileDialog();
            of.Filter = "NPC Text Files (npc-*.txt)|npc-*.txt|All files (*.*)|*.*";
            of.Title = "Open NPC Text Files";
            if (of.ShowDialog() == DialogResult.OK)
            {
                readFile(of.FileName);
                string name = Path.GetFileNameWithoutExtension(of.FileName);
                string path = Path.GetDirectoryName(of.FileName);
                string nameWGif = path + @"\" + name + ".gif";
                curNpcId = name;
                if (File.Exists(nameWGif))
                {
                    try
                    {
                        showSprite(nameWGif);
                        this.Text = "SMBX NPC Editor - " + Path.GetFileName(of.FileName) + "; " + Path.GetFileName(nameWGif);
                    }
                    catch (Exception ex)
                    {
                       Console.WriteLine("Unable to create animation: " + ex.Message);
                       MessageBox.Show("Unable to create animation\n" + ex.Message, "Error While Trying to Animate", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    try
                    {
                        defaultNpc.Text = string.Format("Looks like {0} replaces {1}.", name, defaultConfig.ReadValue("npcconfig", name));
                        npcNameTextBox.CueText = string.Format("Suggested name: {0}", defaultConfig.ReadValue("npcconfig", name));
                    }
                    catch (Exception ex)
                    {
                        defaultNpc.Text = "Couldn't obtain the default configs!";
                        Console.WriteLine("Couldn't obtain the default configs!\nTrace: {0}", ex.Message);
                    }
                }
                else
                {
                    if (Directory.Exists(smbxDirectory + @"\graphics\npc"))
                    {
                        try
                        {
                            showSprite(smbxDirectory + @"\graphics\npc\" + name + ".gif");
                            this.Text = "SMBX NPC Editor - " + Path.GetFileName(of.FileName) + "; " + Path.GetFileName(nameWGif);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Unable to create animation: " + ex.Message);
                        }
                    }
                    this.Text = "SMBX NPC Editor - " + Path.GetFileName(of.FileName);
                    try
                    {
                        defaultNpc.Text = string.Format("Looks like {0} replaces {1}.", name, defaultConfig.ReadValue("npcconfig", name));
                        npcNameTextBox.CueText = string.Format("Suggested name: {0}", defaultConfig.ReadValue("npcconfig", name));
                    }
                    catch (Exception ex)
                    {
                        defaultNpc.Text = "Couldn't obtain the default configs!";
                        Console.WriteLine("Couldn't obtain the default configs!\nTrace: {0}", ex.Message);
                    }
                }
                //
                label26.Focus();
                hasSaved = true;
                workingFile = of.FileName;
            }

        }
        #region Events
        private void scoreCb_CheckedChanged(object sender, EventArgs e)
        {
            scoreList.Enabled = scoreCb.Checked;
        }

        private void menuItem5_Click(object sender, EventArgs e)
        {
            ///TODO: Check if modified logic
            Application.Exit();
        }

        private void npcWCb_CheckedChanged(object sender, EventArgs e)
        {
            npcGfxWidth.Enabled = npcWCb.Checked;
        }

        private void xOffsetCb_CheckedChanged(object sender, EventArgs e)
        {
            xOffset.Enabled = xOffsetCb.Checked;
        }

        private void yOffsetCb_CheckedChanged(object sender, EventArgs e)
        {
            yOffset.Enabled = yOffsetCb.Checked;
        }

        private void framesCb_CheckedChanged(object sender, EventArgs e)
        {
            frames.Enabled = framesCb.Checked;
        }

        private void frameSpeedCb_CheckedChanged(object sender, EventArgs e)
        {
            frameSpeed.Enabled = frameSpeedCb.Checked;
        }

        private void frameStyleCb_CheckedChanged(object sender, EventArgs e)
        {
            frameStyle.Enabled = frameStyleCb.Checked;
        }

        private void foregroundCb_CheckedChanged(object sender, EventArgs e)
        {
            foreground.Enabled = foregroundCb.Checked;
        }

        private void pNpcHeightCb_CheckedChanged(object sender, EventArgs e)
        {
            pNpcHeight.Enabled = pNpcHeightCb.Checked;
        }

        private void pNpcWidthCb_CheckedChanged(object sender, EventArgs e)
        {
            pNpcWidth.Enabled = pNpcWidthCb.Checked;
        }

        private void pCollisionCb_CheckedChanged(object sender, EventArgs e)
        {
            pCollision.Enabled = pCollisionCb.Checked;
        }

        private void pCollisionTopCb_CheckedChanged(object sender, EventArgs e)
        {
            pCollisionTop.Enabled = pCollisionTopCb.Checked;
        }

        private void npcCollisionCb_CheckedChanged(object sender, EventArgs e)
        {
            npcCollision.Enabled = npcCollisionCb.Checked;
        }

        private void npcCollisionTopCb_CheckedChanged(object sender, EventArgs e)
        {
            npcCollisionTop.Enabled = npcCollisionTopCb.Checked;
        }

        private void noBlockCb_CheckedChanged(object sender, EventArgs e)
        {
            noBlockCollision.Enabled = noBlockCb.Checked;
        }

        private void cliffTurnCb_CheckedChanged(object sender, EventArgs e)
        {
            cliffTurn.Enabled = cliffTurnCb.Checked;
        }

        private void noGravityCb_CheckedChanged(object sender, EventArgs e)
        {
            noGravity.Enabled = noGravityCb.Checked;
        }

        private void grabSideCb_CheckedChanged(object sender, EventArgs e)
        {
            grabSide.Enabled = grabSideCb.Checked;
        }

        private void grabTopCb_CheckedChanged(object sender, EventArgs e)
        {
            grabTop.Enabled = grabTopCb.Checked;
        }

        private void jumpHurtCb_CheckedChanged(object sender, EventArgs e)
        {
            jumpHurt.Enabled = jumpHurtCb.Checked;
        }

        private void dontHurtCb_CheckedChanged(object sender, EventArgs e)
        {
            dontHurt.Enabled = dontHurtCb.Checked;
        }

        private void noYoshiCb_CheckedChanged(object sender, EventArgs e)
        {
            noYoshi.Enabled = noYoshiCb.Checked;
        }

        private void speedCb_CheckedChanged(object sender, EventArgs e)
        {
            speed.Enabled = speedCb.Checked;
        }

        private void noFireballCb_CheckedChanged(object sender, EventArgs e)
        {
            noFireball.Enabled = noFireballCb.Checked;
        }

        private void noFreezeCb_CheckedChanged(object sender, EventArgs e)
        {
            noFreeze.Enabled = noFreezeCb.Checked;
        }
        #endregion
        #region This all the various menu item stuff
        private void menuItem7_Click(object sender, EventArgs e)
        {
            if(Directory.Exists(Path.Combine(Environment.CurrentDirectory, "sprites")))
            {
                workingFile = null;
                hasSaved = false;
                //resetAllItems();
                //defaultNpc.Text = "Load a file or save one!";
                NewConfig con = new NewConfig();
                var result = con.ShowDialog();
                switch (result)
                {
                    case (DialogResult.OK):
                        whichNPC(con);
                        break;
                    case (DialogResult.Cancel):
                        break;
                }
                this.Text = "SMBX NPC Editor";
            }
            else
            {
                workingFile = null;
                hasSaved = false;
                this.Text = "SMBX NPC Editor - Sprites Folder Missing";
            }
        }
        private void menuItem3_Click(object sender, EventArgs e)
        {
            if (hasSaved == true)
            {
                if (workingFile != null)
                {
                    saveAll(workingFile);
                    string id = Path.GetFileNameWithoutExtension(workingFile);
                    String text = npcNameTextBox.Text;
                    if(npcNameTextBox.Text == "" || npcNameTextBox.Text == " ")
                    {
                        npcNameTextBox.Text = "";
                        npcNameTextBox.CueText = String.Format("Suggested NPC Name: {0}",defaultConfig.ReadValue("npcconfig", id));
                    }
                    defaultNpc.Text = String.Format("Looks like {0} replaces {1}", id, defaultConfig.ReadValue("npcconfig", id));
                }
                else
                {
                    saveAs();
                    string id = Path.GetFileNameWithoutExtension(workingFile);
                    String text = npcNameTextBox.Text;
                    if (npcNameTextBox.Text == "" || npcNameTextBox.Text == " ")
                    {
                        npcNameTextBox.Text = "";
                        npcNameTextBox.CueText = String.Format("Suggested NPC Name: {0}", defaultConfig.ReadValue("npcconfig", id));
                    }
                    defaultNpc.Text = String.Format("Looks like {0} replaces {1}", id, defaultConfig.ReadValue("npcconfig", id));
                }
            }
            else
            {
                saveAs();
                string id = Path.GetFileNameWithoutExtension(workingFile);
                String text = npcNameTextBox.Text;
                if (npcNameTextBox.Text == "" || npcNameTextBox.Text == " ")
                {
                    npcNameTextBox.Text = "";
                    npcNameTextBox.CueText = String.Format("Suggested NPC Name: {0}", defaultConfig.ReadValue("npcconfig", id));
                }
                defaultNpc.Text = String.Format("Looks like {0} replaces {1}", id, defaultConfig.ReadValue("npcconfig", id));
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            StreamWriter test = new StreamWriter("test.txt");
            if (frameStyleCb.Checked == true)
            {
                var style = frameStyle.SelectedIndex;
                switch (style)
                {
                    case (0):
                        //single sprite
                        test.WriteLine("framestyle=0");
                        break;
                    case (1):
                        //Left/Right
                        test.WriteLine("framestyle=1");
                        break;
                    case (2):
                        test.WriteLine("framestyle=2");
                        //Left/Right/Upside Down
                        break;
                }
            }
            test.Flush();
            test.Close();
        }

        private void menuItem4_Click(object sender, EventArgs e)
        {
            saveAs();
        }

        private void menuItem9_Click(object sender, EventArgs e)
        {
            Process.Start("http://forums.smbxepisodes.tk");
        }

        private void menuItem8_Click(object sender, EventArgs e)
        {
            AboutBox about = new AboutBox();
            about.ShowDialog();
        }

        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to quit?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.No)
            {
                e.Cancel = true;
                //Application.Exit();
            }
            else
            {
                e.Cancel = false;
                Environment.Exit(0);
            }
        }
        #endregion
        #region Drag and Drop Code
        private void menuItem10_Click(object sender, EventArgs e)
        {
            Changelog cl = new Changelog();
            cl.ShowDialog();
        }

        private void Main_DragDrop(object sender, DragEventArgs e)
        {
            string filename;
            validData = GetFilename(out filename, e);
            if (validData)
            {
                //menuItem2_Click(null, null);
                readFile(filename);
            }
        }

        private void Main_DragEnter(object sender, DragEventArgs e)
        {
            string filename;
            validData = GetFilename(out filename, e);
            if (validData)
            {
                e.Effect = DragDropEffects.Link;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void Main_DragLeave(object sender, EventArgs e)
        {

        }

        private void Main_DragOver(object sender, DragEventArgs e)
        {

        }
        /// <summary>
        /// Makes sure that the file you're dragging/dropping is of the proper extension
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="e"></param>
        /// <returns></returns>
        protected bool GetFilename(out string filename, DragEventArgs e)
        {
            bool ret = false;
            filename = String.Empty;

            if ((e.AllowedEffect & DragDropEffects.Copy) == DragDropEffects.Copy)
            {
                Array data = ((IDataObject)e.Data).GetData("FileName") as Array;
                if (data != null)
                {
                    if ((data.Length == 1) && (data.GetValue(0) is String))
                    {
                        filename = ((string[])data)[0];
                        string ext = Path.GetExtension(filename).ToLower();
                        if ((ext == ".txt"))
                        {
                            ret = true;
                        }
                    }
                }
            }
            return ret;
        }
        #endregion
        #region Related to saving, reading, etc
        public void readFile(string file)
        {
            Console.WriteLine("Reading: {0}", file);
            resetAllItems();
            var npcfile = File.ReadAllLines(file);
            
            var config = (from line in npcfile
                              let s = line.Split('=')
                              select new { Key = s[0], Value = s[1] })
                    .ToDictionary(x => x.Key, x => x.Value);

            for (int index = 0; index < config.Count; index++)
            {
                decimal number;
                var item = config.ElementAt(index);
                var itemKey = item.Key;
                var itemValue = item.Value;
                Console.WriteLine("{0} is {1}", itemKey, itemValue);
                switch (itemKey)
                {
                    case ("name"):
                        string quotes = itemValue.ToString();
                        string withOut = quotes.Replace("\"", "");
                        npcNameTextBox.Text = withOut;
                        break;
                    case ("gfxoffsetx"):
                        xOffsetCb.Checked = true;
                        xOffset.Text = itemValue.ToString();
                        xOffset.Enabled = true;
                        break;
                    case ("gfxoffsety"):
                        yOffsetCb.Checked = true;
                        yOffset.Text = itemValue.ToString();
                        xOffset.Enabled = true;
                        break;
                    case ("width"):
                        pNpcWidthCb.Checked = true;
                        if (Decimal.TryParse(itemValue, out number))
                        {
                            pNpcWidth.Value = number;
                            pNpcWidth.Enabled = true;
                        }
                        break;
                    case ("height"):
                        pNpcHeightCb.Checked = true;
                        if (Decimal.TryParse(itemValue, out number))
                        {
                            pNpcHeight.Enabled = true;
                            pNpcHeight.Value = number;
                        }
                        break;
                    case ("gfxwidth"):
                        npcWCb.Checked = true;
                        if (Decimal.TryParse(itemValue, out number))
                        {
                            npcGfxWidth.Enabled = true;
                            npcGfxWidth.Value = number;
                        }
                        break;
                    case ("gfxheight"):
                        npcHCb.Checked = true;
                        if (Decimal.TryParse(itemValue, out number))
                        {
                            npcGfxHeight.Enabled = true;
                            npcGfxHeight.Value = number;
                        }
                        break;
                    case ("score"):
                        scoreCb.Checked = true;
                        switch (itemValue)
                        {
                            case ("0"):
                                scoreList.Text = "None";
                                break;
                            case ("1"):
                                scoreList.Text = "10";
                                break;
                            case ("2"):
                                scoreList.Text = "100";
                                break;
                            case ("3"):
                                scoreList.Text = "200";
                                break;
                            case ("4"):
                                scoreList.Text = "400";
                                break;
                            case ("5"):
                                scoreList.Text = "800";
                                break;
                            case ("6"):
                                scoreList.Text = "1000";
                                break;
                            case ("7"):
                                scoreList.Text = "2000";
                                break;
                            case ("8"):
                                scoreList.Text = "4000";
                                break;
                            case ("9"):
                                scoreList.Text = "8000";
                                break;
                            case ("10"):
                                scoreList.Text = "1-Up";
                                break;
                            case ("11"):
                                scoreList.Text = "2-Up";
                                break;
                            case("12"):
                                scoreList.Text = "3-Up";
                                break;
                            case ("13"):
                                scoreList.Text = "5-Up";
                                break;
                        }
                        scoreList.Enabled = true;
                        break;
                    case ("playerblock"):
                        pCollisionCb.Checked = true;
                        switch (itemValue)
                        {
                            case ("0"):
                                pCollision.Checked = false;
                                break;
                            case ("1"):
                                pCollision.Checked = true;
                                break;
                        }
                        pCollision.Enabled = true;
                        break;
                    case ("playerblocktop"):
                        pCollisionTopCb.Checked = true;
                        switch (itemValue)
                        {
                            case ("0"):
                                pCollisionTop.Checked = false;
                                break;
                            case ("1"):
                                pCollisionTop.Checked = true;
                                break;
                        }
                        pCollisionTop.Enabled = true;
                        break;
                    case ("npcblock"):
                        npcCollisionCb.Checked = true;
                        switch (itemValue)
                        {
                            case ("0"):
                                npcCollision.Checked = false;
                                break;
                            case ("1"):
                                npcCollision.Checked = true;
                                break;
                        }
                        npcCollision.Enabled = true;
                        break;
                    case ("npcblocktop"):
                        npcCollisionTopCb.Checked = true;
                        switch (itemValue)
                        {
                            case ("0"):
                                npcCollisionTop.Checked = false;
                                break;
                            case ("1"):
                                npcCollisionTop.Checked = true;
                                break;
                        }
                        npcCollisionTop.Enabled = true;
                        break;
                    case ("grabside"):
                        grabSideCb.Checked = true;
                        switch (itemValue)
                        {
                            case ("0"):
                                grabSide.Checked = false;
                                break;
                            case ("1"):
                                grabSide.Checked = true;
                                break;
                        }
                        grabSide.Enabled = true;
                        break;
                    case ("grabtop"):
                        grabTopCb.Checked = true;
                        switch (itemValue)
                        {
                            case ("0"):
                                grabTop.Checked = false;
                                break;
                            case ("1"):
                                grabTop.Checked = true;
                                break;
                        }
                        grabTop.Enabled = true;
                        break;
                    case ("jumphurt"):
                        jumpHurtCb.Checked = true;
                        switch (itemValue)
                        {
                            case ("0"):
                                jumpHurt.Checked = false;
                                break;
                            case ("1"):
                                jumpHurt.Checked = true;
                                break;
                        }
                        jumpHurt.Enabled = true;
                        break;
                    case ("nohurt"):
                        dontHurtCb.Checked = true;
                        switch (itemValue)
                        {
                            case ("0"):
                                dontHurt.Checked = false;
                                break;
                            case ("1"):
                                dontHurt.Checked = true;
                                break;
                        }
                        dontHurt.Enabled = true;
                        break;
                    case ("noblockcollision"):
                        noBlockCb.Checked = true;
                        switch (itemValue)
                        {
                            case ("0"):
                                noBlockCollision.Checked = false;
                                break;
                            case ("1"):
                                noBlockCollision.Checked = true;
                                break;
                        }
                        noBlockCollision.Enabled = true;
                        break;
                    case ("cliffturn"):
                        cliffTurnCb.Checked = true;
                        switch (itemValue)
                        {
                            case ("0"):
                                cliffTurn.Checked = false;
                                break;
                            case ("1"):
                                cliffTurn.Checked = true;
                                break;
                        }
                        cliffTurn.Enabled = true;
                        break;
                    case ("noyoshi"):
                        noYoshiCb.Checked = true;
                        switch (itemValue)
                        {
                            case ("0"):
                                noYoshi.Checked = false;
                                break;
                            case ("1"):
                                noYoshi.Checked = true;
                                break;
                        }
                        noYoshiCb.Enabled = true;
                        break;
                    case ("foreground"):
                        foregroundCb.Checked = true;
                        switch (itemValue)
                        {
                            case ("0"):
                                foreground.Checked = false;
                                break;
                            case ("1"):
                                foreground.Checked = true;
                                break;
                        }
                        foreground.Enabled = true;
                        break;
                    case ("speed"):
                        speedCb.Checked = true;
                        speed.Enabled = true;
                        if (Decimal.TryParse(itemValue, out number))
                        {
                            speed.Value = number;
                        }
                        break;
                    case ("nofireball"):
                        noFireballCb.Checked = true;
                        switch (itemValue)
                        {
                            case ("0"):
                                noFireball.Checked = false;
                                break;
                            case ("1"):
                                noFireball.Checked = true;
                                break;
                        }
                        noFireball.Enabled = true;
                        break;
                    case ("nogravity"):
                        noGravityCb.Checked = true;
                        switch (itemValue)
                        {
                            case ("0"):
                                noGravity.Checked = false;
                                break;
                            case ("1"):
                                noGravity.Checked = true;
                                break;
                        }
                        noGravity.Enabled = true;
                        break;
                    case ("frames"):
                        framesCb.Checked = true;
                        frames.Enabled = true;
                        if (Decimal.TryParse(itemValue, out number))
                        {
                            frames.Value = number;
                        }
                        break;
                    case ("framespeed"):
                        frameSpeedCb.Checked = true;
                        frameSpeed.Enabled = true;
                        if (Decimal.TryParse(itemValue, out number))
                        {
                            frameSpeed.Value = number;
                        }
                        break;
                    case ("framestyle"):
                        frameStyleCb.Checked = true;
                        switch (itemValue)
                        {
                            case ("2"):
                                frameStyle.Text = "Left/Right/Upside Down Sprites";
                                break;
                            case ("1"):
                                frameStyle.Text = "Left/Right Sprites";
                                break;
                            case ("0"):
                                frameStyle.Text = "Single Sprite";
                                break;
                        }
                        frameStyle.Enabled = true;
                        break;
                    case ("noiceball"):
                        noFreezeCb.Checked = true;
                        switch (itemValue)
                        {
                            case ("0"):
                                noFreeze.Checked = false;
                                break;
                            case ("1"):
                                noFreeze.Checked = true;
                                break;
                        }
                        noFreeze.Enabled = true;
                        break;
                }
            }
        }
        public void resetAllItems()
        {
            npcNameTextBox.Text = "";
            npcGfxHeight.Enabled = false;
            npcGfxHeight.Value = 1;
            npcGfxWidth.Enabled = false;
            npcGfxWidth.Value = 1;
            xOffset.Enabled = false;
            xOffset.Value = 0;
            yOffset.Enabled = false;
            yOffset.Value = 0;
            frames.Enabled = false;
            frames.Value = 1;
            frameSpeed.Enabled = false;
            frameSpeed.Value = 8;
            frameStyle.Enabled = false;
            frameStyle.Enabled = false;
            foreground.Enabled = false;
            foreground.Checked = false;
            pNpcHeight.Enabled = false;
            pNpcHeight.Value = 1;
            pNpcWidth.Enabled = false;
            pNpcWidth.Value = 1;
            pCollision.Enabled = false;
            pCollision.Checked = false;
            pCollisionTop.Enabled = false;
            pCollisionTop.Checked = false;
            npcCollision.Enabled = false;
            npcCollision.Checked = false;
            npcCollisionTop.Enabled = false;
            npcCollisionTop.Checked = false;
            noBlockCollision.Enabled = false;
            noBlockCollision.Checked = false;
            cliffTurn.Enabled = false;
            cliffTurn.Checked = false;
            noGravity.Enabled = false;
            noGravity.Checked = false;
            scoreList.Enabled = false;
            grabSide.Enabled = false;
            grabSide.Checked = false;
            grabTop.Enabled = false;
            grabTop.Checked = false;
            jumpHurt.Enabled = false;
            jumpHurt.Checked = false;
            dontHurt.Enabled = false;
            dontHurt.Checked = false;
            noYoshi.Enabled = false;
            noYoshi.Checked = false;
            speed.Enabled = false;
            noFireball.Enabled = false;
            noFireball.Checked = false;
            noFreeze.Enabled = false;
            noFreeze.Checked = false;
            //
            npcHCb.Checked = false;
            npcWCb.Checked = false;
            xOffsetCb.Checked = false;
            yOffsetCb.Checked = false;
            framesCb.Checked = false;
            frameSpeedCb.Checked = false;
            frameStyleCb.Checked = false;
            frameStyleCb.Checked = false;
            foregroundCb.Checked = false;
            pNpcHeightCb.Checked = false;
            pNpcWidthCb.Checked = false;
            pCollisionCb.Checked = false;
            pCollisionTopCb.Checked = false;
            npcCollisionCb.Checked = false;
            npcCollisionTopCb.Checked = false;
            noBlockCb.Checked = false;
            cliffTurnCb.Checked = false;
            noGravityCb.Checked = false;
            scoreCb.Checked = false;
            grabSideCb.Checked = false;
            grabTopCb.Checked = false;
            jumpHurtCb.Checked = false;
            dontHurtCb.Checked = false;
            noYoshiCb.Checked = false;
            speedCb.Checked = false;
            noFireballCb.Checked = false;
            noFreezeCb.Checked = false;
        }
        public void saveAll(string fileName)
        {
            StreamWriter sr = new StreamWriter(fileName);
            //Graphics Related
            if (!string.IsNullOrWhiteSpace(npcNameTextBox.Text) && npcNameTextBox.Text.Length > 0)
            {
                sr.WriteLine("name=" + "\"{0}\"", npcNameTextBox.Text);
            }
            if (npcHCb.Checked == true)
            {
                if (npcGfxHeight.Value != 0)
                {
                    sr.WriteLine("gfxheight=" + npcGfxHeight.Value.ToString());
                }
            }
            if (npcWCb.Checked == true)
            {
                if (npcGfxWidth.Value != 0)
                {
                    sr.WriteLine("gfxwidth=" + npcGfxWidth.Value.ToString());
                }
            }
            if (xOffsetCb.Checked == true)
            {
                if (xOffset.Value != 0)
                {
                    sr.WriteLine("gfxoffsetx=" + xOffset.Value.ToString());
                }
            }
            if (yOffsetCb.Checked == true)
            {
                if (yOffset.Value != 0)
                {
                    sr.WriteLine("gfxoffsety=" + yOffset.Value.ToString());
                }
            }
            if (framesCb.Checked == true)
            {
                if (frames.Value != 0)
                {
                    sr.WriteLine("frames=" + frames.Value.ToString());
                }
            }
            if (frameSpeedCb.Checked == true)
            {
                if (frameSpeed.Value != 0)
                {
                    sr.WriteLine("framespeed=" + frameSpeed.Value.ToString());
                }
            }
            if (frameStyleCb.Checked == true)
            {
                var style = frameStyle.SelectedIndex;
                switch (style)
                {
                    case (0):
                        //single sprite
                        sr.WriteLine("framestyle=0");
                        break;
                    case (1):
                        //Left/Right
                        sr.WriteLine("framestyle=1");
                        break;
                    case (2):
                        sr.WriteLine("framestyle=2");
                        //Left/Right/Upside Down
                        break;
                }
            }
            if (foregroundCb.Checked == true)
            {
                if (foreground.Checked == true)
                {
                    //Yes foreground
                    sr.WriteLine("foreground=1");
                }
                else
                {
                    //No, background
                    sr.WriteLine("foreground=0");
                }
            }
            //Physics Related
            if (pNpcHeightCb.Checked == true)
            {
                if (pNpcHeight.Value != 0)
                {
                    sr.WriteLine("height=" + pNpcHeight.Value.ToString());
                }
            }
            if (pNpcWidthCb.Checked == true)
            {
                if (pNpcWidth.Value != 0)
                {
                    sr.WriteLine("width=" + pNpcWidth.Value.ToString());
                }
            }
            if (pCollisionCb.Checked == true)
            {
                if (pCollision.Checked == true)
                {
                    sr.WriteLine("playerblock=1");
                }
                else
                {
                    sr.WriteLine("playerblock=0");
                }
            }
            if (pCollisionTopCb.Checked == true)
            {
                if (pCollisionTop.Checked == true)
                {
                    sr.WriteLine("playerblocktop=1");
                }
                else
                {
                    sr.WriteLine("playerblocktop=0");
                }
            }
            if (npcCollisionCb.Checked == true)
            {
                if (npcCollision.Checked == true)
                {
                    sr.WriteLine("npcblock=1");
                }
                else
                {
                    sr.WriteLine("npcblock=0");
                }
            }
            if (npcCollisionTopCb.Checked == true)
            {
                if (npcCollisionTop.Checked == true)
                {
                    sr.WriteLine("npcblocktop=1");
                }
                else
                {
                    sr.WriteLine("npcblocktop=0");
                }
            }
            if (noBlockCb.Checked == true)
            {
                if (noBlockCollision.Checked == true)
                {
                    sr.WriteLine("noblockcollision=1");
                }
                else
                {
                    sr.WriteLine("noblockcollision=0");
                }
            }
            if (cliffTurnCb.Checked == true)
            {
                if (cliffTurn.Checked == true)
                {
                    sr.WriteLine("cliffturn=1");
                }
                else
                {
                    sr.WriteLine("cliffturn=0");
                }
            }
            if (noGravityCb.Checked == true)
            {
                if (noGravity.Checked == true)
                {
                    sr.WriteLine("nogravity=1");
                }
                else
                {
                    sr.WriteLine("nogravity=0");
                }
            }
            //Game Related Stuff
            if (scoreCb.Checked == true)
            {
                var score = scoreList.SelectedIndex;
                switch (score)
                {
                    case (0):
                        sr.WriteLine("score=0");
                        break;
                    case (1):
                        sr.WriteLine("score=1");
                        break;
                    case (2):
                        sr.WriteLine("score=2");
                        break;
                    case (3):
                        sr.WriteLine("score=3");
                        break;
                    case (4):
                        sr.WriteLine("score=4");
                        break;
                    case (5):
                        sr.WriteLine("score=5");
                        break;
                    case (6):
                        sr.WriteLine("score=6");
                        break;
                    case (7):
                        sr.WriteLine("score=7");
                        break;
                    case (8):
                        sr.WriteLine("score=8");
                        break;
                    case (9):
                        sr.WriteLine("score=9");
                        break;
                    case (10):
                        sr.WriteLine("score=10");
                        break;
                    case (11):
                        sr.WriteLine("score=11");
                        break;
                    case (12):
                        sr.WriteLine("score=12");
                        break;
                    case(13):
                        sr.WriteLine("score=13");
                        break;
                }
            }
            if (grabSideCb.Checked == true)
            {
                if (grabSide.Checked == true)
                {
                    sr.WriteLine("grabside=1");
                }
                else
                {
                    sr.WriteLine("grabside=0");
                }
            }
            if (grabTopCb.Checked == true)
            {
                if (grabTop.Checked == true)
                {
                    sr.WriteLine("grabside=1");
                }
                else
                {
                    sr.WriteLine("grabside=0");
                }
            }
            if (jumpHurtCb.Checked == true)
            {
                if (jumpHurt.Checked == true)
                {
                    sr.WriteLine("jumphurt=1");
                }
                else
                {
                    sr.WriteLine("jumphurt=0");
                }
            }
            if (dontHurtCb.Checked == true)
            {
                if (dontHurt.Checked == true)
                {
                    sr.WriteLine("nohurt=1");
                }
                else
                {
                    sr.WriteLine("nohurt=0");
                }
            }
            if (noYoshiCb.Checked == true)
            {
                if (noYoshi.Checked == true)
                {
                    sr.WriteLine("noyoshi=1");
                }
                else
                {
                    sr.WriteLine("noyoshi=0");
                }
            }
            if (speedCb.Checked == true)
            {
                sr.WriteLine("speed=" + speed.Value.ToString());
            }
            if (noFireballCb.Checked == true)
            {
                if (noFireball.Checked == true)
                {
                    sr.WriteLine("nofireball=1");
                }
                else
                {
                    sr.WriteLine("nofireball=0");
                }

            }
            if (noFreeze.Checked == true)
            {
                if (noFreeze.Checked == true)
                {
                    sr.WriteLine("noiceball=1");
                }
                else
                {
                    sr.WriteLine("noiceball=0");
                }
            }

            sr.Flush();
            sr.Close();
            MessageBox.Show("NPC was saved successfully to: \n" + fileName.ToString(), "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        public void saveAs()
        {
            SaveFileDialog sf = new SaveFileDialog();
            sf.Filter = "NPC Text Files (npc-*.txt)|npc-*.txt|All files (*.*)|*.*";
            if (curNpcId != "blank")
            {
                sf.FileName = String.Format("{0}.txt", curNpcId);
            }
            sf.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments).ToString();

            if (sf.ShowDialog() == DialogResult.OK)
            {
                saveAll(sf.FileName);
                hasSaved = true;
                workingFile = sf.FileName.ToString();
                curNpcId = Path.GetFileNameWithoutExtension(workingFile);
                this.Text = "SMBX NPC Editor - " + Path.GetFileName(workingFile);
            }
        }
        #endregion
        #region Animation stuff, kindly coded by GhostHawk. Can't thank him enough!
        private void timer1_Tick(object sender, EventArgs e)
        {
            CurFrame += 1;

            if (CurFrame >= frames.Value)
            {
                CurFrame = 0;
            }

            SR = new Rectangle(0, gfxHeight * CurFrame, gfxWidth, gfxHeight);

            Graphics g;
            g = Graphics.FromImage(bmp);
            g.DrawImage(animatedImage, new Rectangle(0, 0, gfxWidth, gfxHeight), SR, GraphicsUnit.Pixel);
            pictureBox1.Image = bmp;
            pictureBox1.Update();
            isTimerOn = true;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            openFileDialog1.FileName = "";
            openFileDialog1.ShowDialog();
            
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            showSprite(openFileDialog1.FileName);
            try
            {
                string id = Path.GetFileNameWithoutExtension(openFileDialog1.FileName).ToString();
                string path = Path.GetDirectoryName(openFileDialog1.FileName).ToString();
                readFile(path + @"\" + id + ".txt");
                this.Text = String.Format("SMBX NPC Editor - {0}.txt; {0}.gif", id);
                animateSprite(id);
                defaultNpc.Text = String.Format("Looks like {0} replaces {1}", id, defaultConfig.ReadValue("npcconfig", id));
                npcNameTextBox.CueText = String.Format("Suggested Name: {0}", defaultConfig.ReadValue("npcconfig", id));
            }
            catch
            {
                string id = Path.GetFileNameWithoutExtension(openFileDialog1.FileName).ToString();
                Console.WriteLine("No text file with the graphic..cool cool B)");
                this.Text = String.Format("SMBX NPC Editor - {0}.gif", id);
            }
        }

        public void showSprite(string fileName)
        {
            //fileName = openFileDialog1.FileName;
            //fs = new FileStream(fileName, System.IO.FileMode.Open);
            //animatedImage = Image.FromStream(fs);
            using (Image sourceImg = Image.FromFile(fileName))
            {
                animatedImage = new Bitmap(sourceImg.Width, sourceImg.Height, PixelFormat.Format32bppArgb);
                using (var copy = Graphics.FromImage(animatedImage))
                {
                    copy.DrawImage(sourceImg, 0, 0);
                }
            }

            pictureBox1.Image = animatedImage;

            if (pictureBox1.Image.Width < pictureBox1.Width)
            {
                pictureBox1.SizeMode = PictureBoxSizeMode.CenterImage;
            }
            animateSprite(curNpcId);
            pictureBox1.Update();
        }

        public void animateSprite(string npcid)
        {
            IniFile wohl = new IniFile(executableLocation + @"\lvl_npc.ini");
                int errorLevel = 0; //0 is no error, 1 is gfxheight, 2 is gfxwidth, 3 is both
                if (npcGfxHeight.Value != 0)
                {
                    if (npcHCb.Checked == true)
                    {
                        gfxHeight = Convert.ToInt32(npcGfxHeight.Value);
                    }
                    else
                    {
                        if (curNpcId != "blank")
                        {
                            npcGfxHeight.Value = int.Parse(wohl.ReadValue(curNpcId, "gfx-height"));
                            gfxHeight = (int)npcGfxHeight.Value;
                        }
                        else
                        {
                            gfxHeight = pictureBox1.Image.Height;
                        }
                    }
                }
                else
                {
                    if (npcid != "blank")
                    {
                        try
                        {
                            
                            gfxHeight = int.Parse(wohl.ReadValue(curNpcId, "gfx-height"));
                            npcGfxHeight.Value = gfxHeight;
                        }
                        catch
                        {
                            Console.WriteLine("Can't load the gfxheight");
                        }
                    }
                    else
                    {
                        errorLevel = 1;
                    }
                }
                if (npcGfxWidth.Value != 0)
                {
                    if (npcWCb.Checked == true)
                    {
                        gfxWidth = Convert.ToInt32(npcGfxWidth.Value);
                    }
                    else
                    {
                        if (curNpcId != "blank")
                        {
                            npcGfxWidth.Value = int.Parse(wohl.ReadValue(curNpcId, "gfx-width"));
                            gfxWidth = (int)npcGfxWidth.Value;
                        }
                        else
                        {
                            gfxWidth = pictureBox1.Image.Width;
                        }
                    }
                }
                else
                {
                    if (npcid != "blank")
                    {
                        try
                        {
                            //IniFile wohl = new IniFile(Environment.CurrentDirectory + @"\lvl_npc.ini");
                            gfxWidth = int.Parse(wohl.ReadValue(curNpcId, "gfx-width"));
                            npcGfxWidth.Value = gfxWidth;
                        }
                        catch
                        {
                            Console.WriteLine("Can't load the gfxwidth");
                        }
                    }
                    else
                    {
                        errorLevel = 2;
                    }
                }

                if (npcGfxHeight.Value == 0 && npcGfxWidth.Value == 0)
                {
                    errorLevel = 3;
                }
                switch(errorLevel)
                {
                    case(0):
                        break;
                    case(1):
                        MessageBox.Show("Please input a value for Graphics height!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        break;
                    case(2):
                        MessageBox.Show("Please input a value for Graphics width!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    break;
                    case(3):
                        MessageBox.Show("Please input a value for Graphics height and Graphics Width!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    break;
                }

                if (frames.Value == 1)
                {
                    if (framesCb.Checked == true)
                    {
                        totalFrames = Convert.ToInt32(frames.Value);
                    }
                    else if (curNpcId != "blank")
                    {
                        //IniFile wohl = new IniFile(Environment.CurrentDirectory + @"\lvl_npc.ini");
                        totalFrames = int.Parse(wohl.ReadValue(curNpcId, "frames"));
                        frames.Value = totalFrames;
                    }

                }
                else
                {
                    totalFrames = Convert.ToInt32(frames.Value);
                }

                //totalFrames = Convert.ToInt32(frames.Value);
                
                if (frameSpeedCb.Checked == true)
                {
                    framesSpeed = Convert.ToInt32(frameSpeed.Value);
                }
                else
                {
                    framesSpeed = Convert.ToInt32(8);
                    frameSpeed.Value = 8;
                }
                bmp = new Bitmap(gfxWidth, gfxHeight);
                int caseSwitch = framesSpeed;
                switch (caseSwitch)
                {
                    case 1:
                        timer1.Interval = 13;
                        break;
                    case 2:
                        timer1.Interval = 25;
                        break;
                    case 3:
                        timer1.Interval = 37;
                        break;
                    case 4:
                        timer1.Interval = 50;
                        break;
                    case 5:
                        timer1.Interval = 63;
                        break;
                    case 6:
                        timer1.Interval = 75;
                        break;
                    case 7:
                        timer1.Interval = 88;
                        break;
                    case 8:
                        timer1.Interval = 100;
                        break;
                    case 9:
                        timer1.Interval = 112;
                        break;
                    case 10:
                        timer1.Interval = 125;
                        break;
                    case 11:
                        timer1.Interval = 137;
                        break;
                    case 12:
                        timer1.Interval = 149;
                        break;
                    case 13:
                        timer1.Interval = 161;
                        break;
                    case 14:
                        timer1.Interval = 175;
                        break;
                    case 15:
                        timer1.Interval = 189;
                        break;
                    case 16:
                        timer1.Interval = 200;
                        break;
                }
                timer1.Start();
            //}

            //catch(Exception ex)
            //{
                //timer1.Stop();
                //MessageBox.Show(ex.Message);

            //}
        }

        private void button2_Click(object sender, EventArgs e)
        {
            animateSprite(curNpcId);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            timer1.Stop();
        }
        #endregion

        private void Main_Load(object sender, EventArgs e)
        {
            using (var resource = Assembly.GetExecutingAssembly().GetManifestResourceStream("smbxnpceditor.Resources.SMBX64.ini"))
            {
                using (var file = new FileStream(Environment.CurrentDirectory + @"\npc-config.ini", FileMode.Create, FileAccess.Write))
                {
                    resource.CopyTo(file);
                }
            }
            defaultConfig = new IniFile(Environment.CurrentDirectory + @"\npc-config.ini");

            openFileDialog1.Title = "Open NPC Image";
            openFileDialog1.Filter = "SMBX Sprite Files (*.gif)|*gif";
        TryReadRegistry:
            try
            {
                var reg = mr.Read("SMBXDIRECTORY");
                smbxDirectory = reg.ToString();
                var enableAnimation = mr.Read("SHOWANIMATION");
                //799, 434
                //height needs to be 460 on windows 8 apparently
                Microsoft.VisualBasic.Devices.Computer MyComputer = new Microsoft.VisualBasic.Devices.Computer();
                string ver = MyComputer.Info.OSVersion;
                string[] split = ver.Split(new char[] { '.' });
                string final = split[0].ToString() + "." + split[1].ToString();
                decimal finall = decimal.Parse(final);
                osversion = finall;
                if(finall == (decimal)6.2 || finall == (decimal)6.3 || finall > (decimal)6.3)
                {
                    switch (enableAnimation)
                    {
                        case ("true"):
                            showAnimationPane = true;
                            animationPaneMenuItem.Checked = true;
                            //npcAnimationGroup.Visible = true;
                            this.Size = new System.Drawing.Size(1176, 460);
                            break;
                        case ("false"):
                            showAnimationPane = false;
                            animationPaneMenuItem.Checked = false;
                            //npcAnimationGroup.Visible = true;
                            this.Size = new System.Drawing.Size(799, 460);
                            break;
                    }
                }
                else
                {
                    switch (enableAnimation)
                    {
                        case ("true"):
                            showAnimationPane = true;
                            animationPaneMenuItem.Checked = true;
                            //npcAnimationGroup.Visible = true;
                            this.Size = new System.Drawing.Size(1176, 444);
                            break;
                        case ("false"):
                            showAnimationPane = false;
                            animationPaneMenuItem.Checked = false;
                            //npcAnimationGroup.Visible = true;
                            this.Size = new System.Drawing.Size(799, 444);
                            break;
                    }
                }
                goto CheckDirectory;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Looks like I can't find your SMBX directory.\nPlease go to Edit>Change SMBX Directory to select your SMBX Directory for default graphics reading");
                goto BeginProgram;
            }
        CheckDirectory:
            if (smbxDirectory != null)
            {
                if (Directory.Exists(smbxDirectory) != false)
                {
                    //we all good!
                }
            }
            else
            {
                MessageBox.Show("Looks like I can't find your SMBX directory.\nPlease go to Edit>Change SMBX Directory to select your SMBX Directory for default graphics reading");
            }
        BeginProgram:
            Console.WriteLine("Loaded Configuration: SMBX64 Successfully");
        }
        private static void ExtractEmbeddedResource(string outputDir, string resourceLocation, List<string> files)
        {
            foreach (string file in files)
            {
                using (System.IO.Stream stream = System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceLocation + @"." + file))
                {
                    using (System.IO.FileStream fileStream = new System.IO.FileStream(System.IO.Path.Combine(outputDir, file), System.IO.FileMode.Create))
                    {
                        for (int i = 0; i < stream.Length; i++)
                        {
                            fileStream.WriteByte((byte)stream.ReadByte());
                        }
                        fileStream.Close();
                    }
                }
            }
        }

        private void menuItem12_Click(object sender, EventArgs e)
        {
            EditSMBXDir es = new EditSMBXDir();
            es.ShowDialog();
        }

        private void animationPaneMenuItem_Click(object sender, EventArgs e)
        {
            if (osversion == (decimal)5.2 || osversion == (decimal)5.3)
            {
                switch (animationPaneMenuItem.Checked)
                {
                    case (true):
                        showAnimationPane = false;
                        animationPaneMenuItem.Checked = false;
                        try
                        {
                            mr.Write("SHOWANIMATION", "false");
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        this.Size = new System.Drawing.Size(799, 465);

                        break;
                    case (false):
                        showAnimationPane = true;
                        animationPaneMenuItem.Checked = true;
                        try
                        {
                            mr.Write("SHOWANIMATION", "true");
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        this.Size = new System.Drawing.Size(1176, 465);
                        break;
                }
            }
            else
            {
                switch (animationPaneMenuItem.Checked)
                {
                    case (true):
                        showAnimationPane = false;
                        animationPaneMenuItem.Checked = false;
                        try
                        {
                            mr.Write("SHOWANIMATION", "false");
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        this.Size = new System.Drawing.Size(799, 427);

                        break;
                    case (false):
                        showAnimationPane = true;
                        animationPaneMenuItem.Checked = true;
                        try
                        {
                            mr.Write("SHOWANIMATION", "true");
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        this.Size = new System.Drawing.Size(1176, 427);
                        break;
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            NewConfig con = new NewConfig();
            var result = con.ShowDialog();
            switch (result)
            {
                case (DialogResult.OK):
                    whichNPC(con);
                    break;
                case (DialogResult.Cancel):
                    break;
            }
        }

        public void whichNPC(NewConfig _NewConfig)
        {
            string npc;
            if (_NewConfig.npcId != "blank")
            {
                curNpcId = _NewConfig.npcId;
                npc = _NewConfig.npcId;
                loadFromWohl(npc, _NewConfig);
            }
            else
            {
                npc = "blank";
                resetAllItems();
                pictureBox1.Image = null;
                timer1.Stop();
                npcNameTextBox.CueText = "";
                defaultNpc.Text = "Load or Save a File!";
            }
        }

        public int frameSpeedConversion(int value, int txtFramespeed)
        {
            //answer = iniFramespeed / (8/txtFramespeed)
            //Shoutout to wohlstand for this formula!
            int answer = value / (8 / txtFramespeed);
            if (answer == 32)
                return 8;
            else
                return answer;
        }

        public void loadFromWohl(string npcid, NewConfig nc)
        {

            xOffset.Value = int.Parse(nc.wohlConfig.ReadValue(npcid, "gfx-offset-x")); //xOffsetCb.Checked = true;
            yOffset.Value = int.Parse(nc.wohlConfig.ReadValue(npcid, "gfx-offset-y")); //yOffsetCb.Checked = true;
            npcGfxHeight.Value = int.Parse(nc.wohlConfig.ReadValue(npcid, "gfx-height")); //npcHCb.Checked = true;
            npcGfxWidth.Value = int.Parse(nc.wohlConfig.ReadValue(npcid, "gfx-width")); //npcWCb.Checked = true;
            //framestyle here
            switch(int.Parse(nc.wohlConfig.ReadValue(npcid, "frame-style")))
            {
                case(0):
                    frameStyle.SelectedIndex = 0;
                    break;
                case(1):
                    frameStyle.SelectedIndex = 1;
                    break;
                case(2):
                    frameStyle.SelectedIndex = 2;
                    break;
            }
            frames.Value = int.Parse(nc.wohlConfig.ReadValue(npcid, "frames")); //framesCb.Checked = true;
            //framespeed here
            frameSpeed.Value = frameSpeedConversion(int.Parse(nc.wohlConfig.ReadValue(npcid, "frame-speed")), 1);
            switch (nc.wohlConfig.ReadValue(npcid, "foreground"))
            {
                case ("1"):
                    foreground.Checked = true;
                    //foregroundCb.Checked = true;
                    break;
                case ("0"):
                    foreground.Checked = false;
                    //foregroundCb.Checked = true;
                    break;
            }
            //speed REALLY NEEDS A CONVERSION
            //scoreCb.Checked = true;
            string blah = nc.wohlConfig.ReadValue(npcid, "score");
            int blahh = int.Parse(blah);
            switch (blahh)
            {
                case (0):
                    scoreList.SelectedIndex = 0;
                    break;
                case (1):
                    scoreList.SelectedIndex = 1;
                    break;
                case (2):
                    scoreList.SelectedIndex = 2;
                    break;
                case (3):
                    scoreList.SelectedIndex = 3;
                    break;
                case (4):
                    scoreList.SelectedIndex = 4;
                    break;
                case (5):
                    scoreList.SelectedIndex = 5;
                    break;
                case (6):
                    scoreList.SelectedIndex = 6;
                    break;
                case (7):
                    scoreList.SelectedIndex = 7;
                    break;
                case (8):
                    scoreList.SelectedIndex = 8;
                    break;
                case (9):
                    scoreList.SelectedIndex = 9;
                    break;
                case (10):
                    scoreList.SelectedIndex = 10;
                    break;
                case (11):
                    scoreList.SelectedIndex = 11;
                    break;
                case (12):
                    scoreList.SelectedIndex = 12;
                    break;
            }
            //noyoshi
            //noYoshiCb.Checked = true;
            switch (int.Parse(nc.wohlConfig.ReadValue(npcid, "yoshicaneat")))
            {
                case(1):
                    noYoshi.Checked = false;
                    break;
                case(0):
                    noYoshi.Checked = true;
                    break;
            }
            //grabtop
            //grabTopCb.Checked = true;
            switch (int.Parse(nc.wohlConfig.ReadValue(npcid, "grab-top")))
            {
                case (1):
                    grabTop.Checked = true;
                    break;
                case (0):
                    grabTop.Checked = false;
                    break;
            }
            //grabside
            //grabSideCb.Checked = true;
            switch (int.Parse(nc.wohlConfig.ReadValue(npcid, "grab-side")))
            {
                case (1):
                    grabSide.Checked = true;
                    break;
                case (0):
                    grabSide.Checked = false;
                    break;
            }
            //nohurt needs conversion too
            //dontHurtCb.Checked = true;
            switch (int.Parse(nc.wohlConfig.ReadValue(npcid, "hurtplayer")))
            {
                case(1):
                    dontHurt.Checked = false;
                    break;
                case(0):
                    dontHurt.Checked = true;
                    break;
            }
            //width (physics/hitbox)
            pNpcWidth.Value = int.Parse(nc.wohlConfig.ReadValue(npcid, "fixture-width")); //pNpcWidthCb.Checked = true;
            //height (physics/hitbox)
            pNpcHeight.Value = int.Parse(nc.wohlConfig.ReadValue(npcid, "fixture-height")); //pNpcHeightCb.Checked = true;
            //npcblock
            //npcCollisionCb.Checked = true;
            switch (int.Parse(nc.wohlConfig.ReadValue(npcid, "block-npc")))
            {
                case (1):
                    npcCollision.Checked = true;
                    break;
                case (0):
                    npcCollision.Checked = false;
                    break;
            }
            //npcblocktop
            //npcCollisionTopCb.Checked = true;
            switch (int.Parse(nc.wohlConfig.ReadValue(npcid, "block-npc-top")))
            {
                case (1):
                    npcCollisionTop.Checked = true;
                    break;
                case (0):
                    npcCollisionTop.Checked = true;
                    break;
            }
            //playerblock
            //pCollisionCb.Checked = true;
            switch (int.Parse(nc.wohlConfig.ReadValue(npcid, "block-player")))
            {
                case (1):
                    pCollision.Checked = true;
                    break;
                case (0):
                    pCollision.Checked = false;
                    break;
            }
            //playerblocktop
            //pCollisionTopCb.Checked = true;
            switch (int.Parse(nc.wohlConfig.ReadValue(npcid, "block-player-top")))
            {
                case (1):
                    pCollisionTop.Checked = true;
                    break;
                case (0):
                    pCollisionTop.Checked = true;
                    break;
            }
            //gravity needs conversion
            //noGravityCb.Checked = true;
            switch (int.Parse(nc.wohlConfig.ReadValue(npcid, "gravity")))
            {
                case(1):
                    noGravity.Checked = false;
                    break;
                case(0):
                    noGravity.Checked = true;
                    break;
            }
            //nofireball
            //noFireballCb.Checked = true;
            switch (int.Parse(nc.wohlConfig.ReadValue(npcid, "kill-fireball")))
            {
                case (1):
                    noFireball.Checked = false;
                    break;
                case (0):
                    noFireball.Checked = true;
                    break;
            }
            //noiceball
            //noFreezeCb.Checked = true;
            switch (int.Parse(nc.wohlConfig.ReadValue(npcid, "kill-iceball")))
            {
                case (1):
                    noFreeze.Checked = false;
                    break;
                case (0):
                    noFreeze.Checked = true;
                    break;
            }
            //cliffturn
            //cliffTurnCb.Checked = true;
            switch (int.Parse(nc.wohlConfig.ReadValue(npcid, "cliffturn")))
            {
                case(1):
                    cliffTurn.Checked = true;
                    break;
                case(0):
                    cliffTurn.Checked = false;
                    break;
            }
            //jumphurt
            switch (int.Parse(nc.wohlConfig.ReadValue(npcid, "kill-onjump")))
            {
                case(1):
                    jumpHurt.Checked = false;
                    break;
                case(0):
                    jumpHurt.Checked = true;
                    break;
            }
            //noblockcollision
            switch (int.Parse(nc.wohlConfig.ReadValue(npcid, "collision-blocks")))
            {
                case(1):
                    noBlockCollision.Checked = false;
                    break;
                case(0):
                    noBlockCollision.Checked = true;
                    break;
            }
            //name
            npcNameTextBox.Text = nc.wohlConfig.ReadValue(npcid, "name");
            tryLoadGif(npcid);
            defaultNpc.Text = "This NPC will replace " + nc.wohlConfig.ReadValue(npcid, "name");
        }
        public void tryLoadGif(string npc)
        {
            string dir = mr.Read("SMBXDIRECTORY") + @"\graphics\npc";
            if (Directory.Exists(dir))
            {
                try
                {
                    showSprite(String.Format(@"{0}\{1}.gif", dir, npc));
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error while loading sprite {0}.gif: {1}", npc, ex.Message);
                }
            }
        }

        private void clearButton_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            pictureBox1.Image = null;
        }

        private void reflectButton_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image != null)
            {
                pictureBox1.SuspendLayout();
                animatedImage.RotateFlip(RotateFlipType.Rotate180FlipY);
                pictureBox1.Image = animatedImage;
                pictureBox1.Update();
                timer1.Stop();
                animateSprite(curNpcId);
                pictureBox1.Show();
            }
        }
        //end of class
    }
}
