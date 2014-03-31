﻿using System;
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
using smbxnpceditor;

namespace visualNPCEditor
{
    public partial class Main : Form
    {
        public bool hasSaved;
        public string workingFile;
        bool validData;

        public Main()
        {
            InitializeComponent();
        }

        private void npcHCb_CheckedChanged(object sender, EventArgs e)
        {
            npcHeight.Enabled = npcHCb.Checked;
            //npcHitBoxHeight.npcHbH.Enabled = npcHCb.Checked;
        }
        //All the reading aka absolute chaos
        private void menuItem2_Click(object sender, EventArgs e)
        {
            OpenFileDialog of = new OpenFileDialog();
            of.Filter = "NPC Text Files (npc-*.txt)|npc-*.txt|All files (*.*)|*.*";
            if (of.ShowDialog() == DialogResult.OK)
            {
                Console.WriteLine("Reading: " + of.FileName.ToString());
                resetAllItems();
                //List<NPC> npc = new List<NPC>();
                using (StreamReader sr = new StreamReader(of.FileName.ToString()))
                {
                    //String wholeFile = sr.ReadToEnd();

                    string line;

                    while ((line = sr.ReadLine()) != null)
                    {
                        if (line.Contains("gfxoffsetx"))
                        {
                            var split = line.Split(new char[] { '=' }, 2);
                            //var val = int.Parse(split.ToString());
                            Console.WriteLine(split[0].ToString() + " is equal to " + split[1].ToString());
                            xOffset.Value = Decimal.Parse(split[1].ToString());
                            xOffset.Enabled = true;
                            xOffsetCb.Checked = true;
                        }

                        if (line.Contains("gfxoffsety"))
                        {
                            var split = line.Split(new char[] { '=' }, 2);
                            //var val = int.Parse(split.ToString());
                            Console.WriteLine(split[0].ToString() + " is equal to " + split[1].ToString());
                            yOffset.Value = Decimal.Parse(split[1].ToString());
                            yOffset.Enabled = true;
                            yOffsetCb.Checked = true;
                        }

                        if (line.Contains("width"))
                        {
                                if (line.Contains("gfx"))
                                {
                                    var split = line.Split(new char[] { '=' }, 2);
                                    //var val = int.Parse(split.ToString());
                                    Console.WriteLine(split[0].ToString() + " is equal to " + split[1].ToString());
                                    npcWidth.Value = Decimal.Parse(split[1].ToString());
                                    npcWidth.Enabled = true;
                                    npcWCb.Checked = true;
                                }
                                else
                                {
                                    var split = line.Split(new char[] { '=' }, 2);
                                    Console.WriteLine(split[0].ToString() + " is equal to " + split[1].ToString());
                                    pNpcWidth.Value = Decimal.Parse(split[1].ToString());
                                    pNpcWidth.Enabled = true;
                                    pNpcWidthCb.Checked = true;
                                }
                        }

                        if (line.Contains("height"))
                        {
                               
                                if (line.Contains("gfx"))
                                {
                                    var split = line.Split(new char[] { '=' }, 2);
                                    //var val = int.Parse(split.ToString());
                                    Console.WriteLine(split[0].ToString() + " is equal to " + split[1].ToString());
                                    npcHeight.Value = Decimal.Parse(split[1].ToString());
                                    npcHeight.Enabled = true;
                                    npcHCb.Checked = true;
                                }
                                else
                                {
                                    var split = line.Split(new char[] { '=' }, 2);
                                    Console.WriteLine(split[0].ToString() + " is equal to " + split[1].ToString());
                                    pNpcHeight.Value = Decimal.Parse(split[1].ToString());
                                    pNpcHeight.Enabled = true;
                                    pNpcHeightCb.Checked = true;
                                }
                        }
                        
                        if (line.Contains("score"))
                        {
                            var split = line.Split(new char[] { '=' }, 2);
                            //var val = int.Parse(split.ToString());
                            Console.WriteLine(split[0].ToString() + " is equal to " + split[1].ToString());
                            var score = split[1].ToString();
                            scoreList.Enabled = true;
                            scoreCb.Checked = true;
                            switch (score)
                            {
                                case "0":
                                    scoreList.Text = "None";
                                    break;
                                case "1":
                                    //10
                                    scoreList.Text = "10";
                                    break;
                                case "2":
                                    //100
                                    scoreList.Text = "100";
                                    break;
                                case "3":
                                    //200
                                    scoreList.Text = "200";
                                    break;
                                case "4":
                                    //400
                                    scoreList.Text = "400";
                                    break;
                                case "5":
                                    //800
                                    scoreList.Text = "800";
                                    break;
                                case "6":
                                    //1000
                                    scoreList.Text = "1000";
                                    break;
                                case "7":
                                    //2000
                                    scoreList.Text = "2000";
                                    break;
                                case "8":
                                    //4000
                                    scoreList.Text = "4000";
                                    break;
                                case "9":
                                    //8000
                                    scoreList.Text = "8000";
                                    break;
                                case "10":
                                    //1up
                                    scoreList.Text = "1-Up";
                                    break;
                                case "11":
                                    //2up
                                    scoreList.Text = "2-Up";
                                    break;
                                case "12":
                                    //5up
                                    scoreList.Text = "5-Up";
                                    break;
                            }
                        }

                        if (line.Contains("playerblock"))
                        {
                            if (line.Contains("top"))
                            {
                                var split = line.Split(new char[] { '=' }, 2);
                                //var val = int.Parse(split.ToString());
                                Console.WriteLine(split[0].ToString() + " is equal to " + split[1].ToString());
                                var tf = split[1].ToString();
                                switch (tf)
                                {
                                    case("1"):
                                        pCollisionTop.Checked = true;
                                        break;
                                    case("0"):
                                        pCollisionTop.Checked = false;
                                        break;
                                }
                                pCollisionTop.Enabled = true;
                                pCollisionTopCb.Checked = true;
                            }
                            else
                            {
                                var split = line.Split(new char[] { '=' }, 2);
                                //var val = int.Parse(split.ToString());
                                Console.WriteLine(split[0].ToString() + " is equal to " + split[1].ToString());
                                var tf = split[1].ToString();
                                switch (tf)
                                {
                                    case ("1"):
                                        pCollision.Checked = true;
                                        break;
                                    case ("0"):
                                        pCollision.Checked = false;
                                        break;
                                }
                                pCollision.Enabled = true;
                                pCollisionCb.Checked = true;
                            }
                        }
                        
                        if (line.Contains("npcblock"))
                        {
                            if (line.Contains("top"))
                            {
                                var split = line.Split(new char[] { '=' }, 2);
                                //var val = int.Parse(split.ToString());
                                Console.WriteLine(split[0].ToString() + " is equal to " + split[1].ToString());
                                var tf = split[1].ToString();
                                switch (tf)
                                {
                                    case ("1"):
                                        npcCollisionTop.Checked = true;
                                        break;
                                    case ("0"):
                                        npcCollisionTop.Checked = false;
                                        break;
                                }
                                npcCollisionTop.Enabled = true;
                                npcCollisionTopCb.Checked = true;
                            }
                            else
                            {
                                var split = line.Split(new char[] { '=' }, 2);
                                //var val = int.Parse(split.ToString());
                                Console.WriteLine(split[0].ToString() + " is equal to " + split[1].ToString());
                                var tf = split[1].ToString();
                                switch (tf)
                                {
                                    case ("1"):
                                        npcCollision.Checked = true;
                                        break;
                                    case ("0"):
                                        npcCollision.Checked = false;
                                        break;
                                }
                                npcCollision.Enabled = true;
                                npcCollisionCb.Checked = true;
                            }
                        }
                        
                        if (line.Contains("grabside"))
                        {
                            var split = line.Split(new char[] { '=' }, 2);
                            //var val = int.Parse(split.ToString());
                            Console.WriteLine(split[0].ToString() + " is equal to " + split[1].ToString());
                            var tf = split[1].ToString();
                            switch (tf)
                            {
                                case("1"):
                                    grabSide.Checked = true;
                                    break;
                                case("0"):
                                    grabSide.Checked = false;
                                    break;
                            }
                            grabSide.Enabled = true;
                            grabSideCb.Checked = true;
                        }

                        if (line.Contains("grabtop"))
                        {
                            var split = line.Split(new char[] { '=' }, 2);
                            //var val = int.Parse(split.ToString());
                            Console.WriteLine(split[0].ToString() + " is equal to " + split[1].ToString());
                            var tf = split[1].ToString();
                            switch (tf)
                            {
                                case ("1"):
                                    grabTop.Checked = true;
                                    break;
                                case ("0"):
                                    grabTop.Checked = false;
                                    break;
                            }
                            grabTop.Enabled = true;
                            grabTopCb.Checked = true;
                        }

                        if (line.Contains("jumphurt"))
                        {
                            var split = line.Split(new char[] { '=' }, 2);
                            //var val = int.Parse(split.ToString());
                            Console.WriteLine(split[0].ToString() + " is equal to " + split[1].ToString());
                            var tf = split[1].ToString();
                            switch (tf)
                            {
                                case("1"):
                                    jumpHurt.Checked = true;
                                    break;
                                case("0"):
                                    jumpHurt.Checked = false;
                                    break;
                            }
                            jumpHurt.Enabled = true;
                            jumpHurtCb.Checked = true;
                        }

                        if (line.Contains("nohurt"))
                        {
                            var split = line.Split(new char[] { '=' }, 2);
                            //var val = int.Parse(split.ToString());
                            Console.WriteLine(split[0].ToString() + " is equal to " + split[1].ToString());
                            var tf = split[1].ToString();
                            switch (tf)
                            {
                                case ("1"):
                                    dontHurt.Checked = true;
                                    break;
                                case ("0"):
                                    dontHurt.Checked = false;
                                    break;
                            }
                            dontHurt.Enabled = true;
                            dontHurtCb.Checked = true;
                        }

                        if (line.Contains("noblockcollision"))
                        {
                            var split = line.Split(new char[] { '=' }, 2);
                            //var val = int.Parse(split.ToString());
                            Console.WriteLine(split[0].ToString() + " is equal to " + split[1].ToString());
                            var tf = split[1].ToString();
                            switch (tf)
                            {
                                case("1"):
                                    noBlockCollision.Checked = true;
                                    break;
                                case("0"):
                                    noBlockCollision.Checked = true;
                                    break;
                            }
                            noBlockCollision.Enabled = true;
                            noBlockCb.Checked = true;
                        }

                        if (line.Contains("cliffturn"))
                        {
                            var split = line.Split(new char[] { '=' }, 2);
                            //var val = int.Parse(split.ToString());
                            Console.WriteLine(split[0].ToString() + " is equal to " + split[1].ToString());
                            var tf = split[1].ToString();
                            switch (tf)
                            {
                                case ("1"):
                                    cliffTurn.Checked = true;
                                    break;
                                case ("0"):
                                    cliffTurn.Checked = true;
                                    break;
                            }
                            cliffTurn.Enabled = true;
                            cliffTurnCb.Checked = true;
                        }

                        if (line.Contains("noyoshi"))
                        {
                            var split = line.Split(new char[] { '=' }, 2);
                            //var val = int.Parse(split.ToString());
                            Console.WriteLine(split[0].ToString() + " is equal to " + split[1].ToString());
                            var tf = split[1].ToString();
                            switch (tf)
                            {
                                case ("1"):
                                    noYoshi.Checked = true;
                                    break;
                                case ("0"):
                                    noYoshi.Checked = true;
                                    break;
                            }
                            noYoshi.Enabled = true;
                            noYoshiCb.Checked = true;
                        }

                        if (line.Contains("foreground"))
                        {
                            var split = line.Split(new char[] { '=' }, 2);
                            //var val = int.Parse(split.ToString());
                            Console.WriteLine(split[0].ToString() + " is equal to " + split[1].ToString());
                            var tf = split[1].ToString();
                            switch (tf)
                            {
                                case ("1"):
                                    foreground.Checked = true;
                                    break;
                                case ("0"):
                                    foreground.Checked = true;
                                    break;
                            }
                            foreground.Enabled = true;
                            foregroundCb.Checked = true;
                        }

                        if (line.Contains("speed"))
                        {
                            var split = line.Split(new char[] { '=' }, 2);
                            //var val = int.Parse(split.ToString());
                            Console.WriteLine(split[0].ToString() + " is equal to " + split[1].ToString());
                            speed.Value = Decimal.Parse(split[1].ToString());
                            speed.Enabled = true;
                            speedCb.Checked = true;
                        }

                        if (line.Contains("nofireball"))
                        {
                            var split = line.Split(new char[] { '=' }, 2);
                            //var val = int.Parse(split.ToString());
                            Console.WriteLine(split[0].ToString() + " is equal to " + split[1].ToString());
                            var tf = split[1].ToString();
                            switch (tf)
                            {
                                case ("1"):
                                    noFireball.Checked = true;
                                    break;
                                case ("0"):
                                    noFireball.Checked = true;
                                    break;
                            }
                            noFireball.Enabled = true;
                            noFireballCb.Checked = true;
                        }

                        if (line.Contains("nogravity"))
                        {
                            var split = line.Split(new char[] { '=' }, 2);
                            //var val = int.Parse(split.ToString());
                            Console.WriteLine(split[0].ToString() + " is equal to " + split[1].ToString());
                            var tf = split[1].ToString();
                            switch (tf)
                            {
                                case ("1"):
                                    noGravity.Checked = true;
                                    break;
                                case ("0"):
                                    noGravity.Checked = true;
                                    break;
                            }
                            noGravity.Enabled = true;
                            noGravityCb.Checked = true;
                        }

                        if (line.Contains("frames"))
                        {
                            if (line.Contains("speed"))
                            {
                                var split = line.Split(new char[] { '=' }, 2);
                                //var val = int.Parse(split.ToString());
                                Console.WriteLine(split[0].ToString() + " is equal to " + split[1].ToString());
                                frameSpeed.Value = Decimal.Parse(split[1].ToString());
                            }
                            else if (line.Contains("style"))
                            {
                                var split = line.Split(new char[] { '=' }, 2);
                                //var val = int.Parse(split.ToString());
                                Console.WriteLine(split[0].ToString() + " is equal to " + split[1].ToString());
                                var tf = split[1].ToString();
                                switch (tf)
                                {
                                    case("2"):
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
                                frameStyleCb.Checked = true;
                            }
                            else
                            {
                                var split = line.Split(new char[] { '=' }, 2);
                                //var val = int.Parse(split.ToString());
                                Console.WriteLine(split[0].ToString() + " is equal to " + split[1].ToString());
                                frames.Value = Decimal.Parse(split[1].ToString());
                                frames.Enabled = true;
                                framesCb.Checked = true;
                            }
                        }
                        
                        if (line.Contains("noiceball"))
                        {
                            var split = line.Split(new char[] { '=' }, 2);
                            //var val = int.Parse(split.ToString());
                            Console.WriteLine(split[0].ToString() + " is equal to " + split[1].ToString());
                            var tf = split[1].ToString();
                            switch (tf)
                            {
                                case ("1"):
                                    noFreeze.Checked = true;
                                    break;
                                case ("0"):
                                    noFreeze.Checked = true;
                                    break;
                            }
                            noFreeze.Enabled = true;
                            noFreezeCb.Checked = true;
                        }
                        //end fucntion
                    }
                    //end while
                }
                //end using
            }
            //
            hasSaved = true;
            workingFile = of.FileName;
            this.Text = "SMBX NPC Editor - " + Path.GetFileName(of.FileName);
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
            npcWidth.Enabled = npcWCb.Checked;
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
        public void resetAllItems()
        {
            npcHeight.Enabled = false;
            npcWidth.Enabled = false;
            xOffset.Enabled = false;
            yOffset.Enabled = false;
            frames.Enabled = false;
            frameSpeed.Enabled = false;
            frameStyle.Enabled = false;
            frameStyle.Enabled = false;
            foreground.Enabled = false;
            foreground.Checked = false;
            pNpcHeight.Enabled = false;
            pNpcWidth.Enabled = false;
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
            if (npcHCb.Checked == true)
            {
                if (npcHeight.Value != 0)
                {
                    sr.WriteLine("gfxheight=" + npcHeight.Value.ToString());
                }
            }
            if (npcWCb.Checked == true)
            {
                if (npcWidth.Value != 0)
                {
                    sr.WriteLine("gfxwidth=" + npcWidth.Value.ToString());
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
                    case(0):
                        //single sprite
                        sr.WriteLine("framestyle=0");
                        break;
                    case(1):
                        //Left/Right
                        sr.WriteLine("framestyle=1");
                        break;
                    case(2):
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
                    case(0):
                        sr.WriteLine("score=0");
                        break;
                    case(1):
                        sr.WriteLine("score=1");
                        break;
                    case(2):
                        sr.WriteLine("score=2");
                        break;
                    case(3):
                        sr.WriteLine("score=3");
                        break;
                    case(4):
                        sr.WriteLine("score=4");
                        break;
                    case(5):
                        sr.WriteLine("score=5");
                        break;
                    case(6):
                        sr.WriteLine("score=6");
                        break;
                    case(7):
                        sr.WriteLine("score=7");
                        break;
                    case(8):
                        sr.WriteLine("score=8");
                        break;
                    case(9):
                        sr.WriteLine("score=9");
                        break;
                    case(10):
                        sr.WriteLine("score=10");
                        break;
                    case(11):
                        sr.WriteLine("score=11");
                        break;
                    case(12):
                        sr.WriteLine("score=12");
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
            sf.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments).ToString();

            if (sf.ShowDialog() == DialogResult.OK)
            {
                saveAll(sf.FileName);
            }
            hasSaved = true;
            workingFile = sf.FileName.ToString();
            this.Text = "SMBX NPC Editor - " + Path.GetFileName(workingFile);
        }
        private void menuItem7_Click(object sender, EventArgs e)
        {
            workingFile = null;
            hasSaved = false;
            resetAllItems();
            this.Text = "SMBX NPC Editor";
        }
        private void menuItem3_Click(object sender, EventArgs e)
        {
            if (hasSaved == true)
            {
                if (workingFile != null)
                {
                    saveAll(workingFile);
                }
                else
                {
                    saveAs();
                }
            }
            else
            {
                saveAs();
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
            Process.Start("http://www.supermariobrosx.org/forums/");
        }

        private void menuItem8_Click(object sender, EventArgs e)
        {
            AboutBox about = new AboutBox();
            about.ShowDialog();
        }

        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.Cancel == false)
            {
                Application.Exit();
            }
            else
            {
                if (MessageBox.Show("Are you sure you want to quit?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
                {
                    e.Cancel = false;
                    Application.Exit();

                }
                else
                {
                    e.Cancel = true;
                }
            }
        }
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
        public void readFile(string fileName)
        {
            Console.WriteLine("Reading: " + fileName.ToString());
                resetAllItems();
                //List<NPC> npc = new List<NPC>();
                using (StreamReader sr = new StreamReader(fileName.ToString()))
                {
                    //String wholeFile = sr.ReadToEnd();

                    string line;

                    while ((line = sr.ReadLine()) != null)
                    {
                        if (line.Contains("gfxoffsetx"))
                        {
                            var split = line.Split(new char[] { '=' }, 2);
                            //var val = int.Parse(split.ToString());
                            Console.WriteLine(split[0].ToString() + " is equal to " + split[1].ToString());
                            xOffset.Value = Decimal.Parse(split[1].ToString());
                            xOffset.Enabled = true;
                            xOffsetCb.Checked = true;
                        }

                        if (line.Contains("gfxoffsety"))
                        {
                            var split = line.Split(new char[] { '=' }, 2);
                            //var val = int.Parse(split.ToString());
                            Console.WriteLine(split[0].ToString() + " is equal to " + split[1].ToString());
                            yOffset.Value = Decimal.Parse(split[1].ToString());
                            yOffset.Enabled = true;
                            yOffsetCb.Checked = true;
                        }

                        if (line.Contains("width"))
                        {
                                if (line.Contains("gfx"))
                                {
                                    var split = line.Split(new char[] { '=' }, 2);
                                    //var val = int.Parse(split.ToString());
                                    Console.WriteLine(split[0].ToString() + " is equal to " + split[1].ToString());
                                    npcWidth.Value = Decimal.Parse(split[1].ToString());
                                    npcWidth.Enabled = true;
                                    npcWCb.Checked = true;
                                }
                                else
                                {
                                    var split = line.Split(new char[] { '=' }, 2);
                                    Console.WriteLine(split[0].ToString() + " is equal to " + split[1].ToString());
                                    pNpcWidth.Value = Decimal.Parse(split[1].ToString());
                                    pNpcWidth.Enabled = true;
                                    pNpcWidthCb.Checked = true;
                                }
                        }

                        if (line.Contains("height"))
                        {
                               
                                if (line.Contains("gfx"))
                                {
                                    var split = line.Split(new char[] { '=' }, 2);
                                    //var val = int.Parse(split.ToString());
                                    Console.WriteLine(split[0].ToString() + " is equal to " + split[1].ToString());
                                    npcHeight.Value = Decimal.Parse(split[1].ToString());
                                    npcHeight.Enabled = true;
                                    npcHCb.Checked = true;
                                }
                                else
                                {
                                    var split = line.Split(new char[] { '=' }, 2);
                                    Console.WriteLine(split[0].ToString() + " is equal to " + split[1].ToString());
                                    pNpcHeight.Value = Decimal.Parse(split[1].ToString());
                                    pNpcHeight.Enabled = true;
                                    pNpcHeightCb.Checked = true;
                                }
                        }
                        
                        if (line.Contains("score"))
                        {
                            var split = line.Split(new char[] { '=' }, 2);
                            //var val = int.Parse(split.ToString());
                            Console.WriteLine(split[0].ToString() + " is equal to " + split[1].ToString());
                            var score = split[1].ToString();
                            scoreList.Enabled = true;
                            scoreCb.Checked = true;
                            switch (score)
                            {
                                case "0":
                                    scoreList.Text = "None";
                                    break;
                                case "1":
                                    //10
                                    scoreList.Text = "10";
                                    break;
                                case "2":
                                    //100
                                    scoreList.Text = "100";
                                    break;
                                case "3":
                                    //200
                                    scoreList.Text = "200";
                                    break;
                                case "4":
                                    //400
                                    scoreList.Text = "400";
                                    break;
                                case "5":
                                    //800
                                    scoreList.Text = "800";
                                    break;
                                case "6":
                                    //1000
                                    scoreList.Text = "1000";
                                    break;
                                case "7":
                                    //2000
                                    scoreList.Text = "2000";
                                    break;
                                case "8":
                                    //4000
                                    scoreList.Text = "4000";
                                    break;
                                case "9":
                                    //8000
                                    scoreList.Text = "8000";
                                    break;
                                case "10":
                                    //1up
                                    scoreList.Text = "1-Up";
                                    break;
                                case "11":
                                    //2up
                                    scoreList.Text = "2-Up";
                                    break;
                                case "12":
                                    //5up
                                    scoreList.Text = "5-Up";
                                    break;
                            }
                        }

                        if (line.Contains("playerblock"))
                        {
                            if (line.Contains("top"))
                            {
                                var split = line.Split(new char[] { '=' }, 2);
                                //var val = int.Parse(split.ToString());
                                Console.WriteLine(split[0].ToString() + " is equal to " + split[1].ToString());
                                var tf = split[1].ToString();
                                switch (tf)
                                {
                                    case("1"):
                                        pCollisionTop.Checked = true;
                                        break;
                                    case("0"):
                                        pCollisionTop.Checked = false;
                                        break;
                                }
                                pCollisionTop.Enabled = true;
                                pCollisionTopCb.Checked = true;
                            }
                            else
                            {
                                var split = line.Split(new char[] { '=' }, 2);
                                //var val = int.Parse(split.ToString());
                                Console.WriteLine(split[0].ToString() + " is equal to " + split[1].ToString());
                                var tf = split[1].ToString();
                                switch (tf)
                                {
                                    case ("1"):
                                        pCollision.Checked = true;
                                        break;
                                    case ("0"):
                                        pCollision.Checked = false;
                                        break;
                                }
                                pCollision.Enabled = true;
                                pCollisionCb.Checked = true;
                            }
                        }
                        
                        if (line.Contains("npcblock"))
                        {
                            if (line.Contains("top"))
                            {
                                var split = line.Split(new char[] { '=' }, 2);
                                //var val = int.Parse(split.ToString());
                                Console.WriteLine(split[0].ToString() + " is equal to " + split[1].ToString());
                                var tf = split[1].ToString();
                                switch (tf)
                                {
                                    case ("1"):
                                        npcCollisionTop.Checked = true;
                                        break;
                                    case ("0"):
                                        npcCollisionTop.Checked = false;
                                        break;
                                }
                                npcCollisionTop.Enabled = true;
                                npcCollisionTopCb.Checked = true;
                            }
                            else
                            {
                                var split = line.Split(new char[] { '=' }, 2);
                                //var val = int.Parse(split.ToString());
                                Console.WriteLine(split[0].ToString() + " is equal to " + split[1].ToString());
                                var tf = split[1].ToString();
                                switch (tf)
                                {
                                    case ("1"):
                                        npcCollision.Checked = true;
                                        break;
                                    case ("0"):
                                        npcCollision.Checked = false;
                                        break;
                                }
                                npcCollision.Enabled = true;
                                npcCollisionCb.Checked = true;
                            }
                        }
                        
                        if (line.Contains("grabside"))
                        {
                            var split = line.Split(new char[] { '=' }, 2);
                            //var val = int.Parse(split.ToString());
                            Console.WriteLine(split[0].ToString() + " is equal to " + split[1].ToString());
                            var tf = split[1].ToString();
                            switch (tf)
                            {
                                case("1"):
                                    grabSide.Checked = true;
                                    break;
                                case("0"):
                                    grabSide.Checked = false;
                                    break;
                            }
                            grabSide.Enabled = true;
                            grabSideCb.Checked = true;
                        }

                        if (line.Contains("grabtop"))
                        {
                            var split = line.Split(new char[] { '=' }, 2);
                            //var val = int.Parse(split.ToString());
                            Console.WriteLine(split[0].ToString() + " is equal to " + split[1].ToString());
                            var tf = split[1].ToString();
                            switch (tf)
                            {
                                case ("1"):
                                    grabTop.Checked = true;
                                    break;
                                case ("0"):
                                    grabTop.Checked = false;
                                    break;
                            }
                            grabTop.Enabled = true;
                            grabTopCb.Checked = true;
                        }

                        if (line.Contains("jumphurt"))
                        {
                            var split = line.Split(new char[] { '=' }, 2);
                            //var val = int.Parse(split.ToString());
                            Console.WriteLine(split[0].ToString() + " is equal to " + split[1].ToString());
                            var tf = split[1].ToString();
                            switch (tf)
                            {
                                case("1"):
                                    jumpHurt.Checked = true;
                                    break;
                                case("0"):
                                    jumpHurt.Checked = false;
                                    break;
                            }
                            jumpHurt.Enabled = true;
                            jumpHurtCb.Checked = true;
                        }

                        if (line.Contains("nohurt"))
                        {
                            var split = line.Split(new char[] { '=' }, 2);
                            //var val = int.Parse(split.ToString());
                            Console.WriteLine(split[0].ToString() + " is equal to " + split[1].ToString());
                            var tf = split[1].ToString();
                            switch (tf)
                            {
                                case ("1"):
                                    dontHurt.Checked = true;
                                    break;
                                case ("0"):
                                    dontHurt.Checked = false;
                                    break;
                            }
                            dontHurt.Enabled = true;
                            dontHurtCb.Checked = true;
                        }

                        if (line.Contains("noblockcollision"))
                        {
                            var split = line.Split(new char[] { '=' }, 2);
                            //var val = int.Parse(split.ToString());
                            Console.WriteLine(split[0].ToString() + " is equal to " + split[1].ToString());
                            var tf = split[1].ToString();
                            switch (tf)
                            {
                                case("1"):
                                    noBlockCollision.Checked = true;
                                    break;
                                case("0"):
                                    noBlockCollision.Checked = true;
                                    break;
                            }
                            noBlockCollision.Enabled = true;
                            noBlockCb.Checked = true;
                        }

                        if (line.Contains("cliffturn"))
                        {
                            var split = line.Split(new char[] { '=' }, 2);
                            //var val = int.Parse(split.ToString());
                            Console.WriteLine(split[0].ToString() + " is equal to " + split[1].ToString());
                            var tf = split[1].ToString();
                            switch (tf)
                            {
                                case ("1"):
                                    cliffTurn.Checked = true;
                                    break;
                                case ("0"):
                                    cliffTurn.Checked = true;
                                    break;
                            }
                            cliffTurn.Enabled = true;
                            cliffTurnCb.Checked = true;
                        }

                        if (line.Contains("noyoshi"))
                        {
                            var split = line.Split(new char[] { '=' }, 2);
                            //var val = int.Parse(split.ToString());
                            Console.WriteLine(split[0].ToString() + " is equal to " + split[1].ToString());
                            var tf = split[1].ToString();
                            switch (tf)
                            {
                                case ("1"):
                                    noYoshi.Checked = true;
                                    break;
                                case ("0"):
                                    noYoshi.Checked = true;
                                    break;
                            }
                            noYoshi.Enabled = true;
                            noYoshiCb.Checked = true;
                        }

                        if (line.Contains("foreground"))
                        {
                            var split = line.Split(new char[] { '=' }, 2);
                            //var val = int.Parse(split.ToString());
                            Console.WriteLine(split[0].ToString() + " is equal to " + split[1].ToString());
                            var tf = split[1].ToString();
                            switch (tf)
                            {
                                case ("1"):
                                    foreground.Checked = true;
                                    break;
                                case ("0"):
                                    foreground.Checked = true;
                                    break;
                            }
                            foreground.Enabled = true;
                            foregroundCb.Checked = true;
                        }

                        if (line.Contains("speed"))
                        {
                            var split = line.Split(new char[] { '=' }, 2);
                            //var val = int.Parse(split.ToString());
                            Console.WriteLine(split[0].ToString() + " is equal to " + split[1].ToString());
                            speed.Value = Decimal.Parse(split[1].ToString());
                            speed.Enabled = true;
                            speedCb.Checked = true;
                        }

                        if (line.Contains("nofireball"))
                        {
                            var split = line.Split(new char[] { '=' }, 2);
                            //var val = int.Parse(split.ToString());
                            Console.WriteLine(split[0].ToString() + " is equal to " + split[1].ToString());
                            var tf = split[1].ToString();
                            switch (tf)
                            {
                                case ("1"):
                                    noFireball.Checked = true;
                                    break;
                                case ("0"):
                                    noFireball.Checked = true;
                                    break;
                            }
                            noFireball.Enabled = true;
                            noFireballCb.Checked = true;
                        }

                        if (line.Contains("nogravity"))
                        {
                            var split = line.Split(new char[] { '=' }, 2);
                            //var val = int.Parse(split.ToString());
                            Console.WriteLine(split[0].ToString() + " is equal to " + split[1].ToString());
                            var tf = split[1].ToString();
                            switch (tf)
                            {
                                case ("1"):
                                    noGravity.Checked = true;
                                    break;
                                case ("0"):
                                    noGravity.Checked = true;
                                    break;
                            }
                            noGravity.Enabled = true;
                            noGravityCb.Checked = true;
                        }

                        if (line.Contains("frames"))
                        {
                            if (line.Contains("speed"))
                            {
                                var split = line.Split(new char[] { '=' }, 2);
                                //var val = int.Parse(split.ToString());
                                Console.WriteLine(split[0].ToString() + " is equal to " + split[1].ToString());
                                frameSpeed.Value = Decimal.Parse(split[1].ToString());
                            }
                            else if (line.Contains("style"))
                            {
                                var split = line.Split(new char[] { '=' }, 2);
                                //var val = int.Parse(split.ToString());
                                Console.WriteLine(split[0].ToString() + " is equal to " + split[1].ToString());
                                var tf = split[1].ToString();
                                switch (tf)
                                {
                                    case("2"):
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
                                frameStyleCb.Checked = true;
                            }
                            else
                            {
                                var split = line.Split(new char[] { '=' }, 2);
                                //var val = int.Parse(split.ToString());
                                Console.WriteLine(split[0].ToString() + " is equal to " + split[1].ToString());
                                frames.Value = Decimal.Parse(split[1].ToString());
                                frames.Enabled = true;
                                framesCb.Checked = true;
                            }
                        }
                        
                        if (line.Contains("noiceball"))
                        {
                            var split = line.Split(new char[] { '=' }, 2);
                            //var val = int.Parse(split.ToString());
                            Console.WriteLine(split[0].ToString() + " is equal to " + split[1].ToString());
                            var tf = split[1].ToString();
                            switch (tf)
                            {
                                case ("1"):
                                    noFreeze.Checked = true;
                                    break;
                                case ("0"):
                                    noFreeze.Checked = true;
                                    break;
                            }
                            noFreeze.Enabled = true;
                            noFreezeCb.Checked = true;
                        }
                        //end fucntion
                    }
                    //end while
                }
                //end using
            //
            hasSaved = true;
            workingFile = fileName;
            this.Text = "SMBX NPC Editor - " + Path.GetFileName(fileName);
        }
        #endregion
    }
}

//:D 1700 even!