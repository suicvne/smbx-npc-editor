using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Setting;
using System.Drawing;
using ns;
using System.Windows.Forms;

namespace NPC_Sprite_Generator
{
    class Program
    {
        static IniFile wohlConfig;
        [STAThread]
        static void Main(string[] args)
        {
            Application.SetCompatibleTextRenderingDefault(false);
            Application.EnableVisualStyles();

            Form1 form1 = new Form1();
            form1.Show();
            Application.Run();
        }
        
        //
    }
}
