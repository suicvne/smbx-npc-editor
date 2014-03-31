using System;

namespace visualNPCEditor
{
    [Serializable]
    public class NPC
    {
          int gfxoffsetx;
          int gfxoffsety;
          int width;
          int height;
          int gfxwidth;
          int gfheight;
          int score;
          int playerblock;
          int playerblocktop;
          int npcblock;
          int npcblocktop;
          int grabside;
          int grabtop;
          int jumphurt;
          int nohurt;
          int noblockcollision;
          int cliffturn;
          int noyoshi;
          int foreground;
          int speed;
          string nofireball;
          int nogravity;
          int frames;
          int framespeed;
          int framestyle;
          int noiceball;
        public NPC()
        {
        }
    }
}
