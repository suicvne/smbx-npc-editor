using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Setting;
using ns;
using System.IO;

namespace Real_Npc_Sprite
{
    public partial class Form1 : Form
    {
        IniFile wohlConfig;
        string fullPath;
        string fileOnly;
        int width;
        int height;
        int xOffset;
        int yOffset;
        bool useOffsetVal = false;
        Bitmap bmp;
        int curListIndex;
        List<string> npcList = new List<string>();
        string fileNoExt;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (!File.Exists(Environment.CurrentDirectory + @"\lvl_npc.ini"))
            {
                if (!Directory.Exists(Environment.CurrentDirectory + @"\npc"))
                {
                    Application.Exit();
                }
            }

            if (!Directory.Exists(Environment.CurrentDirectory + @"\converted"))
            {
                Directory.CreateDirectory(Environment.CurrentDirectory + @"\converted");
            }
            wohlConfig = new IniFile(Environment.CurrentDirectory + @"\lvl_npc.ini");
            string filePath = Environment.CurrentDirectory + @"\npc";
            string[] files = System.IO.Directory.GetFiles(filePath);
            NumericComparer ns = new NumericComparer();
            Array.Sort(files, ns);
            foreach (var graphics in files)
            {
                string name = Path.GetFileName(graphics.ToString());
                npcList.Add(graphics);
                
            }
            showNextImage(false);
        }

        public void showNextImage(bool add)
        {
            if (add) { curListIndex++; }
            //C:\blahblahblah
            fullPath = npcList[curListIndex].ToString();
            //npc-1.gif
            fileOnly = Path.GetFileName(fullPath);
            //npc-1
            fileNoExt = Path.GetFileNameWithoutExtension(fullPath);

            currentNpc.Text = wohlConfig.ReadValue(fileNoExt, "name");
            gfxWidth.Text = wohlConfig.ReadValue(fileNoExt, "gfx-width");
            gfxHeight.Text = wohlConfig.ReadValue(fileNoExt, "gfx-height");
            width = int.Parse(gfxWidth.Text);
            height = int.Parse(gfxHeight.Text);
            xOffset = int.Parse(wohlConfig.ReadValue(fileNoExt, "gfx-offset-x"));
            yOffset = int.Parse(wohlConfig.ReadValue(fileNoExt, "gfx-offset-y"));
            bmp = Image.FromFile(fullPath) as Bitmap;
            Rectangle crop; //= new Rectangle(xOffset, yOffset, width, height);
            if (useOffsetVal) { crop = new Rectangle(xOffset, yOffset, width, height); }
            else { crop = new Rectangle(0, 0, width, height); }
            Bitmap clone = bmp.Clone(crop, System.Drawing.Imaging.PixelFormat.DontCare);
            if (clone.Height + clone.Width > 64)
            {
                var scaled = ScaleImage((Image)clone, 32, 32);
                previewBox.Image = scaled;
            }
            else
            {
                previewBox.Image = clone;
            }
            previewBox.Update();
        }

        private void save_Click(object sender, EventArgs e)
        {
            try
            {
                previewBox.Image.Save(string.Format(Environment.CurrentDirectory + @"\converted\{0}.png", fileNoExt), System.Drawing.Imaging.ImageFormat.Png);
                //MessageBox.Show("Saved!");
                showNextImage(true);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void redrawImage()
        {
            switch (useOffsetVal)
            {
                case(true):
                        bmp = Image.FromFile(fullPath) as Bitmap;
                        Rectangle crop = new Rectangle(xOffset, yOffset, width, height);
                        Bitmap clone = bmp.Clone(crop, System.Drawing.Imaging.PixelFormat.DontCare);
                        previewBox.Image = clone;
                        previewBox.Update();
                    break;
                case(false):
                        bmp = Image.FromFile(fullPath) as Bitmap;
                        Rectangle crop1 = new Rectangle(0, 0, width, height);
                        Bitmap clone1 = bmp.Clone(crop1, System.Drawing.Imaging.PixelFormat.DontCare);
                        previewBox.Image = clone1;
                        previewBox.Update();
                    break;
            }

            
        }

        private void useOffset_CheckedChanged(object sender, EventArgs e)
        {
            switch (useOffset.Checked)
            {
                case(true):
                    useOffsetVal = true;
                    redrawImage();
                    break;
                case(false):
                    useOffsetVal = false;
                    redrawImage();
                    break;
            }
        }

        /*public static void Main()
        {
            var image = Image.FromFile(@"c:\logo.png");
            var newImage = ScaleImage(image, 300, 400);
            newImage.Save(@"c:\test.png", ImageFormat.Png);
        }*/

        public static Image ScaleImage(Image image, int maxWidth, int maxHeight)
        {
            var ratioX = (double)maxWidth / image.Width;
            var ratioY = (double)maxHeight / image.Height;
            var ratio = Math.Min(ratioX, ratioY);

            var newWidth = (int)(image.Width * ratio);
            var newHeight = (int)(image.Height * ratio);

            var xoffset = (int)(newWidth - maxWidth);
            
            var newImage = new Bitmap(32, 32);
            Graphics.FromImage(newImage).DrawImage(image, 0, 0, newWidth, newHeight);
            return newImage;
        }

        private void scaleButton_Click(object sender, EventArgs e)
        {
            var imgg = previewBox.Image;
            var newImage = ScaleImage(imgg, 32, 32);
            previewBox.Image = newImage;
            previewBox.SizeMode = PictureBoxSizeMode.CenterImage;
            previewBox.Size = new System.Drawing.Size(32, 32);
        }
        //
    }
}
