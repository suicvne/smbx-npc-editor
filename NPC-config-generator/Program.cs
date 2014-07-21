using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using ns;

namespace NPC_config_generator
{
    class Program
    {
        static void Main(string[] args)
        {
        Beginning:
            Console.WriteLine("Enter SMBX NPC Graphics Directory and Press ENTER\n(Example: C:\\SMBX\\graphics\\npc)");
            string path = Console.ReadLine();
            goto DirCheck;
            //
        DirCheck:
            if (Directory.Exists(path) != true)
            {
                Console.WriteLine("Path not found!");
                goto Beginning;
            }
            else
            {
                goto LookForNPCs;
            }
            //
        LookForNPCs:
            string filePath = path;
            StreamWriter sw = new StreamWriter(Environment.CurrentDirectory + @"\temp.ini");
            sw.AutoFlush = true;
            sw.WriteLine("; Generated on {0}", DateTime.Now.ToString());
            sw.WriteLine("[npcconfig]");
            Console.WriteLine("What is the name of your NPC configuration?");
            sw.WriteLine("name={0}", Console.ReadLine());
            Console.WriteLine("\n\nWrite a short description about this configuration");
            sw.WriteLine("description={0}", Console.ReadLine());
            string[] files = System.IO.Directory.GetFiles(filePath);
            NumericComparer ns = new NumericComparer();
            Array.Sort(files, ns);
            foreach(var graphics in files)
            {
                //Graphics right now would be a full path. C:\SMBX\graphics\npc\npc-1 as an example
                string name = Path.GetFileName(graphics.ToString());
                if (name.Contains("m") != true)
                {
                    //npc-1.gif would be split into npc-1 and .gif respectively
                    var splitFileName = name.Split(new char[] { '.' }, 2);
                NameInput:
                    Console.WriteLine("What is {0}?\n(Example: npc-1 is a Goomba)", splitFileName[0].ToString());
                    string npcName = Console.ReadLine();
                    if (npcName != String.Empty)
                    {
                        Console.WriteLine("{0} is a {1}!\n\n", name, npcName);
                        sw.WriteLine("{0}={1}", splitFileName[0].ToString(), npcName);
                    }
                    else
                    {
                        goto NameInput;
                    }
                }
            }
            //
        FinalFileName:
            Console.WriteLine("\n\n\nAlmost done!");
            Console.WriteLine("Write the final file name (will append .ini automatically): ");
            string saveTo = Console.ReadLine();
            try
            {

            }
            catch (Exception ex)
            {

            }
            //
            
        }
    }
}
